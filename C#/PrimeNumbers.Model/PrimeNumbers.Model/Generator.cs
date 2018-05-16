using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumbers.Model
{

    #region Usings

    using Interfaces;
    using System.Diagnostics;

    #endregion

    public class Generator
    {
        #region Declarations

        #endregion

        #region Constructors / Deconstructors

        #endregion

        #region Methods

        public void FindPrimesTo(IFindPrimesTo finder, IIsPrime primeChecker, int maxNumber)
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();

            // Generating prime numbers.
            finder.Execute(maxNumber, primeChecker);
            ICollection<int> primeNumbers = new List<int>();

            sw.Stop();

            // Verifying the passed back numbers are prime.
            if((primeNumbers != null) &&
               (primeNumbers.Any()))
            {
                StringBuilder result = new StringBuilder();
                string message = string.Empty;

                sw.Restart();

                message = $"Verification started on {primeNumbers.Count.ToString("#,##0")}";
                result.AppendLine(message);

                bool nonPrimeFound = false;

                foreach(int prime in primeNumbers)
                {
                    for(int i = 0; i <= prime; i++)
                    {
                        if((prime % i) == 0)
                        {
                            break;
                        }
                    }

                    if(nonPrimeFound)
                    {
                        message = $"{prime.ToString("#,##0")} was found to be a non prime number during the verification process.";
                        result.AppendLine(message);
                        //Log(message);
                        //throw new Exception();
                        break;
                    }
                }

                sw.Stop();

                message = $"Verification {(nonPrimeFound ? "Failed" : "Passed")} taking {Utilities.ElaspedTimeFormatter(sw)}";
                result.AppendLine(message);
                //Log(message);
            }
            
        }

        public void FindNthPrimes(int numberOfPrimes)
        {

        }

        #endregion

    }
}
