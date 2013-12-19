using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generic
{
    class ProductManager
    {
        private IProductsDataAccess dataAccess;
        private IVendorRuleAlgorithmFactory algorithmFactory;

        public ProductManager()
        {
            // TODO: Complete member initialization
        }

        public ProductManager(IProductsDataAccess dataAccess)
        {
            if (dataAccess == null)
            {
                throw new ArgumentNullException();
            }

            // TODO: Complete member initialization
            this.dataAccess = dataAccess;
        }

        public ProductManager(IProductsDataAccess dataAccess, IVendorRuleAlgorithmFactory algorithmFactory)
        {
            if (dataAccess == null)
            {
                throw new ArgumentNullException();
            }

            // TODO: Complete member initialization
            this.dataAccess = dataAccess;
            this.algorithmFactory = algorithmFactory;
        }

        internal List<ProductRate> GetRatesForProducts(List<int> targetProducts)
        {
            var productRates = dataAccess.GetProductRates(targetProducts);

            foreach (var product in productRates)
            {
                var vendorAlgorithm = algorithmFactory.GetVendorRuleAlrogithm(product.VendorId);

                var discount = 0.0M;
                if (vendorAlgorithm != null)
                {
                    discount = vendorAlgorithm.GetDiscountForProduct(product);
                }

                product.Rate = product.Rate - (product.Rate * discount / 100);
            }

            return productRates;
        }
    }
}
