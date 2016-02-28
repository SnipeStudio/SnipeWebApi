using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SStudio.BudgetManager.Web.API.Models.Requests
{
    public class CreateUpdateCategoryRequest
    {
        [Required, MinLength(1), MaxLength(50)]
        public string Name { get; set; }

        [MinLength(10), MaxLength(1500)]
        public string Description { get; set; }
    }
}
