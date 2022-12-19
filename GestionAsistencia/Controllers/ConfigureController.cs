using GA.Domain.DTO;
using GA.Domain.Services.Interface;
using Infraestructure.Entity.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using static Common.Utils.Constant.Const;
using System.Collections.Generic;

namespace GestionAsistencia.Controllers
{
    public class ConfigureController : Controller
    {

        #region Attribute
        private readonly IUserServices _userServices;
        #endregion

        #region Builder
        public ConfigureController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        #endregion

        public IActionResult Index()
        {
            int idUser = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value);
            UserEntity user = _userServices.GetUser(idUser);
            return View(user);
        }
        [HttpGet]
        public IActionResult GetUser()
        {
            int idUser = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value);
            UserEntity user = _userServices.GetUser(idUser);

            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = user,
                Message = string.Empty
            };

            return Ok(response);
        }
    }
}
