using Infraestructure.Entity.Model.Master;
using Infraestructure.Entity.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GA.Domain.DTO
{
    public class UserDto : LoginDto
    {
       
        public int IdUser { get; set; }
   
        public string Identification { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }
    
        public string Email { get; set; }
       
        public string Telefono { get; set; }

        public string IdFichas { get; set; }
        public string idFicha { get; set; }
        public string ConfirmPassword { get; set; }

        public string FullName { get { return $"{this.Name} {this.LastName}"; } }
        public IEnumerable<int> ListIdFichas { get; set; }
    }
}
