namespace PrimeNumbers.Model.IsPrime
{
    #region Usings

    using System;
    using NLog;
    using PrimeNumbers.Model.Interfaces;

    #endregion

    /// <summary>
    /// Base6 check and Square Root.
    /// </summary>
    public class IsPrimeV7 : IIsPrime
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        #region Properties

        /// <summary>
        /// Returns the version of the class
        /// </summary>
        public Version Version
        {
            get
            {
                return new Version(7, 0);
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

            bool result = true;

            if (number < 2)
            {
                result = false;
            }
            else if (number != 2 &&
                     number != 3 &&
                     number != 5)
            {
                if (number % 2 == 0 ||
                    number % 3 == 0 ||
                    number % 5 == 0)

                {
                    result = false;
                }
                else
                {
                    double maxTestNumber = Math.Sqrt((double)number) + 1;

                    for (ulong i = 7; i < maxTestNumber; i = i + 6)
                    {
                        if ((number % i) == 0)
                        {
                            result = false;
                            break;
                        }
                        if((number % (i + 4)) == 0)
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }
                
            return result;
        }

        #endregion
    }
}
