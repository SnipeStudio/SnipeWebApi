using SStudio.BudgetManager.Web.API.Models;
using SStudio.BudgetManager.Web.API.Models.Requests;
using SStudio.BudgetManager.Web.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SStudio.BudgetManager.Web.API.Business
{
    public class PaymentBusiness : IPaymentBusiness
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentBusiness(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public int Create(int userId, int categoryId, CreateUpdatePaymentRequest request)
        {
            var paymentId = _paymentRepository.Create(new Payment
            {
                UserId = userId,
                CategoryId = categoryId,
                Summary = request.Summary.Value
            });

            return paymentId;
        }

        public bool Delete(int id)
        {
            return _paymentRepository.Delete(id);
        }

        public Payment Get(int id)
        {
            return _paymentRepository.Get(id);
        }

        public IEnumerable<Payment> List()
        {
            return _paymentRepository.List();
        }

        public IEnumerable<Payment> ListByCategoryId(int categoryId)
        {
            return _paymentRepository.ListByCategoryId(categoryId);
        }

        public IEnumerable<Payment> ListByUserId(int userId)
        {
            return _paymentRepository.ListByUserId(userId);
        }

        public bool Update(int paymentId, CreateUpdatePaymentRequest request)
        {
            return _paymentRepository.Update(new Payment
            {
                Id = paymentId,
                Summary = request.Summary.Value
            });
        }
    }

    public interface IPaymentBusiness
    {
        IEnumerable<Payment> List();
        IEnumerable<Payment> ListByUserId(int userId);
        IEnumerable<Payment> ListByCategoryId(int categoryId);
        Payment Get(int id);
        int Create(int userId, int categoryId, CreateUpdatePaymentRequest request);
        bool Update(int paymentId, CreateUpdatePaymentRequest request);
        bool Delete(int id);
    }
}
