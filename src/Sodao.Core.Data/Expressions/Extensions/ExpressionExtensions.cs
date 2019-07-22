using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Sodao.Core.Data.Expressions
{
    /// <summary>
    /// 表达式扩展
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// 是否为空类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNullableType(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// 是否是
        /// </summary>
        /// <param name="type"></param>
        /// <param name="genericTypeDefinition"></param>
        /// <returns></returns>
        public static bool IsOrHasGenericInterfaceTypeOf(this Type type, Type genericTypeDefinition)
        {
            return (type.GetTypeWithGenericTypeDefinitionOf(genericTypeDefinition) != null)
                || (type == genericTypeDefinition);
        }

        /// <summary>
        /// 是否是泛型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="genericTypeDefinition"></param>
        /// <returns></returns>
        public static Type GetTypeWithGenericTypeDefinitionOf(this Type type, Type genericTypeDefinition)
        {
            foreach (var t in type.GetInterfaces())
            {
                if (t.IsGenericType && t.GetGenericTypeDefinition() == genericTypeDefinition)
                {
                    return t;
                }
            }

            var genericType = type.FirstGenericType();
            if (genericType != null && genericType.GetGenericTypeDefinition() == genericTypeDefinition)
            {
                return genericType;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type FirstGenericType(this Type type)
        {
            while (type != null)
            {
                if (type.IsGenericType)
                    return type;

                type = type.BaseType;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string GetMemberExpression(this MemberExpression m)
        {
            return m.Member.Name;
        }

        /// <summary>
        /// Determines whether the expression is the parameter.
        /// </summary>
        /// <returns>Returns true if the specified expression is parameter;
        /// otherwise, false.</returns>
        public static bool IsParameterAccess(Expression e)
        {
            return e.CheckExpressionForTypes(new[] { ExpressionType.Parameter });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool IsParameterOrConvertAccess(this Expression e)
        {
            return e.CheckExpressionForTypes(new[] { ExpressionType.Parameter, ExpressionType.Convert });
        }

        /// <summary>
        /// 检查类型
        /// </summary>
        /// <param name="e"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static bool CheckExpressionForTypes(this Expression e, ExpressionType[] types)
        {
            while (e != null)
            {
                if (types.Contains(e.NodeType))
                {
                    var subUnaryExpr = e as UnaryExpression;
                    var isSubExprAccess = subUnaryExpr?.Operand is IndexExpression;
                    if (!isSubExprAccess)
                        return true;
                }

                var binaryExpr = e as BinaryExpression;
                if (binaryExpr != null)
                {
                    if (CheckExpressionForTypes(binaryExpr.Left, types))
                        return true;

                    if (CheckExpressionForTypes(binaryExpr.Right, types))
                        return true;
                }

                var methodCallExpr = e as MethodCallExpression;
                if (methodCallExpr != null)
                {
                    for (var i = 0; i < methodCallExpr.Arguments.Count; i++)
                    {
                        if (CheckExpressionForTypes(methodCallExpr.Arguments[i], types))
                            return true;
                    }

                    if (CheckExpressionForTypes(methodCallExpr.Object, types))
                        return true;
                }

                var unaryExpr = e as UnaryExpression;
                if (unaryExpr != null)
                {
                    if (CheckExpressionForTypes(unaryExpr.Operand, types))
                        return true;
                }

                var condExpr = e as ConditionalExpression;
                if (condExpr != null)
                {
                    if (CheckExpressionForTypes(condExpr.Test, types))
                        return true;

                    if (CheckExpressionForTypes(condExpr.IfTrue, types))
                        return true;

                    if (CheckExpressionForTypes(condExpr.IfFalse, types))
                        return true;
                }

                var memberExpr = e as MemberExpression;
                e = memberExpr?.Expression;
            }

            return false;
        }

        /// <summary>
        /// 是否是Boolean
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool IsBooleanComparison(this Expression e)
        {
            if (!(e is MemberExpression))
                return false;

            var m = (MemberExpression)e;

            if (m.Member.DeclaringType.IsNullableType() &&
                m.Member.Name == "HasValue") //nameof(Nullable<bool>.HasValue)
                return false;

            return IsParameterAccess(m);
        }

        /// <summary>
        /// 获取Value
        /// </summary>
        /// <param name="binding"></param>
        /// <returns></returns>
        public static object GetValue(this MemberBinding binding)
        {
            switch (binding.BindingType)
            {
                case MemberBindingType.Assignment:
                    var assign = (MemberAssignment)binding;
                    if (assign.Expression is ConstantExpression constant)
                        return constant.Value;

                    try
                    {
                        return SqlExpressionCompiler.Evaluate(assign.Expression);
                    }
                    catch (Exception ex)
                    {
                        var member = Expression.Convert(assign.Expression, typeof(object));
                        var lambda = Expression.Lambda<Func<object>>(member);
                        var getter = lambda.Compile();
                        return getter();
                    }
            }
            return null;
        }

        /// <summary>
        /// 获取Fields
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static string[] GetFieldNames<T>(this Expression<Func<T, object>> expr)
        {
            if (expr == null)
                return null;

            if (expr.Body is MemberExpression member)
            {
                if (member.Member.DeclaringType.IsAssignableFrom(typeof(T)))
                    return new[] { member.Member.Name };

                var array = SqlExpressionCompiler.Evaluate(member);
                if (array is IEnumerable<string> strEnum)
                    return strEnum.ToArray();
            }

            if (expr.Body is NewExpression newExpr)
                return newExpr.Arguments.OfType<MemberExpression>().Select(x => x.Member.Name).ToArray();

            if (expr.Body is MemberInitExpression init)
                return init.Bindings.Select(x => x.Member.Name).ToArray();

            if (expr.Body is UnaryExpression unary)
            {
                member = unary.Operand as MemberExpression;
                if (member != null)
                    return new[] { member.Member.Name };
            }

            throw new ArgumentException("Invalid Fields List Expression: " + expr);
        }
    }
}
