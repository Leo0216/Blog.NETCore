/*---------------------------------------------------------------------------------- 
 * 类 名 称：Production
 * 类 描 述：产品类
 * 作    者：Leo
 * 创建时间：2019/9/25 17:13:12
 * 更新时间：2019/9/25 17:13:12
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.EntityFramework
{
    public class Production : EntityPlus
    {

        /// <summary>
        /// 封面
        /// </summary>
        [Column(TypeName = "varchar(300)")]
        public string Cover { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(20)]
        public string Title { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        [Column(TypeName = "varchar(300)")]
        public string Link { get; set; }

        /// <summary>
        /// 下载地址
        /// </summary>
        [Column(TypeName = "varchar(300)")]
        public string DownloadUrl { get; set; }

        /// <summary>
        /// 介绍
        /// </summary>
        [StringLength(300)]
        public string Intro { get; set; }
    }
}
