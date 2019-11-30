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
    [RoutePrefix("admin/api/sinhvienlophp")]
    public class SinhVienLophpController : ApiController
    {
        ISinhVienLophpService _sinhVienLophpService;

        public SinhVienLophpController(ISinhVienLophpService sinhVienService)
        {
            this._sinhVienLophpService = sinhVienService;
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

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, SinhVienLophp sinhVienLophp)
        {
            HttpResponseMessage response = null;
            if (!ModelState.IsValid)
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                _sinhVienLophpService.Add(sinhVienLophp);
                _sinhVienLophpService.SaveChanges();
                response = request.CreateResponse(HttpStatusCode.Created, sinhVienLophp);

            }

            return response;
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
}