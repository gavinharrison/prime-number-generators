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

    public class FindPrimesBetweenV5 : IFindPrimesBetween
    {
        #region Properties

        private ulong StartNumber { get; set; }
        private ulong EndNumber { get; set; }
        private BitArray PrimeArray { get; set; }

        public ICollection<ulong> PrimeNumbers {
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

        public PrimeDataDto Data {
            get
            {
                return new PrimeDataDto(this.StartNumber, this.EndNumber, this.PrimeArray);
            }
        }

        #endregion

        #region Methods

        public void Execute(ulong startNumber, ulong endNumber, IIsPrime primeChecker)
        {
            if(startNumber > endNumber)
            {
                throw new ArgumentException($"Start number is greater than the end number");
            }

            if (endNumber + 1 < 0)
            {
                throw new ArgumentException($"Number has be be less than {ulong.MaxValue.ToString("#,##0")}", "number");
            }

            if ((endNumber - startNumber) >= (ulong)int.MaxValue)
            {
                throw new ArgumentException($"The range between the start and end numbers have to be less than ${int.MaxValue - 1}");
            }

            this.StartNumber = startNumber;
            this.EndNumber = endNumber;

            this.PrimeArray = new BitArray((int)(endNumber - startNumber) + 1, true);

            decimal rangeCalculation = endNumber - startNumber;
            if(rangeCalculation >= int.MaxValue)
            {
                //throw new ArgumentOutOfRangeException()
            }
                

            if (startNumber == 0)
            {
                this.PrimeArray[0] = false; // Number 0 = Non-Prime
                this.PrimeArray[1] = false; // Number 1 = Non-Prime
            }

            if (startNumber == 1)
            {
                this.PrimeArray[0] = false; // Number 1 = Non-Prime
            }

            ulong maxTestNumber = (ulong)Math.Sqrt((ulong)endNumber) + 1;
            for (ulong i = 2; i <= maxTestNumber; i++)
            {
                if ((primeChecker.CheckIsPrime((ulong)i)))
                {
                    for (ulong j = (i * 2); j <= endNumber && j > 0; j = j + i)
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
    }
}
