using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeNumbers.Model.IsPrime;

namespace PrimeNumbers.Tests
{

    [TestClass]
    public class IsPrimeV2Tests : IsPrimeTestBase
    {

        public IsPrimeV2Tests() : base(new IsPrimeV2())
        { }

    }
}
