using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeNumbers.Model.IsPrime;

namespace PrimeNumbers.Tests
{

    [TestClass]
    public class IsPrimeV7Tests : IsPrimeTestBase
    {
        public IsPrimeV7Tests() : base(new IsPrimeV7())
        { }

    }
}
