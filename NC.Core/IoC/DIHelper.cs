using Microsoft.Extensions.DependencyInjection;
using NC.Core.Attributes;
using NC.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NC.Core.IoC
{
    /// <summary>
    /// net core DI 辅助类
    /// </summary>
    public static class IServiceCollectionExtension
    {

        #region add with reflection
        /// <summary>
        /// 批量注入仓储、服务
        /// 1. 程序集名称NC.xxx
        /// 2. 类名已 Repository、Service 结尾
        /// 3. 生命周期为 Scoped
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="lifetime">实例声明周期</param>
        public static void AddReposAndServices(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var allTypes = ReflectionHelper.GetTypes().Where(p => p.Key.Name.EndsWith("Repository") || p.Key.Name.EndsWith("Service"));
            foreach (var item in allTypes)
            {
                services.AddRange(item.Key, item.Value);
            }
        }
        /// <summary>
        /// 批量注入指定程序集中的类型
        /// </summary>
        public static void AddAssembly(this IServiceCollection services, string assemblyName, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var allTypes = ReflectionHelper.GetTypes(assemblyName);
            foreach (var item in allTypes)
            {
                services.AddRange(item.Key, item.Value, lifetime);
            }
        }
        /// <summary>
        /// 批量注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="implementationType">实现类</param>
        /// <param name="interfaceTypes">接口类</param>
        /// <param name="lifetme">实例生命周期</param>
        public static void AddRange(this IServiceCollection services, Type implementationType, Type[] interfaceTypes, ServiceLifetime lifetme = ServiceLifetime.Scoped)
        {
            foreach (var imType in interfaceTypes)
            {
                var serviceDescriptor = new ServiceDescriptor(imType, implementationType, lifetme);
                services.Add(serviceDescriptor);
            }
        }
        #endregion

        #region add with attribute
        /// <summary>
        /// 注入具体类型(前提：该类型标注了DIAttribute特性)
        /// </summary>
        /// <param name="services"></param>
        /// <param name="implementationType"></param>
        public static void AddByAttribute(this IServiceCollection services, Type implementationType)
        {
            // 获取类型的 UseDIAttribute 属性 对应的对象
            var attr = implementationType.GetCustomAttribute(typeof(DIAttribute)) as DIAttribute;

            ////获取类实现的所有接口
            //Type[] types = ImplementationType.GetInterfaces();
            var types = attr.GetTargetTypes();
            var lifetime = attr.Lifetime;
            //遍历类实现的每一个接口
            foreach (var t in types)
            {
                //将类注册为接口的实现-----但是存在一个问题，就是担心 如果一个类实现了IDisposible接口 担心这个类变成了这个接口的实现
                ServiceDescriptor serviceDescriptor = new ServiceDescriptor(t, implementationType, lifetime);
                services.Add(serviceDescriptor);
            }
        }

        /// <summary>
        /// 注入指定程序集中的类型(标注了DIAttribute特性的所有类型)
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyName"></param>
        public static void AddByAttribute(this IServiceCollection services, string assemblyName)
        {
            // 根据程序集的名字 获取程序集中所有的类型
            // 过滤上述程序集 class类型、public访问修饰符、非抽象类
            var types = ReflectionHelper.GetTypesByAssemblyName(assemblyName).Where(p => p.IsClass && p.IsPublic && !p.IsAbstract);
            foreach (var type in types)
            {
                var hasDIAttribute = type.GetCustomAttributes().Any(p => p is DIAttribute);
                if (hasDIAttribute)
                {
                    services.AddByAttribute(type);
                }
            }
        }

        /// <summary>
        /// 注入当前程序集下的所有类型(标注了DIAttribute特性的)
        /// </summary>
        /// <param name="services"></param>
        public static void AddByAttribute(this IServiceCollection services)
        {
            var allTypes = ReflectionHelper.GetTypes();
            foreach (var item in allTypes)
            {
                var hasDIAttribute = item.Key.GetCustomAttributes().Any(p => p is DIAttribute);
                if (hasDIAttribute)
                {
                    services.AddRange(item.Key, item.Value);
                }
            }
        }

        /// <summary>
        /// 批量注入仓储、服务(标注了DIAttribute特性的)
        /// </summary>
        /// <param name="services"></param>
        public static void AddReposAndServicesByAttribute(this IServiceCollection services)
        {
            var allTypes = ReflectionHelper.GetTypes().Where(p => p.Key.Name.EndsWith("Repository") || p.Key.Name.EndsWith("Service"));
            foreach (var item in allTypes)
            {
                var hasDIAttribute = item.Key.GetCustomAttributes().Any(p => p is DIAttribute);
                if (hasDIAttribute)
                {
                    services.AddRange(item.Key, item.Value);
                }
            }
        }
        #endregion
    }
}
