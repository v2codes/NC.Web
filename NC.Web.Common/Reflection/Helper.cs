using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;

namespace NC.Web.Common.Reflection
{
    /// <summary>
    /// 反射辅助类
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// 获取当前项目下所有程序集,排除所有的系统程序集(Microsoft.***、System.***等)、Nuget下载包
        /// </summary>
        /// <param name="namePattern">程序集名称关键字，默加载 NC. 命名开头的程序集</param>
        /// <returns></returns>
        public static IList<Assembly> GetAllAssemblies(string namePattern = "NC.")
        {
            var assemblies = new List<Assembly>();
            var deps = DependencyContext.Default;
            var libs = deps.CompileLibraries.Where(p => !p.Serviceable && p.Type != "package");
            foreach (var item in libs)
            {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(item.Name));
                assemblies.Add(assembly);
            }

            #region 不适用当前项目结构
            ////1.获取当前程序集(Ray.EssayNotes.AutoFac.Infrastructure.CoreIoc)所有引用程序集
            //Assembly executingAssembly = Assembly.GetExecutingAssembly();//当前程序集
            //var assemblies = executingAssembly.GetReferencedAssemblies()
            //    .Select(Assembly.Load)
            //    .Where(m => m.FullName.Contains(namePattern))
            //    .ToList();
            ////2.获取启动入口程序集（Ray.EssayNotes.AutoFac.CoreApi）
            //Assembly assembly = Assembly.GetEntryAssembly();
            //assemblies.Add(assembly);
            #endregion

            return assemblies;
        }

        /// <summary>
        /// 获取指定程序集
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns></returns>
        public static Assembly GetAssembly(string assemblyName)
        {
            var assembly = Assembly.Load(assemblyName);
            return assembly;
        }

        /// <summary>
        /// 获取当前项目下所有程序集中实现类以及对应的接口
        /// </summary>
        /// <param name="namePattern">程序集名称关键字，默加载 NC. 命名开头的程序集</param>
        /// <returns>key 具体类， value 接口数组</returns>
        public static Dictionary<Type, Type[]> GetTypes(string namePattern = "NC.")
        {
            var dict = new Dictionary<Type, Type[]>();
            var assemblies = GetAllAssemblies(namePattern);
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes().Where(p => p.IsPublic && !p.IsInterface).ToArray();
                foreach (var type in types)
                {
                    var interfaceTypes = type.GetInterfaces();
                    dict.Add(type, interfaceTypes);
                }
            }
            return dict;
        }
    }
}
