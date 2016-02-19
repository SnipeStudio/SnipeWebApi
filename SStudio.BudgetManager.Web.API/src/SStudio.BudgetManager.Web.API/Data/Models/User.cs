using System;

namespace SStudio.BudgetManager.Web.API.Data.Models
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime LastUpdated { get; set; }
    }
}
