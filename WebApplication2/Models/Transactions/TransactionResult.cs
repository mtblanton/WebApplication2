using System;

namespace WebApplication2.Models
{
    public class TransactionResult
    {
        public TransactionResult()
        {
            TransSuccess = true;
            TransDate = DateTime.Now.ToString("s");
        }

        public bool TransSuccess { get; set; }
        public string TransType { get; set; }
        public string TransValue { get; set; }
        public string TransDate { get; set; }
    }
}