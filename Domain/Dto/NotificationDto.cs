using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUSO.Domain.Entities;

namespace TUSO.Domain.Dto
{
   public class NotificationDto
   {
      public List<Notification> Allnot { get; set; }
      public int Unreadnot { get; set; }
   }
}
