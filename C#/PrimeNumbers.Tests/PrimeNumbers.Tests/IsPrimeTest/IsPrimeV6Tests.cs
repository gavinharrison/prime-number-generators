using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeNumbers.Model.IsPrime;

namespace PrimeNumbers.Tests
{

    [TestClass]
    public class IsPrimeV6Tests : IsPrimeTestBase
    {
        public IsPrimeV6Tests() : base(new IsPrimeV6())
        { }
    }
}
