using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using SStudio.BudgetManager.Web.API.Business;
using SStudio.BudgetManager.Web.API.Models;
using SStudio.BudgetManager.Web.API.Models.Requests;

namespace SStudio.BudgetManager.Web.API.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryBusiness _categoryBusiness;
        private readonly IPaymentBusiness _paymentBusiness;

        public CategoryController(ICategoryBusiness categoryBusiness, IPaymentBusiness paymentBusiness)
        {
            _categoryBusiness = categoryBusiness;
            _paymentBusiness = paymentBusiness;
        }

        [HttpGet]
        public IEnumerable<Category> List()
        {
            return _categoryBusiness.List();
        }

        [HttpGet("{id}")]
        public Category Get(int id)
        {
            return _categoryBusiness.Get(id);
        }

        [HttpGet("{id}/Payment")]
        public IEnumerable<Payment> ListCategoryPayments(int id)
        {
            return _paymentBusiness.ListByCategoryId(id);
        }

        [HttpPost]
        public int Create([FromBody]CreateUpdateCategoryRequest request)
        {
            return _categoryBusiness.Create(request);
        }

        [HttpPut("{id}")]
        public bool Update(int id, [FromBody]CreateUpdateCategoryRequest request)
        {
            return _categoryBusiness.Update(id, request);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _categoryBusiness.Delete(id);
        }
    }
}
