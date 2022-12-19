using Common.Utils.Resorces;
using GA.Domain.DTO;
using GA.Domain.DTO.Class;
using GA.Domain.DTO.Excuse;
using GA.Domain.DTO.Ficha;
using GA.Domain.DTO.User;
using GA.Domain.Services;
using GA.Domain.Services.Interface;
using GestionAsistencia.Handlers;
using Infraestructure.Entity.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace GestionAsistencia.Controllers
{
    [Authorize]
    [TypeFilter(typeof(CustomExceptionHandler))]
    public class UserController : Controller
    {

        #region Attribute
        private readonly IUserServices _userServices;
        private readonly IConfiguration _config;
        #endregion

        #region Builder
        public UserController(IUserServices userServices, IConfiguration configuration)
        {
            _userServices = userServices;
            _config = configuration;
        }
        #endregion


        #region View
        [HttpGet]
        public IActionResult Index(bool isInstructor)
        {
            return View(new InsertUserDto() { IsInstructor = isInstructor });
        }

        [HttpGet]
        public IActionResult ViewExcel(int idFicha)
        {
            string ruta = _config.GetSection("RutaFiles").GetValue<string>("FormatoAprendiz");
            return View(new UserViewExcelDto() { IdFicha = idFicha, RutaDescarga = ruta });
        }

        #endregion

        #region Methods

        [HttpGet]
        public IActionResult GetAllUser(bool isInstructor)
        {
            List<UserDto> list = _userServices.GetAllUsers(isInstructor);

            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = list,
                Message = string.Empty
            };

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> InsertUser(UserDto userInsert, bool isInstructor)
        {
            IActionResult response;

            ResponseDto result = await _userServices.InserUserAsync(userInsert, isInstructor);
            if (result.IsSuccess)
                response = Ok(result);
            else
                response = BadRequest(result);

            return response;
        }


        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDto updateUser)
        {
            IActionResult response;

            ResponseDto result = await _userServices.UpdateUserAsync(updateUser);
            if (result.IsSuccess)
                response = Ok(result);
            else
                response = BadRequest(result);

            return response;
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePersonalInformation(UserDto updateUser)
        {
            IActionResult response;

            ResponseDto result = await _userServices.UpdatePersonalInformationAsync(updateUser);
            if (result.IsSuccess)
                response = Ok(result);
            else
                response = BadRequest(result);

            return response;
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int idUser)
        {
            IActionResult response;

            ResponseDto result = await _userServices.DeleteUserAsync(idUser);
            if (result.IsSuccess)
                response = Ok(result);
            else
                response = BadRequest(result);

            return response;
        }

        [HttpPost]
        public IActionResult MostrarDatos([FromForm] IFormFile archivoExcel)
        {
            ResponseDto response = new ResponseDto();

            Stream stream = archivoExcel.OpenReadStream();
            IWorkbook miExcel = null;
            if (Path.GetExtension(archivoExcel.FileName) == ".xlsx")
            {
                miExcel = new XSSFWorkbook(stream);
            }
            else
            {
                miExcel = new XSSFWorkbook(stream);
            }

            ISheet hojaExcel = miExcel.GetSheetAt(0);


            List<UserExcelDto> lista = new List<UserExcelDto>();
            if (hojaExcel.LastRowNum > 0)
            {
                for (int i = 1; i <= hojaExcel.LastRowNum; i++)
                {
                    IRow fila = hojaExcel.GetRow(i);
                    lista.Add(new UserExcelDto
                    {
                        Nombre = fila.GetCell(0).ToString(),
                        Apellido = fila.GetCell(1).ToString(),
                        Identificacion = fila.GetCell(2).ToString(),
                        Email = fila.GetCell(3).ToString(),
                        Telefono = fila.GetCell(4).ToString(),
                    });
                }
                response.Result = lista;
                response.IsSuccess = true;
            }
            else
                response.Message = "El archivo excel no debe ser vacío.";

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> EnviarDatos(List<UserExcelDto> listStudents, int idFicha)
        {
            IActionResult response;

            ResponseDto result = await _userServices.InserUserMasivoAsync(listStudents, idFicha);
            if (result.IsSuccess)
                response = Ok(result);
            else
                response = BadRequest(result);

            return response;
        }
        #endregion
    }
}
