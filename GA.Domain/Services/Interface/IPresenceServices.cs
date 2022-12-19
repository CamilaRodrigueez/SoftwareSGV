using GA.Domain.DTO;
using GA.Domain.DTO.Presence;
using Infraestructure.Entity.Model.GA;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GA.Domain.Services.Interface
{
    public interface IPresenceServices
    {
        List<ResultPresenceDto> GetAllPresence(ConsultPresenceDto consult);
        List<StudentPresenceDto> ConsultStudentsPresence(ConsultPresenceDto consult);
        Task<ResponseDto> InsertPresence(List<StudentPresenceDto> list, int idUserInstructor);
        List<DetailPresenceDto> GetPresenceDetailStudents(ConsultPresenceDto consult);
    }
}
