using Microsoft.AspNet.Mvc;
using SStudio.BudgetManager.Web.API.Business;
using SStudio.BudgetManager.Web.API.Models.Requests;
using SStudio.BudgetManager.Web.API.Extensions;

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
        public IActionResult List()
        {
            var result = _paymentBusiness.List();
            return Ok(result);
        }
        
        [HttpGet("Payment/{id:int}")]
        public IActionResult Get(int id)
        {
            var result = _paymentBusiness.Get(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("User/{userId:int}/Category/{categoryId:int}/Payment")]
        public IActionResult Post(int userId, int categoryId, [FromBody]CreateUpdatePaymentRequest request)
        {
            if (request == null || this.HasRequestErrors())
                return HttpBadRequest(ModelState);

            var result = _paymentBusiness.Create(userId, categoryId, request);
            return Ok(result);
        }

        [HttpPut("Payment/{id:int}")]
        public IActionResult Put(int id, [FromBody]CreateUpdatePaymentRequest request)
        {
            if (request == null || this.HasRequestErrors())
                return HttpBadRequest(ModelState);

            var result = _paymentBusiness.Update(id, request);
            return Ok(result);
        }

        [HttpDelete("Payment/{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _paymentBusiness.Delete(id);
            return Ok(result);
        }
    }
}
