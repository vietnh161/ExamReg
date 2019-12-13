using ExamReg.Model.Models;
using ExamReg.Service;
using ExamReg.WebApp.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ExamReg.WebApp.Common;
using System.Data.Entity.Validation;

namespace ExamReg.WebApp.Api
{
  [RoutePrefix("api/account")]
  public class AccountController : ApiController
  {
    private ApplicationSignInManager _signInManager;
    private ApplicationUserManager _userManager;
    private ISinhVienService _sinhVienService;


    public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ISinhVienService sinhVienService)
    {
      UserManager = userManager;
      SignInManager = signInManager;
      _sinhVienService = sinhVienService;
    }

    public ApplicationSignInManager SignInManager
    {
      get
      {
        return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
      }
      private set
      {
        _signInManager = value;
      }
    }

    public ApplicationUserManager UserManager
    {
      get
      {
        return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
      }
      private set
      {
        _userManager = value;
      }
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("login")]
    public async Task<HttpResponseMessage> Login(HttpRequestMessage request, string userName, string password)
    {
      if (!ModelState.IsValid)
      {
        return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
      }
      // This doesn't count login failures towards account lockout
      // To enable password failures to trigger account lockout, change to shouldLockout: true
      var result = await SignInManager.PasswordSignInAsync(userName, password, false, shouldLockout: false);
      // _userManager.token
      return request.CreateResponse(HttpStatusCode.OK, result);
    }

    [HttpPost]
    [Authorize]
    [Route("logout")]
    public HttpResponseMessage Logout(HttpRequestMessage request)
    {
      
      var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
      authenticationManager.SignOut();
      return request.CreateResponse(HttpStatusCode.OK, new { success = true });
    }

    [HttpGet]
    [Authorize]
    [Route("getUserlogged")]
    public HttpResponseMessage getUser(HttpRequestMessage request)
    {
      
      var id = User.Identity.GetUserId();
      ApplicationUser user = UserManager.FindById(id);
      var role = UserManager.GetRoles<ApplicationUser, string>(id);
      var a = new Dictionary<string, string>()
      {
         {"fullname" , user.FullName },
         {"role", role[0] }
      };


      return request.CreateResponse(HttpStatusCode.OK,a);
    }


    [HttpPost]
    [Route("addUser")]
    public async Task<HttpResponseMessage> AddUser(HttpRequestMessage request)
    {
      Message message = new Message();
      var a = request.Content.ReadAsStringAsync();
      List<SinhVien> list = JsonConvert.DeserializeObject<List<SinhVien>>(a.Result);
      try
      {
        foreach (var item in list)
        {
          if (!_sinhVienService.checkMssv(item.MSSV))
          {


            var user = new ApplicationUser()
            {
              UserName = item.MSSV,
              Email = item.MSSV + "@vnu.edu.vn",
              FullName = item.FullName
            };

            var result = await UserManager.CreateAsync(user, item.MSSV);
            if (result.Succeeded)
            {
              var userr = await UserManager.FindByNameAsync(user.UserName);
              await _userManager.AddToRoleAsync(userr.Id, "Student");
              item.UserId = userr.Id;
              item.email = item.MSSV + "@vnu.edu.vn";
              _sinhVienService.Add(item);
              try
              {
                _sinhVienService.SaveChanges();
                message.successCount++;
              }
              catch (DbEntityValidationException ex)
              {
                
                UserManager.Delete(userr);
                return request.CreateResponse(HttpStatusCode.OK, ex.Message);
              } 
             
            }
            else
            {
              message.message += item.MSSV + ", ";
              message.notSuccessCount++;
            }
          }
          else
          {
            message.notSuccessCount++;
          }
        }
      }
      catch (Exception ex)
      {
        return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
      }

      return request.CreateResponse(HttpStatusCode.OK, message);
    }
    [HttpPost]
    [Route("addAdmin")]
    public async Task<HttpResponseMessage> CreateUserAdm(HttpRequestMessage request, ApplicationUser user)
    {
   
      await UserManager.CreateAsync(user, "123456$");
      var userr = await UserManager.FindByNameAsync(user.UserName);
      await _userManager.AddToRoleAsync(userr.Id, "Admin");
      return request.CreateResponse(HttpStatusCode.OK);
    }


  }
}
