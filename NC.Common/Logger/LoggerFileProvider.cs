using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace NC.Common.log
{
    /// <summary>
    /// TODO 自定义文件日志
    /// </summary>
    public class LoggerFileProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
