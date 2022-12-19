using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infraestructure.Entity.Model.Security
{
    [Table("RolUser", Schema = "Security")]
    public class RolUserEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("RolEntity")]
        public int IdRol { get; set; }
        public RolEntity RolEntity { get; set; }

        [ForeignKey("UserEntity")]
        public int IdUser { get; set; }
        public UserEntity UserEntity { get; set; }
    }
}
