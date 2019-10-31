using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Common
{
    public class JqGridModel<T>
    {
        public int page { get; set; } = 1;

        public int total { get; set; } = 0;

        public int records { get; set; } = 0;

        public List<T> rows { get; set; } = new List<T>();
    }

    public class JqgridParam
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int page { get; set; } = 1;

        /// <summary>
        /// 数据数量
        /// </summary>
        public int rows { get; set; } = 15;

        /// <summary>
        /// 排序字段
        /// </summary>
        public string sidx { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public string sord { get; set; }
    }
}
