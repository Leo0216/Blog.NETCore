/*---------------------------------------------------------------------------------- 
 * 类 名 称：ArticleMusic
 * 类 描 述：文章音乐类，关联文章和音乐
 * 作    者：Leo
 * 创建时间：2019/9/25 17:06:29
 * 更新时间：2019/9/25 17:06:29
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/

namespace Blog.EntityFramework
{
    public class ArticleMusic
    {
        public int ArticleId { get; set; }
        public int MusicId { get; set; }

        public virtual Article Article { get; set; }
        public virtual Music Music { get; set; }
    }
}
