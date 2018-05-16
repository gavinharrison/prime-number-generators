namespace PrimeNumbers.Model.FindPrimesTo
{
    using System;
    #region usings

    using System.Collections.Generic;
    using PrimeNumbers.Model.Interfaces;
    using System.Collections;

    #endregion

    public class FindPrimesToV3 : IFindPrimesTo
    {
        private int StartNumber = 0;
        private int EndNumber = 0;
        private BitArray PrimeArray = null;

        public PrimeDataDto Data
        {
            get
            {
                return new PrimeDataDto((ulong)this.StartNumber, (ulong)this.EndNumber, this.PrimeArray);
            }
        }

        ICollection<int> IFindPrimesTo.PrimeNumbers
        {
            get
            {
                List<int> results = new List<int>();

                for (int i = 0; i < this.PrimeArray.Count; i++)
                {
                    if (this.PrimeArray[i])
                    {
                        results.Add(i + 1);
                    }
                }

                return results;
            }
        }

        public void Execute(int number, IIsPrime primeChecker)
        {
            this.EndNumber = number;

            this.PrimeArray = new BitArray(number, false);

            for (int i = number; i > 0; --i)
            {
                if (primeChecker.CheckIsPrime((uint)i))
                {
                    this.PrimeArray[i-1] = true;
                }
            }

        }

        #region Methods

        #endregion
    }
}
