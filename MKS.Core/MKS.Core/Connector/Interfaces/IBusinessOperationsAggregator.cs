using MKS.Core.Business.Interfaces;
using MKS.Core.Model;

namespace MKS.Core.Connector.Interfaces
{
    public interface IBusinessOperationsAggregator
    {
        void SetRouting<TRouting>() where TRouting : IRoutingAdapter, new();

        void AddContract(IContract contract);
    }

    public interface IBusinessOperationsAggregator<TObject> : IBusinessOperationsAggregator, IAdd<TObject>, IUpdate<TObject>, ISelect<TObject>, IDelete<TObject>, IBusinessOperationAddAggregator<TObject>,
                                                    IBusinessOperationUpdateAggregator<TObject>, IBusinessOperationSelectAggregator<TObject>, IBusinessOperationDeleteAggregator<TObject>,
                                                    IEdit<TObject>, IBusinessOperationEditAggregator<TObject>
    {
        void SetValidation<TValidation>() where TValidation : IValidation<TObject>, new();

        void SetValidationObject<TValidation>() where TValidation : IValidation<TObject>, new();
    }

    public interface IBusinessOperationsAggregator<TObject, TKey> : IBusinessOperationsAggregator, IAdd<TObject>, IUpdate<TObject>, ISelect<TObject>, ISelect<TObject, TKey>,
                                                          IDelete<TObject>, IBusinessOperationAddAggregator<TObject>, IBusinessOperationUpdateAggregator<TObject>,
                                                          IBusinessOperationSelectAggregator<TObject>, IBusinessOperationSelectAggregator<TObject, TKey>, IBusinessOperationDeleteAggregator<TObject>,
                                                          IBusinessOperationDeleteAggregator<TObject, TKey>, IEdit<TObject, TKey>, IBusinessOperationEditAggregator<TObject, TKey>,
                                                          IEdit<TObject>, IBusinessOperationEditAggregator<TObject>
        where TKey : IKey
    {
        void SetValidation<TValidation>() where TValidation : IValidation<TObject>, new();

        void SetValidationKey<TValidation>() where TValidation : IValidation<TKey>, new();

        void SetValidationObject<TValidation>() where TValidation : IValidation<TObject>, new();
    }

    public interface IBusinessOperationsAggregator<TObject, TResult, TSearch> : IBusinessOperationsAggregator, IAdd<TObject>, IUpdate<TObject>, ISelect<TObject>,
                                                                    ISelect<TObject, TResult, TSearch>, IDelete<TObject>,
                                                                    IBusinessOperationAddAggregator<TObject>, IBusinessOperationUpdateAggregator<TObject>,
                                                                    IBusinessOperationSelectAggregator<TObject>, IBusinessOperationSelectAggregator<TObject, TResult, TSearch>,
                                                                    IBusinessOperationDeleteAggregator<TObject>, IBusinessOperationDeleteAggregator<TObject, TResult, TSearch>,
                                                                    IEdit<TObject>, IBusinessOperationEditAggregator<TObject>
        where TSearch : ISearch
    {
        void SetValidation<TValidation>() where TValidation : IValidation<TObject>, new();

        void SetValidationSearch<TValidation>() where TValidation : IValidation<TSearch>, new();

        void SetValidationObject<TValidation>() where TValidation : IValidation<TObject>, new();
    }

    public interface IBusinessOperationsAggregator<TObject, TResult, TSearch, TKey> : IBusinessOperationsAggregator, IAdd<TObject>, IUpdate<TObject>, ISelect<TObject>,
                                                                          ISelect<TObject, TKey>, ISelect<TObject, TResult, TSearch>,
                                                                          IDelete<TObject>,
                                                                          IBusinessOperationAddAggregator<TObject>, IBusinessOperationUpdateAggregator<TObject>,
                                                                          IBusinessOperationSelectAggregator<TObject>, IBusinessOperationSelectAggregator<TObject, TKey>,
                                                                          IBusinessOperationSelectAggregator<TObject, TResult, TSearch>,
                                                                          IBusinessOperationDeleteAggregator<TObject>, IBusinessOperationDeleteAggregator<TObject, TKey>,
                                                                          IBusinessOperationDeleteAggregator<TObject, TResult, TSearch>, IEdit<TObject>,
                                                                          IBusinessOperationEditAggregator<TObject>,
                                                                          IEdit<TObject, TKey>, IBusinessOperationEditAggregator<TObject, TKey>
        where TSearch : ISearch
        where TKey : IKey
    {
        void SetValidation<TValidation>() where TValidation : IValidation<TObject>, new();

        void SetValidationSearch<TValidation>() where TValidation : IValidation<TSearch>, new();

        void SetValidationKey<TValidation>() where TValidation : IValidation<TKey>, new();

        void SetValidationObject<TValidation>() where TValidation : IValidation<TObject>, new();
    }

    public interface IBusinessOperationsExecuteAggregator<TObject> : IBusinessOperationsAggregator, IExecute<TObject>, IBusinessOperationExecuteAggregator<TObject>
    {
        void SetValidation<TValidation>() where TValidation : IValidation<TObject>, new();
    }
}