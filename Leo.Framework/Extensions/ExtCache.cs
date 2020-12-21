/*---------------------------------------------------------------------------------- 
 * 类 名 称：ExtCache
 * 类 描 述：ASP.NET Core缓存扩展类
 * 作    者：Leo
 * 创建时间：2019/9/25 18:02:12
 * 更新时间：2019/9/25 18:02:12
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Leo.Framework.Extensions
{
    public static class ExtCache
    {

        public static async Task SetObjectAsync(this IDistributedCache cache, string key, object value)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            await cache.SetStringAsync(key, JsonConvert.SerializeObject(value, settings));
        }


        public static async Task<T> GetObjectAsync<T>(this IDistributedCache cache, string key)
        {
            var value = await cache.GetStringAsync(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
