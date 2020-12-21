/*---------------------------------------------------------------------------------- 
 * 类 名 称：EntityPlus
 * 类 描 述：包含主键Id、数据状态、创建时间的实体类基类
 * 作    者：Leo
 * 创建时间：2019/9/25 16:53:16
 * 更新时间：2019/9/25 16:53:16
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System;

namespace Blog.EntityFramework
{
    public class EntityPlus : Entity
    {
        /// <summary>
        /// 数据状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 数据创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
