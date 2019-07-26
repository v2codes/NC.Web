using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace NC.Web.Common.DI
{
    /// <summary>
    /// net core DI 辅助类
    /// </summary>
    public static class IServiceCollectionExtension
    {
        /// <summary>
        /// 批量注入
        /// </summary>
        public static void BatchAddScoped(this IServiceCollection services, Type implementationType, Type[] interfaceTypes)
        {
            foreach (var imType in interfaceTypes)
            {
                services.AddScoped(imType, implementationType);
            }
        }
    }
}
