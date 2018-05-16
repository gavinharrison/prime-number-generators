using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeNumbers.Model.IsPrime;
using PrimeNumbers.Model.FindPrimesTo;

namespace PrimeNumbers.Tests
{
    /// <summary>
    /// Summary description for FindPrimesToV1Tests
    /// </summary>
    [TestClass]
    public class FindPrimesToV1Tests : FindPrimesToTestBase
    {
        public FindPrimesToV1Tests() : base(new FindPrimesToV1(), new IsPrimeV1())
        {

        }

    }
}
