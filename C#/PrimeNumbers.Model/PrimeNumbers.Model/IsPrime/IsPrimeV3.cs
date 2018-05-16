namespace PrimeNumbers.Model.IsPrime
{
    #region Usings

    using System;
    using PrimeNumbers.Model.Interfaces;

    #endregion

    /// <summary>
    /// Similar to V1 but only tests to half of the provided number.
    /// </summary>
    public class IsPrimeV3 : IIsPrime
    {
        #region Properties

        /// <summary>
        /// Returns the version of the class
        /// </summary>
        public Version Version
        {
            get
            {
                return new Version(3, 0);
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
            else
            {

                ulong maxTestNumber = number / 2;
                for (ulong i = 2; i <= maxTestNumber; i++)
                {
                    if ((number % i) == 0)
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
