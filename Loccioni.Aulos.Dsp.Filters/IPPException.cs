using System;

namespace Loccioni.Aulos.Dsp.Filters
{
    /// <summary>
    /// An Intel Performance Primitives error
    /// </summary>
    public class IPPException: Exception
    {
        /// <summary>
        /// IPP API returned status. <see cref="IPP.NO_ERRORS"/> for no errors status.
        /// </summary>
        public int Status;

        public IPPException(int status): base(IPP.GetStatusString(status).ToString())
        {
            Status = status;
        }
    }
}
