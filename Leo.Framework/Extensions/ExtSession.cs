/*---------------------------------------------------------------------------------- 
 * 类 名 称：ExtCache
 * 类 描 述：Session扩展方法
 * 作    者：Leo
 * 创建时间：2019/9/25 18:05:43
 * 更新时间：2019/9/25 18:05:43
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Leo.Framework.Extensions
{
    public static class ExtSession
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string str = JsonConvert.SerializeObject(value, settings);
            session.SetString(key, str);
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
