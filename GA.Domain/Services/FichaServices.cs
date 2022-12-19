using Common.Utils.Exceptions;
using Common.Utils.Helpers;
using Common.Utils.Resorces;
using GA.Domain.DTO;
using GA.Domain.DTO.Ficha;
using GA.Domain.Services.Interface;
using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Model.Security;
using Infraestructure.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Common.Utils.Enums.Enums;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using Infraestructure.Entity.Model.GA;
using GA.Domain.DTO.Class;

namespace GA.Domain.Services
{
    public class FichaServices : IFichaServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IConfiguration _configuration;
        #endregion

        #region Builder
        public FichaServices(IUnitOfWork unitOfWork)
        { //IConfiguration configuration
            _unitOfWork = unitOfWork;
            //_configuration = configuration;
        }
        #endregion

        #region Methodos

        public List<ConsultFichaDto> GetAllFichas()
        {
            var ficha = _unitOfWork.FichaRepository.GetAll();
            List<ConsultFichaDto> list = new List<ConsultFichaDto>();

            if (ficha.Any())
            {
                list = ficha.Select(x => new ConsultFichaDto
                {
                    IdFicha = x.Id,
                    NumFicha = x.Num_Ficha,
                    Name = x.Nombre,
                    StrStartDate = x.StartDate.ToString("MMMM - dd, yyyy").ToUpper(),
                    StrEndDate = x.EndDate.ToString("MMMM - dd, yyyy").ToUpper(),
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                }).ToList();
            }

            return list;
        }


        public List<ConsultFichaDto> GetAllFichasClass(int idUser)
        {

            var fichasClass = _unitOfWork.FichaClassRepository.GetAll(c => c.ClassEntity);
            List<ConsultFichaDto> list = new List<ConsultFichaDto>();
            if (idUser != 0)
            {
                var ficha = _unitOfWork.FichaUserRepository.FindAll(x => x.IdUser == idUser, f => f.FichaEntity).ToList();

                list = ficha.Select(f => f.FichaEntity).Select(x => new ConsultFichaDto
                {
                    IdFicha = x.Id,
                    NumFicha = x.Num_Ficha,
                    Name = x.Nombre,
                    StrStartDate = x.StartDate.ToString("MMMM - dd, yyyy").ToUpper(),
                    StrEndDate = x.EndDate.ToString("MMMM - dd, yyyy").ToUpper(),
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    ListClass = fichasClass.Where(f => f.IdFicha == x.Id).Select(c => new ClassDto()
                    {
                        IdClass = c.IdClass,
                        Class = c.ClassEntity.StrClass
                    }).ToList(),
                }).ToList();
            }
            else
            {
                var fichas = _unitOfWork.FichaRepository.GetAll();
                list = fichas.Select(x => new ConsultFichaDto
                {
                    IdFicha = x.Id,
                    NumFicha = x.Num_Ficha,
                    Name = x.Nombre,
                    StrStartDate = x.StartDate.ToString("MMMM - dd, yyyy").ToUpper(),
                    StrEndDate = x.EndDate.ToString("MMMM - dd, yyyy").ToUpper(),
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    ListClass = fichasClass.Where(f => f.IdFicha == x.Id).Select(c => new ClassDto()
                    {
                        IdClass = c.IdClass,
                        Class = c.ClassEntity.StrClass
                    }).ToList(),
                }).ToList();

            }
            return list;
        }


        public async Task<ResponseDto> InsertFichaAsync(ConsultFichaDto fichaDto)
        {
            ResponseDto response = new ResponseDto();

            FichaEntity codigoFicha = GetFichaByFileNumber(fichaDto.NumFicha);
            if (codigoFicha != null)
                throw new BusinessException(string.Format(GeneralMessages.ExistFichaCode, fichaDto.NumFicha));

            if (fichaDto.EndDate.Date <= fichaDto.StartDate.Date)
                throw new BusinessException(GeneralMessages.DatesInvalid);

            FichaEntity fichaEntity = new FichaEntity()
            {
                Nombre = fichaDto.Name,
                Num_Ficha = fichaDto.NumFicha,
                StartDate = fichaDto.StartDate,
                EndDate = fichaDto.EndDate,
            };
            _unitOfWork.FichaRepository.Insert(fichaEntity);

            response.IsSuccess = await _unitOfWork.Save() > 0;
            if (response.IsSuccess)
                response.Message = GeneralMessages.ItemInserted;
            else
                response.Message = GeneralMessages.ItemNoInserted;


            return response;

        }
        private FichaEntity GetFichaByFileNumber(string numeroFicha) => _unitOfWork.FichaRepository.FirstOrDefault(x => x.Num_Ficha == numeroFicha);

        public async Task<bool> UpdateFichaAsync(ConsultFichaDto data)
        {
            bool result = false;

            if (data.EndDate.Date <= data.StartDate.Date)
                throw new BusinessException(GeneralMessages.DatesInvalid);

            var ficha = _unitOfWork.FichaRepository.FirstOrDefault(x => x.Id == data.IdFicha);

            if (ficha != null)
            {
                ficha.Num_Ficha = data.NumFicha;
                ficha.StartDate = data.StartDate.Date;
                ficha.EndDate = data.EndDate.Date;
                ficha.Nombre = data.Name;
                _unitOfWork.FichaRepository.Update(ficha);
                result = await _unitOfWork.Save() > 0;
            }

            return result;
        }
        public async Task<ResponseDto> DeleteFichaAsync(int idFicha)
        {
            ResponseDto response = new ResponseDto();

            _unitOfWork.FichaRepository.Delete(idFicha);
            response.IsSuccess = await _unitOfWork.Save() > 0;
            if (response.IsSuccess)
                response.Message = GeneralMessages.ItemDeleted;
            else
                response.Message = GeneralMessages.ItemNoDeleted;

            return response;
        }

        #endregion


    }
}
