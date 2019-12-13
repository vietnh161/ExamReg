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
  [RoutePrefix("api/lichthi")]
  public class LichThiController : ApiController
  {
    private ILichThiService _lichThiService;
    private IHocPhanService _hocPhanService;

    public LichThiController(ILichThiService lichThiService, IHocPhanService _hocPhanService)
    {
      this._lichThiService = lichThiService;
      this._hocPhanService = _hocPhanService;
    }

    [Route("getall")]
    [HttpGet]
    public HttpResponseMessage GetAll(HttpRequestMessage request)
    {
      var models = _lichThiService.GetAll();
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, models);
      return response;
    }

    [Route("getallprop")]
    [HttpGet]
    public HttpResponseMessage GetAllProp(HttpRequestMessage request, int kiThiId)
    {
      var models = _lichThiService.GetAllProp(kiThiId);
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, models);
      return response;
    }

    [Route("getmultipaging")]
    [HttpGet]
    public HttpResponseMessage GetMultiPaging(HttpRequestMessage request, int currentPage, int pageSize, string sort, string sortBy, string keyword,int kiThiId)
    {
      HttpResponseMessage response;
      //Message message = new Message();

      //var a = request.Content.ReadAsStringAsync();
      //Paging paging = JsonConvert.DeserializeObject<Paging>(a.Result);
      int totalRow = 0;
      try
      {
        IEnumerable<LichThi> model = _lichThiService.GetMultiPaging(currentPage, pageSize, sort, sortBy, keyword,kiThiId, out totalRow);

        ListResult<LichThi> listResult = new ListResult<LichThi>()
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

    [Route("getbyid/{id:int}")]
    [HttpGet]
    public HttpResponseMessage GetById(HttpRequestMessage request, int id)
    {
      var model = _lichThiService.GetById(id);
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
      return response;
    }

    [Route("create")]
    [HttpPost]
    public HttpResponseMessage Create(HttpRequestMessage request, LichThi lichThi)
    {
      HttpResponseMessage response = null;
      //var a = request.Content.ReadAsStringAsync();
      //LichThiPostBody lt = JsonConvert.DeserializeObject<LichThiPostBody>(a.Result);
      //LichThi lichThi = JsonConvert.DeserializeObject<LichThi>(a.Result);

      if (!ModelState.IsValid)
      {
        response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
      }
      else
      {
        LopHocPhan lophp = _hocPhanService.GetByConDition(x => x.Title == lichThi.LopHocPhan.Title && x.KiThiId == lichThi.KiThiId);
        if (lophp == null)
        {
          return request.CreateResponse(HttpStatusCode.BadRequest, "Học phần không tồn tại,kiểm tra lại mã hoặc kì thi");
        }
        else
        {
          lichThi.LichThiId = -1;
          if (!_lichThiService.checkDuplicate(lichThi))
          {
            lichThi.LophpId = lophp.LophpId;
            _lichThiService.Add(lichThi);
            //     _lichThiService.SaveChanges();
            response = request.CreateResponse(HttpStatusCode.Created, lichThi);
          }
          else
          {
            response = request.CreateResponse(HttpStatusCode.BadRequest, "Lỗi trùng lặp dữ liệu");
          }
        }
      }

      return response;
    }

    [Route("update")]
    [HttpPut]
    public HttpResponseMessage Update(HttpRequestMessage request, LichThi lichThi)
    {
      HttpResponseMessage response = null;
      //var a = request.Content.ReadAsStringAsync();
      // lichThi = JsonConvert.DeserializeObject<LichThi>(a.Result);

      if (!ModelState.IsValid)
      {
        response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
      }
      else
      {
        LopHocPhan lophp = _hocPhanService.GetByConDition(x => x.Title == lichThi.LopHocPhan.Title && x.KiThiId == lichThi.KiThiId);
        if (lophp == null)
        {
          return request.CreateResponse(HttpStatusCode.BadRequest, "Học phần không tồn tại,kiểm tra lại mã hoặc kì thi");
        }
        else
        {

          if (!_lichThiService.checkDuplicate(lichThi))
          {
            lichThi.LophpId = lophp.LophpId;
            lichThi.LopHocPhan = null;
            lichThi.CaThi = null;
            lichThi.PhongThi = null;
            _lichThiService.Update(lichThi);
            //     _lichThiService.SaveChanges();
            response = request.CreateResponse(HttpStatusCode.Created, lichThi);
          }
          else
          {
            response = request.CreateResponse(HttpStatusCode.BadRequest, "Lỗi trùng lặp dữ liệu");
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
      foreach(var id in ids)
      {
        _lichThiService.Delete(id);
        message.successCount++;
      }

    // _lichThiService.SaveChanges();
      message.message = "Xoa thanh cong";
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, message);
      return response;
    }
  }
}
