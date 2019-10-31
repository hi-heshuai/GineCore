using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Common
{
    /// <summary>
    /// 分页
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// 每页最大行数
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 排序列
        /// </summary>
        public string sidx { get; set; }

        /// <summary>
        /// 排序类型
        /// </summary>
        public string sortType { get; set; }

        /// <summary>
        /// 数据总条数
        /// </summary>
        public int recordNum { get; set; }

        /// <summary>
        /// 数据总页数
        /// </summary>
        public int total
        {
            get
            {
                if (recordNum > 0)
                {
                    return recordNum % pageSize == 0 ? recordNum / pageSize : recordNum / pageSize + 1;
                }
                else {
                    return 0;
                }
            }
        }
    }
}
