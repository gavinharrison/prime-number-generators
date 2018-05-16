namespace PrimeNumbers.Model.IsPrime
{
    #region Usings

    using System;
    using PrimeNumbers.Model.Interfaces;

    #endregion

    /// <summary>
    /// Similar to V5 and V3 tests up to the Square root and only odd number.
    /// </summary>
    public class IsPrimeV6 : IIsPrime
    {
        #region Properties

        /// <summary>
        /// Returns the version of the class
        /// </summary>
        public Version Version
        {
            get
            {
                return new Version(6, 0);
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
            else if (number != 2)
            {
                if (number % 2 == 0)
                {
                    result = false;
                }
                else
                {
                    double maxTestNumber = Math.Sqrt((double)number) + 1;

                    for (ulong i = 3; i < maxTestNumber; i = i + 2)
                    {
                        if ((number % i) == 0)
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
