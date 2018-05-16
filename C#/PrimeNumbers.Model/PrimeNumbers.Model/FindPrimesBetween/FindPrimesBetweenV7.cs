namespace PrimeNumbers.Model.FindPrimesBetween
{
    #region usings

    using System.Collections.Generic;
    using PrimeNumbers.Model.Interfaces;
    using System;
    using System.Collections;
    using System.Linq;
    using System.IO;
    using System.Threading.Tasks;
    using System.Diagnostics;

    #endregion

    /// <summary>
    ///  Parallel Version of V6
    /// </summary>
    public class FindPrimesBetweenV7 : IFindPrimesBetween
    {
        private ulong StartNumber { get; set; }
        private ulong EndNumber { get; set; }
        private IIsPrime PrimeChecker { get; set; }

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

            this.StartNumber = startNumber;
            this.EndNumber = endNumber;
            this.PrimeChecker = primeChecker;

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

            ulong maxTestNumber = (ulong)Math.Sqrt(endNumber) + 1;

            Console.WriteLine($"Testing to {maxTestNumber.ToString("#,##0")}");
            bool useParallel = false;
            if (useParallel)
            {
                this.ExecuteParallel();
            }
            else
            {
                Stopwatch sw = new Stopwatch();
                
                for (ulong i = 2; i <= maxTestNumber; i++)
                {
                    sw.Restart();
                    Debug.WriteIf(i < 1000,$"{i} took ");
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
                    Debug.WriteIf(i < 100, $"{Utilities.ElaspedTimeFormatter(sw.Elapsed)}");
                    Debug.WriteLineIf(i < 100, "");
                }
            }

        }

        #endregion

        #region Methods
        
        private void ExecuteParallel()
        {
            ulong maxTestNumber = (ulong)Math.Sqrt(this.EndNumber) + 1;
            int parallelNumber = (maxTestNumber <= int.MaxValue - 1 ? (int)maxTestNumber : int.MaxValue);

            Parallel.For(2, parallelNumber, new ParallelOptions() { MaxDegreeOfParallelism = 10 }, x =>
            {
                ulong i = (ulong)x;
                Debug.WriteLine(i.ToString("#,##0"));
                if ((this.PrimeChecker.CheckIsPrime((ulong)i)))
                {
                    ulong diffrence = this.StartNumber % (uint)i;
                    ulong loopStartNumber = (i > this.StartNumber ? (i * 2) : (this.StartNumber - diffrence));

                    for (ulong j = loopStartNumber; j <= this.EndNumber && j > 0; j = j + i)
                    {
                        if (j >= this.StartNumber)
                        {
                            int arrayValue = (int)(j - this.StartNumber);
                            this.PrimeArray[arrayValue] = false;
                        }
                    }
                }
            });

            if (maxTestNumber > int.MaxValue - 1)
            {
                for (ulong i = int.MaxValue - 1; i <= maxTestNumber; i++)
                {
                    if ((this.PrimeChecker.CheckIsPrime((ulong)i)))
                    {
                        ulong diffrence = this.StartNumber % i;
                        ulong loopStartNumber = (i > this.StartNumber ? (i * 2) : (this.StartNumber - diffrence));

                        for (ulong j = loopStartNumber; j <= this.EndNumber && j > 0; j = j + i)
                        {
                            if (j >= this.StartNumber)
                            {
                                int arrayValue = (int)(j - this.StartNumber);
                                this.PrimeArray[arrayValue] = false;
                            }
                        }
                    }
                }
            }

        }

        #endregion
    }
}
