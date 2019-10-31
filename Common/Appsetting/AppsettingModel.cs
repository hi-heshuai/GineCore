using System;
using System.Collections.Generic;
using System.Text;

namespace GineCore.Common.Appsetting
{
    public class AppsettingModel
    {
        //api ip+端口 
        public string IpAndPoint { get; set; }
        //默认密码
        public string DefaultPassword { get; set; }

        //用户默认头像
        public string DefaultHeadPic { get; set; }

        //VERSWorkExamDB 库地址
        public string VERSWorkExamDB { get; set; }
    }
}
