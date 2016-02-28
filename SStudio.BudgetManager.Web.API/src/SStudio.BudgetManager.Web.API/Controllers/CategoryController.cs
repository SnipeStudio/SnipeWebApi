using Microsoft.AspNet.Mvc;
using SStudio.BudgetManager.Web.API.Business;
using SStudio.BudgetManager.Web.API.Models.Requests;
using SStudio.BudgetManager.Web.API.Extensions;

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
        public IActionResult List()
        {
            var result = _categoryBusiness.List();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var result = _categoryBusiness.Get(id);
            return Ok(result);
        }

        [HttpGet("{id:int}/Payment")]
        public IActionResult ListCategoryPayments(int id)
        {
            var result = _paymentBusiness.ListByCategoryId(id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateUpdateCategoryRequest request)
        {
            if (request == null || this.HasRequestErrors())
                return HttpBadRequest(ModelState);

            var result = _categoryBusiness.Create(request);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody]CreateUpdateCategoryRequest request)
        {
            if (request == null || this.HasRequestErrors())
                return HttpBadRequest(ModelState);

            var result = _categoryBusiness.Update(id, request);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _categoryBusiness.Delete(id);
            return Ok(result);
        }
    }
}
