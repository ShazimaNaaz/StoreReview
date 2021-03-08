using System;
using JewelleryStore.PageModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JewelleryStoreUnitTest
{
    [TestClass]
    public class EstimationPageUnitTest
    {
        [TestMethod]
        public void CalculateTest()
        {
            //Arrange
            var vm = new EstimationPageModel(null,null);
            vm.GoldPrice = "490";
            vm.Weight = "20";

            //Act
            vm.CalculateCommand.Execute(null);

            //Assert
            Assert.IsTrue(vm.TotalPrice == "9800", "Total amount is 9800");
        }

        [TestMethod]
        public void CalculatePrevilegedUserTest()
        {
            //Arrange
            var vm = new EstimationPageModel(null, null);
            vm.GoldPrice = "490";
            vm.Weight = "20";
            vm.Discount = "2";

            //Act
            vm.CalculateCommand.Execute(null);

            //Assert
            Assert.IsTrue(vm.TotalPrice == "9800", "Total amount is 9800");
        }
    }
}
