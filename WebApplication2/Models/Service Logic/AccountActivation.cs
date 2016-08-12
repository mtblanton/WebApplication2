using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models.Transactions;

namespace WebApplication2.Models.Service_Logic
{
    public class AccountActivation
    {
        public static TransactionResult ActivateAccount(NewAccountTransaction newAccountTransaction)
        {
            Account_ServiceEntities db = new Account_ServiceEntities();
            TransactionResult transactionResult = new TransactionResult {TransType = "Account Activation"};
            
            if (!newAccountTransaction.TaxId.Contains('-')) //if TaxId is a valid TaxId
            {
                ActivationPending activationPending = new ActivationPending
                {
                    ChiefAdmin = newAccountTransaction.ChiefAdmin,
                    CreateDate = DateTime.Now,
                    EmailAddress = newAccountTransaction.EmailAddress,
                    FirstName = newAccountTransaction.FirstName,
                    LastName = newAccountTransaction.LastName,
                    GroupName = newAccountTransaction.GroupName,
                    GroupNumber = newAccountTransaction.GroupNumber,
                    TaxId = newAccountTransaction.TaxId
                };

                try
                {
                    db.ActivationPendings.Add(activationPending);
                    db.SaveChanges();
                    transactionResult.TransValue = activationPending.ActivationCode;
                }
                catch (Exception)
                {
                    transactionResult.TransSuccess = false;
                }

            }

            else
            {
                transactionResult.TransSuccess = false;
                transactionResult.TransValue = "Invalid tax ID";
                //TODO: Write to error table
            }

            return transactionResult;
        }
    }
}