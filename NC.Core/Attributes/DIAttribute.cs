using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace NC.Core.Attributes
{
    /// <summary>
    /// 用于标注 DI依赖注入 的特性定义
    /// 此属性只能运用于类，并且此特性禁止继承
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class DIAttribute : Attribute
    {
        //Targets用于指示 哪些接口或者类 要被 "被属性修饰了的类" 进行依赖注入
        private List<Type> TargetTypes = new List<Type>();

        public DIAttribute(ServiceLifetime lifetime, params Type[] argTargets)
        {
            Lifetime = lifetime;
            foreach (var target in argTargets)
            {
                TargetTypes.Add(target);
            }
        }

        /// <summary>
        /// 被修饰了的类
        /// </summary>
        /// <returns></returns>
        public List<Type> GetTargetTypes()
        {
            return TargetTypes;
        }

        /// <summary>
        /// 注入实例的生命周期
        /// </summary>
        public ServiceLifetime Lifetime { get; set; }
    }
}
