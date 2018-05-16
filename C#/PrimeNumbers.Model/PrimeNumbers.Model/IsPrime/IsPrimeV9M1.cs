using NLog;
using PrimeNumbers.Model.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumbers.Model.IsPrime
{
    public class TIsPrimeV9M1 : IIsPrime
    {
        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public TIsPrimeV9M1()
        {
            this.Primes = new BitArray(int.MaxValue - 57);
        }

        #endregion

        #region Properties

        private static Logger Logger = LogManager.GetCurrentClassLogger();

        protected bool UseParallel { get { return false; } }

        protected BitArray Primes { get; set; }

        protected IEnumerable<ulong> Results { get; set; }

        /// <summary>
        /// Returns the version of the class
        /// </summary>
        public Version Version
        {
            get
            {
                return new Version(9, 1);
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

            bool result = false;
            
            foreach(ulong prime in this.Primes)
            {
                if(number == prime)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        protected void GeneratePrimes()
        {
            IIsPrime checker = new IsPrimeV7();

            Stopwatch sw = new Stopwatch();

            sw.Restart();

            int number = int.MaxValue;
            List<ulong> primeNumbers = new List<ulong>();

            this.Primes[0] = false;
            this.Primes[1] = false;

            if (this.UseParallel)
            {
                int maxTestNumber = (int)Math.Sqrt(number) + 1;
                Parallel.For(2, maxTestNumber, i =>
                {

                    if ((this.Primes[i]) &&
                       (checker.CheckIsPrime((uint)i)))
                    {
                        for (int j = (i * 2); j <= number && j > 0; j = j + i)
                        {
                            this.Primes[j] = false;
                        }
                    }
                });

                primeNumbers.Sort();
            }
            else
            {

                int maxTestNumber = (int)Math.Sqrt(number) + 1;
                for (int i = 2; i <= maxTestNumber; i++)
                {
                    if ((this.Primes[i]) &&
                       (checker.CheckIsPrime((uint)i)))
                    {
                        for (int j = (i * 2); j <= number && j > 0; j = j + i)
                        {
                            this.Primes[j] = false;
                        }
                    }
                }
            }

            this.Results = primeNumbers;

            sw.Stop();

            TIsPrimeV9M1.Logger.Info($"Prime Generation Took {Utilities.ElaspedTimeFormatter(sw)}");

        }

        #endregion
    }
}
