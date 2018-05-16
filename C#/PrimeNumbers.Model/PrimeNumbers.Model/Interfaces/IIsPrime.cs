namespace PrimeNumbers.Model.Interfaces
{
    public interface IIsPrime : IVersion
    {
        /// <summary>
        /// Indicates if the provided number is Prime.
        /// </summary>
        /// <param name="number">The number to check.</param>
        /// <returns>A Boolean indicating if the number is prime.</returns>
        bool CheckIsPrime(ulong number);
    }
}
