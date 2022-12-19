using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GA.Domain.DTO
{
    public class LoginDto
    {
        [Required(ErrorMessage = "El email es requerido")]
        [MaxLength(200)]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La contraseña es requerida")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
    }
}
