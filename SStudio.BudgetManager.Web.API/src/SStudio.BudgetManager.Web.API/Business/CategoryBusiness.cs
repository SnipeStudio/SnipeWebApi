using SStudio.BudgetManager.Web.API.Models;
using SStudio.BudgetManager.Web.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SStudio.BudgetManager.Web.API.Business
{
    public class CategoryBusiness : ICategoryBusiness
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryBusiness(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> List()
        {
            return _categoryRepository.List();
        }

        public Category Get(int id)
        {
            return _categoryRepository.Get(id);
        }

        public int Create(CreateUpdateCategoryRequest request)
        {
            return _categoryRepository.Create(new Category { Name = request.Name, Description = request.Description });
        }

        public bool Delete(int id)
        {
            return _categoryRepository.Delete(id);
        }

        public bool Update(int id, CreateUpdateCategoryRequest request)
        {
            return _categoryRepository.Update(new Category { Id = id, Name = request.Name, Description = request.Description });
        }
    }

    public interface ICategoryBusiness
    {
        IEnumerable<Category> List();
        Category Get(int id);
        int Create(CreateUpdateCategoryRequest category);
        bool Update(int id, CreateUpdateCategoryRequest category);
        bool Delete(int id);
    }
}
