namespace PrimeNumbers.Model.FindPrimesTo
{
    #region Usings

    using Interfaces;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    public class FindPrimesToV2 : IFindPrimesTo
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
                        results.Add(i);
                    }
                }

                return results;
            }
        }

        public void Execute(int number, IIsPrime primeChecker)
        {
            if(number+1 < 0)
            {
                throw new ArgumentException($"Number has be less than {int.MaxValue.ToString("#,##0")}", "number");
            }
            this.EndNumber = number;

            bool[] primeArray = new bool[number+1].Populate(true);
            
            primeArray[0] = false;
            primeArray[1] = false;

            for(int i = 2; i <= number; i++)
            {
                if((primeArray[i]) &&
                   (primeChecker.CheckIsPrime((uint)i)))
                {
                    //int maxTestNumber = number - (i - 1);
                    //for (int j = i * 2; j <= number; j = j + i)

                    int maxTestNumber = number - (number % i);
                    for (int j = (i + i); j <= maxTestNumber; j = j + i)
                    {
                        primeArray[j] = false;
                    }
                }
            }

            this.PrimeArray = new BitArray(primeArray);
        }

        #region Methods

        #endregion
    }
}
