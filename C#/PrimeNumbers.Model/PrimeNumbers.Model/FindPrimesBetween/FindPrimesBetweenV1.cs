using PrimeNumbers.Model.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumbers.Model.FindPrimesBetween
{
    public class FindPrimesBetweenV1 : IFindPrimesBetween
    {
        #region properties

        private ulong StartNumber { get; set; }
        private ulong EndNumber { get; set; }
        private BitArray PrimeArray { get; set; }

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

        #endregion

        #region Methods

        public void Execute(ulong startNumber, ulong endNumber, IIsPrime primeChecker)
        {

            this.PrimeArray = new BitArray((int)(endNumber - startNumber) + 1);

            this.StartNumber = startNumber;
            this.EndNumber = endNumber;

            ulong loopNumber = startNumber;
            if(startNumber == 0)
            {
                loopNumber = 1;
            }

            for (ulong i = loopNumber; i <= endNumber; i++)
            {
                if (primeChecker.CheckIsPrime(i))
                {
                    this.PrimeArray[(int)(i - startNumber)] = true;
                }
            }

        }

        #endregion
    }
}
