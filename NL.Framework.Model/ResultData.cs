using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.Model
{
    public class ResultData
    {
        public int code { get; set; }
        public string msg { get; set; }
        public TokenData data { get; set; }
    }

    public class TokenData
    {
        public string access_token { get; set; }
    }
}
