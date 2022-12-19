using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infraestructure.Entity.Model.Master
{
    [Table("Notification", Schema = "Master")]
    public class NotificationEntity
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(400)]
        public string Description { get; set; }
        public bool Seen { get; set; }
        public string Url { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? SeenDate { get; set; }

        [ForeignKey("UserEntity")]
        public int IdUser { get; set; }
        public UserEntity UserEntity { get; set; }

    }
}
