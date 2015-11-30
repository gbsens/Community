using MKS.Core.Model;
using System;

namespace MKS.Core.Activity
{
    public interface IActivityLogOperations : IDisposable
    {
    }

    public interface IActivityLogOperations<TObject> : IActivityLogOperations, IActivityLogAdd<TObject>, IActivityLogUpdate<TObject>, IActivityLogSelect<TObject>,
                                                    IActivityLogDelete<TObject>, IActivityLogEdit<TObject>
    {
    }

    public interface IActivityLogOperations<TObject, TKey> : IActivityLogOperations, IActivityLogAdd<TObject>, IActivityLogUpdate<TObject>, IActivityLogSelect<TObject>,
                                                          IActivityLogSelect<TObject, TKey>, IActivityLogDelete<TObject>, IActivityLogDelete<TObject, TKey>,
                                                          IActivityLogEdit<TObject>, IActivityLogEdit<TObject, TKey>
        where TKey : IKey
    {
    }

    public interface IActivityLogOperations<TObject, TResult, TSearch> : IActivityLogOperations, IActivityLogAdd<TObject>, IActivityLogUpdate<TObject>, IActivityLogSelect<TObject>,
                                                                    IActivityLogSelect<TObject, TResult, TSearch>, IActivityLogDelete<TObject>,
                                                                    IActivityLogDelete<TObject, TResult, TSearch>, IActivityLogEdit<TObject>,
                                                                    IActivityLogEdit<TObject, TResult, TSearch>
        where TSearch : ISearch
    {
    }

    public interface IActivityLogOperations<TObject, TResult, TSearch, TKey> : IActivityLogOperations, IActivityLogAdd<TObject>, IActivityLogUpdate<TObject>,
                                                                          IActivityLogSelect<TObject>, IActivityLogSelect<TObject, TKey>, IActivityLogSelect<TObject, TResult, TSearch>,
                                                                          IActivityLogDelete<TObject>, IActivityLogDelete<TObject, TKey>, IActivityLogDelete<TObject, TResult, TSearch>,
                                                                          IActivityLogEdit<TObject>, IActivityLogEdit<TObject, TKey>,
                                                                          IActivityLogEdit<TObject, TResult, TSearch>
        where TSearch : ISearch
        where TKey : IKey
    {
    }

    public interface IActivityLogOperationsExecute<TObject> : IActivityLogOperations, IActivityLogExecute<TObject>
    {
    }
}