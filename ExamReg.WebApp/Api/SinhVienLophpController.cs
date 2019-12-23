using ExamReg.Model.Models;
using ExamReg.Service;
using ExamReg.WebApp.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExamReg.WebApp.Api
{
  [RoutePrefix("api/sinhvienlophp")]
  public class SinhVienLophpController : ApiController
  {
    ISinhVienLophpService _sinhVienLophpService;
    ISinhVienService _sinhVienService;
    IHocPhanService _hocPhanService;
    public SinhVienLophpController(ISinhVienLophpService _sinhVienLophpService, ISinhVienService _sinhVienService, IHocPhanService _hocPhanService)
    {
      this._sinhVienLophpService = _sinhVienLophpService;
      this._sinhVienService = _sinhVienService;
      this._hocPhanService = _hocPhanService;
    }


    [Route("getall")]
    [HttpGet]
    public HttpResponseMessage GetAll(HttpRequestMessage request)
    {
      var results = _sinhVienLophpService.GetAll();
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, results);
      return response;
    }

    [Route("getbyid/{id:int}")]
    [HttpGet]
    public HttpResponseMessage GetById(HttpRequestMessage request, int id)
    {
      var model = _sinhVienLophpService.GetById(id);
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
      return response;
    }
    [Route("getmultipaging")]
    [HttpGet]
    public HttpResponseMessage GetMultiPaging(HttpRequestMessage request, int currentPage, int pageSize, int lopHpId, string keyword)
    {
      HttpResponseMessage response;
      //Message message = new Message();

      //var a = request.Content.ReadAsStringAsync();
      //Paging paging = JsonConvert.DeserializeObject<Paging>(a.Result);
      int totalRow = 0;
      try
      {
        IEnumerable<SinhVienLophp> model = _sinhVienLophpService.GetMultiPaging(currentPage, pageSize, keyword, lopHpId, out totalRow);
        foreach (var item in model)
        {
          item.SinhVien = _sinhVienService.GetById(item.SinhVienId);
        }
        ListResult<SinhVienLophp> listResult = new ListResult<SinhVienLophp>()
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
    public HttpResponseMessage Create(HttpRequestMessage request, SvHpVm SvHpVm)
    {
      HttpResponseMessage response = null;
      Message message = new Message();
      try
      {
        if (!ModelState.IsValid)
        {
          response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        }
        else
        {
          var svId = _sinhVienService.GetByConDition(x => x.MSSV == SvHpVm.MSSV).SinhVienId;
          var hpId = SvHpVm.LophpId;
          bool isContain = _sinhVienLophpService.checkDuplicate(x => x.LophpId == hpId && x.SinhVienId == svId);
          if (!isContain && hpId != null)
          {
            var svhp = new SinhVienLophp()
            {
              LophpId = hpId,
              SinhVienId = svId,
              DuDieuKien = SvHpVm.duDieuKien,

            };
            _sinhVienLophpService.Add(svhp);
            _sinhVienLophpService.SaveChanges();
            message.successCount++;
          }
          else
          {
            message.notSuccessCount++;
          }
        }
      }
      catch (NullReferenceException ex)
      {
        message.message = ex.Message;
        message.notSuccessCount++;
      }

      return request.CreateResponse(HttpStatusCode.OK, message);
    }
 

  [Route("addMulti")]
  [HttpPost]
  public HttpResponseMessage addMulti(HttpRequestMessage request)
  {
    Message message = new Message();
    var a = request.Content.ReadAsStringAsync();
    List<SvHpVm> list = JsonConvert.DeserializeObject<List<SvHpVm>>(a.Result);
    try
    {
      foreach (var item in list)
      {
        var svId = _sinhVienService.GetByConDition(x => x.MSSV == item.MSSV).SinhVienId;
        var hpId = _hocPhanService.GetByConDition(x => x.Title == item.MaHp && x.KiThiId == item.KiThiId).LophpId;
        bool isContain = _sinhVienLophpService.checkDuplicate(x => x.LophpId == hpId && x.SinhVienId == svId);
        if (!isContain && hpId != null)
        {
          var svhp = new SinhVienLophp()
          {
            LophpId = hpId,
            SinhVienId = svId,
            DuDieuKien = true,

          };
          _sinhVienLophpService.Add(svhp);
          _sinhVienLophpService.SaveChanges();
          message.successCount++;
        }
        else
        {
          message.notSuccessCount++;
        }
      }
    }
    catch (NullReferenceException ex)
    {
      message.message = ex.Message;
      message.notSuccessCount++;
    }

    return request.CreateResponse(HttpStatusCode.OK, message);
  }

  [Route("khongquamon")]
  [HttpPost]
  public HttpResponseMessage addMultiKoQuaMon(HttpRequestMessage request)
  {
    Message message = new Message();
    var a = request.Content.ReadAsStringAsync();
    List<SvHpVm> list = JsonConvert.DeserializeObject<List<SvHpVm>>(a.Result);
    try
    {
      foreach (var item in list)
      {
        var svId = _sinhVienService.GetByConDition(x => x.MSSV == item.MSSV).SinhVienId;
        var hpId = _hocPhanService.GetByConDition(x => x.Title == item.MaHp).LophpId;


        var data = _sinhVienLophpService.GetByConDition(x => x.LophpId == hpId && x.SinhVienId == svId);
        if (data != null)
        {
          data.DuDieuKien = false;
          _sinhVienLophpService.Update(data);
          _sinhVienLophpService.SaveChanges();
          message.successCount++;
        }
        else
        {
          message.notSuccessCount++;
        }
      }
    }
    catch (NullReferenceException ex)
    {
      message.message = ex.Message;
      message.notSuccessCount++;
    }

    return request.CreateResponse(HttpStatusCode.OK, message);
  }

  [Route("update")]
  [HttpPut]
  public HttpResponseMessage Update(HttpRequestMessage request, SinhVienLophp sinhVienLophp)
  {
    HttpResponseMessage response = null;
    if (!ModelState.IsValid)
    {
      response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
    }
    else
    {
      _sinhVienLophpService.Update(sinhVienLophp);
      _sinhVienLophpService.SaveChanges();
      response = request.CreateResponse(HttpStatusCode.Created, sinhVienLophp);

    }

    return response;
  }

  [Route("delete")]
  [HttpDelete]
  public HttpResponseMessage Delete(HttpRequestMessage request, int id)
  {
    var model = _sinhVienLophpService.Delete(id);

    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
    return response;
  }
}
public class SvHpVm
{
  public string MSSV { set; get; }
  public string MaHp { set; get; }
  public int KiThiId { set; get; }
    public int LophpId { set; get; }
  public bool duDieuKien { set; get; }
}
}
