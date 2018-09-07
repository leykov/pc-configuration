using System;

namespace WebPCConfigTool.Common
{
    /// <summary>
    /// Represents a unexpected exception. User will see 'Unexpected Exception'.
    /// Exceptions that are related to the businnes logic shall inherit this class.
    /// </summary>
    public class ServiceException : BaseException
    {
        /// <summary>
        /// Constructs a service exception.
        /// </summary>
        /// <param name="message">A helpful message if any.</param>
        /// <param name="innerException">The inner exception if any.</param>
        public ServiceException(string message = null, Exception innerException = null) : base(ErrorType.UNEXPECTED, message, innerException)
        {
        }
    }
}
