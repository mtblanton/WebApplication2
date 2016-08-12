using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.Service_Logic;
using WebApplication2.Models.Transactions;

namespace WebApplication2.Controllers
{
    public class AccountUpdateController : ApiController
    {
        // api/accountupdate/put
        public IEnumerable<TransactionResult> Put([FromBody]UpdateAccountTransaction transaction)
        {
            return AccountUpdate.UpdateAccount(transaction);
        }
    }
     
}
