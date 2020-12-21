/*---------------------------------------------------------------------------------- 
 * 类 名 称：ExtCache
 * 类 描 述：String扩展方法
 * 作    者：Leo
 * 创建时间：2019/9/25 18:07:22
 * 更新时间：2019/9/25 18:07:22
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Leo.Framework.Extensions
{
    public static class ExtString
    {
        /// <summary>
        /// 判断字符串是否为空字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
        /// <summary>
        /// 判断字符串是否为空或者空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
        /// <summary>
        /// string数组转long数组
        /// </summary>
        /// <param name="strArr"></param>
        /// <returns></returns>
        public static long[] ConvertToLongArray(this string[] strArr)
        {
            return Array.ConvertAll(strArr, new Converter<string, long>(str => { return long.Parse(str); }));
        }
        /// <summary>
        /// 字符串MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5Encrypt(this string str)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(str));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                stringBuilder.Append(data[i].ToString("x2"));
            return stringBuilder.ToString();
        }

        #region 字符串验证
        /// <summary>
        /// 验证是否是有效邮箱地址
        /// </summary>
        /// <param name="str">输入的字符</param>
        /// <returns></returns>
        public static bool IsEmail(this string str)
        {
            return Regex.IsMatch(str, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        /// <summary>
        /// 验证输入字符串为电话号码
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <returns>返回一个bool类型的值</returns>
        public static bool IsPhone(this string str)
        {
            return Regex.IsMatch(str, @"(^(\d{2,4}[-_－—]?)?\d{3,8}([-_－—]?\d{3,8})?([-_－—]?\d{1,7})?$)|(^0?1[35]\d{9}$)");
            //弱一点的验证：  @"\d{3,4}-\d{7,8}"         
        }
        /// <summary>
        /// 验证输入字符串为数字
        /// </summary>
        /// <param name="str">输入字符</param>
        /// <returns>返回一个bool类型的值</returns>
        public static bool IsNumber(this string str)
        {
            return Regex.IsMatch(str, "^([0]|([1-9]+\\d{0,}?))(.[\\d]+)?$");
        }
        /// <summary>
        /// 是否Url地址（统一资源定位）
        /// </summary>
        /// <param name="str">url地址</param>
        /// <returns></returns>
        public static bool IsUrl(this string str)
        {
            if (str.IsNullOrEmpty())
                return false;
            return Regex.IsMatch(str, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$",
                    RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 判断字符是否是IP地址
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsIPAddress(this string str)
        {
            return Regex.IsMatch(str, @"((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))",
                    RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 是否中文
        /// </summary>
        /// <param name="str">中文</param>
        /// <returns></returns>
        public static bool IsChinese(this string str)
        {
            if (str.IsNullOrEmpty())
                return false;
            return Regex.IsMatch(str, @"^[\u4e00-\u9fa5]+$", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 是否包含中文
        /// </summary>
        /// <param name="str">中文</param>
        /// <returns></returns>
        public static bool IsContainsChinese(this string str)
        {
            if (str.IsNullOrEmpty())
                return false;
            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]+", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 是否正常字符，字母、数字、下划线的组合
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static bool IsNormalChar(this string str)
        {
            if (str.IsNullOrEmpty())
                return false;
            return Regex.IsMatch(str, @"[\w\d_]+", RegexOptions.IgnoreCase);
        }
        #endregion

        /// <summary>
        /// HMAC-SHA1加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="keyVal"></param>
        /// <returns></returns>
        public static string HMACSHA1(this string str, string keyVal)
        {
            using (HMACSHA1 hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes(keyVal)))
            {
                byte[] hashStr = hmacsha1.ComputeHash(Encoding.UTF8.GetBytes(str));
                return Encoding.UTF8.GetString(hashStr);
            }

        }

        /// <summary>
        /// 字符串转Base64字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <returns>Base64衣服穿</returns>
        public static string ToBase64(this string str)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(str));
        }
    }
}
