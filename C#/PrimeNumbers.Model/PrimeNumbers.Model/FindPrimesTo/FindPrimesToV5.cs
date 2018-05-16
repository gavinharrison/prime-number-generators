namespace PrimeNumbers.Model.FindPrimesTo
{
    #region usings

    using System.Collections.Generic;
    using PrimeNumbers.Model.Interfaces;
    using System;
    using System.Collections;
    using System.Linq;
    using System.IO;

    #endregion

    public class FindPrimesToV5 : IFindPrimesTo
    {

        private int StartNumber = 0;
        private int EndNumber = 0;

        public PrimeDataDto Data
        {
            get
            {
                return new PrimeDataDto((ulong)this.StartNumber, (ulong)this.EndNumber, this.PrimeArray);
            }
        }

        public ICollection<int> PrimeNumbers
        {
            get
            {
                List<int> results = new List<int>();

                for (int i = 0; i < this.PrimeArray.Length; i++)
                {
                    if (this.PrimeArray[i])
                    {
                        results.Add(i);
                    }
                }

                return results;
            }
        }
        #region Properties

        private BitArray PrimeArray { get; set; }


        #endregion

        #region Methods

        public void Execute(int number, IIsPrime primeChecker)
        {
            if (number +1 < 0)
            {
                throw new ArgumentException($"Number has be be less than {int.MaxValue.ToString("#,##0")}", "number");
            }

            this.EndNumber = number;
            this.PrimeArray = new BitArray(number + 1, true);

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
        }

        #endregion
    }
}
