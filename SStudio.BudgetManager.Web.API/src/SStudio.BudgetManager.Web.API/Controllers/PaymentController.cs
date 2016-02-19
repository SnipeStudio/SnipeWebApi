using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using SStudio.BudgetManager.Web.API.Business;
using SStudio.BudgetManager.Web.API.Models;
using SStudio.BudgetManager.Web.API.Models.Requests;

namespace SStudio.BudgetManager.Web.API.Controllers
{
    [Route("api")]
    public class PaymentController : Controller
    {
        private readonly IPaymentBusiness _paymentBusiness;

        public PaymentController(IPaymentBusiness paymentBusiness)
        {
            _paymentBusiness = paymentBusiness;
        }

        [HttpGet]
        [Route("Payment")]
        public IEnumerable<Payment> List()
        {
            return _paymentBusiness.List();
        }
        
        [HttpGet("Payment/{id}")]
        public Payment Get(int id)
        {
            return _paymentBusiness.Get(id);
        }

        [HttpPost]
        [Route("User/{userId}/Category/{categoryId}/Payment")]
        public int Post(int userId, int categoryId, [FromBody]CreateUpdatePaymentRequest request)
        {
            return _paymentBusiness.Create(userId, categoryId, request);
        }

        [HttpPut("Payment/{id}")]
        public bool Put(int id, [FromBody]CreateUpdatePaymentRequest request)
        {
            return _paymentBusiness.Update(id, request);
        }

        [HttpDelete("Payment/{id}")]
        public bool Delete(int id)
        {
            return _paymentBusiness.Delete(id);
        }
    }
}
