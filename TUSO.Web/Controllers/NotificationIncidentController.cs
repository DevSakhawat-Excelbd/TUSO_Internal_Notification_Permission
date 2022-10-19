using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using TUSO.Domain.Dto;
using TUSO.Domain.Entities;
using TUSO.Web.HttpClients;

namespace TUSO.Web.Controllers
{
   public class NotificationIncidentController : Controller
   {
      private readonly HttpClient client;
      private readonly string BaseUrl = "https://localhost:7026/tuso-api/";

      public NotificationIncidentController(HttpClient client)
      {
         this.client = client;
      }

      public IActionResult Notification()
      {
         return View();
      }

      public IActionResult RequestTicket()
      {
         return View();
      }

      // add notification
      public async Task<IActionResult> AddNotification()
      {
         //var users = await new UserAccountHttpClient(client).ReadUserAccounts();
         //long userid = Convert.ToInt64(users.Where(r => r.RoleID == 1).Last().OID);

         //bool inc = await new NotificationHttpClient(client).AddNotification();

         await new NotificationHttpClient(client).AddNotification();
         //return Json(inc);

         return View("RequestTicket");
      }

      // get notification
      public async Task<IActionResult> GetNatification()
      {
         var users = await new UserAccountHttpClient(client).ReadUserAccounts();
         long userid = Convert.ToInt64(users.Where(r => r.RoleID == 1).FirstOrDefault().OID);

         NotificationDto? inc = await new NotificationHttpClient(client).GetNotification(userid);
         NotificationDto incList = new NotificationDto { Allnot = inc.Allnot.OrderByDescending(o => o.DateCreated).ToList(), Unreadnot = inc.Unreadnot };

         return Json(incList);
      }

      // MarkAllRead
      public async Task<IActionResult> MarkAllRead()
      {
         var users = await new UserAccountHttpClient(client).ReadUserAccounts();
         long userid = Convert.ToInt64(users.Where(r => r.RoleID == 1).FirstOrDefault().OID);
         bool inc = await new NotificationHttpClient(client).MarkAllRead(userid);

         return Json(inc);
      }

      // MarkAsRead
      public async Task<IActionResult> MarkasRead(long notid)
      {

         bool inc = await new NotificationHttpClient(client).MarkAsRead(notid);

         return Json(inc);
      }
   }
}
