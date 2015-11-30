using MKS.Core.Activity;
using MKS.Core.Business.Interfaces;
using MKS.Core.Concurrency;
using MKS.Core.Configuration;
using MKS.Core.Model;
using MKS.Core.Security;
using System;

namespace MKS.Core.Business
{
    /// <summary>
    /// Objet de base pour l'éxécution d'une fonction d'affaire simple.
    /// </summary>
    /// <typeparam name="TObject">Objet de traitement</typeparam>
    public abstract class BusinessExecute<TObject> : IBusinessOperationsExecute<TObject>
    {
        private Business business;

        public BusinessExecute()
        {
            business = new Business();
        }

        public BusinessExecute(bool useTransactionScope)
        {
            business = new Business(useTransactionScope);
        }
        public BusinessExecute(bool useTransactionScope, ITrackingAdapter tracking) 
        {
            business=new Business(useTransactionScope,tracking);
        }
        #region Set

        public void SetTracking<TTrackingAdapter>() where TTrackingAdapter : ITrackingAdapter, new()
        {
            business.SetTracking(new TTrackingAdapter());
        }

        public void SetActivityLog<EventLog>() where EventLog : IActivityLogOperationsExecute<TObject>, IActivityAdapter, new()
        {
            business.SetEventLog(new EventLog(), new EventLog());
        }

        public void SetActivityLog<EventLog>(IActivity activityInstance) where EventLog : IActivityLogOperationsExecute<TObject>, IActivityAdapter, new()
        {
            business.SetEventLog(new EventLog(), new EventLog(), activityInstance);
        }

        public void SetConcurrency<Concurrency>() where Concurrency : IConcurrencyOperationsExecute<TObject>, new()
        {
            business.SetConcurrency(new Concurrency());
        }

        public void SetSecurity<Security>() where Security : ISecurityPermission, ISecurityAdapter, new()
        {
            business.SetSecurity(new Security(), new Security());
        }

        public void SetConfiguration<Configuration>() where Configuration : IConfiguration, new()
        {
            business.SetConfiguration(new Configuration());
        }

        public void SetProcessExecute<BusinessProcess>() where BusinessProcess : BusinessProcessExecute<TObject>, new()
        {
            business.SetProcessExecute(new BusinessProcess());
        }

        public void SetProcessFinalize<BusinessProcess>() where BusinessProcess : BusinessProcessExecute<TObject>, new()
        {
            business.SetProcessFinalize(new BusinessProcess());
        }

        public void SetProcessError<BusinessProcess>() where BusinessProcess : BusinessProcessError, new()
        {
            business.SetProcessError(new BusinessProcess());
        }

        public void SetValidation<Validation>() where Validation : IValidation<TObject>, new()
        {
            business.SetValidation(new Validation());
        }

        #endregion Set

        #region Functions

        public virtual void Execute(TObject myObject)
        {
            business.Execute(myObject);
        }

        public void ExecuteThreaded(TObject myObject, int timeoutMs = 30000)
        {
            business.ExecuteThreaded(myObject, timeoutMs);
        }
        public TObject ExecuteSynchrone(TObject myObject)
        {
            return business.ExecuteSynchrone(myObject);
        }

        #endregion Functions

        #region Concurrency

        public void DoConcurrencyExecute(BusinessObjectExecute<TObject> businessObject, BusinessStep step)
        {
            business.DoConcurrencyExecute(businessObject, step);
        }

        #endregion Concurrency

        #region TActivityLog

        public void DoActivityLogExecute(BusinessObjectExecute<TObject> businessObject)
        {
            business.DoActivityLogExecute(businessObject);
        }

        #endregion TActivityLog

        #region Security

        public void DoSecurityCheckExecute()
        {
            business.DoSecurityCheck(FunctionName.Execute);
        }

        #endregion Security

        #region DoProcess

        public bool DoProcessExecute(ref BusinessObjectExecute<TObject> businessObject)
        {
            return business.DoProcessExecute(ref businessObject);
        }

        #endregion DoProcess

        public void HandleException(Exception ex, IBusinessObject businessObject)
        {
            business.HandleException(ex, businessObject);
        }

        public virtual void Dispose()
        {
            business.Dispose();
        }


    }
}