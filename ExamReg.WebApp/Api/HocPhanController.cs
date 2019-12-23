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
  [RoutePrefix("api/hocphan")]
  [Authorize(Roles = "Admin")]
  public class HocPhanController : ApiController
  {
    private ILichThiService _lichThiService;
    private IHocPhanService _hocPhanService;
    private IMonThiService _monThiService;

    public HocPhanController(ILichThiService lichThiService, IHocPhanService _hocPhanService, IMonThiService _monThiService)
    {
      this._lichThiService = lichThiService;
      this._hocPhanService = _hocPhanService;
      this._monThiService = _monThiService;
    }

    [Route("getall")]
    [HttpGet]
    public HttpResponseMessage GetAll(HttpRequestMessage request)
    {
      var models = _hocPhanService.GetAll();
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, models);
      return response;
    }

    [Route("getbyid/{id:int}")]
    [HttpGet]
    public HttpResponseMessage GetById(HttpRequestMessage request, int id)
    {
      var model = _hocPhanService.GetById(id);
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
      return response;
    }

    [Route("getmultipaging")]
    [HttpGet]
    public HttpResponseMessage GetMultiPaging(HttpRequestMessage request, int currentPage, int pageSize,int kithi, string keyword)
    {
      HttpResponseMessage response;
      //Message message = new Message();

      //var a = request.Content.ReadAsStringAsync();
      //Paging paging = JsonConvert.DeserializeObject<Paging>(a.Result);
      int totalRow = 0;
      try
      {
        IEnumerable<LopHocPhan> model = _hocPhanService.GetMultiPaging(currentPage, pageSize, kithi, keyword, out totalRow);
        ListResult<LopHocPhan> listResult = new ListResult<LopHocPhan>()
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

    [Route("getmultipaging-sub")]
    [HttpGet]
    public HttpResponseMessage GetMultiPaging(HttpRequestMessage request, int currentPage, int pageSize,int monThiId, int kithi, string keyword)
    {
      HttpResponseMessage response;
      //Message message = new Message();

      //var a = request.Content.ReadAsStringAsync();
      //Paging paging = JsonConvert.DeserializeObject<Paging>(a.Result);
      int totalRow = 0;
      try
      {
        IEnumerable<LopHocPhan> model = _hocPhanService.GetMultiPaging(currentPage, pageSize,monThiId, kithi, keyword, out totalRow);
        ListResult<LopHocPhan> listResult = new ListResult<LopHocPhan>()
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
    public HttpResponseMessage Create(HttpRequestMessage request, LopHocPhan lopHocPhan)
    {
      HttpResponseMessage response = null;
      if (!ModelState.IsValid)
      {
        response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
      }
      else
      {
        if (!_hocPhanService.checkDuplicate(lopHocPhan.Title))
        {

          _hocPhanService.Add(lopHocPhan);
          //     _hocPhanService.SaveChanges();
          response = request.CreateResponse(HttpStatusCode.Created, lopHocPhan);
        }
        else
        {
          response = request.CreateResponse(HttpStatusCode.BadRequest, "LOI TRUNG LAP DU LIEU");
        }
      }

      return response;
    }

    [Route("addMulti")]
    [HttpPost]
    public HttpResponseMessage addMulti(HttpRequestMessage request)
    {
      Message message = new Message();
      var a = request.Content.ReadAsStringAsync();
      List<HocPhanVm> list = JsonConvert.DeserializeObject<List<HocPhanVm>>(a.Result);
      HttpResponseMessage response = null;
      try
      {
        foreach (var item in list)
        {
          if (!_monThiService.checkDuplicate(item.MaMon))
          {
            var monthi = new MonThi()
            {
              Title = item.MaMon,
              Name = item.TenMon,
            };
            _monThiService.Add(monthi);
            _monThiService.SaveChanges();
            var id = _monThiService.GetByConDition(x => x.Title == item.MaMon).MonThiId;
            if(String.IsNullOrEmpty(item.TenMon) || String.IsNullOrEmpty(item.MaMon))
            {
              //dont do anything
            }
            else if (!_hocPhanService.checkDuplicate(item.MaHp,item.KiThiId))
            {
              var hocphan = new LopHocPhan()
              {
                Title = item.MaHp,
                Name = item.TenMon,
                MonThiId = id,
                KiThiId = item.KiThiId
              };
              _hocPhanService.Add(hocphan);
            
              _hocPhanService.SaveChanges();
              message.successCount++;
            }
            else
            {
              message.notSuccessCount++;
            }
          }
          else
          {
            var id = _monThiService.GetByConDition(x => x.Title == item.MaMon).MonThiId;
            if (!_hocPhanService.checkDuplicate(item.MaHp,item.KiThiId))
            {
              var hocphan = new LopHocPhan()
              {
                Title = item.MaHp,
                Name = item.TenMon,
                MonThiId = id,
                KiThiId = item.KiThiId
              };
              _hocPhanService.Add(hocphan);
              _hocPhanService.SaveChanges();
              message.successCount++;
            }
            else
            {
              message.notSuccessCount++;
            }

          }
        }
      }
      catch (NullReferenceException ex)
      {
        message.message = ex.Message;
        message.notSuccessCount++;
      }
      return request.CreateResponse(HttpStatusCode.OK,message);
    }


    [Route("update")]
    [HttpPut]
    public HttpResponseMessage Update(HttpRequestMessage request, LopHocPhan lopHocPhan)
    {
      HttpResponseMessage response = null;
      if (!ModelState.IsValid)
      {
        response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
      }
      else
      {
        if (!_hocPhanService.checkDuplicate(lopHocPhan.Title))
        {

          _hocPhanService.Update(lopHocPhan);
          _hocPhanService.SaveChanges();
          response = request.CreateResponse(HttpStatusCode.Created, lopHocPhan);

        }
        else
        {
          response = request.CreateResponse(HttpStatusCode.BadRequest, "LOI TRUNG LAP DU LIEU");
        }
      }
      return response;
    }

    [Route("delete")]
    [HttpDelete]
    public HttpResponseMessage Delete(HttpRequestMessage request, int id)
    {
      var model = _hocPhanService.Delete(id);

      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
      return response;
    }
  }
  public class HocPhanVm
  {
    public string MaMon { set; get; }
    public string TenMon { set; get; }
    public string MaHp { set; get; }
    public int KiThiId { set; get; }
  }
}
