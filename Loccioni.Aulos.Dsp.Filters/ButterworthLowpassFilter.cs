using System;

namespace Loccioni.Aulos.Dsp.Filters
{
    /// <summary>
    /// A Butterworth low pass filter.
    /// See https://it.wikipedia.org/wiki/Filtro_Butterworth
    /// </summary>
    public class ButterworthLowpassFilter: IDisposable
    {
        private double[] taps;
        private IntPtr state;
        private int order;
        private IntPtr externalBuffer2;
        private IntPtr externalBuffer;

        /// <summary>
        /// A Butterworth low pass filter.
        /// </summary>
        /// <param name="samplingFrequency">Sampling frequency of input signal in Hertz</param>
        /// <param name="cutoff">Cut off frequency in Hertz. It must be lesser than half <paramref name="samplingFrequency"/></param>
        /// <param name="order">Order of the filter [1..inf[</param>
        public ButterworthLowpassFilter(int order, double samplingFrequency, double cutoff)
        {
            IPP.Initialize();

            this.order = order;
            int externalBuffersize = 0;
            taps = new double[2 * (order + 1)];

            // compute coefficients
            int status;
            if ((status = IPP.ippsIIRGenGetBufferSize(order, ref externalBuffersize)) != IPP.NO_ERRORS)
                throw new IPPException(status);
            externalBuffer2 = IPP.ippsMalloc_8u(externalBuffersize);
            if ((status = IPP.ippsIIRGenLowpass_64f(cutoff / samplingFrequency, 0, order, taps, IPP.IppsIIRFilterType.ippButterworth, externalBuffer2)) != IPP.NO_ERRORS)
                throw new IPPException(status);

            Reset();
        }

        /// <summary>
        /// Initialize filter state
        /// </summary>
        public void Reset()
        {
            int status;
            int externalBuffersize = 0;
            if ((status = IPP.ippsIIRGetStateSize_64f(order, ref externalBuffersize)) != IPP.NO_ERRORS)
                throw new IPPException(status);

            externalBuffer = IPP.ippsMalloc_8u(externalBuffersize);
            if ((status = IPP.ippsIIRInit_64f(ref state, taps, order, IntPtr.Zero, externalBuffer)) != IPP.NO_ERRORS)
                throw new IPPException(status);
        }

        private double[] destination;
        /// <summary>
        /// Apply filter to data
        /// </summary>
        /// <param name="source">Source data</param>
        /// <returns>Filter data. The returned array is reused, so copy it or use it before the next call.</returns>
        public double[] Apply(double[] source)
        {
            int status;
            if (destination == null || destination.Length != source.Length) destination = new double[source.Length];
            if ((status = IPP.ippsIIR_64f(source, destination, source.Length, state)) != IPP.NO_ERRORS)
                throw new IPPException(status);
            return destination;
        }

        public void Dispose()
        {
            IPP.ippsFree(externalBuffer);
            IPP.ippsFree(externalBuffer2);
        }
    }
}