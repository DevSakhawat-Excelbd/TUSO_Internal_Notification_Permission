using Microsoft.AspNetCore.SignalR;

namespace TUSO.Web
{
   public class WebHub : Hub
   {
      public async void SendNotification()
      {
         await Clients.All.SendAsync("Refresh");
      }
   }
}
