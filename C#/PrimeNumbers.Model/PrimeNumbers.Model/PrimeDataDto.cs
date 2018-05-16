using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumbers.Model
{
    public class PrimeDataDto
    {
        public PrimeDataDto(ulong startNumber, ulong endNumber, BitArray primeArray)
        {
            this.StartNumber = startNumber;
            this.EndNumber = endNumber;
            this.PrimeArray = primeArray;
        }

        public ulong StartNumber;
        public ulong EndNumber;
        public BitArray PrimeArray;

        public IEnumerable<ulong> PrimeNumbers
        {
            get
            {
                List<ulong> results = new List<ulong>();

                for (int i = 0; i < this.PrimeArray.Length; i++)
                {
                    if (this.PrimeArray[i])
                    {
                        ulong resultValue = this.StartNumber + (uint)i;
                        yield return resultValue;
                    }
                }
            }
        }

    }
}
