using System;
using System.Collections.Generic;
using System.Linq;

using SStudio.BudgetManager.Web.API.Data;
using SStudio.BudgetManager.Web.API.Data.Mapping;
using SStudio.BudgetManager.Web.API.Models;
using DataUser = SStudio.BudgetManager.Web.API.Data.Models.User;

namespace SStudio.BudgetManager.Web.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ISessionProvider _sessionProvider;

        public UserRepository(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        public User Get(int id)
        {
            using (var session = _sessionProvider.GetSession<UserMap>().OpenSession())
            {
                using (session.BeginTransaction())
                {
                    var dataUser = session.Get<DataUser>(id);

                    return DataUserToUser(dataUser);
                }
            }
        }

        public IEnumerable<User> List()
        {
            using (var session = _sessionProvider.GetSession<UserMap>().OpenSession())
            {
                using (session.BeginTransaction())
                {
                    var items = session.QueryOver<DataUser>()
                                       .List()
                                       .Select(DataUserToUser);
                    return items;
                }
            }
        }

        public int Create(User user)
        {
            using (var session = _sessionProvider.GetSession<UserMap>().OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    var dataItem = new DataUser
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Phone = user.Phone,
                        LastUpdated = DateTime.UtcNow
                    };

                    session.Save(dataItem);
                    trans.Commit();

                    return dataItem.Id;
                }
            }
        }

        public bool Update(User user)
        {
            var updated = true;
            using (var session = _sessionProvider.GetSession<UserMap>().OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    var dataItem = session.Get<DataUser>(user.Id);
                    dataItem.FirstName = string.IsNullOrWhiteSpace(user.FirstName) ? dataItem.FirstName : user.FirstName;
                    dataItem.LastName = string.IsNullOrWhiteSpace(user.LastName) ? dataItem.LastName : user.LastName;
                    dataItem.Email = string.IsNullOrWhiteSpace(user.Email) ? dataItem.Email : user.Email;
                    dataItem.Phone = string.IsNullOrWhiteSpace(user.Phone) ? dataItem.Phone : user.Phone;
                    dataItem.LastUpdated = DateTime.UtcNow;

                    try
                    {
                        session.Update(dataItem);
                        trans.Commit();
                    }
                    catch (Exception ex)
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
            using (var session = _sessionProvider.GetSession<UserMap>().OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    try
                    {
                        var item = session.Get<DataUser>(id);
                        session.Delete(item);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        deleted = false;
                    }
                }
            }

            return deleted;
        }

        private User DataUserToUser(DataUser user)
        {
            return new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone
            };
        }
    }

    public interface IUserRepository
    {
        IEnumerable<User> List();
        User Get(int id);
        int Create(User user);
        bool Update(User user);
        bool Delete(int id);
    }
}
