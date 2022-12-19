using GA.Domain.DTO;
using GA.Domain.DTO.Notification;
using GA.Domain.Services.Interface;
using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Model.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GA.Domain.Services
{
    public class NotificationServices : INotificationServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Builder
        public NotificationServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<bool> InsertNotification(NotificationDto data)
        {
            NotificationEntity notification = new NotificationEntity()
            {
                CreationDate = DateTime.Now,
                Description = data.Description,
                IdUser = data.IdUser,
                Title = data.Title,
                Url = data.Url,
                Seen = false
            };
            _unitOfWork.NotificationRepository.Insert(notification);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> InsertNotification(List<NotificationDto> notifications)
        {
            var listNotification = notifications.Select(x => new NotificationEntity()
            {
                CreationDate = DateTime.Now,
                Description = x.Description,
                IdUser = x.IdUser,
                Title = x.Title,
                Url = x.Url,
                Seen = false
            });
            _unitOfWork.NotificationRepository.Insert(listNotification);

            return await _unitOfWork.Save() > 0;
        }

        public List<NotificationDto> GetNotification(int idUser)
        {
            var result = _unitOfWork.NotificationRepository.FindAll(x => x.IdUser == idUser);

            var list = result.Select(x => new NotificationDto()
            {
                IdNotification = x.Id,
                Title = x.Title,
                Description = x.Description,
                IdUser = x.IdUser,
                Seen = x.Seen,
                Url = x.Url,
                StrDate = x.CreationDate.ToString("MMMM - dd, yyyy").ToUpper(),
                CreationDate = x.CreationDate
            }).OrderByDescending(c => c.CreationDate).ToList();

            return list;
        }
    }
}
