using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumbers.Model.Interfaces
{
    public interface IReadPrimes : IVersion
    {
        PrimeDataDto ReadPrimes(string filePath);

        //BitArray ReadPrimes(Stream dataLocation);
    }
}
