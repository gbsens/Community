using MKS.Core.Model;
using System.ServiceModel;
namespace MKS.Core.Business.Interfaces
{

    public interface IOperation
    {
    }

    public interface IAdd<TObject> : IOperation
    {
        [OperationContract]
        TObject Add(TObject myObject);
    }

    public interface IUpdate<TObject> : IOperation   
    {
        [OperationContract]
        TObject Update(TObject myObject);
    }
    
    public interface IDelete<TObject> : IOperation
    {
        [OperationContract]
        int Delete(TObject myObject);
    }
    
    public interface IDelete<TObject, TKey> : IOperation
    {
        [OperationContract]
        int Delete(TKey keyObject);
    }
    
    public interface IDelete<TObject, TResult, TSearch> : IOperation
    {
        [OperationContract]
        int Delete(TSearch searchObject);
    }
    
    public interface ISelect<TObject> : IOperation
    {
        [OperationContract]
        TObject Select(TObject myObject);
    }
    
    public interface ISelect<TObject, TKey> : IOperation
        where TKey : IKey
    {
        [OperationContract]
        TObject Select(TKey keyObject);
    }
    
    public interface ISelect<TObject, TResult, TSearch> : IOperation
        where TSearch : ISearch
    {
        [OperationContract]
        TResult Select(TSearch searchObject);
    }
    
    public interface IEdit<TObject> : IOperation
    {
        [OperationContract]
        TObject Edit(TObject myObject);
    }
    
    public interface IEdit<TObject, TKey> : IOperation
        where TKey : IKey
    {
        [OperationContract]
        TObject Edit(TKey keyObject);
    }
    
    public interface IExecute<TObject> : IOperation
    {
        [OperationContract]
        void Execute(TObject myObject);
    }
    
    public interface IExecuteSynchrone<TObject> : IOperation
    {
        [OperationContract]
        TObject ExecuteSynchrone(TObject myObject);
    }
    
    public interface IExecuteThreaded<TObject> : IOperation
    {
        [OperationContract]
        void ExecuteThreaded(TObject myObject, int timeoutMs);
    }

#region operation pour palier au polymorphime de la couche service exposé
    public interface ISelectKey<TObject, TKey> : IOperation
    where TKey : IKey
    {
        [OperationContract]
        TObject SelectKey(TKey keyObject);
    }
    public interface ISelectSearch<TObject, TResult, TSearch> : IOperation
    where TSearch : ISearch
    {
        [OperationContract]
        TResult SelectSearch(TSearch searchObject);
    }
    public interface IEditKey<TObject, TKey> : IOperation
        where TKey : IKey
    {
        [OperationContract]
        TObject EditKey(TKey keyObject);
    }
    public interface IDeleteKey<TObject, TKey> : IOperation
    {
        [OperationContract]
        int DeleteKey(TKey keyObject);
    }

    public interface IDeleteSearch<TObject, TResult, TSearch> : IOperation
    {
        [OperationContract]
        int DeleteSearch(TSearch searchObject);
    }
#endregion

}