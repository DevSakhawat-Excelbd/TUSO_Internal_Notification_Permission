using Microsoft.AspNetCore.Mvc;

namespace TUSO.Web.Controllers
{
   public class IncidentController : Controller
   {
      private readonly HttpClient client;
      public IncidentController( HttpClient client)
      {
         this.client = client;
      }

      public IActionResult RequestTicket()
      {
         return View();
      }
   }
}
