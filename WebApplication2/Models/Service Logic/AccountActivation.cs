using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using WebApplication2.Models.Transactions;

namespace WebApplication2.Models.Service_Logic
{
    public class AccountActivation
    {
        public static TransactionResult ActivateAccount(NewAccountTransaction newAccountTransaction)
        {

            Account_ServiceEntities db = new Account_ServiceEntities();
            TransactionResult transactionResult = new TransactionResult { TransType = "Account Activation" };

            if (newAccountTransaction == null)
            {
                Error error = new Error
                {
                    Application = "AccountUpdate Service",
                    ErrDescription = "newAccountTransaction is null. Ensure the request is properly formatted.",
                    ErrDate = DateTime.Now
                };
                db.Errors.Add(error);
                db.SaveChanges();

                return new TransactionResult
                {
                    TransSuccess = false,
                    TransType = "Account Activation",
                    TransValue = "Unable to process request. Ensure the request is formatted properly."
                };
            }
            
            if (!newAccountTransaction.TaxId.Contains('-')) //if TaxId is a valid TaxId
            {
                ActivationPending activationPending = new ActivationPending
                {
                    ActivationCode = Guid.NewGuid().ToString(),
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
                    MailMessage mail = new MailMessage("email@email.com", newAccountTransaction.EmailAddress);
                    SmtpClient smtpClient = new SmtpClient
                    {
                        Port = 25,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Host = "smtp.email.com"
                    };

                    mail.Subject = "Group Site Online Registration";
                    mail.Body = "Dear " + newAccountTransaction.FirstName + " " + newAccountTransaction.LastName + "...";
                    //smtpClient.Send(mail);
                }
                catch (Exception e)
                {
                    Error error = new Error
                    {
                        Application = "AccountActivation Service",
                        ErrDescription = e.Message,
                        ErrDate = DateTime.Now
                    };
                    db.Errors.Add(error);
                    db.SaveChanges();

                    return new TransactionResult
                    {
                        TransSuccess = false,
                        TransType = "Account Activation",
                        TransValue = e.Message
                    };
                }

            }

            else
            {
                transactionResult.TransSuccess = false;
                transactionResult.TransValue = "Invalid tax ID";

                Error error = new Error
                {
                    Application = "AccountActivation Service",
                    ErrDescription = "Invalid tax ID",
                    ErrDate = DateTime.Now
                };
                db.Errors.Add(error);
                db.SaveChanges();
            }

            return transactionResult;
        }
    }
}