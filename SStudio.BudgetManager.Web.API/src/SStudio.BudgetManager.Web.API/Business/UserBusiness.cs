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
        private readonly IUserRepository _userRepository;

        public UserBusiness(IUserRepository UserRepository)
        {
            _userRepository = UserRepository;
        }

        public IEnumerable<User> List()
        {
            return _userRepository.List();
        }

        public User Get(int id)
        {
            return _userRepository.Get(id);
        }

        public int Create(CreateUpdateUserRequest request)
        {
            return _userRepository.Create(new User { FirstName = request.FirstName, LastName = request.LastName, Email = request.Email });
        }

        public bool Delete(int id)
        {
            return _userRepository.Delete(id);
        }

        public bool Update(int id, CreateUpdateUserRequest request)
        {
            return _userRepository.Update(new User { Id = id, FirstName = request.FirstName, LastName = request.LastName });
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
