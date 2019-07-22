using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Sodao.Core.Data.Expressions
{
    class MemberSqlExpression : BaseSqlExpression<MemberExpression>
    {
        protected override SqlGenerate Select(MemberExpression expression, SqlGenerate sqlGenerate)
        {
            sqlGenerate.SelectFields.Add(expression.Member.Name.ParamSql(sqlGenerate));
            return sqlGenerate;
        }

        protected override SqlGenerate Where(MemberExpression expression, SqlGenerate sqlGenerate)
        {
            if (expression.Expression != null)
            {
                if (expression.Member.DeclaringType.IsNullableType())
                {
                    if (expression.Member.Name == "Value") //Can't use C# 6 yet: nameof(Nullable<bool>.Value)
                    {
                        SqlExpressionProvider.Where(expression.Expression, sqlGenerate);
                        return sqlGenerate;
                    }
                    if (expression.Member.Name == "HasValue")
                    {
                        var doesNotEqualNull = Expression.MakeBinary(ExpressionType.NotEqual, expression.Expression, Expression.Constant(null));
                        SqlExpressionProvider.Where(doesNotEqualNull, sqlGenerate);
                        return sqlGenerate;
                    }
                    throw new ArgumentException($"Expression '{expression}' accesses unsupported property '{expression.Member}' of Nullable<T>");
                }

                if (expression.IsParameterOrConvertAccess())
                {
                    sqlGenerate += $" {expression.Member.Name.ParamSql(sqlGenerate)}";
                    return sqlGenerate;
                }
            }

            var val = SqlExpressionCompiler.Evaluate(expression);
            sqlGenerate.AddDbParameter(val);
            return sqlGenerate;

        }

        protected override SqlGenerate In(MemberExpression expression, SqlGenerate sqlGenerate)
        {
            var result = SqlExpressionCompiler.Evaluate(expression);
            var inArgs = (result as IEnumerable).Flatten();

            sqlGenerate += "(";
            if (inArgs?.Count > 0)
            {
                foreach (var item in inArgs)
                {
                    sqlGenerate.AddDbParameter(item);
                    sqlGenerate += ",";
                }
                if (sqlGenerate.Sql.ToString().EndsWith(","))
                    sqlGenerate.Sql.Remove(sqlGenerate.Sql.Length - 1, 1);
            }
            else
            {
                sqlGenerate += "NULL";
            }
            sqlGenerate += ")";
            return sqlGenerate;
        }

        protected override SqlGenerate OrderBy(MemberExpression expression, SqlGenerate sqlGenerate)
        {
            sqlGenerate += expression.Member.Name.ParamSql(sqlGenerate);
            return sqlGenerate;
        }

        protected override SqlGenerate Update(MemberExpression expression, SqlGenerate sqlGenerate)
        {
            var obj = SqlExpressionCompiler.Evaluate(expression);
            if (obj == null)
                throw new ArgumentException($"Expression '{expression}' accesses unsupported property '{expression.Member}' of Nullable<T>");

            if (obj.GetType().IsValueType)
                throw new ArgumentException($"Expression '{expression}' accesses unsupported valuetype");

            if (obj is IDictionary)
            {
                IDictionary dictionary = (IDictionary)obj;
                foreach (string key in dictionary.Keys)
                {
                    sqlGenerate += $"{key.ParamSql(sqlGenerate)} = ";
                    sqlGenerate.AddDbParameter(dictionary[key]);
                    sqlGenerate += ",";
                }
            }
            else
            {
                var pis = obj.GetType().GetProperties();
                foreach (var p in pis)
                {
                    sqlGenerate += $"{p.Name.ParamSql(sqlGenerate)} = ";
                    sqlGenerate.AddDbParameter(p.GetValue(obj));
                    sqlGenerate += ",";
                }
            }

            if (sqlGenerate[sqlGenerate.Length - 1] == ',')
                sqlGenerate.Sql.Remove(sqlGenerate.Length - 1, 1);

            return sqlGenerate;
        }

        protected override SqlGenerate Max(MemberExpression expression, SqlGenerate sqlGenerate)
        {
            sqlGenerate.Sql.AppendFormat("select max({0}) from {1}", expression.Member.Name.ParamSql(sqlGenerate), sqlGenerate.TableName);
            return sqlGenerate;
        }

        protected override SqlGenerate Min(MemberExpression expression, SqlGenerate sqlGenerate)
        {
            sqlGenerate.Sql.AppendFormat("select min({0}) from {1}", expression.Member.Name.ParamSql(sqlGenerate), sqlGenerate.TableName);
            return sqlGenerate;
        }

        protected override SqlGenerate Avg(MemberExpression expression, SqlGenerate sqlGenerate)
        {
            sqlGenerate.Sql.AppendFormat("select avg({0}) from {1}", expression.Member.Name.ParamSql(sqlGenerate), sqlGenerate.TableName);
            return sqlGenerate;
        }

        protected override SqlGenerate Count(MemberExpression expression, SqlGenerate sqlGenerate)
        {
            sqlGenerate.Sql.AppendFormat("select count({0}) from {1}", expression.Member.Name.ParamSql(sqlGenerate), sqlGenerate.TableName);
            return sqlGenerate;
        }

        protected override SqlGenerate Sum(MemberExpression expression, SqlGenerate sqlGenerate)
        {
            sqlGenerate.Sql.AppendFormat("select sum({0}) from {1}", expression.Member.Name.ParamSql(sqlGenerate), sqlGenerate.TableName);
            return sqlGenerate;
        }
    }
}