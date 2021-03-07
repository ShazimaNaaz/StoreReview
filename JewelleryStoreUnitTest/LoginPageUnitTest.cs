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
    }
}
