//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-11 16:03:06
//    说明：
//    版权所有：个人
//***********************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.Common.Office
{
    public interface IExcel
    {
        /// <summary>
        /// 获取Excel内容
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        DataTable GetTableFromExcel(string filename);
        DataTable GetTableFromExcel(string filename,bool hasMapField);

        /// <summary>
        /// 获取指定sheet的内容
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="sheetname"></param>
        /// <returns></returns>
        DataTable GetTableFromExcel(string filename,string sheetname);
        DataTable GetTableFromExcel(string filename, string sheetname, bool hasMapField);
        /// <summary>
        /// 获取excel中所有sheet的数据
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        DataSet GetSetFromExcel(string filename);
        void ExcprtToExcel(DataTable dt, string filename);
        void ExcprtToExcel(DataTable dt, string filename,string sheetname);
        void ExportToExcel(DataSet ds, string filename);
    }
}
