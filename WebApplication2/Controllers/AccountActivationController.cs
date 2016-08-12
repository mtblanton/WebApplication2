using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;
using WebApplication2.Models.Service_Logic;
using WebApplication2.Models.Transactions;

namespace WebApplication2.Controllers
{
    public class AccountActivationController : ApiController
    {
        // api/AccountActivation/put
        public TransactionResult Post(NewAccountTransaction newAccountTransaction)
        {
                return AccountActivation.ActivateAccount(newAccountTransaction);
        }
    }
}
