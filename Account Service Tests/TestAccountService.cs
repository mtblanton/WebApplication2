using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication2.Controllers;
using WebApplication2.Models;
using WebApplication2.Models.Service_Logic;
using WebApplication2.Models.Transactions;

namespace Account_Service_Tests
{
    class TestAccountService
    {
        private string _ActivationCode;

        [TestMethod]
        public void AccountActivate_ShouldReturnTransactionResult()
        {
            NewAccountTransaction newAccountTransaction = new NewAccountTransaction
            {
                ChiefAdmin = true,
                EmailAddress = "johndoe@yahoo.com",
                FirstName = "John",
                GroupName = "ACME, Inc.",
                GroupNumber = "99999",
                LastName = "Doe",
                TaxId = "719999999"
            };

            TransactionResult activateTransactionResult = AccountActivation.ActivateAccount(newAccountTransaction);
            Assert.IsNotNull(activateTransactionResult);
            Assert.AreEqual(activateTransactionResult.TransSuccess, true);
            Assert.AreEqual(activateTransactionResult.TransType, "Account Activation");
            Assert.IsNotNull(activateTransactionResult.TransValue);
            _ActivationCode = activateTransactionResult.TransValue;
        }

        [TestMethod]
        public void AccountUpdate_ShouldReturnTransactionResult()
        {
            UpdateAccountTransaction activateUserAccountTransaction = new UpdateAccountTransaction
            {
                UpdateName = false,
                UpdateEmail = false,
                DisableAccount = false,
                ActivationCode = _ActivationCode,
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "johndoe@yahoo.com"
            };

            IEnumerable<TransactionResult> accountActivateResult = AccountUpdate.UpdateAccount(activateUserAccountTransaction);
            Assert.IsNotNull(accountActivateResult);
        }
    }
}
