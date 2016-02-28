using Microsoft.AspNet.Mvc;
using System.Linq;

namespace SStudio.BudgetManager.Web.API.Extensions
{
    public static class ModelStateChecker
    {
        public static bool HasRequestErrors(this Controller controller)
        {
            return controller.ModelState.Any(e => e.Value.Errors.Any());
        }
    }
}
