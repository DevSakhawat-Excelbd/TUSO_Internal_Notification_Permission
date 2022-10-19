using TUSO.Domain.Dto;
using TUSO.Domain.Entities;

/*
 * Created by: Bithy
 * Date created: 06.09.2022
 * Last modified: 06.10.2022
 * Modified by: Sakhawat
 */
namespace TUSO.Infrastructure.Contracts
{
    public interface INotificationRepository : IRepository<Notification>
    {
        /// <summary>
        /// Returns a notification if key matched.
        /// </summary>
        /// <param name="OID">Primary key of the table Notifications</param>
        /// <returns>Instance of a Notification object.</returns>
        public Task<Notification> GetNotificationByKey(long OID);


      // Notification di
     NotificationDto GetNotification(long key);

      /// <summary>
      /// Returns all notification.
      /// </summary>
      /// <returns>List of Notification object.</returns>
      public Task<IEnumerable<Notification>> GetNotifications();

        bool MarkAllRead(long key);
        bool MarkAsRead(long notid);
    }
}