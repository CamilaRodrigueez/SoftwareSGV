using GA.Domain.DTO.Class;
using System;
using System.Collections.Generic;
using System.Text;

namespace GA.Domain.DTO.Ficha
{
    public class ConsultFichaDto : FichaDto
    {

        public string NumFicha { get; set; }
        public string Name { get; set; }

        public string StrStartDate { get; set; }
        public string StrEndDate { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<ClassDto> ListClass { get; set; }

    }
}
