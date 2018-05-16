﻿namespace PrimeNumbers.App
{
    #region usings

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using PrimeNumbers.Model.Interfaces;
    using PrimeNumbers.Model.IsPrime;
    using PrimeNumbers.Model.FindPrimesTo;
    using PrimeNumbers.Model.FindPrimesBetween;
    using System.Threading.Tasks;
    using System.Reflection;
    using PrimeNumbers.Model;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    class PerformanceTests
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            // Largest Prime Number Less than int.MaxValue.
            PerformanceTests.TestIsPrimePerformance(PerformanceTests.FindLargestPrimeLessThan(int.MaxValue));

            // BigPrimes.Net Largest Prime Number.
            PerformanceTests.TestIsPrimePerformance(32416190071, new int[] { 2, 3, 4, 5, 6, 7, 8 });

            // Largest ulong prime number.
            PerformanceTests.TestIsPrimePerformance(PerformanceTests.FindLargestPrimeLessThan(ulong.MaxValue), new int[] { 5, 6, 7, 8 });

            // Performance of To
            //Program.TestFindPrimesToPerformance(int.MaxValue - 57, new IsPrimeV1());
            PerformanceTests.TestFindPrimesToPerformance(int.MaxValue - 57, new IsPrimeV7());

            // Performance of Between
            //Program.TestFindPrimesBetweenPerformance(0, int.MaxValue - 57, new IsPrimeV1());
            PerformanceTests.TestFindPrimesBetweenPerformance(0, int.MaxValue - 57, new IsPrimeV7());

            // Performance of Between
            //Program.TestFindPrimesBetweenPerformance(ulong.MaxValue - int.MaxValue, ulong.MaxValue - 57, new IsPrimeV1());
            PerformanceTests.TestFindPrimesBetweenPerformance(ulong.MaxValue - int.MaxValue, ulong.MaxValue - 57, new IsPrimeV7());

            //Program.TestFindPrimesBetweenPerformance(ulong.MaxValue - (500000 + 57), ulong.MaxValue - 57, new IsPrimeV1());
            PerformanceTests.TestFindPrimesBetweenPerformance(ulong.MaxValue - (500000 + 57), ulong.MaxValue - 57, new IsPrimeV7());

            Console.WriteLine();
            Console.WriteLine("Application Performance Test Completed!");
            Console.ReadLine();
        }

        /// <summary>
        /// Displays the stopwatches max resolution precision.
        /// </summary>
        protected static void DisplayTimerProperties()
        {
            // Display the timer frequency and resolution.
            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Operations timed using the system's high-resolution performance counter.");
            }
            else
            {
                Console.WriteLine("Operations timed using the DateTime class.");
            }

            long frequency = Stopwatch.Frequency;
            Console.WriteLine("  Timer frequency in ticks per second = {0}",
                              frequency);

            long nanosecPerTick = (1000L * 1000L * 1000L) / frequency;
            Console.WriteLine("  Timer is accurate within {0} nanoseconds",
                              nanosecPerTick);
        }

        /// <summary>
        /// Displays to the console the maximum number for each numeric data type.
        /// </summary>
        protected static void NumericalMaxSizings()
        {
            Console.WriteLine("List of numeric types max values");
            Console.WriteLine($"   sbyte : {PerformanceTests.DisplayPadding(sbyte.MaxValue.ToString("#,##0"))}");
            Console.WriteLine($"    byte : {PerformanceTests.DisplayPadding(byte.MaxValue.ToString("#,##0"))}");
            Console.WriteLine($"   short : {PerformanceTests.DisplayPadding(short.MaxValue.ToString("#,##0"))}");
            Console.WriteLine($"  ushort : {PerformanceTests.DisplayPadding(ushort.MaxValue.ToString("#,##0"))}");
            Console.WriteLine($"     int : {PerformanceTests.DisplayPadding(int.MaxValue.ToString("#,##0"))}");
            Console.WriteLine($"    uint : {PerformanceTests.DisplayPadding(uint.MaxValue.ToString("#,##0"))}");
            Console.WriteLine($"    long : {PerformanceTests.DisplayPadding(long.MaxValue.ToString("#,##0"))}");
            Console.WriteLine($"   ulong : {PerformanceTests.DisplayPadding(ulong.MaxValue.ToString("#,##0"))}");
            Console.WriteLine($" decimal : {PerformanceTests.DisplayPadding(decimal.MaxValue.ToString("#,##0"))}");
        }

        protected static string DisplayPadding(string value)
        {
            int paddToLength = decimal.MaxValue.ToString("#,##0").Length - value.Length;
            string result = "";

            for (int i = 0; i < paddToLength; i++)
            {
                result = result + " ";
            }

            return result + value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        protected static void NumberPercentageUsage(decimal number)
        {
            Console.WriteLine($"Testing with Number : {number.ToString("#,##0")}");
            Console.WriteLine($"    int : {(((decimal)number / (decimal)int.MaxValue) * 100).ToString()}%");
            Console.WriteLine($"   uint : {(((decimal)number / (decimal)uint.MaxValue) * 100).ToString()}%");
            Console.WriteLine($"   long : {(((decimal)number / (decimal)long.MaxValue) * 100).ToString()}%");
            Console.WriteLine($"  ulong : {(((decimal)number / (decimal)ulong.MaxValue) * 100).ToString()}%");
            Console.WriteLine($"decimal : {(((decimal)number / (decimal)decimal.MaxValue) * 100).ToString()}%");

        }

        /// <summary>
        /// Finds the prime number less that the passed number.
        /// </summary>
        /// <param name="number">The number to start the search from.</param>
        /// <returns></returns>
        protected static ulong FindLargestPrimeLessThan(ulong number)
        {
            ulong result = 0;
            ulong loopNumber = number;
            IIsPrime isPrimeTester = new IsPrimeV7();

            do
            {
                if (isPrimeTester.CheckIsPrime(loopNumber))
                {
                    result = loopNumber;
                }

                loopNumber = loopNumber - 1;

            } while (result == 0);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyNameStartsWith"></param>
        /// <param name="typeNameStartWith"></param>
        /// <param name="allowedVersionNumbers"></param>
        /// <returns></returns>
        protected static Type[] GetTestingTypes(string assemblyNameStartsWith, string typeNameStartWith, int[] allowedVersionNumbers)
        {
            Type[] results = null;

            Assembly ass = AppDomain.CurrentDomain
                                    .GetAssemblies()
                                    .FirstOrDefault(a => a.FullName.StartsWith(assemblyNameStartsWith));

            if (ass != null)
            {
                results = ass.GetTypes()
                             .Where(t => t.IsClass &&
                                         t.Name.StartsWith(typeNameStartWith) &&
                                         !t.Name.EndsWith("Save") &&
                                         (allowedVersionNumbers != null && allowedVersionNumbers.Any() ?
                                            allowedVersionNumbers.Contains(int.Parse(t.Name.Split('V').LastOrDefault())) :
                                            true))
                             .OrderBy(t => int.Parse(t.Name.Split('V').LastOrDefault()))
                             .ToArray();


            }

            return results;
        }

        #region IsPrime Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numberToTestWith"></param>
        /// <param name="allowedVersionNumbers"></param>
        protected static void TestIsPrimePerformance(ulong numberToTestWith, int[] allowedVersionNumbers = null)
        {
            Type[] isPrimeTypes = PerformanceTests.GetTestingTypes("PrimeNumbers.Model", "IsPrimeV", allowedVersionNumbers);

            if ((isPrimeTypes != null) &&
               (isPrimeTypes.Any()))
            {
                Console.WriteLine($"IsPrime Testing with {numberToTestWith.ToString("#,##0")}");

                foreach (Type type in isPrimeTypes)
                {
                    IIsPrime isPrimeTester = (IIsPrime)Activator.CreateInstance(type);

                    PerformanceTests.IsPrimeTimer(isPrimeTester, numberToTestWith);
                }

            }
            else
            {
                Console.WriteLine("No IsPrime Classes found to test with");
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Wraps the test with a stopwatch to monitor the time taken to process the requested tester. 
        /// </summary>
        /// <param name="tester">The class to do the testing with.</param>
        /// <param name="numberToTest">The number to use with the test.</param>
        protected static void IsPrimeTimer(IIsPrime tester, ulong numberToTest)
        {
            bool result = false;
            System.Diagnostics.Debug.WriteLine($"{tester.GetType().Name}");
            Console.Write($"{DateTime.Now.ToString()} : {tester.GetType().Name} : ");
            Stopwatch sw = new Stopwatch();

            sw.Restart();
            result = tester.CheckIsPrime(numberToTest);
            sw.Stop();

            Console.Write($"Result = {result} : Time Taken : {PrimeNumbers.Model.Utilities.ElaspedTimeFormatter(sw.Elapsed)}");
            Console.WriteLine();
        }

        #endregion

        #region FindPrimesTo Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numberOfPrimesToFind"></param>
        /// <param name="isPrimeTester"></param>
        /// <param name="allowedVersionNumbers"></param>
        protected static void TestFindPrimesToPerformance(int numberOfPrimesToFind, IIsPrime isPrimeTester, int[] allowedVersionNumbers = null)
        {
            Type[] findPrimesToTypes = PerformanceTests.GetTestingTypes("PrimeNumbers.Model", "FindPrimesToV", allowedVersionNumbers);

            if ((findPrimesToTypes != null) &&
               (findPrimesToTypes.Any()))
            {
                Console.WriteLine($"FindPrimesTo Testing with {numberOfPrimesToFind.ToString("#,##0")}");

                foreach (Type type in findPrimesToTypes)
                {
                    IFindPrimesTo findPrimesToTester = (IFindPrimesTo)Activator.CreateInstance(type);

                    PerformanceTests.FindPrimesToTimer(findPrimesToTester, isPrimeTester, numberOfPrimesToFind);
                }

            }
            else
            {
                Console.WriteLine("No FindPrimesTo Classes found to test with");
            }

            Console.WriteLine();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="findPrimesToTester"></param>
        /// <param name="isPrimeTester"></param>
        /// <param name="numberOfPrimesToFind"></param>
        /// <returns></returns>
        protected static ICollection<int> FindPrimesToTimer(IFindPrimesTo findPrimesToTester, IIsPrime isPrimeTester, int numberOfPrimesToFind)
        {
            ICollection<int> result = new List<int>();

            System.Diagnostics.Debug.WriteLine($"{findPrimesToTester.GetType().Name} : {isPrimeTester.GetType().Name}");
            Stopwatch sw = new Stopwatch();

            sw.Restart();
            findPrimesToTester.Execute(numberOfPrimesToFind, isPrimeTester);
            result = findPrimesToTester.PrimeNumbers;
            sw.Stop();

            Console.WriteLine($"{DateTime.Now.ToString()} : {findPrimesToTester.GetType().Name} : {isPrimeTester.GetType().Name} : Result = {result.Count.ToString("#,##0")} : Time Taken : {PrimeNumbers.Model.Utilities.ElaspedTimeFormatter(sw.Elapsed)}");

            return result;
        }

        #endregion

        #region FindPrimesBetween Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startNumber"></param>
        /// <param name="endNumber"></param>
        /// <param name="isPrimeTester"></param>
        /// <param name="allowedVersionNumbers"></param>
        protected static void TestFindPrimesBetweenPerformance(ulong startNumber, ulong endNumber, IIsPrime isPrimeTester, int[] allowedVersionNumbers = null)
        {
            Type[] findPrimesBetweenTypes = PerformanceTests.GetTestingTypes("PrimeNumbers.Model", "FindPrimesBetweenV", allowedVersionNumbers);

            if ((findPrimesBetweenTypes != null) &&
               (findPrimesBetweenTypes.Any()))
            {
                Console.WriteLine($"FindPrimesBetween Testing with {startNumber.ToString("#,##0")} - {endNumber.ToString("#,##0")}");

                foreach (Type type in findPrimesBetweenTypes)
                {
                    IFindPrimesBetween findPrimesBetweenTester = (IFindPrimesBetween)Activator.CreateInstance(type);

                    PerformanceTests.FindPrimesBetweenTimer(findPrimesBetweenTester, isPrimeTester, startNumber, endNumber);
                }

            }
            else
            {
                Console.WriteLine("No FindPrimesBetween Classes found to test with");
            }

            Console.WriteLine();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tester"></param>
        /// <param name="trier"></param>
        /// <param name="startNumber"></param>
        /// <param name="endNumber"></param>
        /// <returns></returns>
        protected static PrimeDataDto FindPrimesBetweenTimer(IFindPrimesBetween tester, IIsPrime trier, ulong startNumber, ulong endNumber)
        {
            PrimeDataDto results = null;

            System.Diagnostics.Debug.WriteLine($"{tester.GetType().Name} : {trier.GetType().Name} : {startNumber.ToString("#,##0")} - {endNumber.ToString("#,##0")}");
            Console.Write($"{DateTime.Now.ToString()} : {tester.GetType().Name} : {trier.GetType().Name} : {startNumber.ToString("#,##0")} - {endNumber.ToString("#,##0")} : ");
            Stopwatch sw = new Stopwatch();

            sw.Restart();
            tester.Execute(startNumber, endNumber, trier);
            sw.Stop();

            results = tester.Data;

            //Console.WriteLine($"{DateTime.Now.ToString()} {Environment.NewLine}{tester.GetType().Name} : {trier.GetType().Name}{Environment.NewLine}{startNumber.ToString("#,##0")} - {endNumber.ToString("#,##0")}{Environment.NewLine} Time Taken : {PrimeNumbers.Model.Utilities.ElaspedTimeFormatter(sw.Elapsed)}{Environment.NewLine}Result : {result.Count.ToString("#,##0")}{Environment.NewLine}");
            Console.Write($"Result = {results.PrimeNumbers.Count().ToString("#,##0")} : Time Taken : {PrimeNumbers.Model.Utilities.ElaspedTimeFormatter(sw.Elapsed)}");
            Console.WriteLine();

            return results;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="allowedVersionNumbers"></param>
        protected static void TestSavePerformance(PrimeDataDto data, int[] allowedVersionNumbers = null)
        {
            Type[] findPrimesBetweenTypes = PerformanceTests.GetTestingTypes("PrimeNumbers.Model", "PrimeDataV", allowedVersionNumbers);

            if ((findPrimesBetweenTypes != null) &&
               (findPrimesBetweenTypes.Any()))
            {
                Console.WriteLine($"SavingPrimes Testing with {data.StartNumber.ToString("#,##0")} - {data.EndNumber.ToString("#,##0")}");

                foreach (Type type in findPrimesBetweenTypes)
                {
                    ISavePrimes savePrimesTester = (ISavePrimes)Activator.CreateInstance(type);

                    PerformanceTests.SavePrimesTimer(savePrimesTester, data);
                }

            }
            else
            {
                Console.WriteLine("No FindPrimesBetween Classes found to test with");
            }

            Console.WriteLine();
        }

        protected static void SavePrimesTimer(ISavePrimes tester, PrimeDataDto data)
        {
            Console.Write($"{DateTime.Now.ToString()} : {tester.GetType().Name} : ");
            string fileName = string.Format("../saves/{0}_{1}_{2}-{3}.txt",
                                             DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"),
                                             tester.GetType().Name,
                                             data.StartNumber,
                                             data.EndNumber);

            Stopwatch sw = new Stopwatch();

            sw.Restart();
            tester.SavePrimes(fileName, data);
            sw.Stop();

            System.IO.FileInfo f = new System.IO.FileInfo(fileName);

            Console.Write($"Size : {Utilities.CalculateFileSize(f.Length)} : Speed : {Utilities.CalculateTransferSpeed(f.Length, sw.Elapsed.TotalSeconds)} : Time Taken : {PrimeNumbers.Model.Utilities.ElaspedTimeFormatter(sw.Elapsed)}");
            Console.WriteLine();
        }

        #endregion

        protected static void SaveDataTest(int number)
        {
            Console.WriteLine($"Genetating data for Saving ({number})");
            FindPrimesToV6 generator = new FindPrimesToV6();

            Stopwatch sw = new Stopwatch();
            sw.Restart();
            generator.Execute(number, new IsPrimeV7());
            sw.Stop();

            PrimeDataDto results = generator.Data;
            Console.WriteLine($"Generated data in {Utilities.ElaspedTimeFormatter(sw.Elapsed)}");

            PerformanceTests.TestSavePerformance(results);
        }

        protected static void SaveDataTest(ulong startNumber, ulong endNumber)
        {
            Console.WriteLine($"Genetating data for Saving ({startNumber}, {endNumber})");

            FindPrimesBetweenV7 generator = new FindPrimesBetweenV7();

            Stopwatch sw = new Stopwatch();
            sw.Restart();
            generator.Execute(startNumber, endNumber, new IsPrimeV7());
            sw.Stop();
            PrimeDataDto results = generator.Data;

            Console.WriteLine($"Generated data in {Utilities.ElaspedTimeFormatter(sw.Elapsed)}");

            PerformanceTests.TestSavePerformance(results);

        }

        protected static void TestRun2()
        {
            //Program.NumberOfPrimesToFind(new FindPrimesToV5Save(), new IsPrimeV7(), (int.MaxValue - 57));
            //Program.NumberOfPrimesToFind(new FindPrimesToV5Save(), new IsPrimeV7(), 1000000);

            //ulong diffrenceValue = int.MaxValue - 57;
            //List<Task> tasks = new List<Task>();

            //tasks.Add(Task.Factory.StartNew(() => Program.NumberOfPrimesBetweenFind(new FindPrimesBetweenV5Save(), new IsPrimeV7(), diffrenceValue * 0, diffrenceValue * 1)));
            //tasks.Add(Task.Factory.StartNew(() => Program.NumberOfPrimesBetweenFind(new FindPrimesBetweenV5Save(), new IsPrimeV7(), diffrenceValue * 1, diffrenceValue * 2)));
            //tasks.Add(Task.Factory.StartNew(() => Program.NumberOfPrimesBetweenFind(new FindPrimesBetweenV5Save(), new IsPrimeV7(), diffrenceValue * 2, diffrenceValue * 3)));
            //tasks.Add(Task.Factory.StartNew(() => Program.NumberOfPrimesBetweenFind(new FindPrimesBetweenV5Save(), new IsPrimeV7(), diffrenceValue * 3, diffrenceValue * 4)));
            //tasks.Add(Task.Factory.StartNew(() => Program.NumberOfPrimesBetweenFind(new FindPrimesBetweenV5Save(), new IsPrimeV7(), diffrenceValue * 4, diffrenceValue * 5)));
            //tasks.Add(Task.Factory.StartNew(() => Program.NumberOfPrimesBetweenFind(new FindPrimesBetweenV5Save(), new IsPrimeV7(), diffrenceValue * 5, diffrenceValue * 6)));

            //Console.WriteLine($"{DateTime.Now.ToString()} Tasks Running");
            //Task.WaitAll(tasks.ToArray());

            //TestRun();

            //6,875,316,800 - 6,956,202,879
            //var results1 = Program.NumberOfPrimesBetweenFind(new FindPrimesBetweenV6Save(), new IsPrimeV7(), 6875316800, 6956202879);
            //var results2 = Program.NumberOfPrimesBetweenFind(new FindPrimesBetweenV5Save(), new IsPrimeV7(), 6875316800, 6956202879);

            //Console.WriteLine();
            //Console.WriteLine($"Results : {(results1.Intersect(results2).Count() == results1.Union(results2).Count() ? "Equal" : "Not Equal")}");

            ulong diffrenceValue = 80886080; // 10MB file size
            ulong maxNumber = ((long)(int.MaxValue - 57) * 90);
            ulong startNumber = 193236845120;

            int loopIterations = (int)(Math.Ceiling((decimal)(maxNumber / diffrenceValue)));
            //int loopIterations = (int)(Math.Ceiling((decimal)(32416190071 + 10) / diffrenceValue));

            PerformanceTests.NumberPercentageUsage(maxNumber);

            Console.WriteLine($"Increment : {diffrenceValue.ToString("#,##0")}");
            Console.WriteLine($"Loop count : {loopIterations.ToString("#,##0")}");
            Console.WriteLine();

            Parallel.For(0,
                         loopIterations,
                         new ParallelOptions { MaxDegreeOfParallelism = 11 },
                         index =>
                         {
                             PerformanceTests.FindPrimesBetweenTimer(new FindPrimesBetweenV6(),
                                                               new IsPrimeV7(),
                                                               (startNumber + (diffrenceValue * ((ulong)index))),
                                                               (startNumber + ((diffrenceValue * ((ulong)index + 1)) - 1)));
                         });

            //for (int index = 0; index <= loopIterations; index++)
            //{
            //    Program.NumberOfPrimesBetweenFind(new FindPrimesBetweenV6Save(),
            //                                      new IsPrimeV7(),
            //                                      (startNumber + (diffrenceValue * ((ulong)index))),
            //                                      (startNumber + (diffrenceValue * ((ulong)index + 1) - 1)));
            //}

        }

        protected static void TestRun()
        {

            //ulong numberToTest = ulong.MaxValue - 1;
            //for (ulong i = numberToTest; i > 0; i = i - 1)
            //{
            //    if (new IsPrimeV6().CheckIsPrime(i))
            //    {
            //        numberToTest = i;
            //        break;
            //    }
            //    else
            //    {
            //        Console.WriteLine($"{i.ToString("#,##0")} is not prime");
            //    }
            //}
            //ulong numberToTest = (int.MaxValue - 57);
            //ulong numberToTest = 32416190071; // bigprimes.net larges prime number.
            ulong numberToTest = 18446744073709551557;

            Console.WriteLine($"Testing with Number : {numberToTest.ToString("#,##0")}");
            Console.WriteLine($"int : {(((decimal)numberToTest / (decimal)int.MaxValue) * 100).ToString()}%");
            Console.WriteLine($"Uint : {(((decimal)numberToTest / (decimal)uint.MaxValue) * 100).ToString()}%");
            Console.WriteLine($"long : {(((decimal)numberToTest / (decimal)long.MaxValue) * 100).ToString()}%");
            Console.WriteLine($"Ulong : {(((decimal)numberToTest / (decimal)ulong.MaxValue) * 100).ToString()}%");
            Console.WriteLine();

            //Program.IsPrimeTimer(new PrimeNumbers.Model.IsPrime.IsPrimeV1(), numberToTest);
            //Program.IsPrimeTimer(new PrimeNumbers.Model.IsPrime.IsPrimeV2(), numberToTest);
            //Program.IsPrimeTimer(new PrimeNumbers.Model.IsPrime.IsPrimeV3(), numberToTest);
            //Program.IsPrimeTimer(new PrimeNumbers.Model.IsPrime.IsPrimeV4(), numberToTest);
            //Program.IsPrimeTimer(new PrimeNumbers.Model.IsPrime.IsPrimeV5(), numberToTest);
            //Program.IsPrimeTimer(new PrimeNumbers.Model.IsPrime.IsPrimeV6(), numberToTest);
            //Program.IsPrimeTimer(new PrimeNumbers.Model.IsPrime.IsPrimeV7(), numberToTest);
            //Program.IsPrimeTimer(new PrimeNumbers.Model.IsPrime.IsPrimeV8(), numberToTest);

            List<Task> tasks = new List<Task>();

            //tasks.Add(Task.Factory.StartNew(() => Program.IsPrimeTimer(new PrimeNumbers.Model.IsPrime.IsPrimeV1(), numberToTest)));
            //tasks.Add(Task.Factory.StartNew(() => Program.IsPrimeTimer(new PrimeNumbers.Model.IsPrime.IsPrimeV2(), numberToTest)));
            //tasks.Add(Task.Factory.StartNew(() => Program.IsPrimeTimer(new PrimeNumbers.Model.IsPrime.IsPrimeV3(), numberToTest)));
            //tasks.Add(Task.Factory.StartNew(() => Program.IsPrimeTimer(new PrimeNumbers.Model.IsPrime.IsPrimeV4(), numberToTest)));
            tasks.Add(Task.Factory.StartNew(() => PerformanceTests.IsPrimeTimer(new PrimeNumbers.Model.IsPrime.IsPrimeV5(), numberToTest)));
            tasks.Add(Task.Factory.StartNew(() => PerformanceTests.IsPrimeTimer(new PrimeNumbers.Model.IsPrime.IsPrimeV6(), numberToTest)));
            tasks.Add(Task.Factory.StartNew(() => PerformanceTests.IsPrimeTimer(new PrimeNumbers.Model.IsPrime.IsPrimeV7(), numberToTest)));
            tasks.Add(Task.Factory.StartNew(() => PerformanceTests.IsPrimeTimer(new PrimeNumbers.Model.IsPrime.IsPrimeV8(), numberToTest)));

            Console.WriteLine($"{DateTime.Now.ToString()} Tasks Running");
            Task.WaitAll(tasks.ToArray());

            //int numberofPrimesToFind = int.MaxValue - 57;
            //int numberofPrimesToFind = (int.MaxValue - 100) / 2;
            int numberofPrimesToFind = (int.MaxValue - 100) / 4;

            Console.WriteLine($"Finding {numberofPrimesToFind.ToString("#,##0")}");
            Console.WriteLine($"int : {(((decimal)numberofPrimesToFind / (decimal)int.MaxValue) * 100).ToString()}%");
            Console.WriteLine($"Uint : {(((decimal)numberofPrimesToFind / (decimal)uint.MaxValue) * 100).ToString()}%");
            Console.WriteLine($"long : {(((decimal)numberofPrimesToFind / (decimal)long.MaxValue) * 100).ToString()}%");
            Console.WriteLine($"Ulong : {(((decimal)numberofPrimesToFind / (decimal)ulong.MaxValue) * 100).ToString()}%");
            Console.WriteLine();

            //Program.NumberOfPrimesToFind(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV1(), new PrimeNumbers.Model.IsPrime.IsPrimeV1(), numberofPrimesToFind);
            //Program.NumberOfPrimesToFind(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV1(), new PrimeNumbers.Model.IsPrime.IsPrimeV2(), numberofPrimesToFind);
            //Program.NumberOfPrimesToFind(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV1(), new PrimeNumbers.Model.IsPrime.IsPrimeV3(), numberofPrimesToFind);
            //Program.NumberOfPrimesToFind(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV1(), new PrimeNumbers.Model.IsPrime.IsPrimeV4(), numberofPrimesToFind);
            //Program.NumberOfPrimesToFind(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV1(), new PrimeNumbers.Model.IsPrime.IsPrimeV5(), numberofPrimesToFind);
            //Program.NumberOfPrimesToFind(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV1(), new PrimeNumbers.Model.IsPrime.IsPrimeV6(), numberofPrimesToFind);
            //Program.NumberOfPrimesToFind(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV1(), new PrimeNumbers.Model.IsPrime.IsPrimeV7(), numberofPrimesToFind);
            //Program.NumberOfPrimesToFind(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV1(), new PrimeNumbers.Model.IsPrime.IsPrimeV8(), numberofPrimesToFind);

            tasks.Clear();

            tasks.Add(Task.Factory.StartNew(() => PerformanceTests.FindPrimesToTimer(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV1(), new PrimeNumbers.Model.IsPrime.IsPrimeV7(), numberofPrimesToFind)));
            tasks.Add(Task.Factory.StartNew(() => PerformanceTests.FindPrimesToTimer(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV2(), new PrimeNumbers.Model.IsPrime.IsPrimeV7(), numberofPrimesToFind)));
            tasks.Add(Task.Factory.StartNew(() => PerformanceTests.FindPrimesToTimer(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV3(), new PrimeNumbers.Model.IsPrime.IsPrimeV7(), numberofPrimesToFind)));
            tasks.Add(Task.Factory.StartNew(() => PerformanceTests.FindPrimesToTimer(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV4(), new PrimeNumbers.Model.IsPrime.IsPrimeV7(), numberofPrimesToFind)));
            tasks.Add(Task.Factory.StartNew(() => PerformanceTests.FindPrimesToTimer(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV5(), new PrimeNumbers.Model.IsPrime.IsPrimeV7(), numberofPrimesToFind)));

            Console.WriteLine($"{DateTime.Now.ToString()} Tasks Running");
            Task.WaitAll(tasks.ToArray());

            tasks.Clear();

            //tasks.Add(Task.Factory.StartNew(() => Program.NumberOfPrimesToFind(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV5(), new PrimeNumbers.Model.IsPrime.IsPrimeV1(), numberofPrimesToFind)));
            //tasks.Add(Task.Factory.StartNew(() => Program.NumberOfPrimesToFind(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV5(), new PrimeNumbers.Model.IsPrime.IsPrimeV2(), numberofPrimesToFind)));
            //tasks.Add(Task.Factory.StartNew(() => Program.NumberOfPrimesToFind(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV5(), new PrimeNumbers.Model.IsPrime.IsPrimeV3(), numberofPrimesToFind)));
            //tasks.Add(Task.Factory.StartNew(() => Program.NumberOfPrimesToFind(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV5(), new PrimeNumbers.Model.IsPrime.IsPrimeV4(), numberofPrimesToFind)));
            tasks.Add(Task.Factory.StartNew(() => PerformanceTests.FindPrimesToTimer(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV5(), new PrimeNumbers.Model.IsPrime.IsPrimeV5(), numberofPrimesToFind)));
            tasks.Add(Task.Factory.StartNew(() => PerformanceTests.FindPrimesToTimer(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV5(), new PrimeNumbers.Model.IsPrime.IsPrimeV6(), numberofPrimesToFind)));
            tasks.Add(Task.Factory.StartNew(() => PerformanceTests.FindPrimesToTimer(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV5(), new PrimeNumbers.Model.IsPrime.IsPrimeV7(), numberofPrimesToFind)));
            tasks.Add(Task.Factory.StartNew(() => PerformanceTests.FindPrimesToTimer(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV5(), new PrimeNumbers.Model.IsPrime.IsPrimeV8(), numberofPrimesToFind)));

            Console.WriteLine($"{DateTime.Now.ToString()} Tasks Running");
            Task.WaitAll(tasks.ToArray());

            //tasks.Clear();

            //tasks.Add(Task.Factory.StartNew(() => Program.NumberOfPrimesToFind(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV4(), new PrimeNumbers.Model.IsPrime.IsPrimeV7(), numberofPrimesToFind)));
            //tasks.Add(Task.Factory.StartNew(() => Program.NumberOfPrimesToFind(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV5(), new PrimeNumbers.Model.IsPrime.IsPrimeV7(), numberofPrimesToFind)));
            //tasks.Add(Task.Factory.StartNew(() => Program.NumberOfPrimesToFind(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV3(), new PrimeNumbers.Model.IsPrime.IsPrimeV7(), numberofPrimesToFind)));
            //tasks.Add(Task.Factory.StartNew(() => Program.NumberOfPrimesToFind(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV4(), new PrimeNumbers.Model.IsPrime.IsPrimeV7(), numberofPrimesToFind)));

            //Console.WriteLine($"{DateTime.Now.ToString()} Tasks Running");
            //Task.WaitAll(tasks.ToArray());

            //Program.NumberOfPrimesToFind(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV4(), new PrimeNumbers.Model.IsPrime.IsPrimeV7(), numberofPrimesToFind);
            //Program.NumberOfPrimesToFind(new PrimeNumbers.Model.FindPrimesTo.FindPrimesToV5(), new PrimeNumbers.Model.IsPrime.IsPrimeV7(), numberofPrimesToFind);
        }

    }
}