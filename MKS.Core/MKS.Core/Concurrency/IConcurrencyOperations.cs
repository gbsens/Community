using MKS.Core.Model;
using System;

namespace MKS.Core.Concurrency
{
    public interface IConcurrencyOperations : IDisposable
    {
    }

    public interface IConcurrencyOperations<TObject> : IConcurrencyOperations, IConcurrencyAdd<TObject>, IConcurrencyUpdate<TObject>, IConcurrencyDelete<TObject>,
                                                       IConcurrencyEdit<TObject>
    {
    }

    public interface IConcurrencyOperations<TObject, TKey> : IConcurrencyOperations, IConcurrencyAdd<TObject>, IConcurrencyUpdate<TObject>, IConcurrencyDelete<TObject>,
                                                             IConcurrencyEdit<TObject>, IConcurrencyEdit<TObject, TKey>,
                                                             IConcurrencyDelete<TObject, TKey>
        where TKey : IKey
    {
    }

    public interface IConcurrencyOperations<TObject, TResult, TSearch> : IConcurrencyOperations, IConcurrencyAdd<TObject>, IConcurrencyUpdate<TObject>,
                                                                       IConcurrencyDelete<TObject>, IConcurrencyEdit<TObject>,
                                                                       IConcurrencyEdit<TObject, TResult, TSearch>, IConcurrencyDelete<TObject, TResult, TSearch>
        where TSearch : ISearch
    {
    }

    public interface IConcurrencyOperations<TObject, TResult, TSearch, TKey> : IConcurrencyOperations, IConcurrencyAdd<TObject>, IConcurrencyUpdate<TObject>,
                                                                             IConcurrencyDelete<TObject>,
                                                                             IConcurrencyEdit<TObject>, IConcurrencyEdit<TObject, TKey>,
                                                                             IConcurrencyEdit<TObject, TResult, TSearch>, IConcurrencyDelete<TObject, TKey>,
                                                                             IConcurrencyDelete<TObject, TResult, TSearch>
        where TSearch : ISearch
        where TKey : IKey
    {
    }

    public interface IConcurrencyOperationsExecute<TObject> : IConcurrencyOperations, IConcurrencyExecute<TObject>
    {
    }
}