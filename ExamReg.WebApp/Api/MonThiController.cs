using ExamReg.Model.Models;
using ExamReg.Service;
using ExamReg.WebApp.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExamReg.WebApp.Api
{
  [RoutePrefix("api/monthi")]
  [Authorize(Roles = "Admin")]
  public class MonThiController : ApiController
  {
    private IMonThiService _monThiService;
    private IHocPhanService _hocPhanService;

    public MonThiController(IMonThiService monThiService, IHocPhanService _hocPhanService)
    {
      this._monThiService = monThiService;
      this._hocPhanService = _hocPhanService;
    }

    [Route("getall")]
    [HttpGet]
    public HttpResponseMessage GetAll(HttpRequestMessage request)
    {
      var models = _monThiService.GetAll();
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, models);
      return response;
    }

    [Route("getmultipaging")]
    [HttpGet]
    public HttpResponseMessage GetMultiPaging(HttpRequestMessage request, int currentPage, int pageSize, string keyword)
    {
      HttpResponseMessage response = null;
      Message message = new Message();

      int totalRow = 0;
      try
      {
        IEnumerable<MonThi> model = _monThiService.GetMultiPaging(currentPage, pageSize,  keyword, out totalRow);
        ListResult<MonThi> listResult = new ListResult<MonThi>()
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
      var model = _monThiService.GetById(id);
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
      return response;
    }

    [Route("create")]
    [HttpPost]
    public HttpResponseMessage Create(HttpRequestMessage request, MonThi monThi)
    {
      HttpResponseMessage response = null;
      if (!ModelState.IsValid)
      {
        response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
      }
      else
      {
        if (!_monThiService.checkDuplicate(monThi.Title))
        {
          _monThiService.Add(monThi);
          _monThiService.SaveChanges();
          response = request.CreateResponse(HttpStatusCode.Created, monThi);
        }
        else
        {
          response = request.CreateResponse(HttpStatusCode.BadRequest, "Trùng mã môn học");
        }

      }

      return response;
    }

    [Route("update")]
    [HttpPut]
    public HttpResponseMessage Update(HttpRequestMessage request, MonThi monThi)
    {
      HttpResponseMessage response = null;
      if (!ModelState.IsValid)
      {
        response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
      }
      else
      {
        if (_monThiService.checkDuplicateUpdate(monThi))
        {

          _monThiService.Update(monThi);
          try
          {
            _monThiService.SaveChanges();
            response = request.CreateResponse(HttpStatusCode.Created, monThi);

          }
          catch (DbEntityValidationException ex)
          {


            return request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
          }



        }
        else
        {
          if (!_monThiService.checkDuplicate(monThi.Title))
          {
       
            _monThiService.Update(monThi);
            try
            {
              _monThiService.SaveChanges();
              response = request.CreateResponse(HttpStatusCode.Created, monThi);

            }
            catch (DbEntityValidationException ex)
            {


              return request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
          }
          else
          {
            response = request.CreateResponse(HttpStatusCode.BadRequest, "Trùng mã môn học");
          }

        }


      }

      return response;
    }

    [Route("delete")]
    [HttpDelete]
    public HttpResponseMessage Delete(HttpRequestMessage request, int id)
    {
      var list = _hocPhanService.GetMulti(x => x.MonThiId == id);
      foreach( var item in list)
      {
        _hocPhanService.Delete(item.LophpId);
      }

      var model = _monThiService.Delete(id);
      _monThiService.SaveChanges();
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
      return response;
    }
  }

}
