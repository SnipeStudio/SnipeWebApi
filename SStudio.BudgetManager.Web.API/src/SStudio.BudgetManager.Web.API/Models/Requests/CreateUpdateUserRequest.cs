using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SStudio.BudgetManager.Web.API.Models.Requests
{
    public class CreateUpdateUserRequest
    {
        [Required, MinLength(1), MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MinLength(1), MaxLength(50)]
        public string LastName { get; set; }

        [Required, MinLength(5), MaxLength(255), EmailAddress]
        public string Email { get; set; }
        //public string Phone { get; set; }
    }
}
