using GA.Domain.DTO.Notification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GA.Domain.Services.Interface
{
    public interface INotificationServices
    {
        Task<bool> InsertNotification(List<NotificationDto> notifications);
        Task<bool> InsertNotification(NotificationDto data);
        List<NotificationDto> GetNotification(int idUser);
    }
}
