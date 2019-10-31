using System;
using System.Collections.Generic;
using System.Text;

namespace GineCore.Entity.Entities
{
    public class Resources : BaseEntity
    {
        public int Id { get; set; }
        /// <summary>
        /// 资源所属模块  （例如 个人驾照，身份证图片，个人头像，站点图片，车辆图片等  以枚举为实）
        /// </summary>
        public string ResourceType { get; set; }
        /// <summary>
        /// 对应模块的id（例如 人员id）
        /// </summary>
        public string ResourceId { get; set; }
        /// <summary>
        /// 图片存储路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 文件类型  （图片，视频, 本站点暂时为图片）
        /// </summary>
        public string Type { get; set; }
    }
}
