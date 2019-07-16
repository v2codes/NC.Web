using System;
using System.IO;
using log4net;
using log4net.Config;
using log4net.Repository;

namespace NC.Web.Common.log4net
{
    public class LogHelper
    {
        private static ILoggerRepository repository { get; set; }
        private static ILog _log;
        private static ILog log
        {
            get
            {
                if (_log == null)
                {
                    ConfigureLog4Net();
                }
                return _log;
            }
        }

        /// <summary>
        /// 初始化 log4net
        /// </summary>
        /// <param name="repositoryName"></param>
        /// <param name="configFile"></param>
        private static void ConfigureLog4Net(string repositoryName = "NC.Web.Common", string configFile = "log4net.config")
        {
            repository = LogManager.CreateRepository(repositoryName);
            XmlConfigurator.Configure(repository, new FileInfo(configFile));
            _log = LogManager.GetLogger(repositoryName, repositoryName);
        }
        /// <summary>
        /// log info
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(string msg)
        {
            log.Info(msg);
        }
        /// <summary>
        /// log Warn
        /// </summary>
        /// <param name="msg"></param>
        public static void Warn(string msg)
        {
            log.Warn(msg);
        }
        /// <summary>
        /// log Error
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            log.Error(msg);
        }
        /// <summary>
        /// log Error
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(Exception ex)
        {
            log.Error(ex);
        }
        /// <summary>
        /// log Error
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg, Exception ex)
        {
            log.Error(msg, ex);
        }
    }
}
