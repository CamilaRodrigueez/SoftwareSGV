using Infraestructure.Entity.Model.GA;
using Infraestructure.Entity.Model.Master;
using Infraestructure.Entity.Model.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infraestructure.Entity.Model
{
    [Table("User", Schema = "Security")]
    public class UserEntity
    {
        [Key]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "El número de identificación es requerido")]
        [MaxLength(15)]
        public string Identification { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "la contraseña es requerido")]
        [MaxLength(30)]
        public string Password { get; set; }

        [MaxLength(30)]
        public string Telefono { get; set; }
        public IEnumerable<RolUserEntity> RolUserEntities { get; set; }
        public IEnumerable<UserTypeUserEntity> UserTypeUserEntities { get; set; }
        public IEnumerable<PresenceEntity> PresenceEntities { get; set; }
        public IEnumerable<FichaUserEntity> FichaUserEntities { get; set; }
        public IEnumerable<NotificationEntity> NotificationEntities { get; set; }

        [NotMapped]
        public string FullName { get { return $"{this.Name} {this.LastName}"; } }

    }
}
