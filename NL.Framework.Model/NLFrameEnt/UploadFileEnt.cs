//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-17 16:52:19
//    说明：
//    版权所有：个人
//***********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.Model
{
    public class UploadFileEnt
    {
        /// <summary>
        /// 返回状态 200 404 503
        /// </summary>
        public int ResultState { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string ResultMsg { get; set; }
        /// <summary>
        /// 返回存储路径
        /// </summary>
        public string ResultPath { get; set; }
    }
}
