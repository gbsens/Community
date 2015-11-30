using MKS.Core.Model;
namespace MKS.Core.Connector.Interfaces
{
    public interface IBusinessOperationAggregator
    {
    }

    public interface IBusinessOperationAddAggregator<TObject> : IBusinessOperationAggregator
    {
        void SetProcessAdd<TBusinessProcess>() where TBusinessProcess : BusinessProcessAddAggregator<TObject>, new();
    }

    public interface IBusinessOperationUpdateAggregator<TObject> : IBusinessOperationAggregator
    {
        void SetProcessUpdate<TBusinessProcess>() where TBusinessProcess : BusinessProcessUpdateAggregator<TObject>, new();
    }

    public interface IBusinessOperationDeleteAggregator<TObject> : IBusinessOperationAggregator
    {
        void SetProcessDelete<TBusinessProcess>() where TBusinessProcess : BusinessProcessDeleteAggregator<TObject>, new();
    }

    public interface IBusinessOperationDeleteAggregator<TObject, TKey> : IBusinessOperationAggregator
        where TKey : IKey
    {
        void SetProcessDeleteWithKey<TBusinessProcess>() where TBusinessProcess : BusinessProcessDeleteAggregator<TObject, TKey>, new();
    }

    public interface IBusinessOperationDeleteAggregator<TObject, TResult, TSearch> : IBusinessOperationAggregator
        where TSearch : ISearch
    {
        void SetProcessDeleteWithSearch<TBusinessProcess>() where TBusinessProcess : BusinessProcessDeleteAggregator<TObject, TResult, TSearch>, new();
    }

    public interface IBusinessOperationSelectAggregator<TObject> : IBusinessOperationAggregator
    {
        void SetProcessSelect<TBusinessProcess>() where TBusinessProcess : BusinessProcessSelectAggregator<TObject>, new();
    }

    public interface IBusinessOperationSelectAggregator<TObject, TKey> : IBusinessOperationAggregator
        where TKey : IKey
    {
        void SetProcessSelectWithKey<TBusinessProcess>() where TBusinessProcess : BusinessProcessSelectAggregator<TObject, TKey>, new();
    }

    public interface IBusinessOperationSelectAggregator<TObject, TResult, TSearch> : IBusinessOperationAggregator
        where TSearch : ISearch
    {
        void SetProcessSelectWithSearch<TBusinessProcess>() where TBusinessProcess : BusinessProcessSelectAggregator<TObject, TResult, TSearch>, new();
    }

    public interface IBusinessOperationEditAggregator<TObject> : IBusinessOperationAggregator
    {
        void SetProcessEdit<TBusinessProcess>() where TBusinessProcess : BusinessProcessSelectAggregator<TObject>, new();
    }

    public interface IBusinessOperationEditAggregator<TObject, TKey> : IBusinessOperationAggregator
        where TKey : IKey
    {
        void SetProcessEditWithKey<TBusinessProcess>() where TBusinessProcess : BusinessProcessSelectAggregator<TObject, TKey>, new();
    }

    public interface IBusinessOperationEditAggregator<TObject, TResult, TSearch> : IBusinessOperationAggregator
        where TSearch : ISearch
    {
        void SetProcessEditWithSearch<TBusinessProcess>() where TBusinessProcess : BusinessProcessSelectAggregator<TObject, TResult, TSearch>, new();
    }

    public interface IBusinessOperationExecuteAggregator<TObject> : IBusinessOperationAggregator
    {
        void SetProcessExecute<TBusinessProcess>() where TBusinessProcess : BusinessProcessExecuteAggregator<TObject>, new();
    }
}