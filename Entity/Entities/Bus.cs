using System;
using System.Collections.Generic;
using System.Text;

namespace GineCore.Entity.Entities
{
    public class Bus: BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 座位数量
        /// </summary>
        public int? SeatCount { get; set; }
        /// <summary>
        /// 配置（厨房 厕所）
        /// </summary>
        public string Configuration { get; set; }
        /// <summary>
        /// 驾照类型
        /// </summary>
        public string DrivingLicense { get; set; }
        /// <summary>
        /// 车龄
        /// </summary>
        public int? Year { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// 所属站点
        /// </summary>
        public int? SiteId { get; set; }
        /// <summary>
        /// 车辆描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 车辆牌照
        /// </summary>
        public string LicensePlate { get; set; }
    }
}
