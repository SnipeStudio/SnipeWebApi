using System;
using System.Collections.Generic;
using System.Linq;

using SStudio.BudgetManager.Web.API.Data;
using SStudio.BudgetManager.Web.API.Data.Mapping;
using SStudio.BudgetManager.Web.API.Models;
using DataCategory = SStudio.BudgetManager.Web.API.Data.Models.Category;

namespace SStudio.BudgetManager.Web.API.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ISessionProvider _sessionProvider;

        public CategoryRepository(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        public Category Get(int id)
        {
            using (var session = _sessionProvider.GetSession<CategoryMap>().OpenSession())
            {
                using (session.BeginTransaction())
                {
                    var dataCategory = session.Get<DataCategory>(id);

                    return DataCategoryToCategory(dataCategory);
                }
            }
        }

        public IEnumerable<Category> List()
        {
            using (var session = _sessionProvider.GetSession<CategoryMap>().OpenSession())
            {
                using (session.BeginTransaction())
                {
                    var items = session.QueryOver<DataCategory>()
                                       .List()
                                       .Select(DataCategoryToCategory);
                    return items;
                }
            }
        }

        public int Create(Category category)
        {
            using (var session = _sessionProvider.GetSession<CategoryMap>().OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    var dataItem = new DataCategory
                    {
                        Name = category.Name,
                        Description = category.Description,
                        LastUpdated = DateTime.UtcNow
                    };

                    session.Save(dataItem);
                    trans.Commit();

                    return dataItem.Id;
                }
            }
        }

        public bool Update(Category category)
        {
            var updated = true;
            using (var session = _sessionProvider.GetSession<CategoryMap>().OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    var dataItem = session.Get<DataCategory>(category.Id);
                    dataItem.Name = string.IsNullOrWhiteSpace(category.Name) ? dataItem.Name : category.Name;
                    dataItem.Description = string.IsNullOrWhiteSpace(category.Description) ? dataItem.Description : category.Description;
                    dataItem.LastUpdated = DateTime.UtcNow;

                    try
                    {
                        session.Update(dataItem);
                        trans.Commit();
                    }
                    catch(Exception ex)
                    {
                        updated = false;
                    }
                }
            }

            return updated;
        }

        public bool Delete(int id)
        {
            var deleted = true;
            using (var session = _sessionProvider.GetSession<CategoryMap>().OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    try
                    {
                        var item = session.Get<DataCategory>(id);
                        session.Delete(item);
                        trans.Commit();
                    }
                    catch(Exception ex)
                    {
                        deleted = false;
                    }
                }
            }

            return deleted;
        }

        private Category DataCategoryToCategory(DataCategory category)
        {
            return new Category
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
    }

    public interface ICategoryRepository
    {
        IEnumerable<Category> List();
        Category Get(int id);
        int Create(Category category);
        bool Update(Category category);
        bool Delete(int id);
    }
}
