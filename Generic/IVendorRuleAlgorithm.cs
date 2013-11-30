using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generic
{
    public interface IVendorRuleAlgorithm
    {
        decimal GetDiscountForProduct(ProductRate product);
    }
}
