using Microsoft.AspNet.Mvc;

using SStudio.BudgetManager.Web.API.Business;
using SStudio.BudgetManager.Web.API.Models.Requests;
using SStudio.BudgetManager.Web.API.Extensions;

namespace SStudio.BudgetManager.Web.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserBusiness _userBusiness;
        private readonly IPaymentBusiness _paymentBusiness;

        public UserController(IUserBusiness userBusiness, IPaymentBusiness paymentBusiness)
        {
            _userBusiness = userBusiness;
            _paymentBusiness = paymentBusiness;
        }

        [HttpGet]
        public IActionResult List()
        {
            var result = _userBusiness.List();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var result = _userBusiness.Get(id);
            return Ok(result);
        }

        [HttpGet("{id:int}/Payment")]
        public IActionResult ListUserPayments(int id)
        {
            var result = _paymentBusiness.ListByUserId(id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateUpdateUserRequest request)
        {
            if (request == null || this.HasRequestErrors())
                return HttpBadRequest(ModelState);

            var result = _userBusiness.Create(request);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody]CreateUpdateUserRequest request)
        {
            if (request == null || this.HasRequestErrors())
                return HttpBadRequest(ModelState);

            var result = _userBusiness.Update(id, request);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _userBusiness.Delete(id);
            return Ok(result);
        }
    }
}
