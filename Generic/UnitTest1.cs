using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Moq;

namespace Generic
{

    //consolidates different tariffs for different vendors have products which have tariffs
    //applies a set of business rules, for example (if weight is more than tolerance, add surcharge)
    //rule can be different for different vendors.

    //Product A,if purchase 500lb,  VEndor ! will charge 600 
    //verus Vendor B is going to charge 450

    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Product_Manager_Constructor_with_null()
        {
            var productManager = new ProductManager(null);

            Assert.IsNotNull(productManager);

        }

        [TestMethod]
        public void Test_Product_Manager_Constructor()
        {
            var mock = new Mock<IProductsDataAccess>();

            var algorithmFactory = new VendorRuleAlgorithmFactory();
            var productManager = new ProductManager(mock.Object, algorithmFactory);

            Assert.IsNotNull(productManager);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Product_Manager_Constructor_with_algorithm_but_null_data_access()
        {
            var algorithmFactory = new VendorRuleAlgorithmFactory();
            var productManager = new ProductManager(null, algorithmFactory);

            Assert.IsNotNull(productManager);
        }

        [TestMethod]
        public void Test_Product_Manager_Default_Constructor()
        {
            var mock = new Mock<IProductsDataAccess>();
            var productManager = new ProductManager(mock.Object);

            Assert.IsNotNull(productManager);
        }

        [TestMethod]
        public void Test_Product_Manager_No_Change_In_Rates()
        {
            var mock = new Mock<IProductsDataAccess>();
            mock.Setup(foo => foo.GetProductRates(
                It.IsAny<List<int>>())).Returns(new List<ProductRate>()
                {
                    new ProductRate()
                    {
                        Id = 88,
                        VendorId = 99,
                        Weight = 100.0M,
                        Rate = 100.0M
                    }
                }
                );

            var mockVendorAlgorithm = new Mock<IVendorRuleAlgorithm>();
            mockVendorAlgorithm.Setup(foo => foo.GetDiscountForProduct(It.IsAny<ProductRate>())).Returns(4.0M);

            var mockVendorFactory = new Mock<IVendorRuleAlgorithmFactory>();
            mockVendorFactory.Setup(foo => foo.GetVendorRuleAlrogithm(99)).Returns(mockVendorAlgorithm.Object);


            var productManager = new ProductManager(mock.Object, mockVendorFactory.Object);

            var result = productManager.GetRatesForProducts(new List<int>() { 5, 6 });

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

            Assert.IsNotNull(result[0]);
            Assert.AreEqual(88, result[0].Id);
            Assert.AreEqual(96.0M, result[0].Rate);
        }


    }
}
