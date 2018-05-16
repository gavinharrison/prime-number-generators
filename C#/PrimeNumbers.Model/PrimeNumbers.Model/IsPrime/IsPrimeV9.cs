using NLog;
using PrimeNumbers.Model.Interfaces;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumbers.Model.IsPrime
{
    public class IsPrimeV9 : IIsPrime
    {
        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public IsPrimeV9()
        {
            this.Primes = new List<ulong>();
        }

        #endregion

        #region Properties

        private static Logger Logger = LogManager.GetCurrentClassLogger();

        protected bool UseParallel { get { return true; } }

        protected List<ulong> Primes { get; set; }

        /// <summary>
        /// Returns the version of the class
        /// </summary>
        public Version Version
        {
            get
            {
                return new Version(9, 0);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks to see if the given number is a prime number
        /// </summary>
        /// <param name="number">The number to check.</param>
        /// <returns>A boolean that indicates if the number is prime</returns>
        public bool CheckIsPrime(ulong number)
        {
            if (number <= 0)
            {
                throw new ArgumentOutOfRangeException("number", "The provided number needs to be greater than Zero");
            }

            if (number == 1)
            {
                return false;
            }

            if (number == 2)
            {
                return true;
            }

            bool result = true;

            ulong maxNumberToTest = (ulong)Math.Ceiling(Math.Sqrt(number));

            if(this.Primes.LastOrDefault() < maxNumberToTest)
            {
                this.GeneratePrimes(maxNumberToTest);
            }
            
            foreach(ulong prime in this.Primes.Where(x => x <= maxNumberToTest).AsEnumerable<ulong>())
            {
                if(number % prime == 0)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void GeneratePrimes(ulong findPrimesTo)
        {
            if(findPrimesTo > (ulong)uint.MaxValue+1)
            {
                throw new ArgumentOutOfRangeException("findPrimesTo");
            }

            IIsPrime checker = new IsPrimeV7();

            Stopwatch sw = new Stopwatch();
            sw.Restart();

            long startNumber = (this.Primes.Count > 0 ? (long)this.Primes.Last() : 2);

            ConcurrentBag<ulong> primeNumbers = new ConcurrentBag<ulong>();

            if (this.UseParallel)
            {
                Parallel.For(startNumber, (long)findPrimesTo + 1, i =>
                {
                    if (checker.CheckIsPrime((ulong)i))
                    {
                        primeNumbers.Add((ulong)i);
                    }
                });

            }
            else
            {
                if (startNumber == 2)
                {
                    primeNumbers.Add(2);
                }

                for (ulong i = (ulong)(startNumber % 2 == 0 ? startNumber+1 : startNumber); i <= findPrimesTo; i=i+2)
                {
                    if (checker.CheckIsPrime(i))
                    {
                        primeNumbers.Add(i);
                    }
                }
            }

            this.Primes.AddRange(primeNumbers.OrderBy(x => x));

            sw.Stop();

            IsPrimeV9.Logger.Info($"PrimeNumber Generation Took {Utilities.ElaspedTimeFormatter(sw)}");

        }

        #endregion
    }
}
