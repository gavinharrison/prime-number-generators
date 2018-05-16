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
    public class FindPrimesToV3Tests : FindPrimesToTestBase
    {
        public FindPrimesToV3Tests() : base(new FindPrimesToV3(), new IsPrimeV1())
        {

        }

    }
}
