/*---------------------------------------------------------------------------------- 
 * 类 名 称：Carousel
 * 类 描 述：轮播图类
 * 作    者：Leo
 * 创建时间：2019/9/25 17:07:56
 * 更新时间：2019/9/25 17:07:56
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using Blog.EntityFramework.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.EntityFramework
{

    public class Carousel : EntityPlus
    {
        /// <summary>
        /// 图片地址
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string ImgUrl { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string Link { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public int Order { get; set; }


        /// <summary>
        /// 链接目标
        /// </summary>
        public Target Target { get; set; }
    }
}
