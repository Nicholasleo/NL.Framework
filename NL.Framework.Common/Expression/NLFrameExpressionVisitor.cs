using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-03 17:01:08 
//    说明：
//    版权所有：个人
//***********************************************************
namespace NL.Framework.Common
{
    public class NLFrameExpressionVisitor : ExpressionVisitor
    {
        public ParameterExpression _Parameter { get; set; }

        public NLFrameExpressionVisitor(ParameterExpression parameter)
        {
            _Parameter = parameter;
        }

        public override Expression Visit(Expression node)
        {
            return base.Visit(node);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            return _Parameter;
        }
    }
}
