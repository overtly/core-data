using System.Collections.Generic;
using System.Linq.Expressions;

namespace Overt.Core.Data.Expressions
{
    class NewArraySqlExpression : BaseSqlExpression<NewArrayExpression>
    {
        protected override SqlGenerate In(NewArrayExpression expression, SqlGenerate sqlGenerate)
        {
            var list = new List<object>();
            foreach (var expressionItem in expression.Expressions)
            {
                var obj = SqlExpressionCompiler.Evaluate(expressionItem);
                list.Add(obj);
            }
            sqlGenerate.AddDbParameter(list);
            return sqlGenerate;
        }
    }
}
