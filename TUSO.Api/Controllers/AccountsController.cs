//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Net.Mail;
//using System.Net;
//using TUSO.Domain.Dto;
//using TUSO.Infrastructure.Contracts;
//using TUSO.Infrastructure.SqlServer;

//namespace TUSO.Api.Controllers
//{
//   [Route("api/[controller]")]
//   [ApiController]
//   public class AccountsController : ControllerBase
//   {
//      private readonly IUnitOfWork UnitOfWork;

//      private readonly IConfiguration config;
//      private readonly DataContext context;

//      /// <summary>
//      /// Accounts constructor
//      /// </summary>
//      /// <param name="_UnitOfWork"></param>
//      /// <param name="_config"></param>
//      /// <param name="_context"></param>
//      public AccountsController(IUnitOfWork _UnitOfWork, IConfiguration _config, DataContext _context)
//      {
//         UnitOfWork = _UnitOfWork;
//         config = _config;
//         context = _context;
//      }

//      [HttpPost("register")]
//      [AllowAnonymous]
//      public async Task<ActionResult<UserDto>> Register(RegisterViewModel r)
//      {
//         try
//         {
//            var exist = UnitOfWork.AccountRepository.Isuserexists(r.Email);
//            if (exist)
//            {
//               return BadRequest("Email exist");
//            }

//            if (ModelState.IsValid)
//            {
//               int code = getcode();
//               Userinfo user = new Userinfo
//               {
//                  Userid = r.Userid,
//                  Firstname = r.Firstname,
//                  Middlename = r.Middlename,
//                  Lastname = r.Lastname,
//                  Email = r.Email,
//                  Registrationdate = DateTime.Now,
//                  Varifycode = code,
//                  Isvarified = false,
//                  Status = true,
//                  Roleid = context.Roleinfoes.Where(w => w.Role == "User").Select(s => s.Roleid).First(),
//               };

//               context.Userinfoes.Add(user);
//               context.SaveChanges();

//               string haspass = GenerateSaltedHash(r.Password, "rk0365b302s48s");
//               var userpassInDb = new Passwordinfo
//               {
//                  Userid = user.Userid,
//                  Password = haspass,
//                  Status = true,
//               };
//               context.Passowrdinfoes.Add(userpassInDb);
//               context.SaveChanges();

//               senDmail(user.Firstname, user.Email, (int)user.Varifycode);
//               var userToReturn = user;

//               //return RedirectToAction("Confirmation", "Account", us);

//               return Ok(userToReturn);
//            }

//            return BadRequest();
//         }
//         catch (Exception ex)
//         {
//            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
//         }
//      }

//      private void senDmail(string firstname, string email, int varificationcode)
//      {
//         try
//         {
//            MailMessage message = new MailMessage();

//            message.From = new MailAddress("confirmation@code-seekers.com");
//            message.To.Add(new MailAddress(email));
//            message.Subject = "Varification";
//            message.IsBodyHtml = true;
//            message.Body = "<div><h3>Hi " + firstname + "</h3><br><p>Your varification code is <b>" + varificationcode + "</b> <p><br><p>Thank you</p></div>";

//            using (SmtpClient smtp = new SmtpClient())
//            {
//               smtp.Credentials = new NetworkCredential("confirmation@code-seekers.com", "bas8E4^96");
//               smtp.Port = 25;
//               smtp.Host = "code-seekers.com";

//               smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

//               smtp.EnableSsl = false;
//               smtp.Send(message);
//            }
//         }
//         catch (Exception)
//         {

//         }
//      }

//      [HttpPost("login")]
//      [AllowAnonymous]
//      public async Task<ActionResult> Login(LoginDto loginDto)
//      {
//         var result = await UnitOfWork.AccountRepository.GetvalidUser(loginDto);

//         if (result.Email != null && result.Role != null)
//         {
//            return Ok(new LoginReturnDto
//            {
//               Userid = result.Userid,
//               Fullname = result.Fullname,
//               Email = result.Email,
//               Role = result.Role,
//               Designation = result.Designation,
//               Varified = result.Varified,
//               Profileimg = result.Profileimg,
//               Token = new TokenService(config).GenerateToken(result)
//            });
//         }

//         return Unauthorized();
//      }

//      [HttpPost]
//      [Route("[action]")]
//      public async Task<IActionResult> ChangePassword([FromBody] ChangepasswordDto changepass)
//      {
//         try
//         {
//            if (changepass.Userid == 0)
//               return BadRequest();

//            var changepassinfoInDb = UnitOfWork.PassowordRepository.Idpassexist(changepass.Userid, changepass.Oldpassword);

//            if (!changepassinfoInDb)
//            {
//               return BadRequest("User ID and password does not exist");
//            }

//            var changepassinfo = UnitOfWork.PassowordRepository.Changepassword(changepass.Userid, changepass.Newpassword);

//            await UnitOfWork.SaveChangesAsync();

//         }
//         catch (Exception)
//         {
//            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
//         }

//         return Ok();
//      }

//      [HttpGet("is-email-unique")]
//      public async Task<ActionResult<bool>> IsEmailUserUnique([FromQuery] string email)
//      {
//         if (email == null) return true;

//         var exist = UnitOfWork.AccountRepository.Isuserexists(email);
//         return exist;
//      }

//      [HttpPost]
//      [Route("SaveRecoveryRequest")]
//      public async Task<IActionResult> SaveRecoveryRequest([FromBody] User_detailDto user_detailDto)
//      {
//         try
//         {
//            var Check = UnitOfWork.AccountRepository.GetbyRecoveryPassword(user_detailDto.Userid);
//            if (Check != null)
//            {
//               var userRecovery = new User_detailDto()
//               {
//                  Phone = Check.Phone,
//                  Email = Check.Email,
//                  Username = Check.Username,

//               };
//               //UnitOfWork.Accountrepository.Add(userRecovery);
//               //await UnitOfWork.SaveChangesAsync();

//               return Ok(userRecovery);
//            }
//            else
//            {
//               return NotFound("No matching account found !");
//            }
//         }
//         catch (Exception ex)
//         {

//            return BadRequest(ex.Message);
//         }
//      }

//      [HttpGet]
//      [AllowAnonymous]
//      [Route("VerifyCode")]
//      public async Task<ActionResult> VerifyCode(string verifycode, long Userid)
//      {
//         var Check = UnitOfWork.AccountRepository.VerifyCode(verifycode, Userid);
//         return Ok(Check);
//      }
//   }
//}
