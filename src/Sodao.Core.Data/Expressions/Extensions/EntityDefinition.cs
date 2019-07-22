using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sodao.Core.Data
{
    /// <summary>
    /// 字段定义说明
    /// </summary>
    public static class EntityDefinition
    {
        private static ConcurrentDictionary<string, List<PropertyInfo>> _cacheSubmeter = new ConcurrentDictionary<string, List<PropertyInfo>>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAttribute">属性</typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal static TAttribute GetAttribute<TAttribute>(this Type entity) where TAttribute : Attribute
        {
            var attrs = entity.GetCustomAttributes(typeof(TAttribute));
            if (attrs?.Count() > 0)
                return (TAttribute)attrs.First();
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAttribute">属性</typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        internal static TAttribute GetAttribute<TAttribute>(this PropertyInfo property) where TAttribute : Attribute
        {
            var obs = property.GetCustomAttributes(typeof(TAttribute), false);
            if (obs?.Count() > 0)
                return (TAttribute)obs.First();
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal static PropertyInfo GetProperty<TAttribute>(this Type entity) where TAttribute : Attribute
        {
            var propertyInfos = _cacheSubmeter.GetOrAdd($"{entity.Name}_{typeof(TAttribute).Name}",
                key => entity.GetPropertyByAttribute<TAttribute>());
            return propertyInfos.FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal static List<PropertyInfo> GetProperties<TAttribute>(this Type entity) where TAttribute : Attribute
        {
            var propertyInfos = _cacheSubmeter.GetOrAdd($"{entity.Name}_{typeof(TAttribute).Name}",
                key => entity.GetPropertyByAttribute<TAttribute>());
            return propertyInfos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAttribute">属性</typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal static List<PropertyInfo> GetPropertyByAttribute<TAttribute>(this Type entity) where TAttribute : Attribute
        {
            var list = new List<PropertyInfo>();
            var pis = entity.GetProperties();
            foreach (var item in pis)
            {
                var obs = item.GetCustomAttributes(typeof(TAttribute), false);
                if (obs?.Count() > 0)
                    list.Add(item);
            }
            return list;
        }
    }
}
