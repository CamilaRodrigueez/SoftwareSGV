using GA.Domain.DTO;
using GA.Domain.DTO.Ficha;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GA.Domain.Services.Interface
{
    public interface IFichaServices
    {
        List<ConsultFichaDto> GetAllFichas();
        List<ConsultFichaDto> GetAllFichasClass(int idUser);
        Task<ResponseDto> InsertFichaAsync(ConsultFichaDto fichaDto);
        Task<bool> UpdateFichaAsync(ConsultFichaDto data);
        Task<ResponseDto> DeleteFichaAsync(int idFicha);

    }
}
