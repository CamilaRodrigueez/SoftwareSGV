using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infraestructure.Entity.Model.GA
{
    [Table("Class", Schema = "GA")]
    public class ClassEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(200)]
        public string StrClass { get; set; }

        public IEnumerable<PresenceEntity> PresenceEntities { get; set; }
        public IEnumerable<FichaClassEntity> FichaClassEntities { get; set; }
    }
}
