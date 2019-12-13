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
  [RoutePrefix("api/kithi")]
  public class KiThiController : ApiController
  {
    private IKiThiService _kiThiService;

    public KiThiController(IKiThiService _kiThiService)
    {
      this._kiThiService = _kiThiService;
    }

    [Route("getall")]
    [HttpGet]
    public HttpResponseMessage GetAll(HttpRequestMessage request)
    {
      var models = _kiThiService.GetAll();
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, models);
      return response;
    }

    [Route("getbyid/{id:int}")]
    [HttpGet]
    public HttpResponseMessage GetById(HttpRequestMessage request, int id)
    {
      var model = _kiThiService.GetById(id);
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
      return response;
    }

    [Route("create")]
    [HttpPost]
    public HttpResponseMessage Create(HttpRequestMessage request, KiThi kiThi)
    {
      HttpResponseMessage response = null;
      if (!ModelState.IsValid)
      {
        response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
      }
      else
      {

        _kiThiService.Add(kiThi);
        _kiThiService.SaveChanges();
        response = request.CreateResponse(HttpStatusCode.Created, kiThi);

      }

      return response;
    }

    [Route("update")]
    [HttpPut]
    public HttpResponseMessage Update(HttpRequestMessage request, KiThi kiThi)
    {
      HttpResponseMessage response = null;
      if (!ModelState.IsValid)
      {
        response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
      }
      else
      {
        _kiThiService.Update(kiThi);
        _kiThiService.SaveChanges();
        response = request.CreateResponse(HttpStatusCode.Created, kiThi);

      }

      return response;
    }

    [Route("delete")]
    [HttpDelete]
    public HttpResponseMessage Delete(HttpRequestMessage request, int id)
    {
      var model = _kiThiService.Delete(id);
      _kiThiService.SaveChanges();
      HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
      return response;
    }
  }
}
