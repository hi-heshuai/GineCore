using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GineCore.Service.PlanService
{
    public enum PlanEnum
    {
        [Description("计划")]
        Plan = 0,

        [Description("开始")]
        Start = 1,

        [Description("暂停")]
        Suspend = 2,

        [Description("失败")]
        Fail = 3,

        [Description("完成")]
        Done
    }
}
