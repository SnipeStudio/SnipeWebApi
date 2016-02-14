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

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IEnumerable<User>> List()
        {
            return _userBusiness.List();
        }

        // GET api/User/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _userBusiness.Get(id);
        }

        // POST api/User
        [HttpPost]
        public int Create([FromBody]CreateUpdateUserRequest request)
        {
            return _userBusiness.Create(request);
        }

        // PUT api/User/5
        [HttpPut("{id}")]
        public bool Update(int id, [FromBody]CreateUpdateUserRequest request)
        {
            return _userBusiness.Update(id, request);
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _userBusiness.Delete(id);
        }
    }
}
