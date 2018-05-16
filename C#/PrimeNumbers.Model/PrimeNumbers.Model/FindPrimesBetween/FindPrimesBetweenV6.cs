namespace PrimeNumbers.Model.FindPrimesBetween
{
    #region usings

    using System.Collections.Generic;
    using PrimeNumbers.Model.Interfaces;
    using System;
    using System.Collections;
    using System.Linq;
    using System.IO;

    #endregion

    public class FindPrimesBetweenV6 : IFindPrimesBetween
    {
        private ulong StartNumber { get; set; }
        private ulong EndNumber { get; set; }

        public PrimeDataDto Data
        {
            get
            {
                return new PrimeDataDto((ulong)this.StartNumber, (ulong)this.EndNumber, this.PrimeArray);
            }
        }

        public ICollection<ulong> PrimeNumbers
        {
            get
            {
                List<ulong> results = new List<ulong>();

                for (int i = 0; i < this.PrimeArray.Length; i++)
                {
                    if (this.PrimeArray[i])
                    {
                        ulong resultValue = this.StartNumber + (uint)i;
                        results.Add(resultValue);
                    }
                }

                return results;

            }
        }
        #region Properties

        private BitArray PrimeArray { get; set; }

        public void Execute(ulong startNumber, ulong endNumber, IIsPrime primeChecker)
        {
            if (endNumber + 1 <= 0)
            {
                throw new ArgumentException($"Number has be be less than {ulong.MaxValue.ToString("#,##0")}", "number");
            }

            this.PrimeArray = new BitArray((int)(endNumber - startNumber) + 1, true);
            this.StartNumber = startNumber;
            this.EndNumber = endNumber;
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

            ulong maxTestNumber = (ulong)Math.Sqrt((ulong)endNumber);
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

        }

        #endregion

        #region Methods

        public ICollection<ulong> FindPrimesBetween(ulong startNumber, ulong endNumber, IIsPrime primeChecker)
        {
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
