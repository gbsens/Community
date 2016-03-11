using MKS.Core.Activity;
using MKS.Core.Model;

namespace MKS.Core.Business.Interfaces
{
    public interface IBusinessOperation
    {
    }

    public interface IBusinessOperationAdd<TObject> : IBusinessOperation
    {
        TObject DoMappingAdd(BusinessObjectAdd<TObject> businessObject);

        void SetPreProcessAdd<TBusinessProcess>() where TBusinessProcess : BusinessProcessAdd<TObject>, new();

        void SetPostProcessAdd<TBusinessProcess>() where TBusinessProcess : BusinessProcessAdd<TObject>, new();

        bool DoProcessAdd(ref BusinessObjectAdd<TObject> businessObject, BusinessStep step);

        void DoSecurityCheckAdd();

        void DoValidationAdd(BusinessObjectAdd<TObject> businessObject);

        void DoConcurrencyAdd(BusinessObjectAdd<TObject> businessObject, BusinessStep step);

        void DoActivityLogAdd(BusinessObjectAdd<TObject> businessObject);
    }

    public interface IBusinessOperationUpdate<TObject> : IBusinessOperation
    {
        TObject DoMappingUpdate(BusinessObjectUpdate<TObject> businessObject);

        void SetPreProcessUpdate<TBusinessProcess>() where TBusinessProcess : BusinessProcessUpdate<TObject>, new();

        void SetPostProcessUpdate<TBusinessProcess>() where TBusinessProcess : BusinessProcessUpdate<TObject>, new();

        bool DoProcessUpdate(ref BusinessObjectUpdate<TObject> businessObject, BusinessStep step);

        void DoSecurityCheckUpdate();

        void DoValidationUpdate(BusinessObjectUpdate<TObject> businessObject);

        void DoConcurrencyUpdate(BusinessObjectUpdate<TObject> businessObject, BusinessStep step);

        void DoActivityLogUpdate(BusinessObjectUpdate<TObject> businessObject);

        void DoActivityLogUpdate(BusinessObjectUpdate<TObject> businessObject, ChangeDetections detectChanges);
    }

    public interface IBusinessOperationDelete<TObject> : IBusinessOperation
    {
        int DoMappingDelete(BusinessObjectDelete<TObject> businessObject);

        void SetPreProcessDelete<TBusinessProcess>() where TBusinessProcess : BusinessProcessDelete<TObject>, new();

        void SetPostProcessDelete<TBusinessProcess>() where TBusinessProcess : BusinessProcessDelete<TObject>, new();

        bool DoProcessDelete(ref BusinessObjectDelete<TObject> businessObject, BusinessStep step);

        void DoSecurityCheckDelete();

        void DoValidationDelete(BusinessObjectDelete<TObject> businessObject);

        void DoConcurrencyDelete(BusinessObjectDelete<TObject> businessObject, BusinessStep step);

        void DoActivityLogDelete(BusinessObjectDelete<TObject> businessObject);

        int Delete(TObject myObject, bool getDeletedItems);
    }

    public interface IBusinessOperationDelete<TObject, TKey> : IBusinessOperation
        where TKey : IKey
    {
        void SetPreProcessDeleteWithKey<TBusinessProcess>() where TBusinessProcess : BusinessProcessDelete<TObject, TKey>, new();

        void SetPostProcessDeleteWithKey<TBusinessProcess>() where TBusinessProcess : BusinessProcessDelete<TObject, TKey>, new();

        bool DoProcessDelete(ref BusinessObjectDelete<TObject, TKey> businessObject, BusinessStep step);

        void DoSecurityCheckDelete();

        void DoValidationDelete(BusinessObjectDelete<TObject, TKey> businessObject);

        void DoActivityLogDelete(BusinessObjectDelete<TObject, TKey> businessObject);

        int Delete(TKey key, bool getDeletedItems);
    }

    public interface IBusinessOperationDelete<TObject, TResult, TSearch> : IBusinessOperation
        where TSearch : ISearch
    {
        void SetPreProcessDeleteWithSearch<TBusinessProcess>() where TBusinessProcess : BusinessProcessDelete<TObject, TResult, TSearch>, new();

        void SetPostProcessDeleteWithSearch<TBusinessProcess>() where TBusinessProcess : BusinessProcessDelete<TObject, TResult, TSearch>, new();

        bool DoProcessDelete(ref BusinessObjectDelete<TObject, TResult, TSearch> businessObject, BusinessStep step);

        void DoSecurityCheckDelete();

        void DoValidationDelete(BusinessObjectDelete<TObject, TResult, TSearch> businessObject);

        void DoActivityLogDelete(BusinessObjectDelete<TObject, TResult, TSearch> businessObject);

        int Delete(TSearch search, bool getDeletedItems);
    }

    public interface IBusinessOperationSelect<TObject> : IBusinessOperation
    {
        TObject DoMappingSelect(BusinessObjectSelect<TObject> businessObject);

        void SetPreProcessSelect<TBusinessProcess>() where TBusinessProcess : BusinessProcessSelect<TObject>, new();

        void SetPostProcessSelect<TBusinessProcess>() where TBusinessProcess : BusinessProcessSelect<TObject>, new();

        bool DoProcessSelect(ref BusinessObjectSelect<TObject> businessObject, BusinessStep step);

        void DoSecurityCheckSelect();

        void DoValidationSelect(BusinessObjectSelect<TObject> businessObject);

        void DoActivityLogSelect(BusinessObjectSelect<TObject> businessObject);
    }

    public interface IBusinessOperationSelect<TObject, TKey> : IBusinessOperation
        where TKey : IKey
    {
        TObject DoMappingSelect(BusinessObjectSelect<TObject, TKey> businessObject);

        void SetPreProcessSelectWithKey<TBusinessProcess>() where TBusinessProcess : BusinessProcessSelect<TObject, TKey>, new();

        void SetPostProcessSelectWithKey<TBusinessProcess>() where TBusinessProcess : BusinessProcessSelect<TObject, TKey>, new();

        bool DoProcessSelect(ref BusinessObjectSelect<TObject, TKey> businessObject, BusinessStep step);

        void DoSecurityCheckSelect();

        void DoValidationSelect(BusinessObjectSelect<TObject, TKey> businessObject);

        void DoActivityLogSelect(BusinessObjectSelect<TObject, TKey> businessObject);
    }

    public interface IBusinessOperationSelect<TObject, TResult, TSearch> : IBusinessOperation
        where TSearch : ISearch
    {
        TResult DoMappingSelect(BusinessObjectSelect<TObject, TResult, TSearch> businessObject);

        void SetPreProcessSelectWithSearch<TBusinessProcess>() where TBusinessProcess : BusinessProcessSelect<TObject, TResult, TSearch>, new();

        void SetPostProcessSelectWithSearch<TBusinessProcess>() where TBusinessProcess : BusinessProcessSelect<TObject, TResult, TSearch>, new();

        bool DoProcessSelect(ref BusinessObjectSelect<TObject, TResult, TSearch> businessObject, BusinessStep step);

        void DoSecurityCheckSelect();

        void DoValidationSelect(BusinessObjectSelect<TObject, TResult, TSearch> businessObject);

        void DoActivityLogSelect(BusinessObjectSelect<TObject, TResult, TSearch> businessObject);
    }

    public interface IBusinessOperationEdit<TObject> : IBusinessOperation
    {
        TObject DoMappingSelect(BusinessObjectSelect<TObject> businessObject);

        void SetPreProcessEdit<TBusinessProcess>() where TBusinessProcess : BusinessProcessSelect<TObject>, new();

        void SetPostProcessEdit<TBusinessProcess>() where TBusinessProcess : BusinessProcessSelect<TObject>, new();

        bool DoProcessEdit(ref BusinessObjectSelect<TObject> businessObject, BusinessStep step);

        void DoSecurityCheckSelect();

        void DoValidationSelect(BusinessObjectSelect<TObject> businessObject);

        void DoConcurrencyEdit(BusinessObjectSelect<TObject> businessObject, BusinessStep step);

        void DoActivityLogEdit(BusinessObjectSelect<TObject> businessObject);
    }

    public interface IBusinessOperationEdit<TObject, TKey> : IBusinessOperation
        where TKey : IKey
    {
        TObject DoMappingSelect(BusinessObjectSelect<TObject, TKey> businessObject);

        void SetPreProcessWithKey<BusinessProcess>() where BusinessProcess : BusinessProcessSelect<TObject, TKey>, new();

        void SetPostProcessEditWithKey<BusinessProcess>() where BusinessProcess : BusinessProcessSelect<TObject, TKey>, new();

        bool DoProcessEdit(ref BusinessObjectSelect<TObject, TKey> businessObject, BusinessStep step);

        void DoSecurityCheckSelect();

        void DoValidationSelect(BusinessObjectSelect<TObject, TKey> businessObject);

        void DoConcurrencyEdit(BusinessObjectSelect<TObject, TKey> businessObject, BusinessStep step);

        void DoActivityLogEdit(BusinessObjectSelect<TObject, TKey> businessObject);
    }

    public interface IBusinessOperationEdit<TObject, TResult, TSearch> : IBusinessOperation
        where TSearch : ISearch
    {
        TResult DoMappingSelect(BusinessObjectSelect<TObject, TResult, TSearch> businessObject);

        void SetPreProcessEditWithSearch<TBusinessProcess>() where TBusinessProcess : BusinessProcessSelect<TObject, TResult, TSearch>, new();

        void SetPostProcessAfterWithSearch<TBusinessProcess>() where TBusinessProcess : BusinessProcessSelect<TObject, TResult, TSearch>, new();

        bool DoProcessEdit(ref BusinessObjectSelect<TObject, TResult, TSearch> businessObject, BusinessStep step);

        void DoSecurityCheckSelect();

        void DoValidationSelect(BusinessObjectSelect<TObject, TResult, TSearch> businessObject);

        void DoConcurrencyEdit(BusinessObjectSelect<TObject, TResult, TSearch> businessObject, BusinessStep step);

        void DoActivityLogEdit(BusinessObjectSelect<TObject, TResult, TSearch> businessObject);
    }

    public interface IBusinessOperationExecute<TObject> : IBusinessOperation
    {
        void SetProcessExecute<TBusinessProcess>() where TBusinessProcess : BusinessProcessExecute<TObject>, new();

        void SetProcessFinalize<TBusinessProcess>() where TBusinessProcess : BusinessProcessExecute<TObject>, new();

        bool DoProcessExecute(ref BusinessObjectExecute<TObject> businessObject);

        void DoSecurityCheckExecute();

        void DoActivityLogExecute(BusinessObjectExecute<TObject> businessObject);

        void DoConcurrencyExecute(BusinessObjectExecute<TObject> businessObject, BusinessStep step);
    }
}