using Common.Utils.Helpers;
using GA.Domain.DTO;
using GA.Domain.Services.Interface;
using GestionAsistencia.Handlers;
using Infraestructure.Entity.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Common.Utils.Constant.Const;

namespace CrudUsuarios.Controllers
{
    [TypeFilter(typeof(CustomExceptionHandler))]
    public class AuthController : Controller
    {
        #region Attribute
        private readonly IUserServices _userServices;
        #endregion

        #region Builder
        public AuthController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        #endregion

        #region Methods
        [HttpGet]
        public IActionResult Login()
        {
            return View(new UserDto());
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDto user)
        {
            ResponseDto response =  _userServices.Login(user);

            if (!response.IsSuccess)
            {
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, response.Message);
                return View(user);
            }
            else
            {
                UserEntity userEntity = (UserEntity)response.Result;
               
               
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userEntity.FullName),
                    new Claim(TypeClaims.IdUser, userEntity.IdUser.ToString()),
                    new Claim(TypeClaims.UserName, userEntity.Email),
                    new Claim(TypeClaims.IdRol, string.Join(",",userEntity.RolUserEntities.Select(x=>x.IdRol))),
                    new Claim(TypeClaims.IdTypeUser, string.Join(",",userEntity.UserTypeUserEntities.Where(x=>x.Selected==true).Select(x=>x.IdTypeUser))),
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes((token.Expiration / 60) - 10),

                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    IsPersistent = false,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                                              new ClaimsPrincipal(claimsIdentity),
                                              authProperties);


                return Redirect("/Home/Index");
            }

        }




        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        

        #endregion
    }
}
