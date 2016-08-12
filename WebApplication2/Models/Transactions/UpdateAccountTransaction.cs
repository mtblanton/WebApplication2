

namespace WebApplication2.Models.Transactions
{
    public class UpdateAccountTransaction : Transaction
    {
        public bool UpdateName { get; set; }
        public bool UpdateEmail { get; set; }
        public bool DisableAccount { get; set; }
        public string ActivationCode { get; set; }
    }
}