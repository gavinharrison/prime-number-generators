namespace PrimeNumbers.Model.IsPrime
{
    #region Usings

    using System;
    using PrimeNumbers.Model.Interfaces;

    #endregion

    /// <summary>
    /// Default Prime number search the slowest as it searches all possible combinations.
    /// </summary>
    public class IsPrimeV1 : IIsPrime
    {
        #region Properties

        /// <summary>
        /// Returns the version of the class
        /// </summary>
        public Version Version
        {
            get
            {
                return new Version(1, 0);
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
            if(number <= 0)
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
                for (ulong i = 2; i < number; i++)
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
