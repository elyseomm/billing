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
    public class ProductController : ApiControllerBase
    {
        private readonly ProductRepository _repo = null;

        public ProductController(
            ILogger<ProductController> logger,
            ILoggerFactory loggerF,
            BillingContext db,
            IHttpContextAccessor accessor) : base(db, logger, loggerF, accessor)
        {
            BaseRepository._loggerF = loggerF;
            _repo = new ProductRepository(logger, loggerF, db);
        }

        [HttpGet]        
        public IEnumerable<ProductDTO> Get() {
            var resp = new List<ProductDTO>();
            foreach (var item in _repo.GetAll())
            {
                var product = new ProductDTO()
                {
                    Id = item.Id,
                    ProductName = item.ProductName,
                    Active = item.Active
                };
                resp.Add(product);
            }
            return resp;
        }

        [HttpGet]
        [Route("{id}")]
        public ObjectResult GetById(string id)
        {
            //var item = _repo.GetById(CustomConverter.DecodeB64FromHex(id));
            var item = _repo.GetById(id);
            if (item != null)
            {
                var product = new ProductDTO()
                {
                    Id = id,
                    ProductName = item.ProductName,
                    Active = item.Active
                };
                return new ObjectResult(product);
            }
            return null;
        }

        [HttpPost]
        [Route("create")]
        public ResponseBase NewProduct(ProductDTO p)
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
        public ResponseBase UpdateProduct(ProductDTO p)
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
        public ActionResult<bool> DeleteProduct(string id)
        {
            var resp = _repo.Delete(id);
            return new ObjectResult(resp);
        }
    }
}
