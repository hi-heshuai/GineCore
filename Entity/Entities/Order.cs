using System;
using System.Collections.Generic;
using System.Text;

namespace GineCore.Entity.Entities
{
    public class Order: BaseEntity
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? BusId { get; set; }
        /// <summary>
        /// 应付金额
        /// </summary>
        public decimal? Payable { get; set; }
        /// <summary>
        /// 实付金额
        /// </summary>
        public decimal? Payment { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PayAt { get; set; }
        /// <summary>
        /// 支付类型 （支付宝 现金）
        /// </summary>
        public string PayType { get; set; }
        /// <summary>
        /// 赔付金额
        /// </summary>
        public decimal? Compensation { get; set; }
        /// <summary>
        /// 保证金
        /// </summary>
        public decimal? SecurityDeposit { get; set; }
        /// <summary>
        /// 开始租车时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 实际归还时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 预计归还时间
        /// </summary>
        public DateTime? EstEndTime { get; set; }
        /// <summary>
        /// 开始租车站点
        /// </summary>
        public int? StartSiteId { get; set; }
        /// <summary>
        /// 实际归还站点
        /// </summary>
        public int? EndSiteId { get; set; }
        /// <summary>
        /// 预计归还站点
        /// </summary>
        public int? EstEndSiteId { get; set; }
        /// <summary>
        /// 状态(completed已完成，processing进行中，cancelled已取消，error异常-赔付中  具体看枚举)
        /// </summary>
        public string Status { get; set; }
    }
}
