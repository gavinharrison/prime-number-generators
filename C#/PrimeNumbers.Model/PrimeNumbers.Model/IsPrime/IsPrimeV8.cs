namespace PrimeNumbers.Model.IsPrime
{
    /// <summary>
    /// Same as V6 but the caculations are done in reverse.
    #region Usings

    using System;
    using PrimeNumbers.Model.Interfaces;

    #endregion

    /// <summary>
    /// Similar to 6 but searches in reverse
    /// </summary>
    public class IsPrimeV8 : IIsPrime
    {
        #region Properties

        /// <summary>
        /// Returns the version of the class
        /// </summary>
        public Version Version
        {
            get
            {
                return new Version(8, 0);
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
                    if(maxTestNumber % 2 == 0)
                    {
                        maxTestNumber = maxTestNumber - 1;
                    }

                    for (ulong i = (ulong)maxTestNumber; i > 2; i = i - 2)
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
