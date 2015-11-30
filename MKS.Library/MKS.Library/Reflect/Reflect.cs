using System;
using System.Linq.Expressions;
using System.Reflection;
using MKS.Library.Ressources;

namespace MKS.Library
{
    /// <summary>
    ///   Provides strong-typed reflection of the <typeparamref name="TTarget" />
    ///   type.
    /// </summary>
    /// <typeparam name="TTarget"> Type to reflect. </typeparam>
    public static class Reflect<TTarget>
    {
        /// <summary>
        ///   Gets the property or field name.
        /// </summary>
        /// <param name="property"> </param>
        /// <returns> </returns>
        public static string GetName(Expression<Func<TTarget, object>> property)
        {
            try
            {
                return (GetProperty(property).Name);
            }
            catch (ArgumentException e)
            {
                try
                {
                    return (GetField(property).Name);
                }
                catch (ArgumentException e2)
                {
                    throw new ArgumentException(e2.Message, e.Message);
                }
            }
        }

        /// <summary>
        ///   Gets the method represented by the lambda expression.
        /// </summary>
        /// <exception cref="ArgumentNullException">The
        ///   <paramref name="method" />
        ///   is null.</exception>
        /// <exception cref="ArgumentException">The
        ///   <paramref name="method" />
        ///   is not a lambda expression or it does not represent a method invocation.</exception>
        public static MethodInfo GetMethod(Expression<Action<TTarget>> method)
        {
            return GetMethodInfo(method);
        }

        /// <summary>
        ///   Gets the method represented by the lambda expression.
        /// </summary>
        /// <exception cref="ArgumentNullException">The
        ///   <paramref name="method" />
        ///   is null.</exception>
        /// <exception cref="ArgumentException">The
        ///   <paramref name="method" />
        ///   is not a lambda expression or it does not represent a method invocation.</exception>
        public static MethodInfo GetMethod<T1>(Expression<Action<TTarget, T1>> method)
        {
            return GetMethodInfo(method);
        }

        /// <summary>
        ///   Gets the method represented by the lambda expression.
        /// </summary>
        /// <exception cref="ArgumentNullException">The
        ///   <paramref name="method" />
        ///   is null.</exception>
        /// <exception cref="ArgumentException">The
        ///   <paramref name="method" />
        ///   is not a lambda expression or it does not represent a method invocation.</exception>
        public static MethodInfo GetMethod<T1, T2>(Expression<Action<TTarget, T1, T2>> method)
        {
            return GetMethodInfo(method);
        }

        /// <summary>
        ///   Gets the method represented by the lambda expression.
        /// </summary>
        /// <exception cref="ArgumentNullException">The
        ///   <paramref name="method" />
        ///   is null.</exception>
        /// <exception cref="ArgumentException">The
        ///   <paramref name="method" />
        ///   is not a lambda expression or it does not represent a method invocation.</exception>
        public static MethodInfo GetMethod<T1, T2, T3>(Expression<Action<TTarget, T1, T2, T3>> method)
        {
            return GetMethodInfo(method);
        }

        /// <summary>
        ///   Gets the method informations.
        /// </summary>
        /// <param name="method"> </param>
        /// <returns> </returns>
        private static MethodInfo GetMethodInfo(Expression method)
        {
            if (method == null) throw new ArgumentNullException("method");

            var lambda = method as LambdaExpression;
            if (lambda == null)
                throw new ArgumentException(ErrorMessages.EX0032, "method");
            if (lambda.Body.NodeType != ExpressionType.Call)
                throw new ArgumentException(ErrorMessages.EX0033, "method");

            return ((MethodCallExpression) lambda.Body).Method;
        }

        /// <summary>
        ///   Gets the property represented by the lambda expression.
        /// </summary>
        /// <exception cref="ArgumentNullException">The
        ///   <paramref name="property" />
        ///   is null.</exception>
        /// <exception cref="ArgumentException">The
        ///   <paramref name="property" />
        ///   is not a lambda expression or it does not represent a property access.</exception>
        public static PropertyInfo GetProperty(Expression<Func<TTarget, object>> property)
        {
            var info = GetMemberInfo(property) as PropertyInfo;
            if (info == null) throw new ArgumentException("Member is not a property");

            return info;
        }

        /// <summary>
        ///   Gets the property represented by the lambda expression.
        /// </summary>
        /// <typeparam name="P"> Type assigned to the property </typeparam>
        /// <param name="property"> Property Expression </param>
        /// <returns> </returns>
        /// <exception cref="ArgumentNullException">The
        ///   <paramref name="property" />
        ///   is null.</exception>
        /// <exception cref="ArgumentException">The
        ///   <paramref name="property" />
        ///   is not a lambda expression or it does not represent a property access.</exception>
        public static PropertyInfo GetProperty<P>(Expression<Func<TTarget, P>> property)
        {
            var info = GetMemberInfo(property) as PropertyInfo;
            if (info == null) throw new ArgumentException("Member is not a property");

            return info;
        }

        /// <summary>
        ///   Gets the field represented by the lambda expression.
        /// </summary>
        /// <exception cref="ArgumentNullException">The
        ///   <paramref name="field" />
        ///   is null.</exception>
        /// <exception cref="ArgumentException">The
        ///   <paramref name="field" />
        ///   is not a lambda expression or it does not represent a field access.</exception>
        public static FieldInfo GetField(Expression<Func<TTarget, object>> field)
        {
            var info = GetMemberInfo(field) as FieldInfo;
            if (info == null) throw new ArgumentException("Member is not a field");

            return info;
        }

        /// <summary>
        ///   Gets the member informations.
        /// </summary>
        /// <param name="member"> </param>
        /// <returns> </returns>
        private static MemberInfo GetMemberInfo(Expression member)
        {
            if (member == null) throw new ArgumentNullException("member");

            var lambda = member as LambdaExpression;
            if (lambda == null)
                throw new ArgumentException(ErrorMessages.EX0032, "member");

            MemberExpression memberExpr = null;

            // The Func<TTarget, object> we use returns an object, so first statement can be either
            // a cast (if the field/property does not return an object) or the direct member access.
            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                // The cast is an unary expression, where the operand is the
                // actual member access expression.
                memberExpr = ((UnaryExpression) lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
                throw new ArgumentException(ErrorMessages.EX0031, "member");

            return memberExpr.Member;
        }
    }
}