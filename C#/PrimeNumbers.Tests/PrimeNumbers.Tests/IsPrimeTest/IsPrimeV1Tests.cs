using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeNumbers.Model.IsPrime;

namespace PrimeNumbers.Tests
{

    [TestClass]
    public class IsPrimeV1Tests : IsPrimeTestBase
    {
        public IsPrimeV1Tests() : base(new IsPrimeV1())
        { }

    }
}
