using MKS.Core.Business.Interfaces;
using MKS.Core.Model;
using System;
using System.Data;

namespace MKS.Core.Mapping
{
    public interface IDataOperations : IDisposable
    {
        void Initialize(IDbConnection connection);        
    }

    public interface IDataOperations<TObject> : IDataOperations, IAdd<TObject>, IUpdate<TObject>, ISelect<TObject>, IDelete<TObject>, IEdit<TObject>
    {
    }

    public interface IDataOperations<TObject, TKey> : IDataOperations, IAdd<TObject>, IUpdate<TObject>, ISelect<TObject>, ISelect<TObject, TKey>, IDelete<TObject>, IDelete<TObject, TKey>, IEdit<TObject>, IEdit<TObject, TKey>
        where TKey : IKey
    {
    }

    public interface IDataOperations<TObject, TResult, TSearch> : IDataOperations, IAdd<TObject>, IUpdate<TObject>, ISelect<TObject>, ISelect<TObject, TResult, TSearch>, IDelete<TObject>, IDelete<TObject, TResult, TSearch>, IEdit<TObject>
        where TSearch : ISearch
    {
    }

    public interface IDataOperations<TObject, TResult, TSearch, TKey> : IDataOperations, IAdd<TObject>, IUpdate<TObject>, ISelect<TObject>, ISelect<TObject, TKey>, ISelect<TObject, TResult, TSearch>, IDelete<TObject>, IDelete<TObject, TKey>, IDelete<TObject, TResult, TSearch>, IEdit<TObject>, IEdit<TObject, TKey>
        where TSearch : ISearch
        where TKey : IKey
    {
    }
}