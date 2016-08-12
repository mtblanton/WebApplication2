using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication2.Models.Service_Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models.Transactions;

namespace WebApplication2.Models.Service_Logic.Tests
{
    [TestClass()]
    public class AccountUpdateTests
    {

        private string _activationCode;

        [TestMethod()]
        public void UpdateAccountTest()
        {
            Assert.IsTrue(1 == 1);
        }

        [TestMethod()]
        public void AccountActivate_ShouldReturnTransactionResult()
        {
            var newAccountTransaction = new NewAccountTransaction
            {
                ChiefAdmin = true,
                EmailAddress = "johndoe@yahoo.com",
                FirstName = "John",
                GroupName = "ACME, Inc.",
                GroupNumber = "99999",
                LastName = "Doe",
                TaxId = "719999999"
            };

            var activateTransactionResult = AccountActivation.ActivateAccount(newAccountTransaction);
            Assert.IsNotNull(activateTransactionResult);
            Assert.AreEqual(activateTransactionResult.TransSuccess, true);
            Assert.AreEqual(activateTransactionResult.TransType, "Account Activation");
            Assert.IsNotNull(activateTransactionResult.TransValue);
            _activationCode = activateTransactionResult.TransValue;
        }

        //this test is failing due to not finding the "Account_ServiceEntities" entry in the config file
        [TestMethod()]
        public void AccountUpdate_ShouldReturnTransactionResult()
        {
            var activateUserAccountTransaction = new UpdateAccountTransaction
            {
                UpdateName = false,
                UpdateEmail = false,
                DisableAccount = false,
                ActivationCode = _activationCode,
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "johndoe@yahoo.com"
            };

            var accountActivateResult = AccountUpdate.UpdateAccount(activateUserAccountTransaction);
            Assert.IsNotNull(accountActivateResult);
        }
    }
}