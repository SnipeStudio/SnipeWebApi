using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SStudio.BudgetManager.Web.API.Data.Models
{
    public class Payment
    {
        public virtual int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual decimal Summary { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime LastUpdated { get; set; }
    }
}
