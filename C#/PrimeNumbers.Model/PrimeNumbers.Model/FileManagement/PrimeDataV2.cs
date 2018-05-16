using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeNumbers.Model.Interfaces;
using System.IO;
using System.Collections;
using PrimeNumbers.Model;

namespace PrimeNumbers.Model.FileManagement
{
    /// <summary>
    /// Stores the caculated primes as a string of 1's or 0's.
    /// </summary>
    public class PrimeDataV2 : ISavePrimes, IReadPrimes
    {
        public Version Version
        {
            get
            {
                return new Version(2, 0);
            }
        }

        public PrimeDataDto ReadPrimes(string filePath)
        {
            List<bool> primeList = new List<bool>();
            char value;

            using (StreamReader file = new StreamReader(filePath))
            {
                do
                {
                    //BitConverter
                    value = (char)file.Read();
                    primeList.Add(Convert.ToBoolean(value));

                } while (!file.EndOfStream);
            }

            return new PrimeDataDto(0, 0, new BitArray(primeList.ToArray()));
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

                for (int i = 0; i < primeData.PrimeArray.Length; i++)
                {
                    file.Write((primeData.PrimeArray[i] ? "1" : "0"));
                }
            }
        }

    }
}
