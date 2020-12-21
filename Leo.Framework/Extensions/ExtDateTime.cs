/*---------------------------------------------------------------------------------- 
 * 类 名 称：ExtCache
 * 类 描 述：DateTime扩展方法
 * 作    者：Leo
 * 创建时间：2019/9/25 17:49:27
 * 更新时间：2019/9/25 17:49:27
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System;

namespace Leo.Framework.Extensions
{
    public static class ExtDateTime
    {
        /// <summary>
        /// 过去一周以内的时间格式化成 几天前 几小时前 几分钟前 几秒前
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToStringFromNow(this DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.TotalDays > 7)
            {
                return dt.ToString("yyyy-MM-dd");
            }
            else
            {
                if (span.TotalDays > 1)
                {
                    return string.Format($"{(int)Math.Floor(span.TotalDays)}天前");
                }
                else if (span.TotalHours > 1)
                {
                    return string.Format($"{(int)Math.Floor(span.TotalHours)}小时前");
                }
                else if (span.TotalMinutes > 1)
                {
                    return string.Format($"{(int)Math.Floor(span.TotalMinutes)}分钟前");
                }
                else if (span.TotalSeconds >= 1)
                {
                    return string.Format($"{(int)Math.Floor(span.TotalSeconds)}秒前");
                }
                else
                {
                    return "1秒前";
                }
            }
        }

        /// <summary>
        /// 判断日期时间是否是本月
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsCurrMonth(this DateTime dt)
        {
            return dt.Month == DateTime.Now.Month && dt.Year == DateTime.Now.Year;
        }

        /// <summary>
        /// 判断日期是否是今天
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsToday(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day) == DateTime.Today;
        }
    }
}
