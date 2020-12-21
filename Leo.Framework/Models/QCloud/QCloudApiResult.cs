/*---------------------------------------------------------------------------------- 
 * 类 名 称：ExtCache
 * 类 描 述：腾讯云API返回结果Model类
 * 作    者：Leo
 * 创建时间：2019/9/25 18:12:34
 * 更新时间：2019/9/25 18:12:34
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Loe.Framework.Models.QCloud
{
    public class QCloudApiResult<T> where T : class
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
    public class CosUpload
    {
        /// <summary>
        /// 通过 CDN 访问该文件的资源链接（访问速度更快）
        /// </summary>
        public string Access_url { get; set; }
        /// <summary>
        /// 该文件在 COS 中的相对路径名，可作为其 ID 标识。
        /// </summary>
        public string Resource_path { get; set; }
        /// <summary>
        /// 该文件在 COS 中的相对路径名，可作为其 ID 标识。
        /// </summary>
        public string Source_url { get; set; }
        /// <summary>
        /// 操作文件的 url 。业务端可以将该 url 作为请求地址来进一步操作文件，对应 API ：文件属性、更新文件、删除文件、移动文件中的请求地址。
        /// </summary>
        public string Url { get; set; }
    }

    /// <summary>
    /// 目录信息
    /// </summary>
    public class CosFolder
    {
        /// <summary>
        /// 透传字段，用于翻页，业务端不需理解，需要往后翻页则透传给腾讯云
        /// </summary>
        public string Context { get; set; }
        /// <summary>
        /// 是否有内容可以继续往后翻页
        /// </summary>
        public bool Listover { get; set; }
        /// <summary>
        /// 文件和文件夹列表，若当前目录下不存在文件或文件夹，则该返回值可能为空
        /// </summary>
        public List<Folder> Infos { get; set; }
    }

    /// <summary>
    /// 文件或文件夹信息
    /// </summary>
    public class Folder
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件大小，当类型为文件时返回
        /// </summary>
        public int Filesize { get; set; }
        /// <summary>
        /// 文件已传输大小，当类型为文件时返回
        /// </summary>
        public int Filelen { get; set; }
        /// <summary>
        /// 文件 sha，当类型为文件时返回
        /// </summary>
        public string Sha { get; set; }
        /// <summary>
        /// 创建时间，10位 Unix 时间戳
        /// </summary>
        public long Ctime { get; set; }
        /// <summary>
        /// 修改时间，10位 Unix 时间戳
        /// </summary>
        public long Mtime { get; set; }
        /// <summary>
        /// 生成的资源可访问的cdn url，当类型为文件时返回
        /// </summary>
        public string Access_url { get; set; }
        /// <summary>
        /// 如果没有对文件单独设置该属性，则可能不会返回该字段。返回值：eInvalid（表示继承 bucket 的读写权限）；eWRPrivate（私有读写）；eWPrivateRPublic（公有读私有写）。说明：文件可以和 bucket 拥有不同的权限类型，已经设置过权限的文件如果想要撤销，将会直接被赋值为 eInvalid，即继承 bucket 的权限
        /// </summary>
        public string Authority { get; set; }
        /// <summary>
        /// 生成的资源可访问的源站 url，当类型为文件时返回
        /// </summary>
        public string Source_url { get; set; }
    }
}
