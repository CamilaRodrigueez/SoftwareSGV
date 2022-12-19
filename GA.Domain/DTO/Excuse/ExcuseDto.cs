using Infraestructure.Entity.Model.GA;
using Infraestructure.Entity.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GA.Domain.DTO.Excuse
{
    public class ExcuseDto : AddExcuse
    {

        public int IdExcuse { get; set; }

        public string UrlImage { get; set; }
        

        public string Class { get; set; }
        public string Ficha { get; set; }
        public string FullNameUser { get; set; }
        public string Identification { get; set; }
        public string NameTeacher { get; set; }
        public string StrExcuseDate { get; set; }
    }
}
