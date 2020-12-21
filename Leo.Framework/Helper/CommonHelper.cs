/*---------------------------------------------------------------------------------- 
 * 类 名 称：ExtCache
 * 类 描 述：通用辅助类
 * 作    者：Leo
 * 创建时间：2019/9/25 18:08:52
 * 更新时间：2019/9/25 18:08:52
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System;

namespace Leo.Framework.Helper
{
    public static class CommonHelper
    {
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
        /// <summary>
        /// 取随机数
        /// </summary>
        /// <param name="length">随机数的位数</param>
        /// <returns>随机数</returns>
        public static int GetRandom(int length)
        {
            Random rd = new Random();
            int min = Convert.ToInt32(Math.Pow(10, length - 1));
            int max = Convert.ToInt32(Math.Pow(10, length));
            return rd.Next(min, max);
        }
        /// <summary>
        /// 取指定范围内的伪随机数（包括两端）
        /// </summary>
        /// <param name="min">最大值</param>
        /// <param name="max">最小值</param>
        /// <returns></returns>
        public static int GetRandom(int min, int max)
        {
            Random rd = new Random();
            return rd.Next(min, max + 1);
        }
    }
}
