using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using TUSO.Domain.Dto;

namespace TUSO.Web.HttpClients
{
   public class NotificationHttpClient
   {
      private readonly HttpClient client;
      private readonly static string BaseUrl = "https://localhost:7026/tuso-api/";

      public NotificationHttpClient(HttpClient client)
      {
         //client.BaseAddress = new Uri(BaseUrl);
         //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         this.client = client;
      }

      public async Task<bool> AddNotification()
      {

         //var response = await this.client.GetAsync($"{BaseUrl}notification/?key=" + key);

         var response = await client.GetAsync($"{BaseUrl}addNotification");
         if (!response.IsSuccessStatusCode)
         {
            return false;
         }
         string result = await response.Content.ReadAsStringAsync();
         var not = JsonConvert.DeserializeObject<bool>(result);
         return not;
      }

      public async Task<NotificationDto> GetNotification(long key)
      {

         //var response = await client.GetAsync($"{BaseUrl}notifications/key/?key=" + key);
         var response = await client.GetAsync($"https://localhost:7026/tuso-api/notifications/" + key);
         if (!response.IsSuccessStatusCode)
         {
            return new NotificationDto();
         }
         string result = await response.Content.ReadAsStringAsync();
         var not = JsonConvert.DeserializeObject<NotificationDto>(result);
         return not;
      }

      public async Task<bool> MarkAllRead(long key)
      {

         var response = await client.GetAsync($"{BaseUrl}mark-all-read/" + key);
         if (!response.IsSuccessStatusCode)
         {
            return false;
         }
         string result = await response.Content.ReadAsStringAsync();
         var not = JsonConvert.DeserializeObject<bool>(result);
         return not;
      }

      public async Task<bool> MarkAsRead(long key)
      {         
         //var response = await this.client.GetAsync($"{BaseUrl}mark-as-read/" + key);
         var response = await this.client.GetAsync($"https://localhost:7026/tuso-api/mark-as-read/" + key);
         if (!response.IsSuccessStatusCode)
         {
            return false;
         }
         string result = await response.Content.ReadAsStringAsync();
         var not = JsonConvert.DeserializeObject<bool>(result);
         return not;
      }
   }
}
