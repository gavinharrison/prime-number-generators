using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeNumbers.Model.IsPrime;

namespace PrimeNumbers.Tests
{

    [TestClass]
    public class IsPrimeV3Tests : IsPrimeTestBase
    {
        public IsPrimeV3Tests() : base(new IsPrimeV3())
        { }
    }
}
