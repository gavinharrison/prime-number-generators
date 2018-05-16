using PrimeNumbers.Model.Interfaces;
using System;
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace PrimeNumbers.Model.FileManagement
{
    /// <summary>
    /// Stores the prime number as a string e.g. "1,234,567,890".
    /// </summary>
    public class PrimeDataV1 : ISavePrimes, IReadPrimes, IVersion
    {
        public PrimeDataDto ReadPrimes(string filePath)
        {
            throw new NotImplementedException();
        }

        public Version Version
        {
            get
            {
                return new Version(1, 0);
            }
        }

        #region Methods

        #region Read

        public PrimeDataDto ReadPrimes(Stream dataLocation)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Save

        public void SavePrimes(string filePath, PrimeDataDto primeData)
        {
            //for (int i = 0; i < data.Length; i++)
            using (StreamWriter file = new StreamWriter(filePath))
            {
                file.WriteLine($"// {this.GetType().Name}");
                file.WriteLine($"// Start Number: {primeData.StartNumber}");
                file.WriteLine($"// End Number: {primeData.EndNumber}");
                file.WriteLine($"// Total Items : {primeData.PrimeArray.Length}");
                file.WriteLine($"// Prime Count: {Utilities.CountBitArray(primeData.PrimeArray)}");

                for (int i = 0; i < primeData.PrimeArray.Length; i++)
                {
                    if (primeData.PrimeArray[i])
                    {
                        file.WriteLine((primeData.StartNumber + (uint)i + 1).ToString("#,##0"));
                        //if (data[i])
                        //{
                        //    ulong number = startNumber + (ulong)i;
                        //    string value = number.ToString() + Environment.NewLine;

                        //    foreach (byte b in Encoding.ASCII.GetBytes(value))
                        //    {
                        //        savestream.WriteByte(b);
                        //    }
                    }
                }
            }

        }

        //public void SavePrimes(ulong startNumber, ulong endNumber, BitArray data, string filePath)
        //{
        //    this.SavePrimes(startNumber, endNumber, data, new FileStream(filePath, FileMode.CreateNew, FileAccess.Write));
        //}

        #endregion

        #endregion
    }
}
