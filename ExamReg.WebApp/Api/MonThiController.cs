using ExamReg.Model.Models;
using ExamReg.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExamReg.WebApp.Api
{
  [RoutePrefix("api/monthi")]
  public class MonThiController : ApiController
  {
    private IMonThiService _monThiService;

    public MonThiController(IMonThiService monThiService)
    {
      this._monThiService = monThiService;
    }

    [Route("getall")]
    [HttpGet]
    public HttpResponseMessage GetAll(HttpRequestMessage request)
    {
      var models = _monThiService.GetAll();
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, models);
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
        _monThiService.Add(monThi);
        _monThiService.SaveChanges();
        response = request.CreateResponse(HttpStatusCode.Created, monThi);

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
        _monThiService.Update(monThi);
        _monThiService.SaveChanges();
        response = request.CreateResponse(HttpStatusCode.Created, monThi);

      }

      return response;
    }

    [Route("delete")]
    [HttpDelete]
    public HttpResponseMessage Delete(HttpRequestMessage request, int id)
    {
      var model = _monThiService.Delete(id);
      _monThiService.SaveChanges();
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
      return response;
    }
  }
}
