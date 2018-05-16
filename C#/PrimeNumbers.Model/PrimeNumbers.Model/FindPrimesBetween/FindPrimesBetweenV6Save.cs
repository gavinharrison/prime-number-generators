namespace PrimeNumbers.Model.FindPrimesBetween
{
    #region usings

    using System.Collections.Generic;
    using PrimeNumbers.Model.Interfaces;
    using System;
    using System.Collections;
    using System.Linq;
    using System.IO;
    using PrimeNumbers.Model.FileManagement;

    #endregion

    public class FindPrimesBetweenV6Save : IFindPrimesBetween
    {
        #region Properties

        private BitArray PrimeArray { get; set; }

        #endregion

        #region Methods

        public ICollection<ulong> FindPrimesBetween(ulong startNumber, ulong endNumber, IIsPrime primeChecker)
        {
            if (endNumber + 1 < 0)
            {
                throw new ArgumentException($"Number has be be less than {ulong.MaxValue.ToString("#,##0")}", "number");
            }

            this.PrimeArray = new BitArray((int)(endNumber - startNumber) + 1, true);

            if (startNumber == 0)
            {
                this.PrimeArray[0] = false; // Number 0 = Non-Prime
                this.PrimeArray[1] = false; // Number 1 = Non-Prime
            }

            if (startNumber == 1)
            {
                this.PrimeArray[0] = false; // Number 1 = Non-Prime
            }

            ulong maxTestNumber = (ulong)Math.Sqrt(endNumber) + 1;
            for (ulong i = 2; i <= maxTestNumber; i++)
            {
                if ((primeChecker.CheckIsPrime((ulong)i)))
                {
                    ulong diffrence = startNumber % i;
                    ulong loopStartNumber = (i > startNumber ? (i * 2) : (startNumber - diffrence));

                    for (ulong j = loopStartNumber; j <= endNumber && j > 0; j = j + i)
                    {
                        if (j >= startNumber)
                        {
                            int arrayValue = (int)(j - startNumber);
                            this.PrimeArray[arrayValue] = false;
                        }
                    }
                }
            }

            new PrimeDataV2().SavePrimes(string.Format(@"E:\Primes\{0}_V1_{1}-{2}_{3}.txt",
                                                       this.GetType().Name,
                                                       startNumber,
                                                       endNumber,
                                                       DateTime.Now.ToString("yyyy-MM-dd'T'HH-mm-ss-fffffff")),
                                         this.PrimeArray);

            new PrimeDataV3().SavePrimes(string.Format(@"E:\Primes\{0}_V2_{1}-{2}_{3}.txt",
                                                       this.GetType().Name,
                                                       startNumber,
                                                       endNumber,
                                                       DateTime.Now.ToString("yyyy-MM-dd'T'HH-mm-ss-fffffff")),
                                         this.PrimeArray);

            List<ulong> results = new List<ulong>();

            for(int i = 0; i < this.PrimeArray.Length; i++)
            {
                if(this.PrimeArray[i])
                {
                    ulong resultValue = startNumber + (uint)i;
                    results.Add(resultValue);
                }
            }

            return results;
        }

        #endregion
    }
}
