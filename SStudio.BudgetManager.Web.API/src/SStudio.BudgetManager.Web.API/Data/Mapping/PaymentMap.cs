using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SStudio.BudgetManager.Web.API.Data.Mapping
{
    public class PaymentMap : ClassMap<Models.Payment>
    {
        public PaymentMap()
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
