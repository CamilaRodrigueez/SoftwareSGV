using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infraestructure.Entity.Model.GA
{
    [Table("Excuse", Schema = "GA")]
    public class ExcuseEntity
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        public DateTime ExcuseDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string NameImage { get; set; }

        [ForeignKey("UserEntity")]
        public int IdUser { get; set; }
        public UserEntity UserEntity { get; set; }

        [ForeignKey("FichaEntity")]
        public int IdFicha { get; set; }
        public FichaEntity FichaEntity { get; set; } 
        
        [ForeignKey("ClassEntity")]
        public int IdClass { get; set; }
        public ClassEntity ClassEntity { get; set; }
    }
}
