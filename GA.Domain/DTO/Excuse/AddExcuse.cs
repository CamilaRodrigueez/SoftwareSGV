using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace GA.Domain.DTO.Excuse
{
    public class AddExcuse
    {
        public int IdFicha { get; set; }
        public int IdClass { get; set; }
        public string Description { get; set; }
        public DateTime ExcuseDate { get; set; }
        public int IdUser { get; set; }
        public IFormFile FileImage { get; set; }
        public string NameImg { get; set; }
    }
}
