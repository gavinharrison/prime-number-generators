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
    public class FindPrimesToGenerator : GeneratorBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="endNumber"></param>
        /// <param name="primeChecker"></param>
        /// <param name="primeFinder"></param>
        /// <param name="primeFileManagement"></param>
        public FindPrimesToGenerator(int endNumber, IIsPrime primeChecker, IFindPrimesTo primeFinder, IFileManager primeFileManagement)
            : base(0, (ulong)endNumber)
        {
            this.PrimeChecker = primeChecker;
            this.PrimeFinder = primeFinder;
            this.PrimeFileManagement = primeFileManagement;
        }

        /// <summary>
        /// 
        /// </summary>
        protected IFindPrimesTo PrimeFinder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected IFileManager PrimeFileManagement { get; set; }

        /// <summary>
        /// Starts the prime number generation process.
        /// </summary>
        public override void Generate()
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();

            this.PrimeFinder.Execute((int)this.EndNumber, this.PrimeChecker);
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
                if (this.PrimeArray[i])
                {
                    ulong numberToCheck = this.StartNumber + (ulong)i;
                    if (!this.PrimeChecker.CheckIsPrime(numberToCheck))
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
