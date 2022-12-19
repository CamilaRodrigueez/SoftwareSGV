using GA.Domain.DTO;
using GA.Domain.DTO.Notification;
using GA.Domain.Services.Interface;
using GestionAsistencia.Handlers;
using GestionAsistencia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static Common.Utils.Constant.Const;

namespace GestionAsistencia.Controllers
{
    [Authorize]
    [TypeFilter(typeof(CustomExceptionHandler))]
    public class HomeController : Controller
    {
        #region Attributes
        private readonly INotificationServices _notificationServices;
        private readonly ILogger<HomeController> _logger;
        #endregion

        #region Builder
        public HomeController(INotificationServices notificationServices, ILogger<HomeController> logger)
        {
            _notificationServices = notificationServices;
            _logger = logger;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult GetNotification()
        {
            int idUser = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value);
            List<NotificationDto> list = _notificationServices.GetNotification(idUser);

            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = list,
                Message = string.Empty
            };

            return Ok(response);
        }
    }
}
