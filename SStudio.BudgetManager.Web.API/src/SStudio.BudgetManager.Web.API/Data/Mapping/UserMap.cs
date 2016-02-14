using FluentNHibernate.Mapping;
using SStudio.BudgetManager.Web.API.Data.Models;
using System;

namespace SStudio.BudgetManager.Web.API.Data.Mapping
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(item => item.Id).GeneratedBy.Increment();
            Map(item => item.FirstName);
            Map(item => item.LastName);
            Map(item => item.Email);
            Map(item => item.Phone);
            Map(item => item.CreateDate);
            Map(item => item.LastUpdated);
            Table("Users");
        }
    }
}
