using ExamReg.Data.Infrastructure;
using ExamReg.Model.Models;
using ExamReg.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExamReg.WebApp.Areas.Admin
{   
    [RoutePrefix("admin/api/sinhvien")]
    public class SinhViennnController : ApiController
    {
        ISinhVienService _sinhVienService;

        public SinhViennnController(ISinhVienService sinhVienService)
        {
            this._sinhVienService = sinhVienService;
        }

        
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            var results = _sinhVienService.GetAll();
            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, results);
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

            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
            return response;
        }
    }
}
