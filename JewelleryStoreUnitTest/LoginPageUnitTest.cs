using JewelleryStore.PageModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JewelleryStoreUnitTest
{
    [TestClass]
    public class LoginPageUnitTest
    {
        [TestMethod]
        public void LoginTest()
        {
            //Arrange
            var vm = new LoginPageModel(null,null);
            vm.UserName = "shazima";
            vm.Password = "abc";

            //Act
            vm.LoginCommand.Execute(null);

            //Assert
            Assert.IsTrue(vm.UserType == "Normal", "UserType is Normal");
        }

        [TestMethod]
        public void LoginFailedTest()
        {
            //Arrange
            var vm = new LoginPageModel(null, null);
            vm.UserName = "shaz";
            vm.Password = "a";

            //Act
            vm.LoginCommand.Execute(null);

            //Assert
            Assert.IsFalse(vm.UserType.Equals(string.Empty), "UserType is not valid");
        }
    }
}
