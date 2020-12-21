/*---------------------------------------------------------------------------------- 
 * 类 名 称：Entity
 * 类 描 述：包含主键Id的实体类基类
 * 作    者：Leo
 * 创建时间：2019/9/25 16:51:54
 * 更新时间：2019/9/25 16:51:54
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System.ComponentModel.DataAnnotations;

namespace Blog.EntityFramework
{
    public class Entity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int Id { get; set; }
    }
}
