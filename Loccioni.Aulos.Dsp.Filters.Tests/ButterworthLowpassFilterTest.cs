using NUnit.Framework;
using FluentAssertions;

namespace Loccioni.Aulos.Dsp.Filters.Tests
{
    public class ButterworthLowpassFilterTest
    {
        private double[] GenerateStep(int samples)
        {
            var ret = new double[samples];
            for (int i = 0; i < samples; i++)
            {
                ret[i] = i < (samples / 2) ? 0 : 1;
            }
            return ret;
        }

        [Test]
        public void SimpleFiltering()
        {
            // ARRANGE
            var filter = new ButterworthLowpassFilter(8, 1000, 300);
            var inputSignal = GenerateStep(2000);

            // ACT
            var filteredSignal = filter.Apply(inputSignal);

            // ASSERT
            filteredSignal.Length.Should().Be(inputSignal.Length);
        }
    }
}
