using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.Model
{
    public class ResultData : StatusData
    {
        public TokenData data { get; set; }
    }

    public class StatusData
    {
        public int code { get; set; }
        public string msg { get; set; }
    }

    public class TokenData
    {
        public string access_token { get; set; }
    }

    public class AjaxResultData<T> : StatusData
    {
        public string count { get; set; }
        //public List<T> data { get; set; }
        public string data { get; set; }
    }
}
