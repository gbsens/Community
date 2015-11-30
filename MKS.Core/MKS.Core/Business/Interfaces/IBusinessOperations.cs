using MKS.Core.Activity;
using MKS.Core.Concurrency;
using MKS.Core.Configuration;
using MKS.Core.Mapping;
using MKS.Core.Model;
using MKS.Core.Security;
using System;

namespace MKS.Core.Business.Interfaces
{
    public interface IBusinessOperations : IDisposable
    {
        void SetSecurity<TSecurity>() where TSecurity : ISecurityPermission, ISecurityAdapter, new();

        void SetConfiguration<TConfiguration>() where TConfiguration : IConfiguration, new();

        void SetProcessError<TBusinessProcess>() where TBusinessProcess : BusinessProcessError, new();

        void HandleException(Exception ex, IBusinessObject businessObject);

        void SetTracking<TTrackingAdapter>() where TTrackingAdapter:ITrackingAdapter, new();
    }

    public interface IBusinessOperations<TObject> : IBusinessOperations, IAdd<TObject>, IUpdate<TObject>, ISelect<TObject>, IDelete<TObject>, IBusinessOperationAdd<TObject>,
                                                    IBusinessOperationUpdate<TObject>, IBusinessOperationSelect<TObject>, IBusinessOperationDelete<TObject>,
                                                    IEdit<TObject>, IBusinessOperationEdit<TObject>
    {
        void SetDataMap<TMapping>() where TMapping : IDataOperations<TObject>, new();

        void SetActivityLog<TEventLog>() where TEventLog : IActivityLogOperations<TObject>, IActivityAdapter, new();

        void SetActivityLog<TEventLog>(IActivity activityInstance) where TEventLog : IActivityLogOperations<TObject>, IActivityAdapter, new();

        void SetConcurrency<TConcurrency>() where TConcurrency : IConcurrencyOperations<TObject>, new();

        void SetValidation<TValidation>() where TValidation : IValidation<TObject>, new();

        void SetValidationObject<TValidation>() where TValidation : IValidation<TObject>, new();

        void SetDetectChange<TDetectChange>() where TDetectChange : IChangeDetection<TObject>, new();

        void DoAddMessageToOutput(object ouput, IBusinessObject businessObject);
    }

    public interface IBusinessOperations<TObject, TKey> : IBusinessOperations, IAdd<TObject>, IUpdate<TObject>, ISelect<TObject>, ISelect<TObject, TKey>,
                                                          IDelete<TObject>, IBusinessOperationAdd<TObject>, IBusinessOperationUpdate<TObject>,
                                                          IBusinessOperationSelect<TObject>, IBusinessOperationSelect<TObject, TKey>, IBusinessOperationDelete<TObject>,
                                                          IBusinessOperationDelete<TObject, TKey>, IEdit<TObject, TKey>, IBusinessOperationEdit<TObject, TKey>,
                                                          IEdit<TObject>, IBusinessOperationEdit<TObject>
        where TKey : IKey
    {
        void SetDataMap<TMapping>() where TMapping : IDataOperations<TObject, TKey>, new();

        void SetActivityLog<TActivityLog>() where TActivityLog : IActivityLogOperations<TObject, TKey>, IActivityAdapter, new();

        void SetActivityLog<TActivityLog>(IActivity activityInstance) where TActivityLog : IActivityLogOperations<TObject, TKey>, IActivityAdapter, new();

        void SetConcurrency<TConcurrency>() where TConcurrency : IConcurrencyOperations<TObject, TKey>, new();

        void SetValidation<TValidation>() where TValidation : IValidation<TObject>, new();

        void SetValidationKey<TValidation>() where TValidation : IValidation<TKey>, new();

        void SetValidationObject<TValidation>() where TValidation : IValidation<TObject>, new();

        void SetDetectChange<TDetectChange>() where TDetectChange : IChangeDetection<TObject>, new();

        void DoAddMessageToOutput(object ouput, IBusinessObject businessObject);
    }

    public interface IBusinessOperations<TObject, TResult, TSearch> : IBusinessOperations, IAdd<TObject>, IUpdate<TObject>, ISelect<TObject>,
                                                                    ISelect<TObject, TResult, TSearch>, IDelete<TObject>,
                                                                    IBusinessOperationAdd<TObject>, IBusinessOperationUpdate<TObject>,
                                                                    IBusinessOperationSelect<TObject>, IBusinessOperationSelect<TObject, TResult, TSearch>,
                                                                    IBusinessOperationDelete<TObject>, IBusinessOperationDelete<TObject, TResult, TSearch>,
                                                                    IEdit<TObject>, IBusinessOperationEdit<TObject>
        where TSearch : ISearch
    {
        void SetDataMap<TMapping>() where TMapping : IDataOperations<TObject, TResult, TSearch>, new();

        void SetActivityLog<TEventLog>() where TEventLog : IActivityLogOperations<TObject, TResult, TSearch>, IActivityAdapter, new();

        void SetActivityLog<TEventLog>(IActivity activityInstance) where TEventLog : IActivityLogOperations<TObject, TResult, TSearch>, IActivityAdapter, new();

        void SetConcurrency<TConcurrency>() where TConcurrency : IConcurrencyOperations<TObject, TResult, TSearch>, new();

        void SetValidation<TValidation>() where TValidation : IValidation<TObject>, new();

        void SetValidationSearch<TValidation>() where TValidation : IValidation<TSearch>, new();

        void SetValidationObject<TValidation>() where TValidation : IValidation<TObject>, new();

        void SetDetectChange<TDetectChange>() where TDetectChange : IChangeDetection<TObject>, new();

        void DoAddMessageToOutput(object ouput, IBusinessObject businessObject);
    }

    public interface IBusinessOperations<TObject, TResult, TSearch, TKey> : IBusinessOperations, IAdd<TObject>, IUpdate<TObject>, ISelect<TObject>,
                                                                          ISelect<TObject, TKey>, ISelect<TObject, TResult, TSearch>,
                                                                          IDelete<TObject>,
                                                                          IBusinessOperationAdd<TObject>, IBusinessOperationUpdate<TObject>,
                                                                          IBusinessOperationSelect<TObject>, IBusinessOperationSelect<TObject, TKey>,
                                                                          IBusinessOperationSelect<TObject, TResult, TSearch>,
                                                                          IBusinessOperationDelete<TObject>, IBusinessOperationDelete<TObject, TKey>,
                                                                          IBusinessOperationDelete<TObject, TResult, TSearch>, IEdit<TObject>,
                                                                          IBusinessOperationEdit<TObject>,
                                                                          IEdit<TObject, TKey>, IBusinessOperationEdit<TObject, TKey>
        where TSearch : ISearch
        where TKey : IKey
    {
        void SetDataMap<TMapping>() where TMapping : IDataOperations<TObject, TResult, TSearch, TKey>, new();

        void SetActivityLog<TActivityLog>() where TActivityLog : IActivityLogOperations<TObject, TResult, TSearch, TKey>, IActivityAdapter, new();

        void SetActivityLog<TEventLog>(IActivity activityInstance) where TEventLog : IActivityLogOperations<TObject, TResult, TSearch, TKey>, IActivityAdapter, new();

        void SetConcurrency<TConcurrency>() where TConcurrency : IConcurrencyOperations<TObject, TResult, TSearch, TKey>, new();

        void SetValidation<TValidation>() where TValidation : IValidation<TObject>, new();

        void SetValidationSearch<TValidation>() where TValidation : IValidation<TSearch>, new();

        void SetValidationKey<TValidation>() where TValidation : IValidation<TKey>, new();

        void SetValidationObject<TValidation>() where TValidation : IValidation<TObject>, new();

        void SetDetectChange<TDetectChange>() where TDetectChange : IChangeDetection<TObject>, new();

        void DoAddMessageToOutput(object ouput, IBusinessObject businessObject);
    }

    public interface IBusinessOperationsExecute<TObject> : IBusinessOperations, IBusinessOperationExecute<TObject>, IExecute<TObject>, IExecuteThreaded<TObject>, IExecuteSynchrone<TObject>
    {
        void SetActivityLog<TActivityLog>() where TActivityLog : IActivityLogOperationsExecute<TObject>, IActivityAdapter, new();

        void SetActivityLog<TActivityLog>(IActivity activityInstance) where TActivityLog : IActivityLogOperationsExecute<TObject>, IActivityAdapter, new();

        void SetConcurrency<TConcurrency>() where TConcurrency : IConcurrencyOperationsExecute<TObject>, new();
    }
}