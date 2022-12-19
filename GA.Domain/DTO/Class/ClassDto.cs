using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GA.Domain.DTO.Class
{
    public class ClassDto
    {

        public int IdClass { get; set; }

        public string Class { get; set; }
        public string IdFichas { get; set; }
       public IEnumerable<int> ListIdFichas { get; set; }
    }
}
