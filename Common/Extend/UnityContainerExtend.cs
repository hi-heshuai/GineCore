using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace GineCore.Common.Extend
{
    public static class UnityContainerExtend
    {
        /// <summary>
        /// 注入泛型T的子类对象
        /// </summary>
        public static void RegisterInheritedTypes<T>(this IServiceCollection services)
        {
            var baseType = typeof(T);
            services.RegisterInheritedTypes(baseType);
        }

        /// <summary>
        /// 注入baseType的子类对象
        /// </summary>
        public static IServiceCollection RegisterInheritedTypes(this IServiceCollection services, Type baseType)
        {
            var allTypes = baseType.Assembly.GetTypes();
            var baseInterfaces = baseType.GetInterfaces();
            foreach (var type in allTypes)
            {
                if (type.BaseType != null && type.BaseType.GenericEq(baseType))
                {
                    var typeInterface = type.GetInterfaces()
                        .FirstOrDefault(x => !baseInterfaces.Any(bi => bi.GenericEq(x)));
                    if (typeInterface == null)
                    {
                        continue;
                    }
                    services.AddTransient(typeInterface, type);
                }
            }
            return services;
        }

        /// <summary>
        /// 注入实现接口interfaceType的子类对象
        /// </summary>
        /// <param name="interfaceType"></param>
        public static IServiceCollection RegisterInterfaceTypes(this IServiceCollection services, Type baseInterface)
        {
            var allTypes = baseInterface.Assembly.GetTypes();
            var types = allTypes.ToList().FindAll(type => type.GetInterfaces()
                        .ToList().Count > 0 && !type.Attributes.HasFlag(TypeAttributes.Abstract)
                        && !type.Name.Contains("BaseService") && !type.Name.Contains("BaseRepository"));//泛型另外注册

            foreach (var type in types)
            {
                var typeInterfaces = type.GetInterfaces();
                var typeInterface = typeInterfaces.Where(x => "I" + type.Name == x.Name).FirstOrDefault();

                if (typeInterface == null)
                {
                    continue;
                }

                services.AddTransient(typeInterface, type);
            }

            return services;
        }
    }
}
