using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.Model
{
    public class Pagination
    {
        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int Records { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int Total
        {
            get
            {
                if (Records > 0)
                {
                    return Records % this.PageSize == 0 ? Records / this.PageSize : Records / this.PageSize + 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
