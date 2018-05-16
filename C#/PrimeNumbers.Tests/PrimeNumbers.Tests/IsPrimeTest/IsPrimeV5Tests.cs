using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeNumbers.Model.IsPrime;

namespace PrimeNumbers.Tests
{

    [TestClass]
    public class IsPrimeV5Tests : IsPrimeTestBase
    {
        public IsPrimeV5Tests() : base(new IsPrimeV5())
        { }
    }
}
