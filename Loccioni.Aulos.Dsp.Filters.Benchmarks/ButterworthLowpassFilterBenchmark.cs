using BenchmarkDotNet.Attributes;

namespace Loccioni.Aulos.Dsp.Filters.Benchmarks
{
    public class ButterworthLowpassFilterBenchmark
    {
        private const int ORDER = 8;
        private const int SAMPLING_FREQ = 10000;
        private const int CUT_OFF = 3000;

        [Benchmark(Baseline = true)]
        public void NationalInstrumentsFilterCreation()
        {
            using (var filter = new NationalInstruments.Analysis.Dsp.Filters.ButterworthLowpassFilter(ORDER, SAMPLING_FREQ, CUT_OFF)) ;
        }

        [Benchmark]
        public void AulosIntelFilterCreation()
        {
            using (var filter = new Loccioni.Aulos.Dsp.Filters.ButterworthLowpassFilter(ORDER, SAMPLING_FREQ, CUT_OFF)) ;
        }

        private double[] GenerateStep(int samples)
        {
            var ret = new double[samples];
            for (int i = 0; i < samples; i++)
            {
                ret[i] = i < (samples / 2) ? 0 : 1;
            }
            return ret;
        }

        [Benchmark]
        public void NationalInstrumentsFilterCreationAndApplication()
        {
            using (var filter = new NationalInstruments.Analysis.Dsp.Filters.ButterworthLowpassFilter(ORDER, SAMPLING_FREQ, CUT_OFF))
            {
                var filteredData = filter.FilterData(GenerateStep(20000));
            }
        }

        [Benchmark]
        public void AulosIntelFilterCreationAndApplication()
        {
            using (var filter = new Loccioni.Aulos.Dsp.Filters.ButterworthLowpassFilter(ORDER, SAMPLING_FREQ, CUT_OFF))
            {
                var filteredData = filter.Apply(GenerateStep(20000));
            }
        }

        [Benchmark]
        public void NationalInstrumentsFilterCreationAndMultipleApplication()
        {
            using (var filter = new NationalInstruments.Analysis.Dsp.Filters.ButterworthLowpassFilter(8, SAMPLING_FREQ, CUT_OFF))
            {

                for (int i = 0; i < 100; i++)
                {
                    var filteredData = filter.FilterData(GenerateStep(20000));
                }
            }
        }

        [Benchmark]
        public void AulosIntelFilterCreationAndMultipleApplication()
        {
            using (var filter = new Loccioni.Aulos.Dsp.Filters.ButterworthLowpassFilter(ORDER, SAMPLING_FREQ, CUT_OFF))
            {

                for (int i = 0; i < 100; i++)
                {
                    var filteredData = filter.Apply(GenerateStep(20000));
                }
            }
        }
    }
}
