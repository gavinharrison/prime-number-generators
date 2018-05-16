using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumbers.Model.Interfaces
{
    public interface ISavePrimes : IVersion
    {
        void SavePrimes(string filePath, PrimeDataDto data);

        //void SavePrimes(ulong startNumber, ulong endNumber, BitArray data, Stream savestream);
    }
}
