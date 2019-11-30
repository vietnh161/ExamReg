using ExamReg.Model.Models;
using ExamReg.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ExamReg.WebApp.Api
{
  [RoutePrefix("api/sinhvien")]
  public class SinhVienController : ApiController
  {
    private ISinhVienService _sinhVienService;
    private ISinhVienLichThiService _sinhVienLichThiService;
    private ILichThiService _lichThiService;

    public SinhVienController(ISinhVienService sinhVienService, ISinhVienLichThiService sinhVienLichThiService, ILichThiService lichThiService)
    {
      this._sinhVienService = sinhVienService;
      this._sinhVienLichThiService = sinhVienLichThiService;
      this._lichThiService = lichThiService;
    }

    [Route("getall")]
    [HttpGet]
    public HttpResponseMessage GetAll(HttpRequestMessage request)
    {
      var models = _sinhVienService.GetAll();
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, models);
      return response;
    }

    [Route("getbyid/{id:int}")]
    [HttpGet]
    public HttpResponseMessage GetById(HttpRequestMessage request, int id)
    {
      var model = _sinhVienService.GetById(id);
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
      return response;
    }

    [Route("create")]
    [HttpPost]
    public HttpResponseMessage Create(HttpRequestMessage request, SinhVien sinhVien)
    {
      HttpResponseMessage response = null;
      if (!ModelState.IsValid)
      {
        response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
      }
      else
      {
        _sinhVienService.Add(sinhVien);
        _sinhVienService.SaveChanges();
        response = request.CreateResponse(HttpStatusCode.Created, sinhVien);

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
        _sinhVienService.Update(sinhVien);
        _sinhVienService.SaveChanges();
        response = request.CreateResponse(HttpStatusCode.Created, sinhVien);

      }

      return response;
    }

    [Route("delete")]
    [HttpDelete]
    public HttpResponseMessage Delete(HttpRequestMessage request, int id)
    {
      var model = _sinhVienService.Delete(id);
      _sinhVienService.SaveChanges();
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
      return response;
    }

    [Route("dangkylichthi")]
    [HttpPost]
    public HttpResponseMessage DangKyLichThi(HttpRequestMessage request, LichThi lichThi)
    {
      HttpResponseMessage response = null;
      string uid = HttpContext.Current.User.Identity.GetUserId();
      LichThi lt = _lichThiService.GetById(lichThi.LichThiId);
      if (lt.Status)
      {
        SinhVien sv = _sinhVienService.GetByUserId(uid);
        SinhVienLichThi s = new SinhVienLichThi();
        s.LichThiId = lt.LichThiId;
        s.SinhVienId = sv.SinhVienId;
        s.Status = true;  

        _sinhVienLichThiService.Add(s);
        _sinhVienLichThiService.SaveChanges();

        int count = lt.PhongThi.SoLuongMay;
        if (lt.Count <= count)
        {
          lt.Count++;
          _lichThiService.Update(lt);
        }
        else
        {
          lt.Status = false;
          _lichThiService.Update(lt);
        }
        _lichThiService.SaveChanges();
        response = request.CreateResponse(HttpStatusCode.OK, lt);
      }
      else
      {
        response = request.CreateResponse(HttpStatusCode.BadRequest, "Khong the dang ky them");
      }

      return response;
    }

    [Route("huydangkylichthi")]
    [HttpPost]
    public HttpResponseMessage HuyDangKyLichThi(HttpRequestMessage request, SinhVienLichThi sinhVienLichThi)
    {
      HttpResponseMessage response = null;
      string uid = HttpContext.Current.User.Identity.GetUserId();
      SinhVienLichThi svlt = _sinhVienLichThiService.GetById(sinhVienLichThi.Id);
      if (svlt.LichThi.Count > 0)
      {
        SinhVien sv = _sinhVienService.GetByUserId(uid);
        LichThi lt = _lichThiService.GetById(sinhVienLichThi.LichThiId);
        _sinhVienLichThiService.Delete(sinhVienLichThi.Id);
        _sinhVienLichThiService.SaveChanges();
        lt.Count--;
        if (lt.Count <= lt.PhongThi.SoLuongMay)
        {
          lt.Status = true;
        }

        _lichThiService.Update(lt);
        //var test = _sinhVienLichThiService.GetById(sinhVienLichThi.Id);
        _lichThiService.SaveChanges();
        response = request.CreateResponse(HttpStatusCode.OK, "ok");
      }
      else
      {
        response = request.CreateResponse(HttpStatusCode.BadRequest, "co loi xay ra");
      }
      return response;
    }

    [Route("getlichthi")]
    [HttpGet]
    public HttpResponseMessage GetLichThi(HttpRequestMessage request)
    {
      string uid = HttpContext.Current.User.Identity.GetUserId();
      SinhVien sv = _sinhVienService.GetByUserId(uid);
      var models = _sinhVienLichThiService.GetMulti(x => x.SinhVienId == sv.SinhVienId);
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, models);
      return response;
    }

    [Route("gethocphan")]
    [HttpGet]
    public HttpResponseMessage GetHocPhan(HttpRequestMessage request)
    {
      string uid = HttpContext.Current.User.Identity.GetUserId();
      SinhVien sv = _sinhVienService.GetByUserId(uid);
      var models = _sinhVienService.GetLopHocPhan(sv.SinhVienId);
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, models);
      return response;
    }

  }
}

