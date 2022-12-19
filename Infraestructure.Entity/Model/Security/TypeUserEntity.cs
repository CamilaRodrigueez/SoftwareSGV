using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infraestructure.Entity.Model.Security
{
    [Table("TypeUser", Schema = "Security")]
    public class TypeUserEntity
    {
        [Key]
        public int IdTypeUser { get; set; }
        public string TypeUser { get; set; }
        
    }
}
