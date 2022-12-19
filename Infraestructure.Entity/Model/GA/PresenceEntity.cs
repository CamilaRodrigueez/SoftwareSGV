using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infraestructure.Entity.Model.GA
{
    [Table("Presence", Schema = "GA")]
    public class PresenceEntity
    {
        [Key]
        public int Id { get; set; }
        public bool NoExcuse { get; set; }
        public bool Excuse { get; set; }
        public DateTime Date { get; set; }
        public DateTime RegistrationDate { get; set; }

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
