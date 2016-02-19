using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SStudio.BudgetManager.Web.API.Data;
using SStudio.BudgetManager.Web.API.Data.Mapping;
using SStudio.BudgetManager.Web.API.Models;
using DataPayment = SStudio.BudgetManager.Web.API.Data.Models.Payment;

namespace SStudio.BudgetManager.Web.API.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ISessionProvider _sessionProvider;

        public PaymentRepository(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        public IEnumerable<Payment> List()
        {
            using (var session = _sessionProvider.GetSession<PaymentMap>().OpenSession())
            {
                using (session.BeginTransaction())
                {
                    var dataPayment = session.QueryOver<DataPayment>().List().Select(DataPaymentToPayment);

                    return dataPayment;
                }
            }
        }

        public IEnumerable<Payment> ListByUserId(int userId)
        {
            using (var session = _sessionProvider.GetSession<PaymentMap>().OpenSession())
            {
                using (session.BeginTransaction())
                {
                    var list = session.QueryOver<DataPayment>().Where(p => p.UserId == userId).List().Select(DataPaymentToPayment);

                    return list;
                }
            }
        }

        public IEnumerable<Payment> ListByCategoryId(int categoryId)
        {
            using (var session = _sessionProvider.GetSession<PaymentMap>().OpenSession())
            {
                using (session.BeginTransaction())
                {
                    var list = session.QueryOver<DataPayment>().Where(p => p.CategoryId == categoryId).List().Select(DataPaymentToPayment);

                    return list;
                }
            }
        }

        public Payment Get(int id)
        {
            using (var session = _sessionProvider.GetSession<PaymentMap>().OpenSession())
            {
                using (session.BeginTransaction())
                {
                    var dataPayment = session.Get<DataPayment>(id);

                    return DataPaymentToPayment(dataPayment);
                }
            }            
        }

        public int Create(Payment payment)
        {
            using (var session = _sessionProvider.GetSession<PaymentMap>().OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    var currentDate = DateTime.UtcNow;

                    var dataItem = new DataPayment
                    {
                        CategoryId = payment.CategoryId,
                        UserId = payment.UserId,
                        Summary = payment.Summary,
                        CreateDate = currentDate,
                        LastUpdated = currentDate
                    };

                    session.Save(dataItem);
                    trans.Commit();

                    return dataItem.Id;
                }
            }
        }

        public bool Update(Payment payment)
        {
            var updated = true;
            using (var session = _sessionProvider.GetSession<PaymentMap>().OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    var dataItem = (DataPayment)session.Get(typeof(DataPayment), payment.Id);
                    dataItem.Summary = payment.Summary;
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
            using (var session = _sessionProvider.GetSession<PaymentMap>().OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    try
                    {
                        var item = session.Get(typeof(DataPayment), id);
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

        private Payment DataPaymentToPayment(DataPayment dataPayment)
        {
            return new Payment
            {
                Id = dataPayment.Id,
                CategoryId = dataPayment.CategoryId,
                UserId = dataPayment.UserId,
                Summary = dataPayment.Summary
            };
        }

    }


    public interface IPaymentRepository
    {
        IEnumerable<Payment> List();
        IEnumerable<Payment> ListByUserId(int userId);
        IEnumerable<Payment> ListByCategoryId(int categoryId);
        Payment Get(int id);
        int Create(Payment payment);
        bool Update(Payment payment);
        bool Delete(int id);
    }
}
