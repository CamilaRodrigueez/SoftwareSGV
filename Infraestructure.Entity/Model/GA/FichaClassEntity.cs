using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infraestructure.Entity.Model.GA
{
    [Table("FichaClass", Schema = "GA")]
    public class FichaClassEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ClassEntity")]
        public int IdClass { get; set; }
        public ClassEntity ClassEntity { get; set; }



        [ForeignKey("FichaEntity")]
        public int IdFicha { get; set; }
        public FichaEntity FichaEntity { get; set; }
    }
}
