namespace PrimeNumbers.Model.FindPrimesTo
{
    using System;
    #region usings

    using System.Collections;
    using System.Collections.Generic;
    using PrimeNumbers.Model.Interfaces;
    using System.Collections;

    #endregion

    public class FindPrimesToV1 : IFindPrimesTo
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

                for(int i = 0; i < this.PrimeArray.Count; i++)
                {
                    if(this.PrimeArray[i])
                    {
                        results.Add(i);
                    }
                }

                return results;
            }
        }

        #region Methods

        public void Execute(int number, IIsPrime primeChecker)
        {
            BitArray results = new BitArray(number + 1, true);
            this.EndNumber = number;

            results[0] = false;
            results[1] = false;

            for(int i = 2; i <= number; i++)
            {
                if(!primeChecker.CheckIsPrime((uint)i))
                {
                    results[i] = false;
                }
            }

            this.PrimeArray = results;
        }

        #endregion
    }
}
