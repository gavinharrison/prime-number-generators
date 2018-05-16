namespace PrimeNumbers.Model.FindPrimesTo
{
    #region usings

    using System.Collections.Generic;
    using PrimeNumbers.Model.Interfaces;
    using System;
    using System.Collections;
    using System.Linq;
    using System.IO;
    using FileManagement;

    #endregion

    public class FindPrimesToV5Save : IFindPrimesTo
    {
        #region Properties

        private BitArray PrimeArray { get; set; }

        #endregion

        #region Methods

        public ICollection<int> FindPrimesTo(int number, IIsPrime primeChecker)
        {
            if (number + 1 < 0)
            {
                throw new ArgumentException($"Number has be be less than {int.MaxValue.ToString("#,##0")}", "number");
            }

            this.PrimeArray = new BitArray(number + 1, true);

            List<int> results = new List<int>();

            this.PrimeArray[0] = false;
            this.PrimeArray[1] = false;

            int maxTestNumber = (int)Math.Sqrt(number) + 1;
            for (int i = 2; i <= maxTestNumber; i++)
            {
                if ((this.PrimeArray[i]) &&
                   (primeChecker.CheckIsPrime((uint)i)))
                {
                    for (int j = (i * 2); j <= number && j > 0; j = j + i)
                    {
                        this.PrimeArray[j] = false;
                    }
                }
            }


            new PrimeDataV2().SavePrimes(string.Format("{0}-V1_{1}_{2}.txt",
                                                       this.GetType().Name,
                                                       number,
                                                       DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")),
                                         this.PrimeArray);

            new PrimeDataV3().SavePrimes(string.Format("{0}-V2_{1}_{2}.txt",
                                                       this.GetType().Name,
                                                       number,
                                                       DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")),
                                         this.PrimeArray);

            for (int i = 0; i <= number; i++)
            {
                if (this.PrimeArray[i])
                {
                    results.Add(i);
                }
            }

            return results;
        }
        
        #endregion
    }
}
