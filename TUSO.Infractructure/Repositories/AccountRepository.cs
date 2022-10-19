//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TUSO.Domain.Dto;
//using TUSO.Domain.Entities;
//using TUSO.Infrastructure.Contracts;
//using TUSO.Infrastructure.SqlServer;

//namespace TUSO.Infrastructure.Repositories
//{
//   public class AccountRepository : Repository<UserAccount>, IAccountRepository
//   {

//      private readonly DataContext context;
//      public AccountRepository(DataContext _context) : base(_context)
//      {
//         this.context = _context;
//      }

//      public void Add(Userinfo entity)
//      {
//         throw new NotImplementedException();
//      }

//      public void Delete(Userinfo entity)
//      {
//         throw new NotImplementedException();
//      }

//      public User_detailDto GetbyRecoveryPassword(long Userid)
//      {
//         try
//         {
//            if (Userid != 0)
//            {
//               var result = context.Userinfoes.FirstOrDefault(x => x.Userid == Userid);
//               User_detailDto us = new User_detailDto();
//               if (result != null)
//               {
//                  us.Userid = Userid;
//                  us.Email = result.Email;
//                  us.Username = result.Firstname + " " + result.Lastname;
//                  us.Phone = result.Phone;

//               }

//               return us;
//            }

//            else
//            {
//               return null;
//            }
//         }
//         catch (Exception)
//         {
//            throw;
//         }
//      }

//      public Task<Userinfo> GetBySpecAsync(ISpecification<Userinfo> spec)
//      {
//         throw new NotImplementedException();
//      }

//      public Task<IReadOnlyList<Userinfo>> GetListBySpecAsync(ISpecification<Userinfo> spec)
//      {
//         throw new NotImplementedException();
//      }

//      public bool Isuserexists(string Email)
//      {
//         return context.Userinfoes.Any(x => x.Email == Email);
//      }

//      public void Update(Userinfo entity)
//      {
//         throw new NotImplementedException();
//      }

//      public bool VerifyCode(string code, long userid)
//      {
//         int cd = int.Parse(code);

//         var data = context.Userinfoes.Where(w => w.Userid == userid && w.Varifycode == cd).FirstOrDefault();
//         if (data != null)
//         {
//            data.Isvarified = true;
//            context.SaveChanges();
//            return true;
//         }
//         return false;

//      }

//      IQueryable<Userinfo> IRepository<Userinfo>.GetAll()
//      {
//         throw new NotImplementedException();
//      }

//      Userinfo IRepository<Userinfo>.GetById(long id)
//      {
//         throw new NotImplementedException();
//      }

//      Userinfo IRepository<Userinfo>.GetById(int key)
//      {
//         throw new NotImplementedException();
//      }

//      Task<Userinfo?> IRepository<Userinfo>.GetByIdAsync(long id)
//      {
//         throw new NotImplementedException();
//      }

//      Task<Userinfo?> IRepository<Userinfo>.GetByIdAsync(int key)
//      {
//         throw new NotImplementedException();
//      }

//      Task<LoginReturnDto> IAccountRepository.GetvalidUser(LoginDto loginDto)
//      {
//         string haspass = GenerateSaltedHash(loginDto.Password, "rk0365b302s48s");
//         var logdata = (from u in context.Userinfoes.Where(w => w.Email == loginDto.Email)
//                        join p in context.Passowrdinfoes.Where(w => w.Status == true && w.Password == haspass) on u.Userid equals p.Userid
//                        join r in context.Roleinfoes on u.Roleid equals r.Roleid

//                        select new { u.Firstname, u.Email, u.Userid, r.Role, u.Isvarified, u.Imagespath }).FirstOrDefault();
//         LoginReturnDto obj = new LoginReturnDto();
//         if (logdata != null)
//         {
//            obj.Fullname = logdata.Firstname;
//            obj.Email = logdata.Email;
//            obj.Role = logdata.Role;
//            obj.Userid = logdata.Userid;
//            obj.Varified = (bool)logdata.Isvarified;
//            obj.Profileimg = logdata.Imagespath == null || logdata.Imagespath == "" ? "/app-assets/images/avatars/avater.png" : logdata.Imagespath;
//         }
//         return Task.FromResult(obj);
//      }

//      //    //public async Task<AccountResultResponse> UserPasswordSignInAsync(LoginDto loginDto)
//      //    //{
//      //    //    var user = await GetCurrentUserAsync(loginDto.Email);

//      //    //    if (user != null)
//      //    //    {
//      //    //        using var hmac = new HMACSHA256(user.PasswordSalt);

//      //    //        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

//      //    //        for (int i = 0; i < user.PasswordHash.Length; i++)
//      //    //        {
//      //    //            if (computeHash[i] != user.PasswordHash[i]) return new AccountResultResponse(false);
//      //    //        }

//      //    //        return new AccountResultResponse(true, null, user);
//      //    //    }

//      //    //    return new AccountResultResponse(false);
//      //    //}

//      //    //public async Task<Userinfo> GetCurrentUserAsync(string email)
//      //    //{
//      //    //    var spec = new UserSpecification(email);

//      //    //    return await user.GetBySpecAsync(spec);
//      //    //    return user;
//      //    //}

//      //    //public async Task<bool> IsUserExists(string email)
//      //    //{
//      //    //    return await GetCurrentUserAsync(email) != null;
//      //    //}

//      //    //public async Task ChangePassword(string email, string password)
//      //    //{
//      //    //    using var hmac = new HMACSHA256();
//      //    //    var user = await GetCurrentUserAsync(email);
//      //    //    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
//      //    //    user.PasswordSalt = hmac.Key;
//      //    //}

//   }
//}
