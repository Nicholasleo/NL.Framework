//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-12 17:54:39
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Common.Log;
using NL.Framework.IBLL;
using NL.Framework.IDAL;
using NL.Framework.Model.System;

namespace NL.Framework.BLL
{
    public partial class DropdownBll : CommonBll<DropDownOptionsModel>, IDropdownBll
    {
        private readonly IDbContext _context;
        private readonly ILogger _ILogger;

        public DropdownBll(IDbContext db, ILogger logger) : base(db, logger)
        {
            _ILogger = logger;
            _context = db;
        }
    }
}
