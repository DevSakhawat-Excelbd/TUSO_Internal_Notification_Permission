using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Infrastructure;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;
using TUSO.Utilities.Constants;

/*
 * Created by: Bithy
 * Date created: 06.09.2022
 * Last modified: 06.10.2022
 * Modified by: Sakhawat
 */
namespace TUSO.Api.Controllers
{
   /// <summary>
   /// Notification controller.
   /// </summary>
   [Route(RouteConstants.BaseRoute)]
   [ApiController]
   public class NotificationController : ControllerBase
   {
      private readonly IUnitOfWork context;
      private readonly DataContext dataContext;

      /// <summary>
      /// Default constructor
      /// </summary>
      /// <param name="context"></param>
      public NotificationController(IUnitOfWork context, DataContext dataContext)
      {
         this.context = context;
         this.dataContext = dataContext;
      }

      /// <summary>
      /// URL: tuso-api/notifications-sk
      /// </summary>
      /// <returns>List of table object.</returns>
      //[HttpGet]
      //[Route(RouteConstants.ReadNotifications)]
      //public async Task<IActionResult> ReadNotifications()
      //{
      //   try
      //   {
      //      var notification = await context.NotificationRepository.GetNotifications();
      //      return Ok(notification);
      //   }
      //   catch (Exception)
      //   {
      //      return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
      //   }
      //}

      /// <summary>
      /// URL: tuso-api/notification/key/{key}
      /// </summary>
      /// <param name="key">Primary key of the table Notifications</param>
      /// <returns>Instance of a table object.</returns>
      //[HttpGet]
      //[Route(RouteConstants.ReadNotificationByKey)]
      //public async Task<IActionResult> ReadNotificationByKey(int key)
      //{
      //   try
      //   {
      //      if (key <= 0)
      //         return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

      //      var notification = await context.NotificationRepository.GetNotificationByKey(key);

      //      if (notification == null)
      //         return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

      //      return Ok(notification);
      //   }
      //   catch (Exception)
      //   {
      //      return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
      //   }
      //}

      #region CreateNotification
      [HttpGet]
      [Route(RouteConstants.CreateNotification)]
      public async Task<IActionResult> CreateNotification()
      {
         try
         {
            var incidents = await context.IncidentRepository.GetIncidents();
            long incidentId = incidents.Last().OID;
            var notification = new Notification
            {
               NotificationDescription = "You have a new ticket. Please resolve it as soon as possible.",
               NotificationType = "Notification Type",
               //web url
               ReturnURL = "/Incident/incidents",
               IsRead = false,
               DateCreated = DateTime.Now,
               IncidentID = incidentId,
               //UserAccountID = key
            };
            //context.NotificationRepository.Add(notification);
            //await context.SaveChangesAsync();

            var role = (from u in dataContext.UserAccounts
                        join r in dataContext.Roles.Where(w => w.RoleName == "Admin") on u.RoleID equals r.OID
                        //join i in dataContext.Incidents on u.OID equals i.UserAccountID
                        //select u.OID).ToList();
                        select u.OID).FirstOrDefault();
            //foreach (var i in role)
            //{
            //   notification.UserAccountID = i;
            //   context.NotificationRepository.Add(notification);
            //   await context.SaveChangesAsync();
            //}
               notification.UserAccountID = role;
               context.NotificationRepository.Add(notification);
               await context.SaveChangesAsync();

            var notificationToReturn = await context.NotificationRepository.GetNotificationByKey(notification.OID);

            //var notifications = await context.NotificationRepository.GetNotifications();
            //long notificationId = notifications.Last().OID;
            //var notificationToReturn = await context.NotificationRepository.GetNotificationByKey(notificationId);

            return Ok(notification);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
         }
      }
      #endregion

      /// <summary>
      /// Load Notification.
      /// </summary>
      /// <param name="notifications"></param>
      /// <returns></returns>
      [HttpGet]
      [Route(RouteConstants.ReadNotifications)]
      public IActionResult LoadNotification(long key)
      {
         try
         {
            //var users = context.UserAccountRepository.GetAll();
            //long userId = users.Select(u => u.RoleID).FirstOrDefault();
            //var roles = context.RoleRepository.GetAll();
            //long userId = users.Select(r => r.RoleID).Join(roles.Select(r => r.OID));
           // long userid = Convert.ToInt64(users.(r => r.RoleID == 1).firs().OID);

            var notificationToReturn = context.NotificationRepository.GetNotification(key);
            return Ok(notificationToReturn);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
         }
      }

      #region MarkAllRead
      [HttpGet]
      [Route(RouteConstants.MarkAllRead)]
      public IActionResult MarkAllRead(long key)
      {
         try
         {
            var Return = context.NotificationRepository.MarkAllRead(key);

            return Ok(Return);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
         }
      }
      #endregion

      #region Mark as read for single notification
      /// <summary>
      /// tuso-api/notification/key/{notid}
      /// </summary>
      /// <param name="notid"></param>
      /// <returns></returns>
      [HttpGet]
      [Route(RouteConstants.MarkAsRead)]
      public IActionResult MarkAsRead(long key)
      {
         try
         {
            var Return = context.NotificationRepository.MarkAsRead(key);

            return Ok(Return);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
         }
      }
      #endregion
   }
}