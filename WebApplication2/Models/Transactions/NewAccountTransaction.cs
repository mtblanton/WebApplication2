using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Transactions
{
    public class NewAccountTransaction : Transaction
    {
        public string GroupNumber { get; set; }
        public string TaxId { get; set; }
        public string GroupName { get; set; }
        public bool ChiefAdmin { get; set; }
    }

    
}