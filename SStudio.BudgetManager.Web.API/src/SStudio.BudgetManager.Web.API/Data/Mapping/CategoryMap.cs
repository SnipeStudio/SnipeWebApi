using FluentNHibernate.Mapping;
using SStudio.BudgetManager.Web.API.Data.Models;
using System;

namespace SStudio.BudgetManager.Web.API.Data.Mapping
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Id(item => item.Id).GeneratedBy.Increment();
            Map(item => item.Name);
            Map(item => item.Description);
            Map(item => item.LastUpdated);
            Table("Categories");
        }
    }
}
