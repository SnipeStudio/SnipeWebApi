using SStudio.BudgetManager.Web.API.Models;
using SStudio.BudgetManager.Web.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SStudio.BudgetManager.Web.API.Business
{
    public interface IItemBusiness
    {
        IEnumerable<Item> List();
        int Create(Item item);
        bool Delete(int id);
    }

    public class ItemBusiness : IItemBusiness
    {
        private readonly IItemContext _itemRepository;

        public ItemBusiness(IItemContext itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public int Create(Item item)
        {
            return _itemRepository.Create(item);
        }

        public bool Delete(int id)
        {
            return _itemRepository.Delete(id);
        }

        IEnumerable<Item> IItemBusiness.List()
        {
            return _itemRepository.GetAll();
        }
    }
}
