using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Generic
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void Test_For_Null_Algorithm()
        {
            //Arrange
            ProductRate sampleProduct = new ProductRate()
            {
                Rate = 999,
                Id = 2
            };

            var mockDataAccess = new Mock<IProductsDataAccess>();
            mockDataAccess.Setup(foo => foo.GetProductRates(It.IsAny<List<int>>())).
                Returns(new List<ProductRate>() { sampleProduct });

            var mockAlgoFactory = new Mock<IVendorRuleAlgorithmFactory>();
            mockAlgoFactory.Setup( foo=> foo.GetVendorRuleAlrogithm(It.IsAny<int>())).Returns((IVendorRuleAlgorithm)null);

            ProductManager manager = new ProductManager(mockDataAccess.Object, mockAlgoFactory.Object);

            //Act
            var results = manager.GetRatesForProducts(null);

            //Assert

            Assert.AreEqual(999, results.First().Rate);
        }

        [TestMethod]
        public void Test_For_VendorId_15_Discount_20()
        {

        }
    }
}
