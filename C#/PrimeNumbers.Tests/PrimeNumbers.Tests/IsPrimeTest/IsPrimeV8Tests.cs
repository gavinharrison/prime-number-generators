using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeNumbers.Model.IsPrime;

namespace PrimeNumbers.Tests
{

    [TestClass]
    public class IsPrimeV8Tests : IsPrimeTestBase
    {
        public IsPrimeV8Tests() : base(new IsPrimeV8())
        { }

    }
}
