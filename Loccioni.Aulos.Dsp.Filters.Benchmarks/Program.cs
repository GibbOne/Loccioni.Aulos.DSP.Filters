using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loccioni.Aulos.Dsp.Filters.Benchmarks
{
    public static class Program
    {
        static void Main()
        {
            var summary = BenchmarkRunner.Run<SimpleSample>();
        }
    }
}
