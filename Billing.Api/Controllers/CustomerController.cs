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
    public class CustomerController : ApiControllerBase
    {
        private readonly CustomerRepository _repo = null;

        public CustomerController(
            ILogger<CustomerController> logger,
            ILoggerFactory loggerF,
            BillingContext db,
            IHttpContextAccessor accessor) : base(db, logger, loggerF, accessor)
        {
            BaseRepository._loggerF = loggerF;
            _repo = new CustomerRepository(logger, loggerF, db);
        }

        [HttpGet]
        [Route("ping")]
        public ObjectResult Ping() => new("Pong!");

        [HttpGet]        
        public IEnumerable<CustomerDTO> Get() {
            var resp = new List<CustomerDTO>();
            foreach (var item in _repo.GetAll())
            {
                var customer = new CustomerDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    Address = item.Address,
                    Active = item.Active
                };
                resp.Add(customer);
            }
            return resp;
        }

        [HttpGet("{id}")]
        public ObjectResult GetById(string id)
        {
            var item = _repo.GetById(id);
            if (item != null)
            {
                var customer = new CustomerDTO()
                {
                    Id = id,
                    Name = item.Name,
                    Email = item.Email,
                    Address = item.Address,
                    Active = item.Active
                };
                return new ObjectResult(customer);
            }
            return null;
        }

        [HttpPost]
        [Route("create")]
        public ResponseBase NewCustomer(CustomerDTO p)
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
        public ResponseBase UpdateCustomer(CustomerDTO p)
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
        public ActionResult<bool> DeleteCustomer(string id)
        {
            var resp = _repo.Delete(id);
            return new ObjectResult(resp);
        }
    }
}
