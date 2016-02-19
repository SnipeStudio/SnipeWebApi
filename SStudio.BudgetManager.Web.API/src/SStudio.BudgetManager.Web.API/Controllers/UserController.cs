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
        public IEnumerable<User> List()
        {
            return _userBusiness.List();
        }

        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _userBusiness.Get(id);
        }

        [HttpGet("{id}/Payment")]
        public IEnumerable<Payment> ListUserPayments(int id)
        {
            return _paymentBusiness.ListByUserId(id);
        }

        [HttpPost]
        public int Create([FromBody]CreateUpdateUserRequest request)
        {
            return _userBusiness.Create(request);
        }

        [HttpPut("{id}")]
        public bool Update(int id, [FromBody]CreateUpdateUserRequest request)
        {
            return _userBusiness.Update(id, request);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _userBusiness.Delete(id);
        }
    }
}
