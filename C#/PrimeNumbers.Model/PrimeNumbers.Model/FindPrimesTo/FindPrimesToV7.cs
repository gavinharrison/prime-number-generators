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

    public class FindPrimesToV7 : IFindPrimesTo
    {
        protected BitArray PrimeArray { get; set; }

        public PrimeDataDto Data => throw new NotImplementedException();

        public ICollection<int> PrimeNumbers => throw new NotImplementedException();

        #region Methods

        public void Execute(int number, IIsPrime primeChecker)
        {

            if (number + 1 < 0)
            {
                throw new ArgumentException($"Number has be be less than {int.MaxValue.ToString("#,##0")}", "number");
            }

            this.PrimeArray = new BitArray(number + 1, true);


            this.PrimeArray[0] = false;
            this.PrimeArray[1] = false;

            int maxTestNumber = (int)Math.Sqrt(number) + 1;
            for (int i = 3; i <= maxTestNumber; i = i + 2)
            {
                if ((this.PrimeArray[i]))
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
