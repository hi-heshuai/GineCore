using System;
using System.Collections.Generic;
using System.Text;

namespace GineCore.Common.Orc
{
    public class OCRResultModel
    {
        /// <summary>
        /// 处理进度（0-100）之间的数值
        /// </summary>
        public int percentage { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string status { get; set; }

        /// <summary>
        ///  处理信息，一般status= ERROR时，通过info确定原因
        /// </summary>
        public string info { get; set; }

        /// <summary>
        /// 处理信息ID,通过该ID跟踪该任务
        /// </summary>
        public string processId { get; set; }
    }

    /// <summary>
    /// ocr状态model
    /// </summary>
    public class OCRStatusModel
    {
        public int percentage { get; set; }

        public int status { get; set; }

        public string info { get; set; }

        public string processId { get; set; }
    }
}
