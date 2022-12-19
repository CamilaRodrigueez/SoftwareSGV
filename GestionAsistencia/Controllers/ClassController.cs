using GA.Domain.DTO.Ficha;
using GA.Domain.DTO;
using GA.Domain.DTO.User;
using GA.Domain.Services;
using GA.Domain.Services.Interface;
using Infraestructure.Entity.Model.GA;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using GA.Domain.DTO.Class;
using GestionAsistencia.Handlers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace GestionAsistencia.Controllers
{
    [Authorize]
    [TypeFilter(typeof(CustomExceptionHandler))]
    public class ClassController : Controller
    {
        #region Attribute
        private readonly IClassServices _classServices;
        #endregion

        #region Builder
        public ClassController(IClassServices clasServices)
        {
            _classServices = clasServices;
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllClass()
        {
            List<ClassDto> list = _classServices.GetAllClass();

            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = list,
                Message = string.Empty
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> InsertClass(ClassDto classInsert)
        {
            IActionResult response;

            ResponseDto result = await _classServices.InsertClassAsync(classInsert);
            if (result.IsSuccess)
                response = Ok(result);
            else
                response = BadRequest(result);

            return response;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClass(ClassDto classUpdate)
        {
            IActionResult response;

            ResponseDto result = await _classServices.UpdateClassAsync(classUpdate);
            if (result.IsSuccess)
                response = Ok(result);
            else
                response = BadRequest(result);

            return response;
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteClass(int idClass)
        {
            IActionResult response;

            ResponseDto result = await _classServices.DeleteClassAsync(idClass);
            if (result.IsSuccess)
                response = Ok(result);
            else
                response = BadRequest(result);

            return response;
        }
    }

}
