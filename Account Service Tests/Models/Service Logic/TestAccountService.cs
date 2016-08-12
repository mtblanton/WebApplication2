using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication2.Models.Service_Logic;
using WebApplication2.Models.Transactions;

namespace Account_Service_Tests
{
    [TestClass()]
    internal class TestAccountService
    {
        private string _ActivationCode;

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
            _ActivationCode = activateTransactionResult.TransValue;
        }

        [TestMethod]
        public void AccountUpdate_ShouldReturnTransactionResult()
        {
            var activateUserAccountTransaction = new UpdateAccountTransaction
            {
                UpdateName = false,
                UpdateEmail = false,
                DisableAccount = false,
                ActivationCode = _ActivationCode,
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "johndoe@yahoo.com"
            };

            var accountActivateResult = AccountUpdate.UpdateAccount(activateUserAccountTransaction);
            Assert.IsNotNull(accountActivateResult);
        }
    }
}