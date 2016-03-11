using MKS.Core.Activity;
using MKS.Core.Business.Interfaces;
using MKS.Core.Concurrency;
using MKS.Core.Mapping;
using MKS.Core.Model;

namespace MKS.Core.Business
{
    /// <summary>
    /// Classe d'affaire de base pour le traitement simple sur un objet incluant une objet clé de recherche
    /// </summary>
    /// <typeparam name="TObject">Objet de traitement</typeparam>
    /// <typeparam name="TKey">Objet pour effectuer une recherche unique</typeparam>
    public abstract class Business<TObject, TKey> : Business<TObject>, IBusinessOperations<TObject, TKey>
        where TKey : IKey
    {
        public Business() { }


        public Business(bool useTransactionScope)
            : base(useTransactionScope)
        {
           
        }
        public Business(bool useTransactionScope, ITrackingAdapter tracking) 
            : base(useTransactionScope,  tracking)
        {

        }
        #region Set


        public void SetDataMap(IDataOperations<TObject, TKey> mappingInstance)
        {
            business.SetDataMap(mappingInstance);
        }
        public new void SetDataMap<Mapping>() where Mapping : IDataOperations<TObject, TKey>, new()
        {
            business.SetDataMap(new Mapping());
        }

        public void SetActivityLog( IActivityLogOperations<TObject, TKey> activityLogInstance, IActivityAdapter activityAdapterInstance)
        {
            business.SetEventLog(activityLogInstance, activityAdapterInstance);
        }
        public new void SetActivityLog<EventLog>() where EventLog : IActivityLogOperations<TObject, TKey>, IActivityAdapter, new()
        {
            business.SetEventLog(new EventLog(), new EventLog());
        }
        public new void SetActivityLog<EventLog>(IActivity activityInstance) where EventLog : IActivityLogOperations<TObject, TKey>, IActivityAdapter, new()
        {
            business.SetEventLog(new EventLog(), new EventLog(), activityInstance);
        }
        public void SetConcurrency(IConcurrencyOperations<TObject, TKey> concurrencyInstance)
        {
            business.SetConcurrency(concurrencyInstance);
        }
        public new void SetConcurrency<Concurrency>() where Concurrency : IConcurrencyOperations<TObject, TKey>, new()
        {
            business.SetConcurrency(new Concurrency());
        }
        public void SetValidationKey(IValidation<TKey> validationInstance)
        {
            business.SetValidationKey(validationInstance);
        }
        public void SetValidationKey<Validation>() where Validation : IValidation<TKey>, new()
        {
            business.SetValidationKey(new Validation());
        }
        public void SetPreProcessSelectWithKey(BusinessProcessSelect<TObject, TKey> businessProcessInstance)
        {
            business.SetPreProcessSelectKey<TObject, TKey>(businessProcessInstance);
        }
        public void SetPreProcessSelectWithKey<BusinessProcess>() where BusinessProcess : BusinessProcessSelect<TObject, TKey>, new()
        {
            business.SetPreProcessSelectKey<TObject, TKey>(new BusinessProcess());
        }
        public void SetPostProcessSelectWithKey(BusinessProcessSelect<TObject, TKey> businessProcessInstance)
        {
            business.SetPostProcessSelectKey(businessProcessInstance);
        }
        public void SetPostProcessSelectWithKey<BusinessProcess>() where BusinessProcess : BusinessProcessSelect<TObject, TKey>, new()
        {
            business.SetPostProcessSelectKey(new BusinessProcess());
        }
        public void SetPreProcessDeleteWithKey( BusinessProcessDelete<TObject, TKey> businessProcessDeleteInstance)
        {
            business.SetPreProcessDeleteKey(businessProcessDeleteInstance);
        }
        public void SetPreProcessDeleteWithKey<BusinessProcess>() where BusinessProcess : BusinessProcessDelete<TObject, TKey>, new()
        {
            business.SetPreProcessDeleteKey(new BusinessProcess());
        }
        public void SetPostProcessDeleteWithKey(BusinessProcessDelete<TObject, TKey> businessProcessDelete)
        {
            business.SetPostProcessDeleteKey(businessProcessDelete);
        }
        public void SetPostProcessDeleteWithKey<BusinessProcess>() where BusinessProcess : BusinessProcessDelete<TObject, TKey>, new()
        {
            business.SetPostProcessDeleteKey(new BusinessProcess());
        }
        public void SetPreProcessWithKey(BusinessProcessSelect<TObject, TKey> businessProcessSelect)
        {
            business.SetPreProcessEditKey(businessProcessSelect);
        }
        public void SetPreProcessWithKey<BusinessProcess>() where BusinessProcess : BusinessProcessSelect<TObject, TKey>, new()
        {
            business.SetPreProcessEditKey(new BusinessProcess());
        }
        public void SetPostProcessEditWithKey( BusinessProcessSelect<TObject, TKey> businessProcessInstance)
        {
            business.SetPostProcessEditKey(businessProcessInstance);
        }
        public void SetPostProcessEditWithKey<BusinessProcess>() where BusinessProcess : BusinessProcessSelect<TObject, TKey>, new()
        {
            business.SetPostProcessEditKey(new BusinessProcess());
        }

        #endregion Set

        #region Functions

        public virtual int Delete(TKey myObject)
        {
            return business.Delete<TObject, TKey>(myObject, false);
        }

        public virtual int Delete(TKey myObject, bool getDeletedItems)
        {
            return business.Delete<TObject, TKey>(myObject, getDeletedItems);
        }

        public virtual TObject Select(TKey keyObject)
        {
            return business.Select<TObject, TKey>(keyObject);
        }

        public virtual TObject Edit(TKey keyObject)
        {
            return business.Edit<TObject, TKey>(keyObject);
        }

        #endregion Functions

        #region Concurrency

        public void DoConcurrencyDelete(BusinessObjectDelete<TObject, TKey> businessObject, BusinessStep step)
        {
            business.DoConcurrencyDelete<TObject, TKey>(businessObject, step);
        }

        public void DoConcurrencyEdit(BusinessObjectSelect<TObject, TKey> businessObject, BusinessStep step)
        {
            business.DoConcurrencyEdit(businessObject, step);
        }

        #endregion Concurrency

        #region Mapping

        public int DoMappingDelete(BusinessObjectDelete<TObject, TKey> businessObject)
        {
            return business.DoMappingDelete<TObject, TKey>(businessObject);
        }

        public TObject DoMappingSelect(BusinessObjectSelect<TObject, TKey> businessObject)
        {
            return business.DoMappingSelect<TObject, TKey>(businessObject);
        }

        #endregion Mapping

        #region Activity

        public void DoActivityLogDelete(BusinessObjectDelete<TObject, TKey> businessObject)
        {
            business.DoActivityLogDelete<TObject, TKey>(businessObject);
        }

        public void DoActivityLogSelect(BusinessObjectSelect<TObject, TKey> businessObject)
        {
            business.DoActivityLogSelect<TObject, TKey>(businessObject);
        }

        public bool DoProcessEdit(ref BusinessObjectSelect<TObject, TKey> businessObject, BusinessStep step)
        {
            return business.DoProcessEdit<TObject, TKey>(ref businessObject, step);
        }

        #endregion Activity

        #region Validation

        public void DoValidationDelete(BusinessObjectDelete<TObject, TKey> businessObject)
        {
            business.DoValidation<TKey>(businessObject.Key, businessObject, FunctionName.DeleteKey);
        }

        public void DoValidationSelect(BusinessObjectSelect<TObject, TKey> businessObject)
        {
            business.DoValidation<TKey>(businessObject.Key, businessObject, FunctionName.SelectKey);
        }

        #endregion Validation

        #region DoProcess

        public bool DoProcessDelete(ref BusinessObjectDelete<TObject, TKey> businessObject, BusinessStep step)
        {
            return business.DoProcessDelete<TObject, TKey>(ref businessObject, step);
        }

        public bool DoProcessSelect(ref BusinessObjectSelect<TObject, TKey> businessObject, BusinessStep step)
        {
            return business.DoProcessSelect<TObject, TKey>(ref businessObject, step);
        }

        public void DoActivityLogEdit(BusinessObjectSelect<TObject, TKey> businessObject)
        {
            business.DoActivityLogEdit(businessObject);
        }

        #endregion DoProcess



    }
}