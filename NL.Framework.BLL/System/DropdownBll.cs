//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-12 17:54:39
//    说明：
//    版权所有：个人
//***********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
using NL.Framework.Common;
using NL.Framework.Common.Log;
using NL.Framework.IBLL;
using NL.Framework.IDAL;
using NL.Framework.Model;
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

        public override AjaxResultEnt Create(DropDownOptionsModel model)
        {
            AjaxResultEnt result = new AjaxResultEnt();
            if (_context.IsExist<DropDownOptionsModel>(t => t.OptionsCode.Equals(model.OptionsCode)))
            {
                result.Code = 501;
                result.Message = $"{model.OptionsCode}已存在!";
                return result;
            }
            model.CreatePerson = OperatorProvider.Provider.GetCurrent().UserName;
            model.CreateTime = DateTime.Now;
            Guid fid = Guid.NewGuid();
            model.Fid = fid;
            result.ExtMsg = fid;
            int i = _context.Insert<DropDownOptionsModel>(model);
            if (i > 0)
            {
                result.Code = 200;
                result.Message = "添加成功!";
            }
            else
            {
                result.Code = 503;
                result.Message = "添加失败!";
            }
            return result;

        }

        public override List<DropDownOptionsModel> GetLists(int page, int limit, out int total, object obj)
        {
            string filtter = obj.ToString();
            Expression<Func<DropDownOptionsModel, bool>> where = null;
            if (!string.IsNullOrEmpty(filtter))
                where = t => t.Fid.ToString().Equals(filtter) || t.ParentId.ToString().Equals(filtter);
            where = t => t.ParentId.Equals(Guid.Empty);
            IQueryable data = _context.GetLists<DropDownOptionsModel>(page, limit, out total, where);
            List<DropDownOptionsModel> result = new List<DropDownOptionsModel>();
            foreach (DropDownOptionsModel item in data)
            {
                result.Add(item);
            }
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"获取下拉列表：{JsonConvert.SerializeObject(result)}");
            }
            return result;
        }

        public List<DropDownTreeEnt> GetTreeLists(Guid fid)
        {
            List<DropDownTreeEnt> lists = new List<DropDownTreeEnt>();
            //通过fid获取对应的根节点
            DropDownOptionsModel root = _context.GetEntity<DropDownOptionsModel>(fid);
            if (root == null)
                return lists;
            bool isLast = false;
            DropDownTreeEnt ent = new DropDownTreeEnt();
            ent.Id = root.Fid;
            ent.Name = root.MyName;
            ent.ParentId = root.ParentId;
            ent.Childrens = GetChildNodes(root.Fid,out isLast);
            ent.Leaf = ent.Childrens.Count > 0;
            ent.Last = ent.Childrens.Count <= 0;
            lists.Add(ent);
            return lists;
        }

        private List<DropDownTreeEnt> GetChildNodes(Guid id,out bool isLast)
        {
            isLast = true;
            List<DropDownTreeEnt> list = new List<DropDownTreeEnt>();
            IQueryable nodes = _context.GetLists<DropDownOptionsModel>(t => t.ParentId.Equals(id));
            if (nodes != null)
            {
                foreach (DropDownOptionsModel node in nodes)
                {
                    DropDownTreeEnt treeData = new DropDownTreeEnt();
                    treeData.Id = node.Fid;
                    treeData.ParentId = node.ParentId;
                    treeData.Name = node.MyName;
                    treeData.Childrens = GetChildNodes(node.Fid,out isLast);
                    treeData.Leaf = treeData.Childrens.Count > 0;
                    isLast = treeData.Childrens.Count <= 0;
                    treeData.Last = isLast;
                    list.Add(treeData);
                }
            }
            return list;
        }
    }
}
