using Common.Utils.Resorces;
using GA.Domain.DTO;
using GA.Domain.DTO.Class;
using GA.Domain.Services.Interface;
using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Model.GA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GA.Domain.Services
{
    public class ClassServices : IClassServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Builder
        public ClassServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion


        #region Methodos CRUD
        public List<ClassDto> GetAllClass()
        {
            var fichasClass = _unitOfWork.FichaClassRepository.GetAll();
            var classes = _unitOfWork.ClassRepository.GetAll();

            List<ClassDto> list = classes.Select(x => new ClassDto
            {
                IdClass = x.Id,
                Class = x.StrClass,
                ListIdFichas = fichasClass.Where(c => c.IdClass == x.Id).Select(f => f.IdFicha)

            }).ToList();

            return list;

        }
        private ClassEntity GetClass(int idClass) => _unitOfWork.ClassRepository.FirstOrDefault(x => x.Id == idClass);

        public async Task<ResponseDto> InsertClassAsync(ClassDto classInsert)
        {
            ResponseDto response = new ResponseDto();

            ClassEntity classEntity = new ClassEntity()
            {
                StrClass = classInsert.Class,
            };

            _unitOfWork.ClassRepository.Insert(classEntity);
            if (await _unitOfWork.Save() > 0)
            {
                classInsert.IdClass = classEntity.Id;

                response.IsSuccess = await InsertFichasClass(classInsert);
                if (response.IsSuccess)
                    response.Message = GeneralMessages.ItemInserted;
                else
                    response.Message = GeneralMessages.ItemNoInserted;
            }
            else
                response.Message = GeneralMessages.ItemNoInserted;

            return response;
        }

        private async Task<bool> InsertFichasClass(ClassDto data)
        {
            var listClass = new List<FichaClassEntity>();

            var idFichas = data.IdFichas.Split(',');
            foreach (var item in idFichas)
            {
                FichaClassEntity entity = new FichaClassEntity()
                {
                    IdFicha = Convert.ToInt32(item),
                    IdClass = data.IdClass
                };
                listClass.Add(entity);
            }

            _unitOfWork.FichaClassRepository.Insert(listClass);
            return await _unitOfWork.Save() > 0;
        }
        public async Task<ResponseDto> UpdateClassAsync(ClassDto UpdateClass)
        {
            ResponseDto response = new ResponseDto();

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var list = GetFichaClassByClass(UpdateClass.IdClass);

                    if (list.Any())
                    {
                        //Elimina los registros existentes
                        _unitOfWork.FichaClassRepository.Delete(list);
                        await _unitOfWork.Save();
                    }
                    //inserta los nuevos registros
                    await InsertFichasClass(UpdateClass);

                    //actualiza la clase
                    var _class = GetClass(UpdateClass.IdClass);
                    _class.StrClass = UpdateClass.Class;
                    _unitOfWork.ClassRepository.Update(_class);

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

        public async Task<ResponseDto> DeleteClassAsync(int idClass)
        {
            ResponseDto response = new ResponseDto();

            _unitOfWork.ClassRepository.Delete(idClass);
            response.IsSuccess = await _unitOfWork.Save() > 0;
            if (response.IsSuccess)
                response.Message = GeneralMessages.ItemDeleted;
            else
                response.Message = GeneralMessages.ItemNoDeleted;

            return response;
        }


        private IEnumerable<FichaClassEntity> GetFichaClassByClass(int idClass) => _unitOfWork.FichaClassRepository.FindAll(x => x.IdClass == idClass);
        #endregion
    }
}
