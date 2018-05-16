namespace PrimeNumbers.Model.FindPrimesTo
{
    #region Usings

    using Interfaces;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    public class FindPrimesToV4 : IFindPrimesTo
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
            if (number + 1 < 0)
            {
                throw new ArgumentException($"Number has be be less than {int.MaxValue.ToString("#,##0")}", "number");
            }
            this.EndNumber = number;

            bool[] primeArray = new bool[number + 1];
            primeArray.Populate(true);

            System.Diagnostics.Debug.WriteLine($"Total Memory Used : {GC.GetTotalMemory(false).ToString("#,##0")}");

            primeArray[0] = false;
            primeArray[1] = false;

            int maxTestNumber = (int)Math.Sqrt(number) + 1;
            for (int i = 2; i <= maxTestNumber; i++)
            {
                if ((primeArray[i]) &&
                   (primeChecker.CheckIsPrime((uint)i)))
                {
                    for (int j = (i * 2); j <= number && j > 0; j = j + i)
                    {
                        primeArray[j] = false;
                    }
                }
            }

            //List<int> results = new List<int>();
            //results = primeArray.Select((item, index) => new { item, index })
            //      .Where(o => o.item)
            //      .Select(o => o.index)
            //      .ToList();

            this.PrimeArray = new BitArray(primeArray);

        }

        #region Methods


        #endregion

    }
}
