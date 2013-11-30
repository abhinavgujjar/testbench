using System;
namespace Generic
{
    public interface IVendorRuleAlgorithmFactory
    {
        IVendorRuleAlgorithm GetVendorRuleAlrogithm(int vendorId);
    }
}
