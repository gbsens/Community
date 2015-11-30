using MKS.Core.Model;
using System;
using System.Linq.Expressions;

namespace MKS.Core
{
    /// <summary>
    /// Permet de specifier un Expression Lambda sur un objet de recherche.
    /// </summary>
    /// <remarks>
    /// 	<code lang="C#"><![CDATA[SearchClient.Query= s.Query = c => c.Lastname="toto";]]></code>
    /// </remarks>
    /// <typeparam name="TObject"></typeparam>
    public abstract class ExpressionQuery<TObject> : ISearch
    {
        public Expression<Func<TObject, bool>> Query { get; set; }
        
    }
}