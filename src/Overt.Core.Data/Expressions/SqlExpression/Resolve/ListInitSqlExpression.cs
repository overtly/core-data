using System.Collections.Generic;
using System.Linq.Expressions;

namespace Overt.Core.Data.Expressions
{
    class ListInitSqlExpression : BaseSqlExpression<ListInitExpression>
    {
        protected override SqlGenerate In(ListInitExpression expression, SqlGenerate sqlGenerate)
        {
            var list = new List<object>();
            foreach (var elementInit in expression.Initializers)
            {
                foreach (var expre in elementInit.Arguments)
                {
                    var obj = SqlExpressionCompiler.Evaluate(expre);
                    list.Add(obj);
                }
            }
            sqlGenerate.AddDbParameter(list);
            return sqlGenerate;
        }

        protected override SqlGenerate Select(ListInitExpression expression, SqlGenerate sqlGenerate)
        {
            foreach (var elementInit in expression.Initializers)
            {
                foreach (var expre in elementInit.Arguments)
                {
                    var obj = SqlExpressionCompiler.Evaluate(expre);
                    if (obj == null)
                        continue;

                    var fieldName = obj.ToString();
                    if (string.IsNullOrEmpty(fieldName))
                        continue;
                    sqlGenerate.SelectFields.Add(fieldName.ParamSql(sqlGenerate));
                }
            }
            return sqlGenerate;
        }
    }
}
