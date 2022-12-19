using Common.Utils.Enums;
using GA.Domain.DTO;
using GA.Domain.DTO.Presence;
using GA.Domain.Services.Interface;
using GestionAsistencia.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Common.Utils.Constant.Const;

namespace GestionAsistencia.Controllers
{
    [Authorize]
    [TypeFilter(typeof(CustomExceptionHandler))]
    public class PresenceController : Controller
    {
        #region Atttributes
        private readonly IPresenceServices _presenceServices;
        #endregion

        #region Builder
        public PresenceController(IPresenceServices presenceServices)
        {
            this._presenceServices = presenceServices;
        }
        #endregion

        #region Views
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        [HttpGet]
        public IActionResult GetAllPresence(ConsultPresenceDto consult)
        {
            var idTypeUser = User.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdTypeUser).Value;
            bool isInstructor = idTypeUser == Convert.ToInt32(Enums.TypeUser.Instructor).ToString();
            if (!isInstructor)
            {
                //consulta por aprendiz si es un aprendiz
                int idUser= Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value);
                consult.IdUser=idUser;
            }
            List<ResultPresenceDto> list = _presenceServices.GetAllPresence(consult);

            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = list,
                Message = string.Empty
            };

            return Ok(response);
        }

        [HttpGet]
        public IActionResult ConsultStudentsPresence(ConsultPresenceDto consult)
        {
            List<StudentPresenceDto> list = _presenceServices.ConsultStudentsPresence(consult);

            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = list,
                Message = string.Empty
            };

            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetDetailPresence(ConsultPresenceDto consult)
        {
            List<DetailPresenceDto> presences = _presenceServices.GetPresenceDetailStudents(consult);

            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = presences,
                Message = string.Empty
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> InsertPresence(List<StudentPresenceDto> list)
        {
            IActionResult response;

            int idUser = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value);
            ResponseDto result = await _presenceServices.InsertPresence(list, idUser);
            if (result.IsSuccess)
                response = Ok(result);
            else
                response = BadRequest(result);

            return response;
        }


    }
}
