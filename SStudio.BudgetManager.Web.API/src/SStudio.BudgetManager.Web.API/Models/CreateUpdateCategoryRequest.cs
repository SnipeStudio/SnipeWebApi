using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SStudio.BudgetManager.Web.API.Models
{
    public class CreateUpdateCategoryRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
