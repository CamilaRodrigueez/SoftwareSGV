using Common.Utils.Exceptions;
using Common.Utils.Helpers;
using Common.Utils.Resorces;
using GA.Domain.DTO;
using GA.Domain.DTO.Class;
using GA.Domain.DTO.Excuse;
using GA.Domain.Services.Interface;
using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Model;
using Infraestructure.Entity.Model.GA;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.Domain.Services
{
    public class ExcuseServices : IExcuseServices
    {

        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        #endregion

        #region Builder
        public ExcuseServices(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _config = configuration;
        }
        #endregion


        #region Methods CRUD

        public List<ExcuseDto> GetAllExcuses(ConsultExcuseDto consult)
        {
            var excuses = _unitOfWork.ExcuseRepository.FindAll(x => x.IdFicha == consult.IdFicha
                                                                && (x.ExcuseDate >= consult.StartDate && x.ExcuseDate <= consult.EndDate),
                                                                u => u.UserEntity,
                                                                c => c.ClassEntity,
                                                                f => f.FichaEntity);

            string ruta = _config.GetSection("RutaFiles").GetValue<string>("Excusas");
            List<ExcuseDto> list = excuses.Select(x => new ExcuseDto
            {
                IdExcuse = x.Id,
                IdFicha = x.IdFicha,
                IdUser = x.IdUser,
                Description = x.Description,
                ExcuseDate = x.ExcuseDate,
                UrlImage = String.IsNullOrEmpty(x.NameImage) ? string.Empty : $"{ruta}/{x.NameImage}",
                Class = x.ClassEntity.StrClass,
                Ficha = x.FichaEntity.Num_Ficha,
                IdClass = x.IdClass,
                Identification = x.UserEntity.Identification,
                FullNameUser = x.UserEntity.FullName,
                NameTeacher = NameTeache(x.IdFicha),
                StrExcuseDate = x.ExcuseDate.ToString("MMMM - dd, yyyy").ToUpper()
            }).ToList();

            return list;
        }

        public List<ExcuseDto> GetAllExcusesAprendiz(int idUserAprendiz)
        {
            var excuses = _unitOfWork.ExcuseRepository.FindAll(x => x.IdUser == idUserAprendiz,
                                                                u => u.UserEntity,
                                                                c => c.ClassEntity,
                                                                f => f.FichaEntity);

            string ruta = _config.GetSection("RutaFiles").GetValue<string>("Excusas");
            List<ExcuseDto> list = excuses.Select(x => new ExcuseDto
            {
                IdExcuse = x.Id,
                IdFicha = x.IdFicha,
                IdUser = x.IdUser,
                Description = x.Description,
                ExcuseDate = x.ExcuseDate,
                UrlImage = String.IsNullOrEmpty(x.NameImage) ? string.Empty : $"{ruta}/{x.NameImage}",
                Class = x.ClassEntity.StrClass,
                Ficha = x.FichaEntity.Num_Ficha,
                IdClass = x.IdClass,
                Identification = x.UserEntity.Identification,
                FullNameUser = x.UserEntity.FullName,
                NameTeacher = NameTeache(x.IdFicha),
                StrExcuseDate = x.ExcuseDate.ToString("MMMM - dd, yyyy").ToUpper()

            }).ToList();

            return list;
        }

        private ExcuseEntity GetExcuseEntity(int idExcuse) => _unitOfWork.ExcuseRepository.FirstOrDefault(x => x.Id == idExcuse);
        public async Task<ResponseDto> InsertExcuseAsync(AddExcuse excuse, IHostingEnvironment hostingEnvironment)
        {
            ResponseDto response = new ResponseDto();

            if (DateTime.Now.Date > excuse.ExcuseDate.Date)
                throw new BusinessException(String.Format(GeneralMessages.ExcuseDateInvalid, DateTime.Now.ToString("dd-MM-yyyy")));

            if (excuse.FileImage != null)
            {
                string ruta = _config.GetSection("RutaFiles").GetValue<string>("Excusas");
                var uniqueFileName = Utils.GetUniqueFileName(excuse.FileImage.FileName);
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, ruta);


                if (!Directory.Exists(uploads))
                {
                    Console.WriteLine("Creando el directorio: {0}", uploads);
                    DirectoryInfo di = Directory.CreateDirectory(uploads);
                }
                var filePath = Path.Combine(uploads, uniqueFileName);

                excuse.FileImage.CopyTo(new FileStream(filePath, FileMode.Create));
                excuse.NameImg = uniqueFileName;
            }
            else
                throw new BusinessException(GeneralMessages.ExcuseFileRequired);

            _unitOfWork.ExcuseRepository.Insert(new ExcuseEntity()
            {
                IdClass = excuse.IdClass,
                IdFicha = excuse.IdFicha,
                Description = excuse.Description,
                ExcuseDate = excuse.ExcuseDate,
                IdUser = excuse.IdUser,
                NameImage = excuse.NameImg,
                RegistrationDate = DateTime.Now
            });

            response.IsSuccess = await _unitOfWork.Save() > 0;
            if (response.IsSuccess)
                response.Message = GeneralMessages.ItemInserted;
            else
                response.Message = GeneralMessages.ItemNoInserted;


            return response;
        }

        public async Task<ResponseDto> DeleteExcuse(int idExcuse, IHostingEnvironment hostingEnvironment)
        {
            ResponseDto response = new ResponseDto();

            var excuse = GetExcuseEntity(idExcuse);
            _unitOfWork.ExcuseRepository.Delete(excuse);
            response.IsSuccess = await _unitOfWork.Save() > 0;
            if (response.IsSuccess)
            {
                string rutaLocal = _config.GetSection("RutaFiles").GetValue<string>("Excusas");
                string ruta = Path.Combine(hostingEnvironment.WebRootPath, $"{rutaLocal}/{excuse.NameImage}");
                if (System.IO.File.Exists(ruta))
                    System.IO.File.Delete(ruta);

                response.Message = GeneralMessages.ItemDeleted;
            }
            else
                response.Message = GeneralMessages.ItemNoDeleted;

            return response;
        }
        #endregion

        #region Private
        private string NameTeache(int idFicha)
        {
            string name = string.Empty;

            var user = _unitOfWork.UserRepository.InstructoresFicha(idFicha);
            if (user.Any())
                name = user.First().FullName;

            return name;
        }

        public List<ExcuseDto> GetExcuseByDate(DateTime dateExcuse, int idFicha = 0, int idClass = 0)
        {
            IEnumerable<ExcuseEntity> excuses = _unitOfWork.ExcuseRepository.FindAll(x => x.ExcuseDate == dateExcuse
                                                                 && x.IdFicha == (idFicha == 0 ? x.IdFicha : idFicha)
                                                                 && x.IdClass == (idClass == 0 ? x.IdClass : idClass),
                                                               u => u.UserEntity,
                                                               c => c.ClassEntity,
                                                               f => f.FichaEntity);

            List<ExcuseDto> list = excuses.Select(x => new ExcuseDto
            {
                IdExcuse = x.Id,
                IdFicha = x.IdFicha,
                IdUser = x.IdUser,
                Description = x.Description,
                ExcuseDate = x.ExcuseDate,
                UrlImage = $"img/Excusas/{x.NameImage}",
                Class = x.ClassEntity.StrClass,
                Ficha = x.FichaEntity.Num_Ficha,
                IdClass = x.IdClass,
                Identification = x.UserEntity.Identification,
                FullNameUser = x.UserEntity.FullName,
                NameTeacher = NameTeache(x.IdFicha),
                StrExcuseDate = x.ExcuseDate.ToString("MMMM - dd, yyyy").ToUpper()

            }).ToList();

            return list;
        }
        #endregion
    }
}
