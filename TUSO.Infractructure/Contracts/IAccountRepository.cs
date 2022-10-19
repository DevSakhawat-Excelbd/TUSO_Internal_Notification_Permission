using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUSO.Domain.Dto;
using TUSO.Domain.Entities;

namespace TUSO.Infrastructure.Contracts
{
   public interface IAccountRepository : IRepository<UserAccount>
   {
      //Task<LoginReturnDto> GetvalidUser(LoginDto loginDto);
      //User_detailDto GetbyRecoveryPassword(long Userid);
      bool Isuserexists(string Email);
      bool VerifyCode(string code, long userid);
   }
}
