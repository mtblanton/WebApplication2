using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class TransactionResult
    {
        public bool TransSuccess { get; set; }
        public string TransType { get; set; }
        public string TransValue { get; set; }
        public string TransDate { get; set; }


        public TransactionResult()
        {
            TransSuccess = true;
            TransDate = DateTime.Now.ToString("s");
        }
    }
}