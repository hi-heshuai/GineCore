using System;
using System.Collections.Generic;
using System.Text;

namespace GineCore.Entity.Entities
{
    public class Site: BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal Latitude { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 详情描述
        /// </summary>
        public string Description { get; set; }
    }
}
