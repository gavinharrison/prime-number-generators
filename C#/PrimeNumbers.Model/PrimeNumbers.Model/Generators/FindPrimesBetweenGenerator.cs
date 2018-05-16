using PrimeNumbers.Model.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumbers.Model.Generators
{
    public class FindPrimesBetweenGenerator : GeneratorBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startNumber"></param>
        /// <param name="endNumber"></param>
        /// <param name="primeChecker"></param>
        /// <param name="primeFinder"></param>
        /// <param name="primeFileManagement"></param>
        public FindPrimesBetweenGenerator(ulong startNumber, ulong endNumber, IIsPrime primeChecker, IFindPrimesBetween primeFinder, ISavePrimes primeFileManagement)
            : base(startNumber, endNumber)
        {
            if(primeChecker == null)
            {
                throw new ArgumentNullException("primeChecker");
            }
            
            if(primeFinder == null)
            {
                throw new ArgumentNullException("primeFinder");
            }

            if(primeFileManagement == null)
            {
                throw new ArgumentNullException("primeFileManagement");
            }

            this.PrimeChecker = primeChecker;
            this.PrimeFinder = primeFinder;
            this.PrimeFileSaver = primeFileManagement;
        }

        /// <summary>
        /// 
        /// </summary>
        protected IFindPrimesBetween PrimeFinder { get; set; }


        /// <summary>
        /// Starts the prime number generation process.
        /// </summary>
        public override void Generate()
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();

            this.PrimeFinder.Execute(this.StartNumber, this.EndNumber, this.PrimeChecker);
            this.PrimeArray = this.PrimeFinder.Data.PrimeArray;

            sw.Stop();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool Verify()
        {
            bool primesVerified = true;

            for (int i = 0; i < this.PrimeArray.Length; i++)
            {
                if(this.PrimeArray[i])
                {
                    ulong numberToCheck = this.StartNumber + (ulong)i;
                    if(!this.PrimeChecker.CheckIsPrime(numberToCheck))
                    {
                        primesVerified = false;
                        throw new Exception($"{numberToCheck.ToString("#,##0")} is not a valid prime number");

                    }
                }
            }

            return primesVerified;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Save()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public override void Load(string filePath)
        {

        }

    }
}
