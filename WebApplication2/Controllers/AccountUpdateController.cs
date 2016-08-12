using System.Collections.Generic;
using System.Web.Http;
using WebApplication2.Models;
using WebApplication2.Models.Service_Logic;
using WebApplication2.Models.Transactions;

namespace WebApplication2.Controllers
{
    public class AccountUpdateController : ApiController
    {
        // api/accountupdate/put
        public IEnumerable<TransactionResult> Put([FromBody] UpdateAccountTransaction transaction)
        {
            return AccountUpdate.UpdateAccount(transaction);
        }
    }
}