using PrimeNumbers.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumbers.Model.FileManagement
{
    /// <summary>
    /// Stores the numerical diffrence between prime numbers as a string.
    /// </summary>
    public class PrimeDataV4 : ISavePrimes, IReadPrimes
    {
        public Version Version => throw new NotImplementedException();

        public PrimeDataDto ReadPrimes(string filePath)
        {
            throw new NotImplementedException();
        }

        public void SavePrimes(string filePath, PrimeDataDto primeData)
        {
            using (StreamWriter file = new StreamWriter(filePath))
            {
                file.WriteLine($"// {this.GetType().Name}");
                file.WriteLine($"// Start Number: {primeData.StartNumber}");
                file.WriteLine($"// End Number: {primeData.EndNumber}");
                file.WriteLine($"// Total Items : {primeData.PrimeArray.Length}");
                file.WriteLine($"// Prime Count: {Utilities.CountBitArray(primeData.PrimeArray)}");

                int diffrence = 0;

                for(int i = 0; i < primeData.PrimeArray.Length; i++)
                {
                    diffrence++;

                    if(primeData.PrimeArray[i])
                    {
                        file.WriteLine(diffrence.ToString("#,##0"));
                        diffrence = 0;
                    }
                }
            }
        }
    }
}
