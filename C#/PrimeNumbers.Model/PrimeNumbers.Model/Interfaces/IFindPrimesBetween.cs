using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumbers.Model.Interfaces
{
    public interface IFindPrimesBetween
    {
        PrimeDataDto Data { get; }

        ICollection<ulong> PrimeNumbers { get; }

        void Execute(ulong startNumber, ulong endNumber, IIsPrime primeChecker);
    }
}
