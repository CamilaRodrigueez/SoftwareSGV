using GA.Domain.DTO;
using GA.Domain.DTO.Class;
using Infraestructure.Entity.Model;
using Infraestructure.Entity.Model.GA;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GA.Domain.Services.Interface
{
    public interface IClassServices
    {
        List<ClassDto> GetAllClass();
        Task<ResponseDto>  InsertClassAsync(ClassDto classInsert);
        Task<ResponseDto> UpdateClassAsync(ClassDto classInsert);
        Task<ResponseDto> DeleteClassAsync(int idClass);
    }
}
