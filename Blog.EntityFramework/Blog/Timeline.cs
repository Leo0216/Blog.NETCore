/*---------------------------------------------------------------------------------- 
 * 类 名 称：Timeline
 * 类 描 述：时光轴类
 * 作    者：Leo
 * 创建时间：2019/9/25 17:15:34
 * 更新时间：2019/9/25 17:15:34
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.EntityFramework
{
    public class Timeline : EntityPlus
    {
        [StringLength(20)]
        public string Title { get; set; }
        [StringLength(100)]
        public string Content { get; set; }
        [Column(TypeName = "varchar(300)")]
        public string Link { get; set; }

    }
}
