using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace MKS.Library.LinqKit
{
    /// <summary>
    /// Cette classe permet de créer des prédicats LINQ complexes facilement
    /// Pour plus d'info : http://www.albahari.com/nutshell/predicatebuilder.aspx
    /// </summary>
	public static class PredicateBuilder
	{
        /// <summary>
        /// Permet d'avoir un prédicat de départ à True (Pour une suite de "And")
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Un prédicat "true"</returns>
		public static Expression<Func<T, bool>> True<T> () { return f => true; }

        /// <summary>
        /// Permet d'avoir un prédicat de départ à False (Pour une suite de "Or")
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Un prédicat "false"</returns>
		public static Expression<Func<T, bool>> False<T> () { return f => false; }

        /// <summary>
        /// Permet d'ajouter une condition OR entre deux prédicats
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns>Le nouveau prédicat</returns>
		public static Expression<Func<T, bool>> Or<T> (this Expression<Func<T, bool>> expr1,
												  Expression<Func<T, bool>> expr2)
		{
			var invokedExpr = Expression.Invoke (expr2, expr1.Parameters.Cast<Expression> ());
			return Expression.Lambda<Func<T, bool>>
				 (Expression.OrElse (expr1.Body, invokedExpr), expr1.Parameters);
		}

        /// <summary>
        /// Permet d'ajouter une condition AND entre deux prédicats
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns>Le nouveau prédicat</returns>
		public static Expression<Func<T, bool>> And<T> (this Expression<Func<T, bool>> expr1,
												   Expression<Func<T, bool>> expr2)
		{
			var invokedExpr = Expression.Invoke (expr2, expr1.Parameters.Cast<Expression> ());
			return Expression.Lambda<Func<T, bool>>
				 (Expression.AndAlso (expr1.Body, invokedExpr), expr1.Parameters);
		}
	}
}
