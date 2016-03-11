using MKS.Core.Activity;
using MKS.Core.Business.Interfaces;
using MKS.Core.Concurrency;
using MKS.Core.Mapping;
using MKS.Core.Model;

namespace MKS.Core.Business
{
    /// <summary>
    /// Classe d'affaire de base pour le traitement simple sur un objet incluant une objet clé de recherche multiple et permet la définition de l'objet de sortie.
    /// </summary>
    /// <typeparam name="TObject">Objet de traitement</typeparam>
    /// <typeparam name="TResult">Objet du résultat de recherche multiple</typeparam>
    /// <typeparam name="TSearch">Objet de recherche multiple</typeparam>
    public abstract class Business<TObject, TResult, TSearch> : Business<TObject>, IBusinessOperations<TObject, TResult, TSearch>
        where TSearch : ISearch
    {
        public Business()
        {
        }

        public Business(bool _useTransactionScope)
            : base(_useTransactionScope)
        {
        }
        public Business(bool useTransactionScope, ITrackingAdapter tracking) 
            : base(useTransactionScope,  tracking)
        {

        }
        #region Set


        public void SetDataMap(IDataOperations<TObject, TResult, TSearch> mappingInstance)
        {
            business.SetDataMap(mappingInstance);
        }
        public new void SetDataMap<Mapping>() where Mapping : IDataOperations<TObject, TResult, TSearch>, new()
        {
            business.SetDataMap(new Mapping());
        }
        public void SetActivityLog( IActivityLogOperations<TObject, TResult, TSearch> activityLogInstance,IActivityAdapter activityAdapterInstance )
        {
            business.SetEventLog(activityLogInstance, activityAdapterInstance);
        }
        public new void SetActivityLog<EventLog>() where EventLog : IActivityLogOperations<TObject, TResult, TSearch>, IActivityAdapter, new()
        {
            business.SetEventLog(new EventLog(), new EventLog());
        }

        public new void SetActivityLog<EventLog>(IActivity activityInstance) where EventLog : IActivityLogOperations<TObject, TResult, TSearch>, IActivityAdapter, new()
        {
            business.SetEventLog(new EventLog(), new EventLog(), activityInstance);
        }

        public void SetConcurrency(IConcurrencyOperations<TObject, TResult, TSearch> concurrencyOperatorInstance)
        {
            business.SetConcurrency(concurrencyOperatorInstance);
        }
        public new void SetConcurrency<Concurrency>() where Concurrency : IConcurrencyOperations<TObject, TResult, TSearch>, new()
        {
            business.SetConcurrency(new Concurrency());
        }

        public void SetValidationSearch(IValidation<TSearch> validationInstance)
        {
            business.SetValidationSearch(validationInstance);
        }
        public void SetValidationSearch<Validation>() where Validation : IValidation<TSearch>, new()
        {
            business.SetValidationSearch(new Validation());
        }

        void SetPreProcessSelectWithSearch(BusinessProcessSelect<TObject, TResult, TSearch> processSelectInstance)
        {
            business.SetPreProcessSelectSearch(processSelectInstance);
        }
        public void SetPreProcessSelectWithSearch<BusinessProcess>() where BusinessProcess : BusinessProcessSelect<TObject, TResult, TSearch>, new()
        {
            business.SetPreProcessSelectSearch(new BusinessProcess());
        }
        public void SetPostProcessSelectWithSearch(BusinessProcessSelect<TObject, TResult, TSearch> businessProcessSelectInstance)
        {
            business.SetPostProcessSelectSearch(businessProcessSelectInstance);
        }
        public void SetPostProcessSelectWithSearch<BusinessProcess>() where BusinessProcess : BusinessProcessSelect<TObject, TResult, TSearch>, new()
        {
            business.SetPostProcessSelectSearch(new BusinessProcess());
        }

        public void SetPreProcessDeleteWithSearch( BusinessProcessDelete<TObject, TResult, TSearch> processDeleteInstance)
        {
            business.SetPreProcessDeleteSearch(processDeleteInstance);
        }
        public void SetPreProcessDeleteWithSearch<BusinessProcess>() where BusinessProcess : BusinessProcessDelete<TObject, TResult, TSearch>, new()
        {
            business.SetPreProcessDeleteSearch(new BusinessProcess());
        }
        public void SetPostProcessDeleteWithSearch(BusinessProcessDelete<TObject, TResult, TSearch> processInstance)
        {
            business.SetPostProcessDeleteSearch(processInstance);
        }
        public void SetPostProcessDeleteWithSearch<BusinessProcess>() where BusinessProcess : BusinessProcessDelete<TObject, TResult, TSearch>, new()
        {
            business.SetPostProcessDeleteSearch(new BusinessProcess());
        }
        public void SetPreProcessEditWithSearch( BusinessProcessSelect<TObject, TResult, TSearch> processInstance)
        {
            business.SetPreProcessEditSearch(processInstance);
        }
        public void SetPreProcessEditWithSearch<BusinessProcess>() where BusinessProcess : BusinessProcessSelect<TObject, TResult, TSearch>, new()
        {
            business.SetPreProcessEditSearch(new BusinessProcess());
        }
        public void SetPostProcessEditWithSearch(BusinessProcessSelect<TObject, TResult, TSearch> processInstance)
        {
            business.SetPostProcessEditSearch(processInstance);
        }
        public void SetPostProcessEditWithSearch<BusinessProcess>() where BusinessProcess : BusinessProcessSelect<TObject, TResult, TSearch>, new()
        {
            business.SetPostProcessEditSearch(new BusinessProcess());
        }

        #endregion Set

        #region Functions

        public virtual int Delete(TSearch searchObject)
        {
            return business.Delete<TObject, TResult, TSearch>(searchObject, false);
        }

        public virtual int Delete(TSearch searchObject, bool getDeletedItems)
        {
            return business.Delete<TObject, TResult, TSearch>(searchObject, getDeletedItems);
        }

        public virtual TResult Select(TSearch searchObject)
        {
            return business.Select<TObject, TResult, TSearch>(searchObject);
        }

        public virtual TResult Edit(TSearch searchObject)
        {
            return business.Edit<TObject, TResult, TSearch>(searchObject);
        }

        #endregion Functions

        #region Concurrency

        public void DoConcurrencyDelete(BusinessObjectDelete<TObject, TResult, TSearch> businessObject, BusinessStep step)
        {
            business.DoConcurrencyDelete<TObject, TResult, TSearch>(businessObject, step);
        }

        public void DoConcurrencyEdit(BusinessObjectSelect<TObject, TResult, TSearch> businessObject, BusinessStep step)
        {
            business.DoConcurrencyEdit(businessObject, step);
        }

        #endregion Concurrency

        #region Mapping

        public TResult DoMappingSelect(BusinessObjectSelect<TObject, TResult, TSearch> businessObject)
        {
            return business.DoMappingSelect<TObject, TResult, TSearch>(businessObject);
        }

        public int DoMappingDelete(BusinessObjectDelete<TObject, TResult, TSearch> businessObject)
        {
            return business.DoMappingDelete<TObject, TResult, TSearch>(businessObject);
        }

        #endregion Mapping

        #region TActivityLog

        public void DoActivityLogSelect(BusinessObjectSelect<TObject, TResult, TSearch> businessObject)
        {
            business.DoActivityLogSelect<TObject, TResult, TSearch>(businessObject);
        }

        public void DoActivityLogDelete(BusinessObjectDelete<TObject, TResult, TSearch> businessObject)
        {
            business.DoActivityLogDelete<TObject, TResult, TSearch>(businessObject);
        }

        public void DoEventLogEdit(BusinessObjectSelect<TObject, TResult, TSearch> businessObject)
        {
            business.DoActivityLogEdit(businessObject);
        }

        #endregion TActivityLog

        #region Validation

        public void DoValidationSelect(BusinessObjectSelect<TObject, TResult, TSearch> businessObject)
        {
            business.DoValidation<TSearch>(businessObject.Search, businessObject, FunctionName.SelectSearch);
        }

        public void DoValidationDelete(BusinessObjectDelete<TObject, TResult, TSearch> businessObject)
        {
            business.DoValidation<TSearch>(businessObject.Search, businessObject, FunctionName.DeleteSearch);
        }

        #endregion Validation

        #region DoProcess

        public bool DoProcessSelect(ref BusinessObjectSelect<TObject, TResult, TSearch> businessObject, BusinessStep step)
        {
            return business.DoProcessSelect<TObject, TResult, TSearch>(ref businessObject, step);
        }

        public bool DoProcessDelete(ref BusinessObjectDelete<TObject, TResult, TSearch> businessObject, BusinessStep step)
        {
            return business.DoProcessDelete<TObject, TResult, TSearch>(ref businessObject, step);
        }

        public bool DoProcessEdit(ref BusinessObjectSelect<TObject, TResult, TSearch> businessObject, BusinessStep step)
        {
            return business.DoProcessEdit<TObject, TResult, TSearch>(ref businessObject, step);
        }

        #endregion DoProcess
    }
}