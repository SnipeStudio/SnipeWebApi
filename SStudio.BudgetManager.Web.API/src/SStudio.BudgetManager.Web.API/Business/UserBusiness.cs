using SStudio.BudgetManager.Web.API.Models;
using SStudio.BudgetManager.Web.API.Models.Requests;
using SStudio.BudgetManager.Web.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SStudio.BudgetManager.Web.API.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _UserRepository;

        public UserBusiness(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }

        public IEnumerable<User> List()
        {
            return _UserRepository.List();
        }

        public User Get(int id)
        {
            return _UserRepository.Get(id);
        }

        public int Create(CreateUpdateUserRequest request)
        {
            return _UserRepository.Create(new User { FirstName = request.FirstName, LastName = request.LastName, Email = request.Email, Phone = request.Phone });
        }

        public bool Delete(int id)
        {
            return _UserRepository.Delete(id);
        }

        public bool Update(int id, CreateUpdateUserRequest request)
        {
            return _UserRepository.Update(new User { Id = id, FirstName = request.FirstName, LastName = request.LastName });
        }
    }

    public interface IUserBusiness
    {
        IEnumerable<User> List();
        User Get(int id);
        int Create(CreateUpdateUserRequest User);
        bool Update(int id, CreateUpdateUserRequest User);
        bool Delete(int id);
    }
}
