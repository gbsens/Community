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
    /// <typeparam name="TKey">Objet pour effectuer une recherche unique</typeparam>
    public abstract class Business<TObject, TResult, TSearch, TKey> : Business<TObject, TKey>, IBusinessOperations<TObject, TResult, TSearch, TKey>
        where TSearch : ISearch
        where TKey : IKey
    {
        public Business() { }



        public Business(bool _useTransactionScope)
            : base(_useTransactionScope)
        {
   
        }
        public Business(bool useTransactionScope, ITrackingAdapter tracking) 
            : base(useTransactionScope,  tracking)
        {

        }
        #region Set

        public void SetDataMap(IDataOperations<TObject, TResult, TSearch, TKey> mappingInsance)
        {
            business.SetDataMap(mappingInsance);
        }
        public new void SetDataMap<Mapping>() where Mapping : IDataOperations<TObject, TResult, TSearch, TKey>, new()
        {
            business.SetDataMap(new Mapping());
        }


        public void SetActivityLog(IActivityLogOperations<TObject, TResult, TSearch, TKey> instance, IActivityAdapter adapter)     
        {
            business.SetEventLog(instance, adapter);
        }

        public new void SetActivityLog<EventLog>() where EventLog : IActivityLogOperations<TObject, TResult, TSearch, TKey>, IActivityAdapter, new()
        {
            business.SetEventLog(new EventLog(), new EventLog());
        }
        /// <summary>
        /// Cette fonction permet de prendre en charge un log d'activié déjè démarrer dans d'autres procesuss d'affaire.
        /// </summary>
        /// <typeparam name="TActivityLog">Type d'activié a loguer</typeparam>
        /// <param name="activityInstance">Instance déjà en cours de l'activité</param>
        public new void SetActivityLog<TActivityLog>(IActivity activityInstance) where TActivityLog : IActivityLogOperations<TObject, TResult, TSearch, TKey>, IActivityAdapter, new()
        {
            business.SetEventLog(new TActivityLog(), new TActivityLog(), activityInstance);
        }

        public new void SetActivityLog<TEventLog, TEventLogAdapter>()
            where TEventLog : IActivityLogOperations<TObject, TResult, TSearch, TKey>, new()
            where TEventLogAdapter : IActivityAdapter, new()
        {
            business.SetEventLog(new TEventLog(), new TEventLogAdapter());
        }

        public void SetActivityLog<TEventLog, TEventLogAdapter>(TEventLog activityInstance)
            where TEventLog : IActivityLogOperations<TObject, TResult, TSearch, TKey>, new()
            where TEventLogAdapter : IActivityAdapter, new()
        {
            business.SetEventLog(activityInstance, new TEventLogAdapter());
        }

        /// <summary>
        /// Cette fonction permet l'injection de dépendance pour la gestion des concurrences
        /// </summary>
        /// <typeparam name="Concurrency">Type de classe de concurrence</typeparam>
        /// <param name="instance">Instance de la concurrence</param>
        public void SetConcurrency(IConcurrencyOperations<TObject, TResult, TSearch, TKey> instance) 
        {
            business.SetConcurrency(instance);
        }
        public void SetConcurrency<Concurrency>() where Concurrency : IConcurrencyOperations<TObject, TResult, TSearch, TKey>, new()
        {
            business.SetConcurrency(new Concurrency());
        }

        public void SetValidationSearch(IValidation<TSearch> validationInstance)
        {
            business.SetValidationSearch(validationInstance);
        }
        public  void SetValidationSearch<Validation>() where Validation : IValidation<TSearch>, new()
        {
            business.SetValidationSearch(new Validation());
        }

        public  void SetPreProcessSelectWithSearch(BusinessProcessSelect<TObject, TResult, TSearch> processInstance)
        {
            business.SetPreProcessSelectSearch(processInstance);
        }
        public  void SetPreProcessSelectWithSearch<BusinessProcess>() where BusinessProcess : BusinessProcessSelect<TObject, TResult, TSearch>, new()
        {
            business.SetPreProcessSelectSearch(new BusinessProcess());
        }

        public void SetPostProcessSelectWithSearch(BusinessProcessSelect<TObject, TResult, TSearch> processInstance)
        {
            business.SetPostProcessSelectSearch(processInstance);
        }
        public  void SetPostProcessSelectWithSearch<BusinessProcess>() where BusinessProcess : BusinessProcessSelect<TObject, TResult, TSearch>, new()
        {
            business.SetPostProcessSelectSearch(new BusinessProcess());
        }

        public void SetPreProcessDeleteWithSearch(BusinessProcessDelete<TObject, TResult, TSearch> processInstance)
        {
            business.SetPreProcessDeleteSearch(processInstance);
        }
        public   void SetPreProcessDeleteWithSearch<BusinessProcess>() where BusinessProcess : BusinessProcessDelete<TObject, TResult, TSearch>, new()
        {
            business.SetPreProcessDeleteSearch(new BusinessProcess());
        }

        public void SetPostProcessDelteWithSearch(BusinessProcessDelete<TObject, TResult, TSearch> processInstance)
        {
            business.SetPostProcessDeleteSearch(processInstance);
        }
        public  void SetPostProcessDeleteWithSearch<BusinessProcess>() where BusinessProcess : BusinessProcessDelete<TObject, TResult, TSearch>, new()
        {
            business.SetPostProcessDeleteSearch(new BusinessProcess());
        }

        public void SetProcessEditBeforeWithSearch(BusinessProcessSelect<TObject, TResult, TSearch> processInstance)
        {
            business.SetPreProcessEditSearch(processInstance);
        }
        public   void SetProcessEditBeforeWithSearch<BusinessProcess>() where BusinessProcess : BusinessProcessSelect<TObject, TResult, TSearch>, new()
        {
            business.SetPreProcessEditSearch(new BusinessProcess());
        }

        public void SetProcessEditAfterWithSearch(BusinessProcessSelect<TObject, TResult, TSearch> processInstance)
        {
            business.SetPostProcessEditSearch(processInstance);

        }
        public void SetProcessEditAfterWithSearch<BusinessProcess>() where BusinessProcess : BusinessProcessSelect<TObject, TResult, TSearch>, new()
        {
            business.SetPostProcessEditSearch(new BusinessProcess());
        }

        #endregion Set

        #region Functions

        public virtual TResult Select(TSearch searchObject)
        {
            return business.Select<TObject, TResult, TSearch>(searchObject);
        }

        public virtual int Delete(TSearch searchObject)
        {
            return business.Delete<TObject, TResult, TSearch>(searchObject, false);
        }

        public virtual int Delete(TSearch searchObject, bool getDeletedItems)
        {
            return business.Delete<TObject, TResult, TSearch>(searchObject, getDeletedItems);
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
            business.DoValidation<TSearch>(businessObject.Search, businessObject, FunctionName.Select);
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