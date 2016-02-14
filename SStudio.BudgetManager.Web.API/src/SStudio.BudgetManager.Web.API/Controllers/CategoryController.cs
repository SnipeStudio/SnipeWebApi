using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using SStudio.BudgetManager.Web.API.Business;
using SStudio.BudgetManager.Web.API.Models;

namespace SStudio.BudgetManager.Web.API.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryBusiness _categoryBusiness;

        public CategoryController(ICategoryBusiness categoryBusiness)
        {
            _categoryBusiness = categoryBusiness;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<IEnumerable<Category>> List()
        {
            return _categoryBusiness.List();
        }

        // GET api/Category/5
        [HttpGet("{id}")]
        public Category Get(int id)
        {
            return _categoryBusiness.Get(id);
        }

        // POST api/Category
        [HttpPost]
        public int Create([FromBody]CreateUpdateCategoryRequest request)
        {
            return _categoryBusiness.Create(request);
        }

        // PUT api/Category/5
        [HttpPut("{id}")]
        public bool Update(int id, [FromBody]CreateUpdateCategoryRequest request)
        {
            return _categoryBusiness.Update(id, request);
        }

        // DELETE api/Category/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _categoryBusiness.Delete(id);
        }
    }
}
