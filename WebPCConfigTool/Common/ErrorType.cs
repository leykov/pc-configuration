namespace WebPCConfigTool.Common
{
    /// <summary>
    /// Contains the list of error types of the system.
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// Unexpected exception. Could be anything.
        /// </summary>
        UNEXPECTED = 0,

        /// <summary>
        /// Bad user request. User shall see these.
        /// </summary>
        BAD_REQUEST = 400,

        /// <summary>
        /// Unauthenticated exception, i.e. missing or expired session.
        /// </summary>
        UNAUTHENTICATED = 401,

        /// <summary>
        /// Unauthoriazed exception, i.e. user does not have permissions for the requested action.
        /// </summary>
        UNAUTHORIZED = 403,
    }
}
