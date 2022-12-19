using System;
using System.Collections.Generic;
using System.Text;

namespace GA.Domain.DTO.Presence
{
    public class StudentPresenceDto : ResultPresenceDto
    {
        public DateTime ExcuseDate { get; set; }
        public bool NoExcuse { get; set; }
        public bool Excuse { get; set; }
    }
}
