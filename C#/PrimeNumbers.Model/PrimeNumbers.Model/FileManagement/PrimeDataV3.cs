using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeNumbers.Model.Interfaces;
using System.IO;
using System.Collections;

namespace PrimeNumbers.Model.FileManagement
{
    /// <summary>
    /// Stores the data as binary 1's or 0's eight times smaller than version 2.
    /// </summary>
    public class PrimeDataV3 : ISavePrimes, IReadPrimes
    {
        public Version Version
        {
            get
            {
                return new Version(3, 0);
            }
        }

        public PrimeDataDto ReadPrimes(string filePath)
        {
            throw new NotImplementedException();
        }

        public void SavePrimes(string filePath, PrimeDataDto primeData)
        {
            using (StreamWriter file = new StreamWriter(filePath))
            {
                int loopCount = 0;
                bool partial = false;

                if (primeData.PrimeArray.Count % 8 == 0)
                {
                    loopCount = primeData.PrimeArray.Count / 8;
                }
                else
                {
                    loopCount = (primeData.PrimeArray.Count - (primeData.PrimeArray.Count % 8)) / 8;
                    partial = true;
                }

                file.WriteLine($"// {this.GetType().Name}");
                file.WriteLine($"// Start Number: {primeData.StartNumber}");
                file.WriteLine($"// End Number: {primeData.EndNumber}");
                file.WriteLine($"// Total Items : {primeData.PrimeArray.Length}");
                file.WriteLine($"// Prime Count: {Utilities.CountBitArray(primeData.PrimeArray)}");

                for(int i = 0; i < loopCount; i++)
                {
                    int index = i * 8;

                    byte data = 0;

                    if (primeData.PrimeArray[index]) data += 1;
                    if (primeData.PrimeArray[index + 1]) data += 2;
                    if (primeData.PrimeArray[index + 2]) data += 4;
                    if (primeData.PrimeArray[index + 3]) data += 8;
                    if (primeData.PrimeArray[index + 4]) data += 16;
                    if (primeData.PrimeArray[index + 5]) data += 32;
                    if (primeData.PrimeArray[index + 6]) data += 64;
                    if (primeData.PrimeArray[index + 7]) data += 128;

                    file.Write(data);
                }
                
                if(partial)
                {
                    file.WriteLine();
                    int index = loopCount * 8;

                    for (int i = index; i < primeData.PrimeArray.Count; i++)
                    {
                        file.Write((primeData.PrimeArray[i] ? "1" : "0"));
                    }
                }
            }

        }

        public void SavePrimes(ulong startNumber, ulong endNumber, BitArray data, Stream savestream)
        {
            throw new NotImplementedException();
        }

        public void SavePrimes(ulong startNumber, ulong endNumber, BitArray data, string filePath)
        {
            throw new NotImplementedException();
        }

    }
}
