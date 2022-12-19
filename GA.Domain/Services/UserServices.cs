

using Common.Utils.Enums;
using Common.Utils.Exceptions;
using Common.Utils.Helpers;
using Common.Utils.Resorces;
using GA.Domain.DTO;
using GA.Domain.DTO.Class;
using GA.Domain.DTO.User;
using GA.Domain.Services.Interface;
using Infraestructure.Core.Data;
using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Model;
using Infraestructure.Entity.Model.GA;
using Infraestructure.Entity.Model.Master;
using Infraestructure.Entity.Model.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Common.Utils.Enums.Enums;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Net.WebRequestMethods;

namespace GA.Domain.Services
{
    public class UserServices : IUserServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        private readonly DataContext _context;

        #endregion

        #region Builder
        public UserServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region authentication

        public ResponseDto Login(UserDto user)
        {
            ResponseDto response = new ResponseDto();
            UserEntity result = _unitOfWork.UserRepository.FirstOrDefault(x => x.Email == user.UserName
                                                                            && x.Password == user.Password,
                                                                            r => r.RolUserEntities,
                                                                            t => t.UserTypeUserEntities);
            if (result == null)
            {
                response.Message = "Usuario o contraseña inválida!";
                response.IsSuccess = false;
            }
            else
            {
                response.Result = result;
                response.IsSuccess = true;
                response.Message = "Usuario autenticado!";
            }
            return response;

        }
        #endregion

        #region Methods CRUD
        public List<UserDto> GetAllUsers(bool isInstructor)
        {
            List<UserTypeUserEntity> listUser = _unitOfWork.UserTypeUserRepository.FindAll(x => x.IdTypeUser == (isInstructor
                                                                                                 ? (int)TypeUser.Instructor
                                                                                                 : (int)TypeUser.Aprendiz),
                                                                                                 u => u.UserEntity
                                                                                                 ).ToList();
            var fichasUsers = _unitOfWork.FichaUserRepository.GetAll();

            List<UserDto> list = listUser.Select(x => new UserDto
            {

                IdUser = x.IdUser,
                Identification = x.UserEntity.Identification,
                Name = x.UserEntity.Name,
                LastName = x.UserEntity.LastName,
                UserName = x.UserEntity.Email,
                Telefono = string.IsNullOrEmpty(x.UserEntity.Telefono) ? "Sin Telefono" : x.UserEntity.Telefono,
                ListIdFichas = fichasUsers.Where(c => c.IdUser == x.IdUser).Select(f => f.IdFicha)
            }).ToList();



            return list;
        }

        public async Task<ResponseDto> InserUserAsync(UserDto userInsert, bool isInstructor)
        {
            ResponseDto response = new ResponseDto();

            UserEntity userIdentification = GetUserByIdentification(userInsert.Identification);
            if (userIdentification != null)
                throw new BusinessException(string.Format(GeneralMessages.ExistUserIndentification, userInsert.Identification));

            bool emailValid = Utils.ValidateEmail(userInsert.Email);
            if (!emailValid)
                throw new BusinessException(string.Format(GeneralMessages.InvalidEmail, userInsert.Email));

            UserEntity userEmail = GetUserByEmail(userInsert.Email);
            if (userEmail != null)
                throw new BusinessException(string.Format(GeneralMessages.ExistUserEmail, userInsert.Email));


            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    UserEntity userEntity = new UserEntity()
                    {
                        Name = userInsert.Name,
                        LastName = userInsert.LastName,
                        Identification = userInsert.Identification,
                        Email = userInsert.Email,
                        Telefono = string.IsNullOrEmpty(userInsert.Telefono) ? "Sin Telefono" : userInsert.Telefono,
                        Password = userInsert.Identification,
                    };
                    _unitOfWork.UserRepository.Insert(userEntity);
                    await _unitOfWork.Save();

                    UserTypeUserEntity userTypeUserEntity = new UserTypeUserEntity()
                    {
                        IdTypeUser = isInstructor ? (int)TypeUser.Instructor : (int)TypeUser.Aprendiz,
                        IdUser = userEntity.IdUser,
                        Selected = true
                    };
                    _unitOfWork.UserTypeUserRepository.Insert(userTypeUserEntity);

                    RolUserEntity rolUserEntity = new RolUserEntity()
                    {
                        IdRol = isInstructor ? (int)RolUser.Administrador : (int)RolUser.Estandar,
                        IdUser = userEntity.IdUser,
                    };
                    _unitOfWork.RolUserRepository.Insert(rolUserEntity);

                    await InsertFichasUser(userInsert);
                    await _unitOfWork.Save();

                    response.Message = GeneralMessages.ItemInserted;
                    response.IsSuccess = true;
                    transaction.Commit();
                }
                catch (Exception)
                {
                    response.Message = GeneralMessages.ItemNoInserted;
                    transaction.Rollback();
                }
            }

            return response;
        }
        public async Task<ResponseDto> InserUserMasivoAsync(List<UserExcelDto> listStudents, int idFicha)
        {
            ResponseDto response = new ResponseDto();
            foreach (UserExcelDto user in listStudents)
            {
                UserEntity userIdentification = GetUserByIdentification(user.Identificacion);
                if (userIdentification != null)
                    throw new BusinessException(string.Format(GeneralMessages.ExistUserIndentification, user.Identificacion));

                bool emailValid = Utils.ValidateEmail(user.Email);
                if (!emailValid)
                    throw new BusinessException(string.Format(GeneralMessages.InvalidEmail, user.Email));

                UserEntity userEmail = GetUserByEmail(user.Email);
                if (userEmail != null)
                    throw new BusinessException(string.Format(GeneralMessages.ExistUserEmail, user.Email));
            }

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    //se guardan los usuarios
                    List<UserEntity> usersList = listStudents.Select(x => new UserEntity()
                    {
                        Name = x.Nombre,
                        LastName = x.Apellido,
                        Identification = x.Identificacion,
                        Email = x.Email,
                        Telefono = string.IsNullOrEmpty(x.Telefono) ? "Sin Telefono" : x.Telefono,
                        Password = x.Identificacion,
                    }).ToList();
                    _unitOfWork.UsersRepository.Insert(usersList);
                    await _unitOfWork.Save();

                    //se les asigna un tipo usuario
                    List<UserTypeUserEntity> listUserTypeUser = usersList.Select(x => new UserTypeUserEntity()
                    {
                        IdTypeUser = (int)TypeUser.Aprendiz,
                        IdUser = x.IdUser,
                        Selected = true
                    }).ToList();
                    _unitOfWork.UserTypeUserRepository.Insert(listUserTypeUser);

                    //se les asigna rol
                    List<RolUserEntity> listRolUsers = usersList.Select(x => new RolUserEntity()
                    {
                        IdRol = (int)RolUser.Estandar,
                        IdUser = x.IdUser,

                    }).ToList();
                    _unitOfWork.RolUserRepository.Insert(listRolUsers);

                    //se les asigna ficha
                    List<FichaUserEntity> listFichaUser = usersList.Select(x => new FichaUserEntity()
                    {
                        IdUser = x.IdUser,
                        IdFicha = idFicha,

                    }).ToList();
                    _unitOfWork.FichaUserRepository.Insert(listFichaUser);
                    await _unitOfWork.Save();

                    response.Message = GeneralMessages.ItemsMasivoInserted;
                    response.IsSuccess = true;
                    transaction.Commit();
                }
                catch
                {
                    response.Message = GeneralMessages.ItemsMasivoNoInserted;
                    transaction.Rollback();
                }
            }

            return response;
        }

        private async Task<bool> InsertFichasUser(UserDto data)
        {
            var listFichaUser = new List<FichaUserEntity>();

            var idFichas = data.IdFichas.Split(',');
            foreach (var item in idFichas)
            {
                FichaUserEntity entity = new FichaUserEntity()
                {
                    IdFicha = Convert.ToInt32(item),
                    IdUser = data.IdUser,
                };
                listFichaUser.Add(entity);
            }

            _unitOfWork.FichaUserRepository.Insert(listFichaUser);
            return await _unitOfWork.Save() > 0;
        }

        private UserEntity GetUserByIdentification(string identification) => _unitOfWork.UserRepository.FirstOrDefault(x => x.Identification == identification);
        private UserEntity GetUserByEmail(string email) => _unitOfWork.UserRepository.FirstOrDefault(x => x.Email == email);
        public UserEntity GetUser(int idUser) => _unitOfWork.UserRepository.FirstOrDefault(x => x.IdUser == idUser);
        public async Task<ResponseDto> UpdateUserAsync(UserDto updateUser)
        {
            ResponseDto response = new ResponseDto();

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var list = GetFichaUserByUser(updateUser.IdUser);

                    if (list.Any())
                    {
                        //Elimina los registros existentes en FichaUserEntity
                        _unitOfWork.FichaUserRepository.Delete(list);
                        await _unitOfWork.Save();
                    }
                    //inserta los nuevos registros
                    await InsertFichasUser(updateUser);

                    //actualiza el Usuario
                    var _user = GetUser(updateUser.IdUser);
                    _user.Name = updateUser.Name;
                    _user.LastName = updateUser.LastName;
                    _user.Telefono = updateUser.Telefono;
                    _user.Email = updateUser.Email;
                    _user.Identification = updateUser.Identification;

                    _unitOfWork.UserRepository.Update(_user);

                    response.IsSuccess = await _unitOfWork.Save() > 0;
                    response.Message = GeneralMessages.ItemUpdated;
                    transaction.Commit();
                }
                catch (Exception)
                {
                    response.Message = GeneralMessages.ItemNoUpdated;
                    transaction.Rollback();
                }
            }

            return response;
        }
        private IEnumerable<FichaUserEntity> GetFichaUserByUser(int idUser) => _unitOfWork.FichaUserRepository.FindAll(x => x.IdUser == idUser);

        public async Task<ResponseDto> DeleteUserAsync(int idUser)
        {
            ResponseDto response = new ResponseDto();

            _unitOfWork.UserRepository.Delete(idUser);
            response.IsSuccess = await _unitOfWork.Save() > 0;
            if (response.IsSuccess)
                response.Message = GeneralMessages.ItemDeleted;
            else
                response.Message = GeneralMessages.ItemNoDeleted;

            return response;
        }


        public List<UserDto> GetAllStudentByFicha(int idFicha)
        {
            var list = _unitOfWork.FichaUserRepository.FindAll(x => x.IdFicha == idFicha, u => u.UserEntity);

            var result = list.Select(u => u.UserEntity).Select(x => new UserDto
            {

                IdUser = x.IdUser,
                Identification = x.Identification,
                Name = x.Name,
                LastName = x.LastName,
                UserName = x.Email,
                Telefono = string.IsNullOrEmpty(x.Telefono) ? "Sin Telefono" : x.Telefono,
            }).ToList();

            return result;
        }

        public async Task<ResponseDto> UpdatePersonalInformationAsync(UserDto updateUser)
        {
            ResponseDto response = new ResponseDto();
            try
            {

                if (!string.IsNullOrEmpty(updateUser.Password) && !string.IsNullOrEmpty(updateUser.ConfirmPassword))
                {
                    if (updateUser.ConfirmPassword == updateUser.Password)
                    {
                        //actualiza el Usuario
                        var _user = GetUser(updateUser.IdUser);
                        if (_user != null)
                            _user.Name = updateUser.Name;
                        _user.LastName = updateUser.LastName;
                        _user.Telefono = updateUser.Telefono;
                        _user.Password = updateUser.Password;
                        //_user.Email = updateUser.Email;
                        //_user.Identification = updateUser.Identification;

                        _unitOfWork.UserRepository.Update(_user);

                        response.IsSuccess = await _unitOfWork.Save() > 0;
                        response.Message = GeneralMessages.ItemUpdatedPersonalInformation;

                    }
                    else
                    {
                        response.Message = GeneralMessages.PasswordsNotMatch;
                    }

                }
                else
                {
                    response.Message = GeneralMessages.ConfirmPassword;
                }

            }
            catch (Exception)
            {
                response.Message = GeneralMessages.ItemNoUpdatedPersonalInformation;

            }

            return response;
        }
        #endregion


    }
}
