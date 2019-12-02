using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Loccioni.Aulos.Dsp.Filters
{
    /// <summary>
    /// Intel Performance Primitives for IIR filters
    /// </summary>
    static class IPP
    {
        public static int NO_ERRORS = 0;
        private static bool initialized = false;
        /// <summary>
        /// Automatically initializes the library code that is most appropriate for the current processor type.
        /// </summary>
        public static void Initialize()
        {
            if (!initialized)
            {
                var ret = ippInit();
                if (ret != NO_ERRORS)
                    throw new IPPException(ret);
                initialized = true;
            }
        }

        [SuppressUnmanagedCodeSecurity()]
        [DllImport("ipps.dll")]
        public extern static IntPtr ippsMalloc_8u(int sizeInByte);

        [SuppressUnmanagedCodeSecurity()]
        [DllImport("ipps.dll")]
        public extern static void ippsFree(IntPtr pointerToMemory);

        /// <summary>
        /// Force an automatically initialization of the library code that is most appropriate 
        /// for the current processor type.
        /// </summary>
        [SuppressUnmanagedCodeSecurity()]
        [DllImport("ippcore.dll")]
        public extern static int ippInit();

        [SuppressUnmanagedCodeSecurity()]
        [DllImport("ippcore.dll")]
        private extern static IntPtr ippGetStatusString(int status);
        public static string GetStatusString(int status)
        {
            var ptr = ippGetStatusString(status);
            return Marshal.PtrToStringAnsi(ptr);
        }

        [SuppressUnmanagedCodeSecurity()]
        [DllImport("ipps.dll")]
        public extern static int ippsIIRGetStateSize_64f(int order, ref int externalBufferSize);

        [SuppressUnmanagedCodeSecurity()]
        [DllImport("ipps.dll")]
        public extern static int ippsIIRInit_64f(ref IntPtr state, double[] taps, int order, IntPtr delayLine, IntPtr externalBuffer);


        [SuppressUnmanagedCodeSecurity()]
        [DllImport("ipps.dll")]
        public extern static int ippsIIRGenGetBufferSize(int order, ref int externalBufferSize);

        public enum IppsIIRFilterType
        {
            ippButterworth,
            ippChebyshev1
        }

        [SuppressUnmanagedCodeSecurity()]
        [DllImport("ipps.dll")]
        public extern static int ippsIIRGenLowpass_64f(double cutoffFrequency, double ripple, int order, double[] taps, IppsIIRFilterType filterType, IntPtr externalBuffer);



        [SuppressUnmanagedCodeSecurity()]
        [DllImport("ipps.dll")]
        public extern static int ippsIIR_64f(double[] src, double[] dst, int len, IntPtr state);
    }
}
