using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKS.Core.Business;
using MKS.Core.Model;

// Rend accessible en instance la création de Business.
namespace MKS.Core.Business
{

    public class MKSBusiness<TObject>:Business<TObject> {  }
    public class MKSBusiness<TObject, TKey>:Business<TObject,TKey> where TKey : IKey { }
    public class MKSBusiness<TObject, TResult, TSearch> : Business<TObject, TResult, TSearch> where TSearch:ISearch { }
    public class MKSBusiness<TObject, TResult, TSearch, TKey> : Business<TObject, TResult, TSearch, TKey> where TSearch:ISearch where TKey:IKey { }

}
