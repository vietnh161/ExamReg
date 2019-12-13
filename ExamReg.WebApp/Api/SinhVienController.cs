using ExamReg.Model.Models;
using ExamReg.Service;
using ExamReg.WebApp.App_Start;
using ExamReg.WebApp.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ExamReg.WebApp.Api
{
  [RoutePrefix("api/sinhvien")]
  public class SinhVienController : ApiController
  {
    private ISinhVienService _sinhVienService;
    private ApplicationUserManager _userManager;
    private ISinhVienLichThiService _sinhVienLichThiService;
    private ILichThiService _lichThiService;


    public SinhVienController(ISinhVienService sinhVienService, ISinhVienLichThiService sinhVienLichThiService, ILichThiService lichThiService, ApplicationUserManager _userManager)
    {
      this._sinhVienService = sinhVienService;
      this._sinhVienLichThiService = sinhVienLichThiService;
      this._lichThiService = lichThiService;
      this._userManager = _userManager;
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

    [Route("getall")]
    [HttpGet]
    public HttpResponseMessage GetAll(HttpRequestMessage request)
    {
      var id = User.Identity.Name;
      var models = _sinhVienService.GetAll();
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, models);
      return response;
    }

    [Route("getbyid/{id:int}")]
    [HttpGet]
    public HttpResponseMessage GetById(HttpRequestMessage request, int id)
    {
      HttpResponseMessage response;
      try
      {
        var model = _sinhVienService.GetById(id);
        response = request.CreateResponse(HttpStatusCode.OK, model);
      }
      catch (NullReferenceException ex)
      {
        response = request.CreateResponse(HttpStatusCode.OK, ex.Message);
      }

      return response;
    }
    [Route("getmultipagingbylichthi")]
    [HttpGet]
    public HttpResponseMessage GetMultiPaging(HttpRequestMessage request, int currentPage, int pageSize, int lichThiId, string sort, string keyword, string sortBy)
    {
      HttpResponseMessage response;

      //Message message = new Message();

      //var a = request.Content.ReadAsStringAsync();
      //Paging paging = JsonConvert.DeserializeObject<Paging>(a.Result);
      int totalRow = 0;
      try
      {
        IEnumerable<SinhVien> model = _sinhVienService.GetMultiPaging(currentPage, pageSize, lichThiId, sort, sortBy, keyword, out totalRow);
        ListResult<SinhVien> listResult = new ListResult<SinhVien>()
        {
          result = model,
          totalRow = totalRow,
        };

        response = request.CreateResponse(HttpStatusCode.OK, listResult);
      }
      catch (NullReferenceException ex)
      {
        response = request.CreateResponse(HttpStatusCode.OK, ex.Message);
      }

      return response;
    }

    [Route("getmultipaging")]
    [HttpGet]
    public HttpResponseMessage GetMultiPaging(HttpRequestMessage request, int currentPage, int pageSize, string sort, string keyword, string sortBy)
    {
      HttpResponseMessage response;

      //Message message = new Message();

      //var a = request.Content.ReadAsStringAsync();
      //Paging paging = JsonConvert.DeserializeObject<Paging>(a.Result);
      int totalRow = 0;
      try
      {
        IEnumerable<SinhVien> model = _sinhVienService.GetMultiPaging(currentPage, pageSize, sort, sortBy, keyword, out totalRow);
        foreach (var item in model)
        {

          item.User = UserManager.FindById(item.UserId);
        }

        ListResult<SinhVien> listResult = new ListResult<SinhVien>()
        {
          result = model,
          totalRow = totalRow,
        };

        response = request.CreateResponse(HttpStatusCode.OK, listResult);
      }
      catch (NullReferenceException ex)
      {
        response = request.CreateResponse(HttpStatusCode.OK, ex.Message);
      }

      return response;
    }

    [Route("create")]
    [HttpPost]
    public async Task<HttpResponseMessage> Create(HttpRequestMessage request, SinhVien sinhVien)
    {
      HttpResponseMessage response = null;
      if (!ModelState.IsValid)
      {
        response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
      }
      else
      {

        if (!_sinhVienService.checkMssv(sinhVien.MSSV))
        {
          var user = new ApplicationUser()
          {
            UserName = sinhVien.User.UserName,
            Email = sinhVien.email,
            FullName = sinhVien.FullName
          };
          var result = await UserManager.CreateAsync(user, sinhVien.MSSV);
          if (result.Succeeded)
          {
            var userr = await UserManager.FindByNameAsync(user.UserName);
            await _userManager.AddToRoleAsync(userr.Id, "Student");
            sinhVien.UserId = userr.Id;
            sinhVien.email = sinhVien.MSSV + "@vnu.edu.vn";
            sinhVien.User = null;
            _sinhVienService.Add(sinhVien);
            try
            {
              _sinhVienService.SaveChanges();

            }
            catch (DbEntityValidationException ex)
            {

              UserManager.Delete(userr);
              return request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            response = request.CreateResponse(HttpStatusCode.OK, sinhVien);
          }
          else
          {
            response = request.CreateResponse(HttpStatusCode.BadRequest, "không thành công");
          }
        }
        else
        {
          response = request.CreateResponse(HttpStatusCode.BadRequest, "Trùng mssv");
        }
      }


      return response;
    }



    [Route("update")]
    [HttpPut]
    public HttpResponseMessage Update(HttpRequestMessage request, SinhVien sinhVien)
    {
      HttpResponseMessage response = null;
      if (!ModelState.IsValid)
      {
        response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
      }
      else
      {
        if (_sinhVienService.checkDuplicateUpdate(sinhVien))
        {

          sinhVien.User = null;
          _sinhVienService.Update(sinhVien);
          try
          {
            _sinhVienService.SaveChanges();
            response = request.CreateResponse(HttpStatusCode.Created, sinhVien);

          }
          catch (DbEntityValidationException ex)
          {


            return request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
          }



        }
        else
        {
          if (!_sinhVienService.checkMssv(sinhVien.MSSV))
          {
            sinhVien.User = null;
            _sinhVienService.Update(sinhVien);
            try
            {
              _sinhVienService.SaveChanges();
              response = request.CreateResponse(HttpStatusCode.Created, sinhVien);

            }
            catch (DbEntityValidationException ex)
            {


              return request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
          }
          else
          {
            response = request.CreateResponse(HttpStatusCode.BadRequest, "Sửa không thành công");
          }
        
        }

      }

        return response;
    }

    [Route("delete")]
    [HttpDelete]
    public HttpResponseMessage Delete(HttpRequestMessage request, int[] ids)
    {

      Message message = new Message();
      if (ids == null)
      {
        message.message = "co loi xay ra";
        return request.CreateResponse(HttpStatusCode.BadRequest, message);
      }
      foreach (var id in ids)
      {
        var userId = _sinhVienService.GetById(id).UserId;
        var sv =  _sinhVienService.Delete(id);
        if (sv != null)
        {
          try
          {
            _sinhVienService.SaveChanges();
            UserManager.Delete(UserManager.FindById(userId));
            message.successCount++;

          }
          catch (DbEntityValidationException ex)
          {
            return request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
          }
         
        }
        else
        {
          message.notSuccessCount++;
        }      
      }

      _sinhVienService.SaveChanges();
      message.message = "Xoa thanh cong";
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, message);
      return response;
    }

    [Route("dangkylichthi")]
    [HttpPost]
    public HttpResponseMessage DangKyLichThi(HttpRequestMessage request, LichThi lichThi)
    {
      //HttpResponseMessage response = null;
      //string uid = HttpContext.Current.User.Identity.GetUserId();
      //LichThi lt = _lichThiService.GetById(lichThi.LichThiId);
      //if (lt.Status)
      //{
      //  SinhVien sv = _sinhVienService.GetByUserId(uid);
      //  SinhVienLichThi s = new SinhVienLichThi();
      //  s.LichThiId = lt.LichThiId;
      //  s.SinhVienId = sv.SinhVienId;
      //  s.Status = true;  

      //  _sinhVienLichThiService.Add(s);
      //  _sinhVienLichThiService.SaveChanges();

      //  int count = lt.PhongThi.SoLuongMay;
      //  if (lt.Count <= count)
      //  {
      //    lt.Count++;
      //    _lichThiService.Update(lt);
      //  }
      //  else
      //  {
      //    lt.Status = false;
      //    _lichThiService.Update(lt);
      //  }
      //  _lichThiService.SaveChanges();
      //  response = request.CreateResponse(HttpStatusCode.OK, lt);
      //}
      //else
      //{
      //  response = request.CreateResponse(HttpStatusCode.BadRequest, "Khong the dang ky them");
      //}

      //return response;
      return null;
    }

    [Route("huydangkylichthi")]
    [HttpPost]
    public HttpResponseMessage HuyDangKyLichThi(HttpRequestMessage request, SinhVienLichThi sinhVienLichThi)
    {
      //HttpResponseMessage response = null;
      //string uid = HttpContext.Current.User.Identity.GetUserId();
      //SinhVienLichThi svlt = _sinhVienLichThiService.GetById(sinhVienLichThi.Id);
      //if (svlt.LichThi.Count > 0)
      //{
      //  SinhVien sv = _sinhVienService.GetByUserId(uid);
      //  LichThi lt = _lichThiService.GetById(sinhVienLichThi.LichThiId);
      //  _sinhVienLichThiService.Delete(sinhVienLichThi.Id);
      //  _sinhVienLichThiService.SaveChanges();
      //  lt.Count--;
      //  if (lt.Count <= lt.PhongThi.SoLuongMay)
      //  {
      //    lt.Status = true;
      //  }

      //  _lichThiService.Update(lt);
      //  //var test = _sinhVienLichThiService.GetById(sinhVienLichThi.Id);
      //  _lichThiService.SaveChanges();
      //  response = request.CreateResponse(HttpStatusCode.OK, "ok");
      //}
      //else
      //{
      //  response = request.CreateResponse(HttpStatusCode.BadRequest, "co loi xay ra");
      //}
      //return response;
      return null;
    }

    [Route("getlichthi")]
    [Authorize(Roles = "Student")]
    [HttpGet]
    public HttpResponseMessage GetLichThi(HttpRequestMessage request)
    {
      var id = User.Identity.GetUserId();
      SinhVien sv = _sinhVienService.GetByUserId(id);
      var result = _lichThiService.GetAllBySv(sv.SinhVienId);
      ListResult<LichThi> listResult = new ListResult<LichThi>()
      {
        result = result,
        totalRow = 0,
      };
      return request.CreateResponse(HttpStatusCode.OK, listResult);
    }


    [Route("getlichthidadky")]
    [Authorize(Roles = "Student")]
    [HttpGet]
    public HttpResponseMessage GetLichThiDaDky(HttpRequestMessage request)
    {
      var id = User.Identity.GetUserId();
      SinhVien sv = _sinhVienService.GetByUserId(id);
      var result = _lichThiService.GetDaDky(sv.SinhVienId);
      ListResult<LichThi> listResult = new ListResult<LichThi>()
      {
        result = result,
        totalRow = 0,
      };
      return request.CreateResponse(HttpStatusCode.OK, listResult);
    }



    [Route("gethocphan")]
    [Authorize(Roles = "Student")]
    [HttpGet]
    public HttpResponseMessage GetHocPhan(HttpRequestMessage request)
    {
      //string uid = HttpContext.Current.User.Identity.GetUserId();
      //SinhVien sv = _sinhVienService.GetByUserId(uid);
      //var models = _sinhVienService.GetLopHocPhan(sv.SinhVienId);
      //HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, models);
      //return response;
      return null;
    }

  }
}

