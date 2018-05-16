using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeNumbers.Model.IsPrime;
using PrimeNumbers.Model.FindPrimesBetween;

namespace PrimeNumbers.Tests
{
    /// <summary>
    /// Summary description for FindPrimesToV1Tests
    /// </summary>
    [TestClass]
    public class FindPrimesBetweenV5Tests : FindPrimesBetweenTestBase
    {
        public FindPrimesBetweenV5Tests() : base(new FindPrimesBetweenV5(), new IsPrimeV1())
        {

        }

    }
}
