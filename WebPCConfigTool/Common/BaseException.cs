using System;
using System.Collections.Generic;

namespace WebPCConfigTool.Common
{
    /// <summary>
    /// Base exception for Grace Project.
    /// </summary>
    public class BaseException : Exception
    {
        /// <summary>
        /// The code of this error.
        /// </summary>
        public ErrorType ErrorType { get; set; }

        /// <summary>
        /// List of error messages.
        /// </summary>
        public List<string> ErrorMessages { set; get; }

        /// <summary>
        /// Constructs an exeception.
        /// </summary>
        /// <param name="errorType">The <see cref="ErrorType"/> of the exception.</param>     
        /// <param name="message">A helpful message if any.</param>
        /// <param name="innerException">The inner exception if any.</param>      
        public BaseException(ErrorType errorType, string message = null,  Exception innerException = null)
            : base(message, innerException)
        {
            ErrorType = errorType;
            ErrorMessages = new List<string>();
        }

        /// <summary>
        /// Constructs an exeception.
        /// </summary>
        /// <param name="errorType">The <see cref="ErrorType"/> of the exception.</param>
        /// <param name="errorMessages">List of error messages.</param>
        /// <param name="message">A helpful message if any.</param>
        /// <param name="innerException">The inner exception if any.</param>      
        public BaseException(ErrorType errorType, List<string> errorMessages, string message = null, Exception innerException = null)
            : base(message, innerException)
        {
            ErrorType = errorType;
            ErrorMessages = errorMessages;
        }
    }
}
