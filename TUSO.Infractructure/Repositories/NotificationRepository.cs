using TUSO.Domain.Dto;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Bithy
 * Date created: 06.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Repositories
{
   public class NotificationRepository : Repository<Notification>, INotificationRepository
   {
      public NotificationRepository(DataContext context) : base(context)
      {

      }

      public async Task<Notification> GetNotificationByKey(long OID)
      {
         try
         {
            return await FirstOrDefaultAsync(c => c.OID == OID && c.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IEnumerable<Notification>> GetNotifications()
      {
         try
         {
            return await QueryAsync(c => c.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public NotificationDto GetNotification(long key)
      {
         var data = context.Notifications.Where(w => w.UserAccountID == key).OrderByDescending(o => o.DateCreated).ToList();
         var unread = data.Where(w => w.IsRead == false).Count();
         return new NotificationDto { Allnot = data, Unreadnot = unread };
      }

      public bool MarkAllRead(long key)
      {
         bool res = false;
         var data = context.Notifications.Where(w => w.UserAccountID == key && w.IsRead == false).ToList();
         if (data.Count > 0)
         {
            foreach (var i in data)
            {
               i.IsRead = true;
               context.SaveChanges();
            }
            res = true;
         }
         return res;
      }

      public bool MarkAsRead(long key)
      {
         bool res = false;
         var data = context.Notifications.Where(w => w.OID == key).FirstOrDefault();
         if (data != null)
         {
            data.IsRead = true;
            context.SaveChanges();
            res = true;
         }
         return res;
      }
   }
}