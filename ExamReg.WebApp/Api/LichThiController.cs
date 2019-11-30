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
    [RoutePrefix("api/lichthi")]
    public class LichThiController : ApiController
    {
        private ILichThiService _lichThiService;

        public LichThiController(ILichThiService lichThiService)
        {
            this._lichThiService = lichThiService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            var models = _lichThiService.GetAll();
            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, models);
            return response;
        }
        
        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request ,int id)
        {
            var model = _lichThiService.GetById(id);
            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK,model);
            return response;
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, LichThi lichThi)
        {
            HttpResponseMessage response = null;
        if (!ModelState.IsValid)
        {
            response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        }
        else
        {
            if (!_lichThiService.checkDuplicate(lichThi))
            {

                _lichThiService.Add(lichThi);
           //     _lichThiService.SaveChanges();
                response = request.CreateResponse(HttpStatusCode.Created, lichThi);
            }
            else
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest, "LOI TRUNG LAP DU LIEU");
            }
        }

            return response;
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, LichThi lichThi)
        {
            HttpResponseMessage response = null;
            if (!ModelState.IsValid)
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                _lichThiService.Update(lichThi);
                _lichThiService.SaveChanges();
                response = request.CreateResponse(HttpStatusCode.Created, lichThi);

            }

            return response;
        }
        
        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            var model = _lichThiService.Delete(id);
        
            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
            return response;
        }
    }
}
