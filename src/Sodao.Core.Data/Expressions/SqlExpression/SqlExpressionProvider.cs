using System;
using System.Linq.Expressions;

namespace Sodao.Core.Data.Expressions
{
    internal class SqlExpressionProvider
    {
        internal static ISqlExpression GetSqlExpression(Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression", "不能为null");
            }

            if (expression is LambdaExpression)
            {
                return new LambdaSqlExpression();
            }

            if (expression is BinaryExpression)
            {
                return new BinarySqlExpression();
            }
            if (expression is BlockExpression)
            {
                throw new NotImplementedException("未实现的BlockSqlExpression");
            }
            if (expression is ConditionalExpression)
            {
                throw new NotImplementedException("未实现的ConditionalSqlExpression");
            }
            if (expression is ConstantExpression)
            {
                return new ConstantSqlExpression();
            }
            if (expression is DebugInfoExpression)
            {
                throw new NotImplementedException("未实现的DebugInfoSqlExpression");
            }
            if (expression is DefaultExpression)
            {
                throw new NotImplementedException("未实现的DefaultSqlExpression");
            }
            if (expression is DynamicExpression)
            {
                throw new NotImplementedException("未实现的DynamicSqlExpression");
            }
            if (expression is GotoExpression)
            {
                throw new NotImplementedException("未实现的GotoSqlExpression");
            }
            if (expression is IndexExpression)
            {
                throw new NotImplementedException("未实现的IndexSqlExpression");
            }
            if (expression is InvocationExpression)
            {
                throw new NotImplementedException("未实现的InvocationSqlExpression");
            }
            if (expression is LabelExpression)
            {
                throw new NotImplementedException("未实现的LabelSqlExpression");
            }
            if (expression is LambdaExpression)
            {
                throw new NotImplementedException("未实现的LambdaSqlExpression");
            }
            if (expression is ListInitExpression)
            {
                throw new NotImplementedException("未实现的ListInitSqlExpression");
            }
            if (expression is LoopExpression)
            {
                throw new NotImplementedException("未实现的LoopSqlExpression");
            }
            if (expression is MemberExpression)
            {
                return new MemberSqlExpression();
            }
            if (expression is MemberInitExpression)
            {
                throw new NotImplementedException("未实现的MemberInitSqlExpression");
            }
            if (expression is MethodCallExpression)
            {
                return new MethodCallSqlExpression();
            }
            if (expression is NewArrayExpression)
            {
                return new NewArraySqlExpression();
            }
            if (expression is NewExpression)
            {
                return new NewSqlExpression();
            }
            if (expression is ParameterExpression)
            {
                throw new NotImplementedException("未实现的ParameterSqlExpression");
            }
            if (expression is RuntimeVariablesExpression)
            {
                throw new NotImplementedException("未实现的RuntimeVariablesSqlExpression");
            }
            if (expression is SwitchExpression)
            {
                throw new NotImplementedException("未实现的SwitchSqlExpression");
            }
            if (expression is TryExpression)
            {
                throw new NotImplementedException("未实现的TrySqlExpression");
            }
            if (expression is TypeBinaryExpression)
            {
                throw new NotImplementedException("未实现的TypeBinarySqlExpression");
            }
            if (expression is UnaryExpression)
            {
                return new UnarySqlExpression();
            }
            throw new NotImplementedException("未实现的SqlExpression");
        }

        public static void Update(Expression expression, SqlGenerate sqlGenerate)
        {
            GetSqlExpression(expression).Update(expression, sqlGenerate);
        }

        public static void Select(Expression expression, SqlGenerate sqlGenerate)
        {
            GetSqlExpression(expression).Select(expression, sqlGenerate);
        }

        public static void Where(Expression expression, SqlGenerate sqlGenerate)
        {
            GetSqlExpression(expression).Where(expression, sqlGenerate);
        }

        public static void In(Expression expression, SqlGenerate sqlGenerate)
        {
            GetSqlExpression(expression).In(expression, sqlGenerate);
        }

        public static void OrderBy(Expression expression, SqlGenerate sqlGenerate)
        {
            GetSqlExpression(expression).OrderBy(expression, sqlGenerate);
        }

        public static void Max(Expression expression, SqlGenerate sqlGenerate)
        {
            GetSqlExpression(expression).Max(expression, sqlGenerate);
        }

        public static void Min(Expression expression, SqlGenerate sqlGenerate)
        {
            GetSqlExpression(expression).Min(expression, sqlGenerate);
        }

        public static void Avg(Expression expression, SqlGenerate sqlGenerate)
        {
            GetSqlExpression(expression).Avg(expression, sqlGenerate);
        }

        public static void Count(Expression expression, SqlGenerate sqlGenerate)
        {
            GetSqlExpression(expression).Count(expression, sqlGenerate);
        }

        public static void Sum(Expression expression, SqlGenerate sqlGenerate)
        {
            GetSqlExpression(expression).Sum(expression, sqlGenerate);
        }
    }
}
