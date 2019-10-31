using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Service.MenusService
{
    public class MenusTypeEnum
    {
        public enum MenuBtnTypeEnum
        {
            [Description("添加")]
            Add,

            [Description("修改")]
            Update,

            [Description("删除")]
            Delete,

            [Description("查询")]
            Search,

            [Description("导出excel")]
            ExpertExcel
        }
    }
}
