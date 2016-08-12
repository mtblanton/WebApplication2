using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApplication2.Models.Transactions;

namespace WebApplication2.Models.Service_Logic
{
    public class AccountUpdate
    {
        public static IEnumerable<TransactionResult> UpdateAccount(UpdateAccountTransaction transaction)
        {
            var db = new Account_ServiceEntities();
            var resultList = new List<TransactionResult>();

            if (transaction.ActivationCode == null)
            {
                resultList.Add(
                    new TransactionResult
                    {
                        TransSuccess = false,
                        TransType = "Account Update",
                        TransValue = "Null activation code. Ensure the request is properly formatted."
                    }
                    );
                return resultList;
            }


            //This assumes that this ActivationCode isn't also in UserAccounts
            // This also assumes that activating an account and updating an account are mutually exclusive.
            if (db.ActivationPendings.Any(a => a.ActivationCode == transaction.ActivationCode))
                //if ActivationCode is a pending account
            {
                var activationPending = db.ActivationPendings.Find(transaction.ActivationCode);
                var transactionResult = new TransactionResult {TransType = "Account Activation"};
                var userAccount = new UserAccount
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
                    var error = new Error
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
                var guid = new Guid(transaction.ActivationCode);

                if (db.UserAccounts.Any(a => a.UserId == guid)) //If account is in UserAccount table
                {
                    var userAccount = db.UserAccounts.Find(guid);
                    if (transaction.UpdateEmail)
                    {
                        var transactionResult = new TransactionResult
                        {
                            TransType = "Email Update",
                            TransValue = transaction.EmailAddress
                        };
                        try
                        {
                            userAccount.EmailAddress = transaction.EmailAddress;
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            transactionResult.TransSuccess = false;
                            var error = new Error
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
                        var transactionResult = new TransactionResult
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
                            var error = new Error
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
                        var transactionResult = new TransactionResult
                        {
                            TransType = "Disable Acccount",
                            TransValue = transaction.ActivationCode
                        };

                        try
                        {
                            userAccount.IdActive = false;
                            userAccount.InactiveDate = DateTime.Now;
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            transactionResult.TransSuccess = false;
                            var error = new Error
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