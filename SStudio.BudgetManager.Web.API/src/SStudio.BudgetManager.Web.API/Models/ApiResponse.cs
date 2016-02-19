using System.Collections.Generic;

namespace SStudio.BudgetManager.Web.API.Models
{
    public class ApiResponse<TStatus, TBody>
    {
        public TStatus Status { get; set; }
        public TBody Body { get; set; }
        public List<string> Messages { get; set; }
    }
}
