using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeNumbers.Model.IsPrime;

namespace PrimeNumbers.Tests
{

    [TestClass]
    public class IsPrimeV4Tests : IsPrimeTestBase
    {

        public IsPrimeV4Tests() : base(new IsPrimeV4())
        { }
    }
}
