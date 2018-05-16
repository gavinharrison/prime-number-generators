using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeNumbers.Model.IsPrime;
using PrimeNumbers.Model.FindPrimesTo;

namespace PrimeNumbers.Tests
{
    /// <summary>
    /// Summary description for FindPrimesToV2Tests
    /// </summary>
    [TestClass]
    public class FindPrimesToV2Tests : FindPrimesToTestBase
    {
        public FindPrimesToV2Tests() : base(new FindPrimesToV2(), new IsPrimeV1())
        {

        }

    }
}
