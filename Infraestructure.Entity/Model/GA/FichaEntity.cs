using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infraestructure.Entity.Model.GA
{
    [Table("Ficha", Schema = "GA")]
    public class FichaEntity
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(15)]
        public string Num_Ficha { get; set; }
        [MaxLength(300)]
        public string Nombre { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public IEnumerable<PresenceEntity> PresenceEntities { get; set; }
        public IEnumerable<FichaClassEntity> FichaClassEntities { get; set; }
        public IEnumerable<FichaUserEntity> FichaUserEntities { get; set; }
    }
}
