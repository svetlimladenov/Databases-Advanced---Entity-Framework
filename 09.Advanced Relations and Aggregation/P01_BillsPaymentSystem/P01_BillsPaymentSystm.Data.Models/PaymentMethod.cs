using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }

        public PaymentMethodType PaymentMethodType { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public BankAccount BankAccount { get; set; }
        public int? BankAccountId { get; set; }

        public CreditCard CreditCard { get; set; }
        public int? CreditCardId { get; set; }
    }
}
