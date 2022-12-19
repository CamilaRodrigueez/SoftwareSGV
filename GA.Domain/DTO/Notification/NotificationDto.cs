using System;

namespace GA.Domain.DTO.Notification
{
    public class NotificationDto
    {
        public int IdNotification { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Seen { get; set; }
        public string Url { get; set; }
        public int IdUser { get; set; }
        public string StrDate { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
