using Billing.Api.Responses;
using Billing.Core;
using Billing.Core.DTO;
using Billing.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Billing.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ApiControllerBase
    {
        private readonly InvoiceRepository _repo = null;

        public InvoiceController(
            ILogger<InvoiceController> logger,
            ILoggerFactory loggerF,
            BillingContext db,
            IHttpContextAccessor accessor) : base(db, logger, loggerF, accessor)
        {
            BaseRepository._loggerF = loggerF;
            _repo = new InvoiceRepository(logger, loggerF, db);
        }

        [HttpGet]
        [Route("ping")]
        public ObjectResult Ping() => new("Pong!");

        [HttpGet]        
        public IEnumerable<InvoiceDTO> Get() {
            var resp = new List<InvoiceDTO>();
            foreach (var item in _repo.GetAll())
            {
                var tuple = InvoiceRepository.ToDTO<InvoiceDTO>(item);
                resp.Add(tuple);
            }
            return resp;
        }

        [HttpGet]
        [Route("{id}")]
        public ObjectResult GetById(long id)
        {
            //var item = _repo.GetById(CustomConverter.DecodeB64FromHex(id));
            var item = _repo.GetById(id);
            if (item != null)
            {
                var tuple = InvoiceRepository.ToDTO<InvoiceDTO>(item);
                return new ObjectResult(tuple);
            }
            return null;
        }

        [HttpPost]
        [Route("create")]
        public ResponseBase NewProduct(InvoiceDTO p)
        {
            var response = new ResponseBase();
            try
            {
                var resp = _repo.New(p);
                response.Status = true;
                response.Data = resp;
            }
            catch (Exception ex)
            {
                response = ResponseBase.ResponseError(ex.Message);
            }
            return response;
        }

        [HttpPut]
        [Route("update")]
        public ResponseBase UpdateProduct(InvoiceDTO p)
        {
            var response = new ResponseBase();
            try
            {
                var resp = _repo.Update(p);
                response.Status = true;
                response.Data = resp;
            }
            catch (Exception ex)
            {
                response = ResponseBase.ResponseError(ex.Message);
            }
            return response;
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public ActionResult<bool> DeleteProduct(long id)
        {
            var resp = _repo.Delete(id);
            return new ObjectResult(resp);
        }
    }
}
