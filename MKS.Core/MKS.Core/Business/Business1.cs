using MKS.Core.Activity;
using MKS.Core.Business.Interfaces;
using MKS.Core.Concurrency;
using MKS.Core.Configuration;
using MKS.Core.Mapping;
using MKS.Core.Model;
using MKS.Core.Security;
using System;

namespace MKS.Core.Business
{
    /// <summary>
    /// Classe d'affaire de base pour le traitement simple sur un objet
    /// </summary>
    /// <typeparam name="TObject">Objet de traitement</typeparam>
    public abstract class Business<TObject> : IBusinessOperations<TObject>
    {
        internal Business business;


        public Business()
        {
            business = new Business();
        }

        public Business(bool _useTransactionScope)
        {
            business = new Business(_useTransactionScope);
        }
        internal Business(bool _useTransactionScope, ITrackingAdapter tracking) 
        {
            business = new Business(_useTransactionScope,tracking);
            
        }
        #region Set
        public void SetTracking(ITrackingAdapter trackingInstance)
        {
            business.SetTracking(trackingInstance);
        }
        public void SetTracking<TTrackingAdapter>() where TTrackingAdapter : ITrackingAdapter, new()
        {
            business.SetTracking(new TTrackingAdapter());
        }
        public void SetDataMap(IDataOperations<TObject> dataOperatorInstance )
        {
            business.SetDataMap(dataOperatorInstance);
        }
        public void SetDataMap<Mapping>() where Mapping : IDataOperations<TObject>, new()
        {
            business.SetDataMap(new Mapping());
        }
        public void SetActivityLog(IActivityLogOperations<TObject> activityInstance, IActivityAdapter activityAdapterInstance)
        {
            business.SetEventLog(activityInstance, activityAdapterInstance);
        }
        public void SetActivityLog<EventLog>() where EventLog : IActivityLogOperations<TObject>, IActivityAdapter, new()
        {
            business.SetEventLog(new EventLog(), new EventLog());
        }
        public void SetActivityLog<EventLog>(IActivity activityInstance) where EventLog : IActivityLogOperations<TObject>, IActivityAdapter, new()
        {
            business.SetEventLog(new EventLog(), new EventLog(), activityInstance);
        }
        public void SetConcurrency(IConcurrencyOperations<TObject> concurrencyInstance)
        {
            business.SetConcurrency(concurrencyInstance);
        }
        public void SetConcurrency<Concurrency>() where Concurrency : IConcurrencyOperations<TObject>, new()
        {
            business.SetConcurrency(new Concurrency());
        }
        public void SetSecurity(ISecurityPermission securityPermission, ISecurityAdapter securityAdapter)
        {
            business.SetSecurity(securityPermission, securityAdapter);
        }
        public void SetSecurity<Security>() where Security : ISecurityPermission, ISecurityAdapter, new()
        {
            business.SetSecurity(new Security(), new Security());
        }
        public void SetConfiguration(IConfiguration configurationInstance)
        {
            business.SetConfiguration(configurationInstance);
        }
        public void SetConfiguration<Configuration>() where Configuration : IConfiguration, new()
        {
            business.SetConfiguration(new Configuration());
        }
        public void SetValidation(IValidation<TObject> validationInstance)
        {
            business.SetValidation(validationInstance);
        }
        public void SetValidation<Validation>() where Validation : IValidation<TObject>, new()
        {
            business.SetValidation(new Validation());
        }
        public void SetValidationObject(IValidation<TObject> validaitonInstance)
        {
            business.SetValidationObject(validaitonInstance);
        }
        public void SetValidationObject<Validation>() where Validation : IValidation<TObject>, new()
        {
            business.SetValidationObject(new Validation());
        }
        public void SetDetectChange(IChangeDetection<TObject> changeDetectionInstance)
        {
            business.SetDetectChange(changeDetectionInstance);
        }
        public void SetDetectChange<DetectChange>() where DetectChange : IChangeDetection<TObject>, new()
        {
            business.SetDetectChange(new DetectChange());
        }
        public void SetProcessError(BusinessProcessError businessProcessErrorInstance)
        {
            business.SetProcessError(businessProcessErrorInstance);
        }
        public void SetProcessError<BusinessProcess>() where BusinessProcess : BusinessProcessError, new()
        {
            business.SetProcessError(new BusinessProcess());
        }
        public void SetPreProcessAdd(BusinessProcessAdd<TObject> businessProcessAdd)
        {
            business.SetPreProcessAdd(businessProcessAdd);
        }
        public void SetPreProcessAdd<BusinessProcess>() where BusinessProcess : BusinessProcessAdd<TObject>, new()
        {
            business.SetPreProcessAdd(new BusinessProcess());
        }
        public void SetPostProcessAdd(BusinessProcessAdd<TObject> businessProcessAddInstance)
        {
            business.SetPostProcessAdd(businessProcessAddInstance);
        }
        public void SetPostProcessAdd<BusinessProcess>() where BusinessProcess : BusinessProcessAdd<TObject>, new()
        {
            business.SetPostProcessAdd(new BusinessProcess());
        }
        public void SetPreProcessUpdate(BusinessProcessUpdate<TObject> businessProcessUpdateInstance)
        {
            business.SetProcessUpdateBefore(businessProcessUpdateInstance);
        }
        public void SetPreProcessUpdate<BusinessProcess>() where BusinessProcess : BusinessProcessUpdate<TObject>, new()
        {
            business.SetProcessUpdateBefore(new BusinessProcess());
        }
        public void SetPostProcessUpdate(BusinessProcessUpdate<TObject> businessProcessInstance) 
        {
            business.SetPostProcessUpdate(businessProcessInstance);
        }
        public void SetPostProcessUpdate<BusinessProcess>() where BusinessProcess : BusinessProcessUpdate<TObject>, new()
        {
            business.SetPostProcessUpdate(new BusinessProcess());
        }
        public void SetPreProcessSelect(BusinessProcessSelect<TObject> businessProcessInstance)
        {
            business.SetPreProcessSelect(businessProcessInstance);
        }
        public void SetPreProcessSelect<BusinessProcess>() where BusinessProcess : BusinessProcessSelect<TObject>, new()
        {
            business.SetPreProcessSelect(new BusinessProcess());
        }
        public void SetPostProcessSelect(BusinessProcessSelect<TObject> businessProcessInstance)
        {
            business.SetPostProcessSelect(businessProcessInstance);
        }
        public void SetPostProcessSelect<BusinessProcess>() where BusinessProcess : BusinessProcessSelect<TObject>, new()
        {
            business.SetPostProcessSelect(new BusinessProcess());
        }
        public void SetPreProcessDelete(BusinessProcessDelete<TObject> businessProcessInstance) 
        {
            business.SetPreProcessDelete(businessProcessInstance);
        }
        public void SetPreProcessDelete<BusinessProcess>() where BusinessProcess : BusinessProcessDelete<TObject>, new()
        {
            business.SetPreProcessDelete(new BusinessProcess());
        }
        public void SetPostProcessDelete(BusinessProcessDelete<TObject> businessProcessInstance)
        {
            business.SetPostProcessDelete(businessProcessInstance);
        }
        public void SetPostProcessDelete<BusinessProcess>() where BusinessProcess : BusinessProcessDelete<TObject>, new()
        {
            business.SetPostProcessDelete(new BusinessProcess());
        }
        public void SetPreProcessEdit(BusinessProcessSelect<TObject> businessProcessInstance) 
        {
            business.SetPreProcessEdit(businessProcessInstance);
        }
        public void SetPreProcessEdit<BusinessProcess>() where BusinessProcess : BusinessProcessSelect<TObject>, new()
        {
            business.SetPreProcessEdit(new BusinessProcess());
        }
        public void SetPostProcessEdit(BusinessProcessSelect<TObject> businessProcessSelectInstance) 
        {
            business.SetPostProcessEdit(businessProcessSelectInstance);
        }
        public void SetPostProcessEdit<BusinessProcess>() where BusinessProcess : BusinessProcessSelect<TObject>, new()
        {
            business.SetPostProcessEdit(new BusinessProcess());
        }

        #endregion Set

        #region Functions

        public virtual TObject Add(TObject myObject)
        {
            return business.Add<TObject>(myObject);
        }

        public virtual TObject Update(TObject myObject)
        {
            return business.Update<TObject>(myObject);
        }

        public virtual TObject Select(TObject myObject)
        {
            return business.Select<TObject>(myObject);
        }

        public virtual int Delete(TObject myObject)
        {
            return business.Delete<TObject>(myObject, false);
        }

        public virtual int Delete(TObject myObject, bool getDeletedItems)
        {
            return business.Delete<TObject>(myObject, getDeletedItems);
        }

        public virtual TObject Edit(TObject myObject)
        {
            return business.Edit<TObject>(myObject);
        }

        #endregion Functions

        #region Concurrency

        public void DoConcurrencyAdd(BusinessObjectAdd<TObject> businessObject, BusinessStep step)
        {
            business.DoConcurrencyAdd<TObject>(businessObject, step);
        }

        public void DoConcurrencyUpdate(BusinessObjectUpdate<TObject> businessObject, BusinessStep step)
        {
            business.DoConcurrencyUpdate<TObject>(businessObject, step);
        }

        public void DoConcurrencyDelete(BusinessObjectDelete<TObject> businessObject, BusinessStep step)
        {
            business.DoConcurrencyDelete<TObject>(businessObject, step);
        }

        public void DoConcurrencyEdit(BusinessObjectSelect<TObject> businessObject, BusinessStep step)
        {
            business.DoConcurrencyEdit<TObject>(businessObject, step);
        }

        #endregion Concurrency

        #region Mapping

        public TObject DoMappingAdd(BusinessObjectAdd<TObject> businessObject)
        {
            return business.DoMappingAdd<TObject>(businessObject);
        }

        public TObject DoMappingUpdate(BusinessObjectUpdate<TObject> businessObject)
        {
            return business.DoMappingUpdate<TObject>(businessObject);
        }

        public TObject DoMappingSelect(BusinessObjectSelect<TObject> businessObject)
        {
            return business.DoMappingSelect<TObject>(businessObject);
        }

        public int DoMappingDelete(BusinessObjectDelete<TObject> businessObject)
        {
            return business.DoMappingDelete<TObject>(businessObject);
        }

        #endregion Mapping

        #region Activity

        public void DoActivityLogAdd(BusinessObjectAdd<TObject> businessObject)
        {
            business.DoActivityLogAdd<TObject>(businessObject);
        }

        public void DoActivityLogUpdate(BusinessObjectUpdate<TObject> businessObject)
        {
            business.DoActivityLogUpdate<TObject>(businessObject);
        }

        public void DoActivityLogUpdate(BusinessObjectUpdate<TObject> businessObject, ChangeDetections detectChanges)
        {
            business.DoActivityLogUpdate<TObject>(businessObject, detectChanges);
        }

        public void DoActivityLogSelect(BusinessObjectSelect<TObject> businessObject)
        {
            business.DoActivityLogSelect<TObject>(businessObject);
        }

        public void DoActivityLogDelete(BusinessObjectDelete<TObject> businessObject)
        {
            business.DoActivityLogDelete<TObject>(businessObject);
        }

        public void DoActivityLogEdit(BusinessObjectSelect<TObject> businessObject)
        {
            business.DoActivityLogEdit<TObject>(businessObject);
        }

        #endregion Activity

        #region DetectChange

        /// <summary>
        /// Permet de détecter les changements apportés à un objet
        /// </summary>
        /// <param name="businessObject"></param>
        /// <returns></returns>
        public ChangeDetections DoDetectChange(BusinessObjectUpdate<TObject> businessObject)
        {
            return business.DoDetectChange<TObject>(businessObject);
        }

        /// <summary>
        /// Permet de détecter les changements apportés à un objet
        /// </summary>
        /// <param name="updatedObject"></param>
        /// <param name="oldObject"></param>
        /// <returns></returns>
        public ChangeDetections DoDetectChange(TObject updatedObject, TObject oldObject)
        {
            return business.DoDetectChange<TObject>(updatedObject, oldObject);
        }

        #endregion DetectChange

        #region Security

        public void DoSecurityCheckAdd()
        {
            business.DoSecurityCheck(FunctionName.Add);
        }

        public void DoSecurityCheckUpdate()
        {
            business.DoSecurityCheck(FunctionName.Update);
        }

        public void DoSecurityCheckSelect()
        {
            business.DoSecurityCheck(FunctionName.Select);
        }

        public void DoSecurityCheckDelete()
        {
            business.DoSecurityCheck(FunctionName.Delete);
        }

        #endregion Security

        #region Validation

        public void DoValidationAdd(BusinessObjectAdd<TObject> businessObject)
        {
            business.DoValidation<TObject>(businessObject.Parameter, businessObject, FunctionName.Add);
        }

        public void DoValidationUpdate(BusinessObjectUpdate<TObject> businessObject)
        {
            business.DoValidation<TObject>(businessObject.Parameter, businessObject, FunctionName.Update);
        }

        public void DoValidationSelect(BusinessObjectSelect<TObject> businessObject)
        {
            business.DoValidation<TObject>(businessObject.Parameter, businessObject, FunctionName.Select);
        }

        public void DoValidationDelete(BusinessObjectDelete<TObject> businessObject)
        {
            business.DoValidation<TObject>(businessObject.Parameter, businessObject, FunctionName.Delete);
        }

        #endregion Validation

        #region DoProcess

        public bool DoProcessAdd(ref BusinessObjectAdd<TObject> businessObject, BusinessStep step)
        {
            return business.DoProcessAdd<TObject>(ref businessObject, step);
        }

        public bool DoProcessUpdate(ref BusinessObjectUpdate<TObject> businessObject, BusinessStep step)
        {
            return business.DoProcessUpdate<TObject>(ref businessObject, step);
        }

        public bool DoProcessSelect(ref BusinessObjectSelect<TObject> businessObject, BusinessStep step)
        {
            return business.DoProcessSelect<TObject>(ref businessObject, step);
        }

        public bool DoProcessDelete(ref BusinessObjectDelete<TObject> businessObject, BusinessStep step)
        {
            return business.DoProcessDelete<TObject>(ref businessObject, step);
        }

        public bool DoProcessEdit(ref BusinessObjectSelect<TObject> businessObject, BusinessStep step)
        {
            return business.DoProcessEdit<TObject>(ref businessObject, step);
        }

        #endregion DoProcess

        public void DoAddMessageToOutput(object ouput, IBusinessObject businessObject)
        {
            business.DoAddMessageToOutput(ouput, businessObject.ProcessResults);
        }

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