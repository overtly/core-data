using System;
using System.Linq.Expressions;

namespace Overt.Core.Data.Expressions
{
    class BinarySqlExpression : BaseSqlExpression<BinaryExpression>
    {
        private void OperatorParser(ExpressionType expressionNodeType, int operatorIndex, SqlGenerate sqlGenerate, bool useIs = false)
        {
            switch (expressionNodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    sqlGenerate.Sql.Insert(operatorIndex, " and ");
                    break;
                case ExpressionType.Equal:
                    if (useIs)
                        sqlGenerate.Sql.Insert(operatorIndex, " is ");
                    else
                        sqlGenerate.Sql.Insert(operatorIndex, " = ");
                    break;
                case ExpressionType.GreaterThan:
                    sqlGenerate.Sql.Insert(operatorIndex, " >");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    sqlGenerate.Sql.Insert(operatorIndex, " >=");
                    break;
                case ExpressionType.NotEqual:
                    if (useIs)
                        sqlGenerate.Sql.Insert(operatorIndex, " is not ");
                    else
                        sqlGenerate.Sql.Insert(operatorIndex, " <> ");
                    break;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    sqlGenerate.Sql.Insert(operatorIndex, " or ");
                    break;
                case ExpressionType.LessThan:
                    sqlGenerate.Sql.Insert(operatorIndex, " < ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    sqlGenerate.Sql.Insert(operatorIndex, " <= ");
                    break;
                default:
                    throw new NotImplementedException("未实现的节点类型" + expressionNodeType);
            }
        }

        protected override SqlGenerate Where(BinaryExpression expression, SqlGenerate sqlGenerate)
        {
            int leftBracketIndex = -1, rightBracketIndex = -1, signIndex = -1, sqlLength = -1;

            leftBracketIndex = sqlGenerate.Length;

            #region 内部内容
            if (expression.NodeType == ExpressionType.AndAlso || expression.NodeType == ExpressionType.OrElse)
            {
                if (expression.Left.IsBooleanComparison())
                {
                    SqlExpressionProvider.Where(expression.Left, sqlGenerate);
                    sqlGenerate += $" = 1";
                }
                else
                {
                    SqlExpressionProvider.Where(expression.Left, sqlGenerate);
                }
                signIndex = sqlGenerate.Length;


                if (expression.Right.IsBooleanComparison())
                {
                    SqlExpressionProvider.Where(expression.Right, sqlGenerate);
                    sqlGenerate += $" = 1";
                }
                else
                {
                    SqlExpressionProvider.Where(expression.Right, sqlGenerate);
                }
                sqlLength = sqlGenerate.Length;
            }
            else
            {
                SqlExpressionProvider.Where(expression.Left, sqlGenerate);
                signIndex = sqlGenerate.Length;

                SqlExpressionProvider.Where(expression.Right, sqlGenerate);
                sqlLength = sqlGenerate.Length;
            }

            if (sqlLength - signIndex == 5 && sqlGenerate.ToString().EndsWith("null"))
                OperatorParser(expression.NodeType, signIndex, sqlGenerate, true);
            else
                OperatorParser(expression.NodeType, signIndex, sqlGenerate);
            #endregion

            if (expression.NodeType == ExpressionType.OrElse || expression.NodeType == ExpressionType.AndAlso)
            {
                sqlGenerate.Sql.Insert(leftBracketIndex, " ( ");
                rightBracketIndex = sqlGenerate.Length;
                sqlGenerate.Sql.Insert(rightBracketIndex, " ) ");
            }

            return sqlGenerate;
        }
    }
}