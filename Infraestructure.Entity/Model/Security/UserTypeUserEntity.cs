using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Infraestructure.Entity.Model.Security
{
    [Table("UserTypeUser", Schema = "Security")]
    public class UserTypeUserEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("TypeUserEntity")]
        public int IdTypeUser { get; set; }
        public TypeUserEntity TypeUserEntity { get; set; }

        [ForeignKey("UserEntity")]
        public int IdUser { get; set; }
        public UserEntity UserEntity { get; set; }

        public bool Selected { get; set; }
    }
}
