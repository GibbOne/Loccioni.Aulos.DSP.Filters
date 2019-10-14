using BenchmarkDotNet.Attributes;

namespace Loccioni.Aulos.Dsp.Filters.Benchmarks
{
    public class ButterworthLowpassFilterBenchmark
    {
        [Benchmark(Baseline = true)]
        public void NationalInstrumentsFiltering()
        {
        }

        [Benchmark(Baseline = true)]
        public void AulosIntelFiltering()
        {
        }
    }
}
