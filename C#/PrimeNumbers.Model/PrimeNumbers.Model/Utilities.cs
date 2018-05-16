namespace PrimeNumbers.Model
{
    #region usings

    using System;
    using System.Collections;
    using System.Diagnostics;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public static class Utilities
    {
        #region Elapsed Time Formatters

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        public static string CalculateFileSize(long fileSize)
        {
            decimal caculation = fileSize;

            decimal b = caculation;
            decimal kb = caculation / 1024;
            decimal mb = caculation / (1024 * 1024);
            decimal gb = caculation / (1024 * 1024 * 1024);

            string result = "";
            if ((int)gb > 0)
            {
                result = $"{gb.ToString("0.##")} GB";
            }
            else if ((int)mb > 0)
            {
                result = $"{mb.ToString("0.##")} MB";
            }
            else if ((int)kb > 0)
            {
                result = $"{kb.ToString("0.##")} KB";
            }
            else
            {
                result = $"{b.ToString("0.##")} B";
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileSize"></param>
        /// <param name="timeTakenInSeconds"></param>
        /// <returns></returns>
        public static string CalculateTransferSpeed(long fileSize, double timeTakenInSeconds)
        {
            decimal caculation = ((decimal)fileSize / (decimal)timeTakenInSeconds);

            decimal bps = caculation;
            decimal kbps = caculation / 1024;
            decimal mbps = caculation / (1024 * 1024);
            decimal gbps = caculation / (1024 * 1024 * 1024);

            string result = "";
            if ((int)gbps > 0)
            {
                result = $"{gbps.ToString("0.##")} GBps";
            }
            else if ((int)mbps > 0)
            {
                result = $"{mbps.ToString("0.##")} MBps";
            }
            else if((int) kbps > 0)
            {
                result = $"{kbps.ToString("0.##")} KBps";
            }
            else
            {
                result = $"{bps.ToString("0.##")} Bps";
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitArray"></param>
        /// <returns></returns>
        public static int CountBitArray(BitArray bitArray)
        {
            int count = 0;
            foreach (bool bit in bitArray)
            {
                if (bit)
                {
                    count++;
                }
            }
            return count;
        }

        /// Formats the passed elapsed timespan in to human readable text.
        /// </summary>
        /// <param name="timer">The stopwatch to get the elapsed time from.</param>
        /// <param name="showAll">Wither the output should show all time properties.</param>
        public static string ElaspedTimeFormatter(Stopwatch timer, bool showAll = false)
        {
            return Utilities.ElaspedTimeFormatter(timer.Elapsed, showAll);
        }

        /// <summary>
        /// Formats the passed elapsed timespan in to human readable text.
        /// </summary>
        /// <param name="elaspedTime">The time span to format.</param>
        /// <param name="showAll">Wither the output should show all time properties.</param>
        public static string ElaspedTimeFormatter(TimeSpan elaspedTime, Boolean showAll = false)
        {
            string outputFormat = string.Empty;
            string elaspedCustom = string.Empty;

            if ((elaspedTime.Days > 0) ||
                (showAll))
            {
                // Time output showing all parameters except ticks.
                outputFormat = "{0}d, {1}h, {2}m, {3}s, {4}ms";
            }
            else if (elaspedTime.TotalHours >= 1)
            {
                // Time output showing hours down to milliseconds.
                outputFormat = "{1}h {2}m {3}s {4}ms";
            }
            else if (elaspedTime.TotalMinutes >= 1)
            {
                // Time output showing minuets to milliseconds.
                outputFormat = "{2}m {3}s {4}ms";
            }
            else if (elaspedTime.TotalSeconds >= 1)
            {
                // Time output showing seconds to milliseconds.
                outputFormat = "{3}s {4}ms";
            }
            else if (elaspedTime.TotalMilliseconds >= 10)
            {
                // Time output showing only milliseconds.
                outputFormat = "{4} Milliseconds";
            }
            else if (elaspedTime.TotalMilliseconds > 1)
            {
                long converision = 10000;
                long ms = (elaspedTime.Ticks / converision);
                long ticks = elaspedTime.Ticks - (ms * converision);
                outputFormat = $"{ms}.{ticks}ms";
                //outputFormat = "{4}ms {5} ticks";

            }
            else
            {
                // Time output showing the number of ticks.
                outputFormat = "{5} Ticks";
            }

            // Returning the formatted elapsed time.
            return String.Format(outputFormat, 
                                 elaspedTime.Days, 
                                 elaspedTime.Hours.ToString("0#"), 
                                 elaspedTime.Minutes.ToString("0#"), 
                                 elaspedTime.Seconds.ToString("0#"), 
                                 elaspedTime.Milliseconds.ToString("000#"), 
                                 elaspedTime.Ticks);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T[] Populate<T>(this T[] arr, T value)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                arr[i] = value;
            }

            return arr;
        }

        // http://stackoverflow.com/questions/4124189/performing-math-operations-on-decimal-datatype-in-c
        // x - a number, from which we need to calculate the square root
        // epsilon - an accuracy of calculation of the root from our number.
        // The result of the calculations will differ from an actual value
        // of the root on less than epslion.
        public static decimal SqrtV1(decimal x, decimal epsilon = 0.0M)
        {
            if (x < 0) throw new OverflowException("Cannot calculate square root from a negative number");

            decimal current = (decimal)Math.Sqrt((double)x), previous;
            do
            {
                previous = current;
                if (previous == 0.0M) return 0;
                current = (previous + x / previous) / 2;
            }
            while (Math.Abs(previous - current) > epsilon);
            return current;
        }

        //http://stackoverflow.com/questions/4124189/performing-math-operations-on-decimal-datatype-in-c
        public static decimal SqrtV2(decimal x, decimal? guess = null)
        {
            var ourGuess = guess.GetValueOrDefault(x / 2m);
            var result = x / ourGuess;
            var average = (ourGuess + result) / 2m;

            if (average == ourGuess) // This checks for the maximum precision possible with a decimal.
                return average;
            else
                return SqrtV2(x, average);
        }


    }
}
