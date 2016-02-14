using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SStudio.BudgetManager.Web.API.Data.Mapping
{
    public class ActionMap : ClassMap<Models.Action>
    {
        public ActionMap()
        {
            Id(item => item.Id).GeneratedBy.Increment();
            Map(item => item.UserId);
            Map(item => item.CategoryId);
            Map(item => item.Summary);
            Map(item => item.CreateDate);
            Map(item => item.LastUpdated);
            Table("Actions");
        }
    }
}
