using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infraestructure.Entity.Model.GA
{
    [Table("FichaUser", Schema = "GA")]
    public class FichaUserEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserEntity")]
        public int IdUser { get; set; }
        public UserEntity UserEntity { get; set; }

        [ForeignKey("FichaEntity")]
        public int IdFicha { get; set; }
        public FichaEntity FichaEntity { get; set; }
    }
}
