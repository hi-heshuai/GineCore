using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Common
{
    public class ResponseModel<T>
    {
        /// <summary>
        /// 状态
        /// </summary>
        public int code { get; set; } = 200;

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool result { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string errorInfo { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public T data { get; set; }
    }
}
