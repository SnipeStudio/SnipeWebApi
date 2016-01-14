using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace SStudio.BudgetManager.Web.API.Data
{
    public class ItemMap : ClassMap<Item>
    {
        public ItemMap()
        {
            Id(item => item.Id).GeneratedBy.Increment();
            Map(item => item.Name);
            Table("Items");
        }
    }
}
