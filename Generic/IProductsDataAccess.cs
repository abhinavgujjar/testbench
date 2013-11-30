using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic
{
    public interface IProductsDataAccess
    {
        List<ProductRate> GetProductRates(List<int> productIds);
    }
}
