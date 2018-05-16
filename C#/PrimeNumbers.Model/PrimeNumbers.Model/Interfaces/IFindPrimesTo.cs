namespace PrimeNumbers.Model.Interfaces
{
    #region Usings

    using System.Collections.Generic;

    #endregion

    public interface IFindPrimesTo
    {
        PrimeDataDto Data { get; }

        ICollection<int> PrimeNumbers { get; }

        /// <summary>
        /// Finds prime numbers from 2 to the given number.
        /// </summary>
        /// <param name="number">The maximum number to find primes up to.</param>
        /// <param name="primeChecker">The function to use to validate numbers as primes.</param>
        void Execute(int number, IIsPrime primeChecker);

    }
}
