namespace PrimeNumbers.Model.Generators
{
    #region Usings

    using PrimeNumbers.Model.Interfaces;
    using System.Collections;
    using System.Collections.Generic;
    using System;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public abstract class GeneratorBase
    {
        #region Constructor / De-constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startNumber"></param>
        /// <param name="endNumber"></param>
        protected GeneratorBase(ulong startNumber, ulong endNumber)
        {
            this.StartNumber = startNumber;
            this.EndNumber = endNumber;

            ulong range = (this.EndNumber - this.StartNumber);
            if(range > (int.MaxValue-57))
            {
                throw new ArgumentOutOfRangeException($"The numeric range between {this.StartNumber} and {this.EndNumber} is {range} which is greater than the internal array size of int.MaxValue", new Exception());
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public readonly ulong StartNumber;

        /// <summary>
        /// 
        /// </summary>
        public readonly ulong EndNumber;

        /// <summary>
        /// 
        /// </summary>
        protected IIsPrime PrimeChecker { get; set; }

        // TODO : If the Prime Array is not set before the get this need to set with the range between the start and end.
        /// <summary>
        /// 
        /// </summary>
        protected BitArray PrimeArray { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<decimal> Results
        {
            get
            {
                if (this.PrimeArray != null)
                {
                    for (int i = 0; i < this.PrimeArray.Length; i++)
                    {
                        if (this.PrimeArray[i])
                        {
                            yield return (this.StartNumber + (ulong)i);
                        }
                    }
                }
                else
                {
                    yield break;
                }
            }
        }

        public ISavePrimes PrimeFileSaver { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public abstract void Generate();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract bool Verify();

        /// <summary>
        /// 
        /// </summary>
        public abstract void Save();

        /// <summary>
        /// 
        /// </summary>
        public abstract void Load(string filePath);

        #endregion
    }
}
