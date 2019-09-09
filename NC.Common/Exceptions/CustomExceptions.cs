using System;
using System.Collections.Generic;
using System.Text;

namespace NC.Common.Exceptions
{
    /// <summary>
    /// 自定义通用 Exception
    /// </summary>
    public class CustomException : Exception
    {
        public CustomException()
        {
        }

        public CustomException(string message) : base(message)
        {
        }

        public CustomException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// 自定义 NotFound 异常
    /// </summary>
    public class CustomNotFoundException : Exception
    {
        public CustomNotFoundException()
        {
        }
        public CustomNotFoundException(string message)
            : base(message)
        {

        }
        public CustomNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }

    /// <summary>
    /// 自定义 授权失败异常
    /// </summary>
    public class CustomUnauthorizedException : Exception
    {
        public CustomUnauthorizedException()
        {
        }

        public CustomUnauthorizedException(string message) : base(message)
        {
        }

        public CustomUnauthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

}
