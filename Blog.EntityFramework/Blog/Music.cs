/*---------------------------------------------------------------------------------- 
 * 类 名 称：Music
 * 类 描 述：音乐类
 * 作    者：Leo
 * 创建时间：2019/9/25 17:12:05
 * 更新时间：2019/9/25 17:12:05
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.EntityFramework
{
    public class Music : EntityPlus
    {
        /// <summary>
        /// 音乐名称
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// 音乐链接
        /// </summary>
        [Column(TypeName = "varchar(231)")]
        public string Url { get; set; }
        /// <summary>
        /// 歌手名称
        /// </summary>
        [StringLength(50)]
        public string Artist { get; set; }
        /// <summary>
        /// 音乐封面
        /// </summary>
        [Column(TypeName = "varchar(231)")]
        public string Cover { get; set; }
        /// <summary>
        /// 音乐主题色
        /// </summary>
        [Column(TypeName = "varchar(10)")]
        public string Theme { get; set; }

        #region 导航属性
        public virtual ICollection<ArticleMusic> ArticleMusics { get; set; }
        #endregion
    }
}
