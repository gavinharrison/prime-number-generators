using PrimeNumbers.Model.Interfaces;
using System;
using System.Collections;
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
    public class PrimeDataV5 : ISavePrimes, IReadPrimes
    {
        public Version Version => throw new NotImplementedException();

        public PrimeDataDto ReadPrimes(string filePath)
        {
            throw new NotImplementedException();

            BitArray primeArray = new BitArray(101);

            List<int> diffrences = new List<int>();

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            using (BinaryReader br = new BinaryReader(fs))
            {
                long pos = 0;
                long length = br.BaseStream.Length;
                while(pos < length)
                {
                    int v = br.ReadInt32();
                    diffrences.Add(v);
                }
            }

            foreach(int i in diffrences)
            {
                Console.WriteLine(i);
            }

        }

        public void SavePrimes(string filePath, PrimeDataDto primeData)
        {
            throw new NotImplementedException();

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            using (StreamWriter file = new StreamWriter(fs))
            {
                file.WriteLine($"// {this.GetType().Name}");
                file.WriteLine($"// Start Number: {primeData.StartNumber}");
                file.WriteLine($"// End Number: {primeData.EndNumber}");
                file.WriteLine($"// Total Items : {primeData.PrimeArray.Length}");
                file.WriteLine($"// Prime Count: {Utilities.CountBitArray(primeData.PrimeArray)}");

                //int diffrence = 0;

                //for (int i = 0; i < primeData.PrimeArray.Length; i++)
                //{
                //    diffrence++;

                //    if (primeData.PrimeArray[i])
                //    {
                //        bw.Write(diffrence);
                //        diffrence = 0;
                //    }
                //}
            }
        }


    }
}
