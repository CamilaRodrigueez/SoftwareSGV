using GA.Domain.DTO;
using GA.Domain.DTO.Excuse;
using GA.Domain.Services.Interface;
using GestionAsistencia.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
    public class ExcuseController : Controller
    {
        #region Attribute
        private readonly IExcuseServices _excuseServices;
        private readonly IHostingEnvironment _hostingEnvironment;
        #endregion

        #region Builder
        public ExcuseController(IExcuseServices excuseServices, IHostingEnvironment hostingEnvironment)
        {
            _excuseServices = excuseServices;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region View
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Methods

        [HttpGet]
        public IActionResult GetAllExcusesInstructor(ConsultExcuseDto consult)
        {
            List<ExcuseDto> list = _excuseServices.GetAllExcuses(consult);

            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = list,
                Message = string.Empty
            };

            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetAllExcuses()
        {
            var user = HttpContext.User;
            string idUser = user.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value;
            List<ExcuseDto> list = _excuseServices.GetAllExcusesAprendiz(Convert.ToInt32(idUser));

            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = list,
                Message = string.Empty
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> InsertExcuse(AddExcuse excuse)
        {
            IActionResult response;
            var user = HttpContext.User;
            string idUser = user.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value;
            excuse.IdUser = Convert.ToInt32(idUser);
            ResponseDto result = await _excuseServices.InsertExcuseAsync(excuse, _hostingEnvironment);
            if (result.IsSuccess)
                response = Ok(result);
            else
                response = BadRequest(result);

            return response;
        }

        [HttpGet]
        public IActionResult GetExcuseByDate(DateTime dateExcuse, int idFicha = 0, int idClass = 0)
        {
            List<ExcuseDto> list = _excuseServices.GetExcuseByDate(dateExcuse, idFicha, idClass);

            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = list,
                Message = string.Empty
            };

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteExcuse(int idExcuse)
        {
            IActionResult response;
            ResponseDto result = await _excuseServices.DeleteExcuse(idExcuse,_hostingEnvironment);

            if (result.IsSuccess)
                response = Ok(result);
            else
                response = BadRequest(result);

            return response;
        }

        [HttpGet]
        public FileResult Download(string fileName)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes($@"{_hostingEnvironment.WebRootPath}/{fileName}");
            
            //string fileName = "myfile.ext";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        #endregion
    }
}
