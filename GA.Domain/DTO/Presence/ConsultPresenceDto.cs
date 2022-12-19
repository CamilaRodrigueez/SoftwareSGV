using GA.Domain.DTO.Excuse;
using System;
using System.Collections.Generic;
using System.Text;

namespace GA.Domain.DTO.Presence
{
    public class ConsultPresenceDto : ConsultExcuseDto
    {
        public int IdClass { get; set; }
        public int IdUser { get; set; }
    }
}
