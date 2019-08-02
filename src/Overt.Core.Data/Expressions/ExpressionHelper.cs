using System.Collections.Generic;
using System.Linq.Expressions;

namespace Overt.Core.Data.Expressions
{
    /// <summary>
    /// 解析为 Dictionary 获取表达式中的key - value
    /// 只需要有等于号的
    /// </summary>
    internal class ExpressionHelper
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="dictionary"></param>
        public static void Resolve(Expression expression, ref Dictionary<object, object> dictionary)
        {
            if (expression == null)
                return;

            if (expression is BinaryExpression)
            {
                var binaryExpression = ((BinaryExpression)expression);
                ResolveBinary(binaryExpression, ref dictionary);
            }
        }

        #region Binary
        private static void ResolveBinary(BinaryExpression expression, ref Dictionary<object, object> dictionary)
        {
            object left = null, right = null;
            if (expression.NodeType == ExpressionType.AndAlso || expression.NodeType == ExpressionType.OrElse)
            {
                if (expression.Left.IsBooleanComparison())
                    left = ResolveMemberOrConstant(expression.Left);
                else
                    Resolve(expression.Left, ref dictionary);

                if (expression.Right.IsBooleanComparison())
                    right = ResolveMemberOrConstant(expression.Right);
                else
                    Resolve(expression.Right, ref dictionary);
            }
            else if (expression.NodeType == ExpressionType.Equal)
            {
                left = ResolveMemberOrConstant(expression.Left);
                right = ResolveMemberOrConstant(expression.Right);
            }

            if (left != null && !dictionary.ContainsKey(left))
            {
                dictionary.Add(left, right);
            }
        }
        #endregion

        #region Member / Constant
        private static object ResolveMemberOrConstant(Expression expression)
        {
            if (expression == null)
                return null;

            if (expression is MemberExpression)
                return ResolveMember((MemberExpression)expression);
            if (expression is ConstantExpression)
                return ResolveConstant((ConstantExpression)expression);
            return null;
        }

        private static object ResolveMember(MemberExpression expression)
        {
            if (expression.Expression != null)
            {
                if (expression.Member.DeclaringType.IsNullableType())
                    return null;

                if (expression.IsParameterOrConvertAccess())
                    return expression.Member.Name;
            }

            var val = SqlExpressionCompiler.Evaluate(expression);
            return val;
        }

        private static object ResolveConstant(ConstantExpression expression)
        {
            return expression.Value;
        }
        #endregion
    }
}
