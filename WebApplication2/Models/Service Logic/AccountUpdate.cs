using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication2.Models.Transactions;

namespace WebApplication2.Models.Service_Logic
{
    public class AccountUpdate
    {
        

        public static IEnumerable<TransactionResult> UpdateAccount(UpdateAccountTransaction transaction)
        {
            Account_ServiceEntities db = new Account_ServiceEntities();
            List<TransactionResult> resultList = new List<TransactionResult>();
            

            //This assumes that this ActivationCode isn't also in UserAccounts
            // This also assumes that activating an account and updating an account are mutually exclusive.
            if (db.ActivationPendings.Any(a => a.ActivationCode == transaction.ActivationCode)) //if ActivationCode is a pending account
            {
                ActivationPending activationPending = db.ActivationPendings.Find(transaction.ActivationCode);
                TransactionResult transactionResult = new TransactionResult{TransType = "Account Activation"};
                UserAccount userAccount = new UserAccount
                {
                    ActivationCode = activationPending.ActivationCode,
                    UserId = new Guid(activationPending.ActivationCode),
                    FirstName = activationPending.FirstName,
                    LastName = activationPending.LastName,
                    EmailAddress = activationPending.EmailAddress,
                    IdActive = true,
                    ChiefAdmin = activationPending.ChiefAdmin,
                    GroupNumber = activationPending.GroupNumber,
                    ActiveDate = DateTime.Now,
                    InactiveDate = DateTime.MaxValue
                };

                try
                {
                    //move activationPending to UserAccounts
                    db.UserAccounts.Add(userAccount);
                    db.Entry(activationPending).State = EntityState.Deleted;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Error error = new Error
                    {
                        Application = "AccountUpdate Service",
                        ErrDescription = e.Message,
                        ErrDate = DateTime.Now
                    };
                    db.Errors.Add(error);
                    db.SaveChanges();
                    transactionResult.TransSuccess = false;
                }
                finally
                {
                    resultList.Add(transactionResult);
                }
                
            }
            else
            {
                Guid guid = new Guid(transaction.ActivationCode);

                if (db.UserAccounts.Any(a => a.UserId == guid)) //If account is in UserAccount table
                {
                    UserAccount userAccount = db.UserAccounts.Find(guid);
                    if (transaction.UpdateEmail)
                    {
                        TransactionResult transactionResult = new TransactionResult { TransType = "Email Update" };
                        try
                        {
                            userAccount.EmailAddress = transaction.EmailAddress;
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            transactionResult.TransSuccess = false;
                            Error error = new Error
                            {
                                Application = "AccountUpdate Service - Update Email",
                                ErrDescription = e.Message,
                                ErrDate = DateTime.Now
                            };
                            db.Errors.Add(error);
                            db.SaveChanges();
                        }
                        finally
                        {
                            resultList.Add(transactionResult);
                        }
                    }

                    if (transaction.UpdateName)
                    {
                        TransactionResult transactionResult = new TransactionResult()
                        {
                            TransType = "Name Change",
                            TransValue = transaction.FirstName + " " + transaction.LastName
                        };
                        try
                        {
                            userAccount.FirstName = transaction.FirstName;
                            userAccount.LastName = transaction.LastName;
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            transactionResult.TransSuccess = false;
                            Error error = new Error
                            {
                                Application = "AccountUpdate Service - Update Name",
                                ErrDescription = e.Message,
                                ErrDate = DateTime.Now
                            };
                            db.Errors.Add(error);
                            db.SaveChanges();
                        }
                        finally
                        {
                            resultList.Add(transactionResult);
                        }
                    }

                    if (transaction.DisableAccount)
                    {
                        TransactionResult transactionResult = new TransactionResult
                        {
                            TransType = "Disable Acccount",
                            TransValue = transaction.ActivationCode
                        };

                        try
                        {
                            userAccount.IdActive = false;
                            userAccount.InactiveDate = DateTime.Now;
                        }
                        catch (Exception e)
                        {
                            transactionResult.TransSuccess = false;
                            Error error = new Error
                            {
                                Application = "AccountUpdate Service - Disable Account",
                                ErrDescription = e.Message,
                                ErrDate = DateTime.Now
                            };
                            db.Errors.Add(error);
                            db.SaveChanges();
                        }
                        finally
                        {
                            resultList.Add(transactionResult);
                        }
                    }
                }
            }
            return resultList;
        }
    }
}