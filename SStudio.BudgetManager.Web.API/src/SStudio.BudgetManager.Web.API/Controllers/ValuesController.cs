using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;

using SStudio.BudgetManager.Web.API.Business;
using SStudio.BudgetManager.Web.API.Models;

namespace SStudio.BudgetManager.Web.API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IItemBusiness _itemBusiness;

        public ValuesController(IItemBusiness itemBusiness)
        {
            _itemBusiness = itemBusiness;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return _itemBusiness.List();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody]string value)
        {
            return _itemBusiness.Create(new Item { Name = value });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _itemBusiness.Delete(id);
        }
    }
}
