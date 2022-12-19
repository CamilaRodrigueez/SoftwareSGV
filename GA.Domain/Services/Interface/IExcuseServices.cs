using GA.Domain.DTO;
using GA.Domain.DTO.Excuse;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GA.Domain.Services.Interface
{
    public interface IExcuseServices
    {
        List<ExcuseDto> GetAllExcuses(ConsultExcuseDto consult);

        List<ExcuseDto> GetAllExcusesAprendiz(int idUserAprendiz);
        Task<ResponseDto> InsertExcuseAsync(AddExcuse excuse, IHostingEnvironment hostingEnvironment);
        List<ExcuseDto> GetExcuseByDate(DateTime dateExcuse, int idFicha = 0, int idClass = 0);

        Task<ResponseDto> DeleteExcuse(int idExcuse, IHostingEnvironment hostingEnvironment);

    }
}
