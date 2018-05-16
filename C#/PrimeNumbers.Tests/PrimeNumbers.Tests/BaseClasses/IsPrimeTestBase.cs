using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeNumbers.Model.Interfaces;
using System;

namespace PrimeNumbers.Tests
{
    /// <summary>
    /// Summary description for IsPrimeTests
    /// </summary>
    [TestClass]
    public abstract class IsPrimeTestBase
    {
        protected IIsPrime Tester;

        public IsPrimeTestBase(IIsPrime tester)
        {
            this.Tester = tester;
        }

        [TestMethod]
        public void IsPrime_False_IsOnePrime()
        {
            bool result = this.Tester.CheckIsPrime(1);

            Assert.IsFalse(result, "One is not seen as a prime number");

        }

        [TestMethod]
        public void IsPrime_True_IsTwoPrime()
        {
            bool result = this.Tester.CheckIsPrime(2);

            Assert.IsTrue(result, "Two is a prime number");
        }

        [TestMethod]
        public void IsPrime_True_IsThreePrime()
        {
            bool result = this.Tester.CheckIsPrime(3);

            Assert.IsTrue(result, "Three is a prime number");
        }

        [TestMethod]
        public void IsPrime_False_IsFourPrime()
        {
            bool result = this.Tester.CheckIsPrime(4);

            Assert.IsFalse(result, "Four is not a prime number");
        }

        [TestMethod]
        public void IsPrime_True_IsFivePrime()
        {
            bool result = this.Tester.CheckIsPrime(5);

            Assert.IsTrue(result, "Three is a prime number");
        }

        [TestMethod]
        public void IsPrime_False_IsSixPrime()
        {
            bool result = this.Tester.CheckIsPrime(6);

            Assert.IsFalse(result, "Four is not a prime number");
        }

        [TestMethod]
        public void IsPrime_True_IsSevenPrime()
        {
            bool result = this.Tester.CheckIsPrime(7);

            Assert.IsTrue(result, "Seven is a prime number");
        }

        [TestMethod]
        public void IsPrime_False_IsEightPrime()
        {
            bool result = this.Tester.CheckIsPrime(8);

            Assert.IsFalse(result, "Four is not a prime number");
        }

        [TestMethod]
        public void IsPrime_False_IsNinePrime()
        {
            bool result = this.Tester.CheckIsPrime(9);

            Assert.IsFalse(result, "Nine is not a prime number");
        }

        [TestMethod]
        public void IsPrime_True_IsElevenPrime()
        {
            bool result = this.Tester.CheckIsPrime(11);

            Assert.IsTrue(result, "Eleven is a prime number");
        }

        [TestMethod]
        public void IsPrime_Exception_NumberLessThanOne()
        {
            try
            {
                bool result = this.Tester.CheckIsPrime(0);
                Assert.Fail("An exception should have been thrown");
            }
            catch (ArgumentOutOfRangeException ae)
            {
                Assert.AreEqual("number", ae.ParamName);
                Assert.AreEqual("The provided number needs to be greater than Zero\r\nParameter name: number", ae.Message);
            }
            catch (Exception e)
            {
                Assert.Fail(
                     string.Format("Unexpected exception of type {0} caught: {1}",
                                    e.GetType(), 
                                    e.Message)
                );
            }
        }

        [TestMethod]
        public void IsPrime_True_BigPrimesNetLargestPrime()
        {
            ulong numberToTest = 32416190071;

            bool result = (this.Tester.CheckIsPrime(numberToTest));

            Assert.IsTrue(result, "BigPrimes.net largest prime is prime");

        }
    }
}
