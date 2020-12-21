/*---------------------------------------------------------------------------------- 
 * 类 名 称：ExtCache
 * 类 描 述：泛型扩展方法
 * 作    者：Leo
 * 创建时间：2019/9/25 18:03:56
 * 更新时间：2019/9/25 18:03:56
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System;

namespace Leo.Framework.Extensions
{
    public static class ExtGeneric
    {
        public static T CopyByObject<T>(this T obj, T dataSource, string[] ignorePropName = null, bool onlyBasicType = true)
        {
            Type type = typeof(T);
            foreach (var item in type.GetProperties())
            {
                if (onlyBasicType)
                    //忽略引用类型
                    if (!item.PropertyType.IsValueType && item.PropertyType != typeof(string)) continue;
                //忽略指定属性
                if (ignorePropName != null && Array.IndexOf(ignorePropName, item.Name) != -1) continue;
                //复制属性值
                item.SetValue(obj, item.GetValue(dataSource));
            }
            return obj;
        }

        public static T CopyToNewObject<T>(this T dataSource, string[] ignorePropName = null, bool onlyBasicType = true) where T : new()
        {
            T newObj = new T();
            Type type = typeof(T);
            foreach (var item in type.GetProperties())
            {
                if (onlyBasicType)
                    //忽略引用类型
                    if (!item.PropertyType.IsValueType && item.PropertyType != typeof(string)) continue;
                //忽略指定属性
                if (ignorePropName != null && Array.IndexOf(ignorePropName, item.Name) != -1) continue;
                //复制属性值
                item.SetValue(newObj, item.GetValue(dataSource));
            }
            return newObj;
        }
    }
}
