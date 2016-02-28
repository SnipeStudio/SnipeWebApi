using System.ComponentModel.DataAnnotations;

namespace SStudio.BudgetManager.Web.API.Models.Requests
{
    public class CreateUpdatePaymentRequest
    {
        [Required]
        public decimal? Summary { get; set; }
    }
}