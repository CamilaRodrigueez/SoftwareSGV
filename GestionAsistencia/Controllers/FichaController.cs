using Common.Utils.Resorces;
using GA.Domain.DTO;
using GA.Domain.DTO.Ficha;
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
    public class FichaController : Controller
    {

        #region Attribute
        private readonly IFichaServices _fichaServices;
        #endregion

        #region Builder
        public FichaController(IFichaServices fichaServices)
        {
            _fichaServices = fichaServices;
        }
        #endregion

        #region Views

        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region Methods
        [HttpGet]
        public IActionResult GetAllFichas()
        {
            List<ConsultFichaDto> list = _fichaServices.GetAllFichas();

            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = list,
                Message = string.Empty
            };

            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetFichasClassByUser()
        {
            var user = HttpContext.User;
            string idUser = user.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value;

            List<ConsultFichaDto> list = _fichaServices.GetAllFichasClass(Convert.ToInt32(idUser));

            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = list,
                Message = string.Empty
            };

            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetAllFichasClass()
        {
            List<ConsultFichaDto> list = _fichaServices.GetAllFichasClass(idUser: 0);

            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = list,
                Message = string.Empty
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> InsertFicha(ConsultFichaDto fichaDto)
        {
            IActionResult response;

            ResponseDto result = await _fichaServices.InsertFichaAsync(fichaDto);
            if (result.IsSuccess)
                response = Ok(result);
            else
                response = BadRequest(result);

            return response;
        }


        [HttpPut]
        public async Task<IActionResult> UpdateFicha(ConsultFichaDto data)
        {
            IActionResult response;

            bool result = await _fichaServices.UpdateFichaAsync(data);
            ResponseDto responseDto = new ResponseDto()
            {
                IsSuccess = result,
                Result = result,
                Message = result ? GeneralMessages.ItemInserted : GeneralMessages.ItemNoInserted
            };

            if (result)
                response = Ok(responseDto);
            else
                response = BadRequest(responseDto);

            return response;

        }


        [HttpDelete]
        public async Task<IActionResult> DeleteFicha(int idFicha)
        {
            IActionResult response;
            ResponseDto result = await _fichaServices.DeleteFichaAsync(idFicha);

            if (result.IsSuccess)
                response = Ok(result);
            else
                response = BadRequest(result);

            return response;
        }
        #endregion

    }
}
