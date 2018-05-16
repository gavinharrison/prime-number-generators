using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeNumbers.Model.Interfaces;

namespace PrimeNumbers.Model.IsPrime
{
    public class IsPrimeV10 : IIsPrime
    {
        public bool CheckIsPrime(ulong number)
        {
            if (number <= 0)
            {
                throw new ArgumentOutOfRangeException("number", "The provided number needs to be greater than Zero");
            }

            bool result = true;

            if (number < 2)
            {
                result = false;
            }
            else if (number != 2)
            {
                if (number % 2 == 0)
                {
                    result = false;
                }
                else
                {
                    double maxTestNumber = Math.Sqrt((double)number) + 1;
                    if(maxTestNumber % 2 == 0)
                    {
                        maxTestNumber = maxTestNumber - 1;
                    }

                    for (ulong i = (ulong)maxTestNumber; i > 2; i = i - 2)
                    {
                        if ((number % i) == 0)
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }

            return result;
        }
    }
}
