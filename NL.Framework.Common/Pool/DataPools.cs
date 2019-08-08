//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-07 18:24:18
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.Common
{
   public  class DataPools
    {
        private static readonly DataPools _instance = null;

        public static DataPools Instance
        {
            get {
                if (_instance == null)
                    return new DataPools();
                return _instance;
            }
        }

        public void SetLoginInfo(LoginUserEnt data)
        {
            _LoginInfo = data;
        }

        private static LoginUserEnt _LoginInfo = null;

        public static LoginUserEnt LoginInfo
        {
            get {
                return _LoginInfo;
            }
            private set {
                if (_LoginInfo == null)
                    _LoginInfo = value;
            }
        }
    }
}
