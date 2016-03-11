using MKS.Core.Activity;
using MKS.Core.Business.Interfaces;
using MKS.Core.Concurrency;
using MKS.Core.Configuration;
using MKS.Core.Connector;
using MKS.Core.Mapping;
using MKS.Core.Model;
using MKS.Core.Model.Error;
using MKS.Core.Resources;
using MKS.Core.Security;
using MKS.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace MKS.Core.Business
{
    internal class Business : IDisposable, IBusiness
    {
        //Modifier pour publique la classe pour satisfaire le DLL de d'aggregation.
        internal Business()
        {
            //DoTracking(CA_CODE.CA_START, "Create Business", null);
        }

        internal Business(bool _useTransactionScope)
        {
            useTransactionScope = _useTransactionScope;
            //DoTracking(CA_CODE.CA_START, "Create Business", null);

        }
        
        internal Business(bool _useTransactionScope, ITrackingAdapter tracking) 
        {
            useTransactionScope = _useTransactionScope;
            
            SetTracking(tracking);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("OSVersion:{0} , Processor:{1}, RunTimeVersion:{2} , MKSVersion:{3}", Environment.OSVersion.ToString(), Environment.ProcessorCount.ToString(), Environment.Version.ToString(), ApplicationInfo.ToString());

            DoTracking(CA_CODE.CA_IDENITIFICATION, Globals.GetUserEnvironment.ToString(), sb.ToString());
            DoTracking(CA_CODE.CA_START, "Create Business", null);
            
        }

        #region Private members
        
        private string _GUID_TRACING_EVENT = Guid.NewGuid().ToString();// (DateTime.Now.Ticks.ToString()).ToString();
        private IDataOperations mapping;
        private IActivityLogOperations eventLog;
        private IActivity _activityObject;
        private IConcurrencyOperations reservation;
        private IChangeDetection detectChange;
        private ISearchContract searchContract;
        private IRoutingAdapter routingUsage;
        private BusinessProcessError processError;
        
        private ITrackingAdapter trackingAdapter;
        private IBusinessProcess processExecute;
        private IBusinessProcess processFinalize;
        private IBusinessProcess processAddBefore;
        private IBusinessProcess processUpdateBefore;
        private IBusinessProcess processDeleteBefore;
        private IBusinessProcess processDeleteKeyBefore;
        private IBusinessProcess processDeleteSearchBefore;
        private IBusinessProcess processSelectBefore;
        private IBusinessProcess processSelectKeyBefore;
        private IBusinessProcess processSelectSearchBefore;
        private IBusinessProcess processEditBefore;
        private IBusinessProcess processEditKeyBefore;
        private IBusinessProcess processEditSearchBefore;

        private IBusinessProcess processAddAfter;
        private IBusinessProcess processUpdateAfter;
        private IBusinessProcess processDeleteAfter;
        private IBusinessProcess processDeleteKeyAfter;
        private IBusinessProcess processDeleteSearchAfter;
        private IBusinessProcess processSelectAfter;
        private IBusinessProcess processSelectKeyAfter;
        private IBusinessProcess processSelectSearchAfter;
        private IBusinessProcess processEditAfter;
        private IBusinessProcess processEditKeyAfter;
        private IBusinessProcess processEditSearchAfter;

        private IBusinessProcess processAddAggregator;
        private IBusinessProcess processUpdateAggregator;
        private IBusinessProcess processDeleteAggregator;
        private IBusinessProcess processDeleteKeyAggregator;
        private IBusinessProcess processDeleteSearchAggregator;
        private IBusinessProcess processSelectAggregator;
        private IBusinessProcess processSelectKeyAggregator;
        private IBusinessProcess processSelectSearchAggregator;
        private IBusinessProcess processEditAggregator;
        private IBusinessProcess processEditKeyAggregator;
        private IBusinessProcess processEditSearchAggregator;
        private IBusinessProcess processExecuteAggregator;

        private ISecurityPermission securityPermission;
        private IConfiguration configuration;

        private IValidation validation;
        private IValidation validationKey;
        private IValidation validationSearch;
        private IValidation validationObject;

        private IActivityAdapter _activityLogLoad;
        private ISecurityAdapter securityUsage;

        private List<IContract> contracts;
        private bool useTransactionScope = true;

        #endregion Private members

        #region Set

        public void SetTracking(ITrackingAdapter tracking)
        {
            
            trackingAdapter = tracking;
            
            DoTracking(CA_CODE.CA_SET_PROCESS, CA_CODE.CA_PROCESS_TRACKING, tracking.GetType().FullName);
        }

        public void SetRouting(IRoutingAdapter routingUsageInstance)
        {
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_PROCESS_ROUTING,routingUsageInstance.GetType().FullName);
            routingUsage = routingUsageInstance;
            searchContract = routingUsageInstance.SearchContract;
        }

        public void SetRouting(IRoutingAdapter routingUsageInstance, ISearchContract searchContractInstance)
        {
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_PROCESS_ROUTING,routingUsageInstance.GetType().FullName,searchContractInstance.GetType().FullName);
            routingUsage = routingUsageInstance;
            searchContract = searchContractInstance;
        }

        public void SetDataMap(IDataOperations mappingInstance)
        {
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_PROCESS_MAPPING,mappingInstance.GetType().FullName);
            mapping = mappingInstance;
        }

        public void SetEventLog(IActivityLogOperations eventLogInstance, IActivityAdapter activityLogInstance)
        {
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_PROCESS_EVENT,eventLogInstance.GetType().FullName,activityLogInstance.GetType().FullName);
            eventLog = eventLogInstance;
            _activityLogLoad = activityLogInstance;
            _activityObject = activityLogInstance.Activity;
        }

        public void SetEventLog(IActivityLogOperations eventLogInstance, IActivityAdapter activityLogInstance, IActivity activityInstance)
        {
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_PROCESS_EVENT,eventLogInstance.GetType().FullName,activityLogInstance.GetType().FullName);
            eventLog = eventLogInstance;
            _activityLogLoad = activityLogInstance;
            _activityObject = activityInstance;
        }

        public void SetDetectChange(IChangeDetection detectChangeInstance)
        {
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_PROCESS_EVENT,detectChangeInstance.GetType().FullName);
            detectChange = detectChangeInstance;
        }

        public void SetConcurrency(IConcurrencyOperations concurrencyInstance)
        {
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_PROCESS_RESERV,concurrencyInstance.GetType().FullName);
            reservation = concurrencyInstance;
        }

        public void SetSecurity(ISecurityPermission securityPermissionInstance, ISecurityAdapter securityUsageInstance)
        {
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_PROCESS_SECURITY,securityPermissionInstance.GetType().FullName,securityUsageInstance.GetType().FullName);
            securityPermission = securityPermissionInstance;
            securityUsage = securityUsageInstance;
        }

        public void SetConfiguration(IConfiguration configurationInstance)
        {
            DoTracking(CA_CODE.CA_SET_PROCESS, CA_CODE.CA_CONFIG, configurationInstance.GetType().FullName);
            configuration = configurationInstance;
        }

        public void SetValidation(IValidation validationInstance)
        {
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_PROCESS_VALIDATION,"Object",validationInstance.GetType().FullName);
            validation = validationInstance;
        }

        public void SetValidationKey(IValidation validationInstance)
        {
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_PROCESS_VALIDATION,"Key",validationInstance.GetType().FullName);
            validationKey = validationInstance;
        }

        public void SetValidationSearch(IValidation validationInstance)
        {
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_PROCESS_VALIDATION,"Search",validationInstance.GetType().FullName);
            validationSearch = validationInstance;
        }

        public void SetValidationObject(IValidation validationInstance)
        {
            
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_PROCESS_VALIDATION,"Object",validationInstance.GetType().FullName);
            validationObject = validationInstance;
        }
        
        public void SetPreProcessAdd<TObject>(BusinessProcessAdd<TObject> businessProcessAddInstance)
        {
            Type g=typeof(TObject);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_FONC_ADD,"Object",g.FullName);
            processAddBefore = businessProcessAddInstance;
        }

        public void SetProcessUpdateBefore<TObject>(BusinessProcessUpdate<TObject> businessProcessUpdateInstance)
        {
            Type g=typeof(TObject);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_FONC_UPDATE,"PRE Object",g.FullName);
            processUpdateBefore = businessProcessUpdateInstance;
        }

        public void SetPreProcessSelect<TObject>(BusinessProcessSelect<TObject> businessProcessSelectInstance)
        {
            Type g=typeof(TObject);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_FONC_SELECT,"Object",g.FullName);
            processSelectBefore = businessProcessSelectInstance;
        }

        public void SetPreProcessSelectKey<TObject, TKey>(BusinessProcessSelect<TObject, TKey> businessProcessSelectInstance)
            where TKey : IKey
        {
            Type g=typeof(TKey);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_FONC_SELECT,"Key",g.FullName);
            processSelectKeyBefore = businessProcessSelectInstance;
        }

        public void SetPreProcessSelectSearch<TObject, TResult, TSearch>(BusinessProcessSelect<TObject, TResult, TSearch> businessProcessSelectInstance)
            where TSearch : ISearch
        {
            Type g=typeof(TSearch);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_FONC_SELECT,"Search",g.FullName);
            processSelectSearchBefore = businessProcessSelectInstance;
        }

        public void SetPreProcessDelete<TObject>(BusinessProcessDelete<TObject> businessProcessDeleteInstance)
        {
            Type g=typeof(TObject);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_FONC_DELETE,"Object",g.FullName);
            processDeleteBefore = businessProcessDeleteInstance;
        }

        public void SetPreProcessDeleteKey<TObject, TKey>(BusinessProcessDelete<TObject, TKey> businessProcessSelectInstance)
            where TKey : IKey
        {
            Type g=typeof(TKey);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_FONC_DELETE,"Key",g.FullName);
            processDeleteKeyBefore = businessProcessSelectInstance;
        }

        public void SetPreProcessDeleteSearch<TObject, TResult, TSearch>(BusinessProcessDelete<TObject, TResult, TSearch> businessProcessSelectInstance)
            where TSearch : ISearch
        {
            Type g=typeof(TSearch);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_FONC_DELETE,"Search",g.FullName);
            processDeleteSearchBefore = businessProcessSelectInstance;
        }

        public void SetPreProcessEdit<TObject>(BusinessProcessSelect<TObject> businessProcessEditInstance)
        {
            Type g=typeof(TObject);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_FONC_EDIT,"Object",g.FullName);
            processEditBefore = businessProcessEditInstance;
        }

        public void SetPreProcessEditKey<TObject, TKey>(BusinessProcessSelect<TObject, TKey> businessProcessEditInstance)
            where TKey : IKey
        {
            Type g=typeof(TKey);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_FONC_EDIT,"Key", g.FullName);
            processEditKeyBefore = businessProcessEditInstance;
        }

        public void SetPreProcessEditSearch<TObject, TResult, TSearch>(BusinessProcessSelect<TObject, TResult, TSearch> businessProcessEditInstance)
            where TSearch : ISearch
        {
            Type g=typeof(TSearch);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_FONC_EDIT,"Search",g.FullName);
            processEditSearchBefore = businessProcessEditInstance;
        }

        public void SetPostProcessAdd<TObject>(BusinessProcessAdd<TObject> businessProcessAddInstance)
        {
            Type g=typeof(TObject);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_FONC_ADD,"Object",g.FullName);
            processAddAfter = businessProcessAddInstance;
        }

        public void SetPostProcessUpdate<TObject>(BusinessProcessUpdate<TObject> businessProcessUpdateInstance)
        {
            Type g=typeof(TObject);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_FONC_UPDATE,"Object",g.FullName);
            processUpdateAfter = businessProcessUpdateInstance;
        }

        public void SetPostProcessDelete<TObject>(BusinessProcessDelete<TObject> businessProcessDeleteInstance)
        {
            Type g = typeof(TObject);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_FONC_DELETE,"Object",g.FullName);
            processDeleteAfter = businessProcessDeleteInstance;
        }

        public void SetPostProcessDeleteKey<TObject, TKey>(BusinessProcessDelete<TObject, TKey> businessProcessSelectInstance)
            where TKey : IKey
        {
            Type g=typeof(TKey);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_FONC_DELETE,"Key",g.FullName);
            processDeleteKeyAfter = businessProcessSelectInstance;
        }

        public void SetPostProcessDeleteSearch<TObject, TResult, TSearch>(BusinessProcessDelete<TObject, TResult, TSearch> businessProcessSelectInstance)
            where TSearch : ISearch
        {
            Type g=typeof(TSearch);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_FONC_DELETE,"Search",g.FullName);
            processDeleteSearchAfter = businessProcessSelectInstance;
        }

        public void SetPostProcessSelect<TObject>(BusinessProcessSelect<TObject> businessProcessSelectInstance)
        {
            Type g=typeof(TObject);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_FONC_SELECT,"Object",g.FullName);
            processSelectAfter = businessProcessSelectInstance;
        }

        public void SetPostProcessSelectKey<TObject, TKey>(BusinessProcessSelect<TObject, TKey> businessProcessSelectInstance)
            where TKey : IKey
        {
            Type g=typeof(TKey);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_FONC_SELECT,"Key",g.FullName);
            processSelectKeyAfter = businessProcessSelectInstance;
        }

        public void SetPostProcessSelectSearch<TObject, TResult, TSearch>(BusinessProcessSelect<TObject, TResult, TSearch> businessProcessSelectInstance)
            where TSearch : ISearch
        {

            Type g=typeof(TSearch);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_FONC_SELECT,"Search",g.FullName);
            processSelectSearchAfter = businessProcessSelectInstance;
        }

        public void SetPostProcessEdit<TObject>(BusinessProcessSelect<TObject> businessProcessEditInstance)
        {
            Type g=typeof(TObject);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_FONC_EDIT,"Object",g.FullName);
            processEditAfter = businessProcessEditInstance;
        }

        public void SetPostProcessEditKey<TObject, TKey>(BusinessProcessSelect<TObject, TKey> businessProcessEditInstance)
            where TKey : IKey
        {
            Type g=typeof(TKey);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_FONC_EDIT,"Key",g.FullName);
            processEditKeyAfter = businessProcessEditInstance;
        }

        public void SetPostProcessEditSearch<TObject, TResult, TSearch>(BusinessProcessSelect<TObject, TResult, TSearch> businessProcessEditInstance)
            where TSearch : ISearch
        {
            Type g=typeof(TSearch);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_FONC_EDIT,"Search",g.FullName);
            processEditSearchAfter = businessProcessEditInstance;
        }

        public void SetProcessError(BusinessProcessError businessProcessErrorInstance)
        {                        
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_PROCESS_ERROR,null);
            processError = businessProcessErrorInstance;
        }

        public void SetProcessExecute<TObject>(BusinessProcessExecute<TObject> businessProcessExecuteInstance)
        {
            Type g=typeof(TObject);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_PROCESS_EXECUTE,g.FullName);
            processExecute = businessProcessExecuteInstance;
        }

        public void SetProcessFinalize<TObject>(BusinessProcessExecute<TObject> businessProcessFinalizeInstance)
        {
            Type g=typeof(TObject);
            DoTracking(CA_CODE.CA_SET_PROCESS,CA_CODE.CA_PROCESS_EXECUTE,"Finalize",g.FullName);
            processFinalize = businessProcessFinalizeInstance;
        }

        #endregion Set

        #region Set Aggregator

        public void SetProcessAddAggregator<TObject>(BusinessProcessAddAggregator<TObject> businessProcessAddInstance)
        {
            processAddAggregator = businessProcessAddInstance;
        }

        public void SetProcessUpdateAggregator<TObject>(BusinessProcessUpdateAggregator<TObject> businessProcessUpdateInstance)
        {
            processUpdateAggregator = businessProcessUpdateInstance;
        }

        public void SetProcessSelectAggregator<TObject>(BusinessProcessSelectAggregator<TObject> businessProcessSelectInstance)
        {
            processSelectAggregator = businessProcessSelectInstance;
        }

        public void SetProcessSelectKeyAggregator<TObject, TKey>(BusinessProcessSelectAggregator<TObject, TKey> businessProcessSelectInstance)
            where TKey : IKey
        {
            processSelectKeyAggregator = businessProcessSelectInstance;
        }

        public void SetProcessSelectSearchAggregator<TObject, TResult, TSearch>(BusinessProcessSelectAggregator<TObject, TResult, TSearch> businessProcessSelectInstance)
            where TSearch : ISearch
        {
            processSelectSearchAggregator = businessProcessSelectInstance;
        }

        public void SetProcessDeleteAggregator<TObject>(BusinessProcessDeleteAggregator<TObject> businessProcessDeleteInstance)
        {
            processDeleteAggregator = businessProcessDeleteInstance;
        }

        public void SetProcessDeleteKeyAggregator<TObject, TKey>(BusinessProcessDeleteAggregator<TObject, TKey> businessProcessSelectInstance)
            where TKey : IKey
        {
            processDeleteKeyAggregator = businessProcessSelectInstance;
        }

        public void SetProcessDeleteSearchAggregator<TObject, TResult, TSearch>(BusinessProcessDeleteAggregator<TObject, TResult, TSearch> businessProcessSelectInstance)
            where TSearch : ISearch
        {
            processDeleteSearchAggregator = businessProcessSelectInstance;
        }

        public void SetProcessEditAggregator<TObject>(BusinessProcessSelectAggregator<TObject> businessProcessEditInstance)
        {
            processEditAggregator = businessProcessEditInstance;
        }

        public void SetProcessEditKeyAggregator<TObject, TKey>(BusinessProcessSelectAggregator<TObject, TKey> businessProcessEditInstance)
            where TKey : IKey
        {
            processEditKeyAggregator = businessProcessEditInstance;
        }

        public void SetProcessEditSearchAggregator<TObject, TResult, TSearch>(BusinessProcessSelectAggregator<TObject, TResult, TSearch> businessProcessEditInstance)
            where TSearch : ISearch
        {
            processEditSearchAggregator = businessProcessEditInstance;
        }

        public void SetProcessExecuteAggregator<TObject>(BusinessProcessExecuteAggregator<TObject> businessProcessExecuteInstance)
        {
            processExecuteAggregator = businessProcessExecuteInstance;
        }

        #endregion Set Aggregator

        #region Functions

        public TObject Add<TObject>(TObject myObject)
        {
            BusinessObjectAdd<TObject> businessObject = null;
            
            try
            {
                using (TransactionScope scope = TransactionUtils.CreateTransactionScope())
                {
                    businessObject = new BusinessObjectAdd<TObject>(myObject, _activityObject);
                    //businessObject.DataMap = (IDataOperations<TObject>)mapping;
                    businessObject.DataMap = mapping;

                    DoSecurityCheck(FunctionName.Add);

                    DoValidation<TObject>(businessObject.Parameter, businessObject, FunctionName.Add);

                    DoConcurrencyAdd<TObject>(businessObject, BusinessStep.BeforeDataMap);

                    if (DoProcessAdd<TObject>(ref businessObject, BusinessStep.BeforeDataMap))
                    {
                        businessObject.Output = DoMappingAdd<TObject>(businessObject);

                        if (DoProcessAdd<TObject>(ref businessObject, BusinessStep.AfterDataMap))
                            DoActivityLogAdd<TObject>(businessObject);
                    }

                    DoAddMessageToOutput(businessObject.Output, businessObject.ProcessResults);

                    DoConcurrencyAdd<TObject>(businessObject, BusinessStep.AfterDataMap);

                    scope.Complete();

                    return businessObject.Output;
                }
            }
            catch (Exception ex)
            {
                
                HandleException(ex, businessObject);
                throw;
            }
        }

        public TObject Update<TObject>(TObject myObject)
        {
            BusinessObjectUpdate<TObject> businessObject = null;
            
            try
            {
                using (TransactionScope scope = TransactionUtils.CreateTransactionScope())
                {
                    businessObject = new BusinessObjectUpdate<TObject>(myObject, _activityObject);
                    //businessObject.DataMap = (IDataOperations<TObject>)mapping;
                    businessObject.DataMap = mapping;

                    DoSecurityCheck(FunctionName.Update);

                    DoValidation<TObject>(businessObject.Parameter, businessObject, FunctionName.Update);

                    DoConcurrencyUpdate<TObject>(businessObject, BusinessStep.BeforeDataMap);

                    if (DoProcessUpdate<TObject>(ref businessObject, BusinessStep.BeforeDataMap))
                    {
                        ChangeDetections dtchs = DoDetectChange<TObject>(businessObject);
                        businessObject.Output = DoMappingUpdate<TObject>(businessObject);

                        if (DoProcessUpdate<TObject>(ref businessObject, BusinessStep.AfterDataMap))
                            DoActivityLogUpdate<TObject>(businessObject, dtchs);
                    }

                    DoAddMessageToOutput(businessObject.Output, businessObject.ProcessResults);

                    DoConcurrencyUpdate<TObject>(businessObject, BusinessStep.AfterDataMap);

                    scope.Complete();

                    return businessObject.Output;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        public int Delete<TObject>(TObject myObject, bool getDeletedItems)
        {
            BusinessObjectDelete<TObject> businessObject = null;
            try
            {
                using (TransactionScope scope = TransactionUtils.CreateTransactionScope())
                {
                    int retour = 0;

                    businessObject = new BusinessObjectDelete<TObject>(myObject, _activityObject);
                    //businessObject.DataMap = (IDataOperations<TObject>)mapping;
                    businessObject.DataMap = mapping;

                    DoSecurityCheck(FunctionName.Delete);

                    DoValidation<TObject>(businessObject.Parameter, businessObject, FunctionName.Delete);

                    DoConcurrencyDelete<TObject>(businessObject, BusinessStep.BeforeDataMap);

                    if (DoProcessDelete<TObject>(ref businessObject, BusinessStep.BeforeDataMap))
                    {
                        if (getDeletedItems)
                            businessObject.DeletedItem = DoMappingSelect<TObject>(new BusinessObjectSelect<TObject>(myObject, _activityObject));

                        retour = DoMappingDelete<TObject>(businessObject);

                        if (DoProcessDelete<TObject>(ref businessObject, BusinessStep.AfterDataMap))
                            DoActivityLogDelete<TObject>(businessObject);
                    }

                    DoAddMessageToOutput(businessObject.DeletedItem, businessObject.ProcessResults);

                    DoConcurrencyDelete<TObject>(businessObject, BusinessStep.AfterDataMap);

                    scope.Complete();

                    return retour;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        public TObject Select<TObject>(TObject myObject)
        {
            BusinessObjectSelect<TObject> businessObject = null;
            try
            {
                using (TransactionScope scope = TransactionUtils.CreateTransactionScope())
                {
                    businessObject = new BusinessObjectSelect<TObject>(myObject, _activityObject);
                    //businessObject.DataMap = (IDataOperations<TObject>)mapping;
                    businessObject.DataMap = mapping;

                    DoSecurityCheck(FunctionName.Select);

                    DoValidation<TObject>(businessObject.Parameter, businessObject, FunctionName.Select);

                    if (DoProcessSelect<TObject>(ref businessObject, BusinessStep.BeforeDataMap))
                    {
                        businessObject.Output = DoMappingSelect<TObject>(businessObject);

                        if (DoProcessSelect<TObject>(ref businessObject, BusinessStep.AfterDataMap))
                            DoActivityLogSelect<TObject>(businessObject);
                    }

                    DoAddMessageToOutput(businessObject.Output, businessObject.ProcessResults);

                    scope.Complete();

                    return businessObject.Output;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        public int Delete<TObject, TKey>(TKey key, bool getDeletedItems)
            where TKey : IKey
        {
            BusinessObjectDelete<TObject, TKey> businessObject = null;
            try
            {
                using (TransactionScope scope = TransactionUtils.CreateTransactionScope())
                {
                    int retour = 0;

                    businessObject = new BusinessObjectDelete<TObject, TKey>(key, _activityObject);
                    //businessObject.DataMap = (IDataOperations<TObject, TKey>)mapping;
                    businessObject.DataMap = mapping;

                    DoSecurityCheck(FunctionName.DeleteKey);

                    DoValidation<TKey>(businessObject.Key, businessObject, FunctionName.DeleteKey);

                    DoConcurrencyDelete<TObject, TKey>(businessObject, BusinessStep.BeforeDataMap);

                    if (DoProcessDelete<TObject, TKey>(ref businessObject, BusinessStep.BeforeDataMap))
                    {
                        if (getDeletedItems)
                            businessObject.DeletedItem = DoMappingSelect<TObject, TKey>(new BusinessObjectSelect<TObject, TKey>(key, _activityObject));

                        retour = DoMappingDelete<TObject, TKey>(businessObject);

                        if (DoProcessDelete<TObject, TKey>(ref businessObject, BusinessStep.AfterDataMap))
                            DoActivityLogDelete<TObject, TKey>(businessObject);
                    }

                    DoAddMessageToOutput(businessObject.DeletedItem, businessObject.ProcessResults);

                    DoConcurrencyDelete<TObject, TKey>(businessObject, BusinessStep.AfterDataMap);

                    scope.Complete();

                    return retour;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        public int Delete<TObject, TResult, TSearch>(TSearch search, bool getDeletedItems)
            where TSearch : ISearch
        {
            BusinessObjectDelete<TObject, TResult, TSearch> businessObject = null;
            try
            {
                using (TransactionScope scope = TransactionUtils.CreateTransactionScope())
                {
                    int retour = 0;

                    businessObject = new BusinessObjectDelete<TObject, TResult, TSearch>(search, _activityObject);
                    //businessObject.DataMap = (IDataOperations<TObject, TResult, TSearch>)mapping;
                    businessObject.DataMap = mapping;

                    DoSecurityCheck(FunctionName.DeleteSearch);

                    DoValidation<TSearch>(businessObject.Search, businessObject, FunctionName.DeleteSearch);

                    DoConcurrencyDelete<TObject, TResult, TSearch>(businessObject, BusinessStep.BeforeDataMap);

                    if (DoProcessDelete<TObject, TResult, TSearch>(ref businessObject, BusinessStep.BeforeDataMap))
                    {
                        if (getDeletedItems)
                            businessObject.DeletedItems = DoMappingSelect<TObject, TResult, TSearch>(new BusinessObjectSelect<TObject, TResult, TSearch>(search, _activityObject));

                        retour = DoMappingDelete<TObject, TResult, TSearch>(businessObject);

                        if (DoProcessDelete<TObject, TResult, TSearch>(ref businessObject, BusinessStep.AfterDataMap))
                            DoActivityLogDelete<TObject, TResult, TSearch>(businessObject);
                    }

                    DoAddMessageToOutput(businessObject.DeletedItems, businessObject.ProcessResults);

                    DoConcurrencyDelete<TObject, TResult, TSearch>(businessObject, BusinessStep.AfterDataMap);

                    scope.Complete();

                    return retour;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        public TObject Select<TObject, TKey>(TKey key)
            where TKey : IKey
        {
            BusinessObjectSelect<TObject, TKey> businessObject = null;
            try
            {
                using (TransactionScope scope = TransactionUtils.CreateTransactionScope())
                {
                    businessObject = new BusinessObjectSelect<TObject, TKey>(key, _activityObject);
                    //businessObject.DataMap = (IDataOperations<TObject, TKey>)mapping;
                    businessObject.DataMap = mapping;

                    DoSecurityCheck(FunctionName.SelectKey);

                    DoValidation<TKey>(businessObject.Key, businessObject, FunctionName.SelectKey);

                    if (DoProcessSelect<TObject, TKey>(ref businessObject, BusinessStep.BeforeDataMap))
                    {
                        businessObject.Output = DoMappingSelect<TObject, TKey>(businessObject);

                        if (DoProcessSelect<TObject, TKey>(ref businessObject, BusinessStep.AfterDataMap))
                            DoActivityLogSelect<TObject, TKey>(businessObject);
                    }

                    DoAddMessageToOutput(businessObject.Output, businessObject.ProcessResults);

                    scope.Complete();

                    return businessObject.Output;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        public TResult Select<TObject, TResult, TSearch>(TSearch search)
            where TSearch : ISearch
        {
            BusinessObjectSelect<TObject, TResult, TSearch> businessObject = null;
            try
            {
                using (TransactionScope scope = TransactionUtils.CreateTransactionScope())
                {
                    businessObject = new BusinessObjectSelect<TObject, TResult, TSearch>(search, _activityObject);
                    //businessObject.DataMap = (IDataOperations<TObject, TResult, TSearch>)mapping;
                    businessObject.DataMap = mapping;

                    DoSecurityCheck(FunctionName.SelectSearch);

                    DoValidation<TSearch>(businessObject.Search, businessObject, FunctionName.SelectSearch);

                    if (DoProcessSelect<TObject, TResult, TSearch>(ref businessObject, BusinessStep.BeforeDataMap))
                    {
                        businessObject.ListOutput = DoMappingSelect<TObject, TResult, TSearch>(businessObject);

                        if (DoProcessSelect<TObject, TResult, TSearch>(ref businessObject, BusinessStep.AfterDataMap))
                            DoActivityLogSelect<TObject, TResult, TSearch>(businessObject);
                    }

                    DoAddMessageToOutput(businessObject.ListOutput, businessObject.ProcessResults);

                    scope.Complete();

                    return businessObject.ListOutput;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        public TObject Edit<TObject>(TObject myObject)
        {
            BusinessObjectSelect<TObject> businessObject = null;
            try
            {
                using (TransactionScope scope = TransactionUtils.CreateTransactionScope())
                {
                    businessObject = new BusinessObjectSelect<TObject>(myObject, _activityObject);
                    //businessObject.DataMap = (IDataOperations<TObject>)mapping;
                    businessObject.DataMap = mapping;

                    DoSecurityCheck(FunctionName.Edit);

                    DoValidation<TObject>(businessObject.Parameter, businessObject, FunctionName.Edit);

                    DoConcurrencyEdit<TObject>(businessObject, BusinessStep.BeforeDataMap);

                    if (DoProcessEdit<TObject>(ref businessObject, BusinessStep.BeforeDataMap))
                    {
                        businessObject.Output = DoMappingSelect<TObject>(businessObject);

                        if (DoProcessEdit<TObject>(ref businessObject, BusinessStep.AfterDataMap))
                            DoActivityLogEdit<TObject>(businessObject);
                    }

                    DoAddMessageToOutput(businessObject.Output, businessObject.ProcessResults);

                    DoConcurrencyEdit<TObject>(businessObject, BusinessStep.AfterDataMap);

                    scope.Complete();

                    return businessObject.Output;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        public TObject Edit<TObject, TKey>(TKey key)
            where TKey : IKey
        {
            BusinessObjectSelect<TObject, TKey> businessObject = null;
            try
            {
                using (TransactionScope scope = TransactionUtils.CreateTransactionScope())
                {
                    businessObject = new BusinessObjectSelect<TObject, TKey>(key, _activityObject);
                    //businessObject.DataMap = (IDataOperations<TObject, TKey>)mapping;
                    businessObject.DataMap = mapping;

                    DoSecurityCheck(FunctionName.EditKey);

                    DoValidation<TKey>(businessObject.Key, businessObject, FunctionName.EditKey);

                    DoConcurrencyEdit<TObject, TKey>(businessObject, BusinessStep.BeforeDataMap);

                    if (DoProcessEdit<TObject, TKey>(ref businessObject, BusinessStep.BeforeDataMap))
                    {
                        businessObject.Output = DoMappingSelect<TObject, TKey>(businessObject);

                        if (DoProcessEdit<TObject, TKey>(ref businessObject, BusinessStep.AfterDataMap))
                            DoActivityLogEdit<TObject, TKey>(businessObject);
                    }

                    DoAddMessageToOutput(businessObject.Output, businessObject.ProcessResults);

                    DoConcurrencyEdit<TObject, TKey>(businessObject, BusinessStep.AfterDataMap);

                    scope.Complete();

                    return businessObject.Output;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        public TResult Edit<TObject, TResult, TSearch>(TSearch search)
            where TSearch : ISearch
        {
            BusinessObjectSelect<TObject, TResult, TSearch> businessObject = null;
            try
            {
                using (TransactionScope scope = TransactionUtils.CreateTransactionScope())
                {
                    businessObject = new BusinessObjectSelect<TObject, TResult, TSearch>(search, _activityObject);
                    //businessObject.DataMap = (IDataOperations<TObject, TResult, TSearch>)mapping;
                    businessObject.DataMap = mapping;

                    DoSecurityCheck(FunctionName.EditSearch);

                    DoValidation<TSearch>(businessObject.Search, businessObject, FunctionName.EditSearch);

                    DoConcurrencyEdit<TObject, TResult, TSearch>(businessObject, BusinessStep.BeforeDataMap);

                    if (DoProcessEdit<TObject, TResult, TSearch>(ref businessObject, BusinessStep.BeforeDataMap))
                    {
                        businessObject.ListOutput = DoMappingSelect<TObject, TResult, TSearch>(businessObject);

                        if (DoProcessEdit<TObject, TResult, TSearch>(ref businessObject, BusinessStep.AfterDataMap))
                            DoActivityLogEdit<TObject, TResult, TSearch>(businessObject);
                    }

                    DoAddMessageToOutput(businessObject.ListOutput, businessObject.ProcessResults);

                    DoConcurrencyEdit<TObject, TResult, TSearch>(businessObject, BusinessStep.AfterDataMap);

                    scope.Complete();

                    return businessObject.ListOutput;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        private void ExecuteLogic<TObject>(TObject myObject, ref BusinessObjectExecute<TObject> businessObject)
        {
            businessObject = new BusinessObjectExecute<TObject>(myObject, _activityObject);
            //businessObject.DataMap = (IDataOperations<TObject>)mapping;
            //businessObject.DataMap = mapping;

            businessObject.Activity = _activityObject;

            DoSecurityCheck(FunctionName.Execute);

            DoValidation<TObject>(businessObject.Parameter, businessObject, FunctionName.Execute);

            DoConcurrencyExecute(businessObject, BusinessStep.BeforeDataMap);

            if (DoProcessExecute(ref businessObject, (BusinessProcessExecute<TObject>)processExecute))
                DoActivityLogExecute(businessObject);

            DoConcurrencyExecute(businessObject, BusinessStep.AfterDataMap);
        }

        public void Execute<TObject>(TObject myObject)
        {
            BusinessObjectExecute<TObject> businessObject = null;
            //if(mapping!=null) businessObject.DataMap = mapping;
            try
            {
                if (useTransactionScope)
                {
                    using (TransactionScope scope = TransactionUtils.CreateTransactionScope())
                    {
                        ExecuteLogic(myObject, ref businessObject);
                        
                        scope.Complete();
                    }
                }
                else
                    ExecuteLogic(myObject, ref businessObject);
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
            finally
            {
                DoProcessExecute(ref businessObject, (BusinessProcessExecute<TObject>)processFinalize);
            }
        }
        /// <summary>
        /// Execute un traitement unique sur l'objet d'opération de traitement
        /// </summary>
        /// <typeparam name="TObject">Type de l'objet de traitement</typeparam>
        /// <param name="myObject">Intance de l'objet de traitement</param>
        /// <returns></returns>
        public TObject ExecuteSynchrone<TObject>(TObject myObject)
        {
            BusinessObjectExecute<TObject> businessObject = null;
            businessObject.DataMap = mapping;
            try
            {
                using (TransactionScope scope = TransactionUtils.CreateTransactionScope())
                {
                    businessObject = new BusinessObjectExecute<TObject>(myObject, _activityObject);                   
                    businessObject.DataMap = (IDataOperations<TObject>)mapping;


                    DoSecurityCheck(FunctionName.Execute);

                    DoValidation<TObject>(businessObject.Parameter, businessObject, FunctionName.Execute);

                    if (DoProcessExecute<TObject>(ref businessObject))
                    {
                                            
                            DoProcessExecute<TObject>(ref businessObject);
                    }

                    DoAddMessageToOutput(businessObject.Parameter, businessObject.ProcessResults);

                    scope.Complete();

                    return businessObject.Parameter;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        /// <summary>
        /// Cette méthode Execute est utilisée seulement avec une transaction dépendante.
        /// </summary>
        /// <param name="dt">Transaction dépendante créée par l'appelant</param>
        private void Execute<TObject>(TObject myObject, DependentTransaction dt)
        {
            BusinessObjectExecute<TObject> businessObject = null;
            //businessObject.DataMap = mapping;
            try
            {
                if (useTransactionScope)
                {
                    using (TransactionScope scope = new TransactionScope(dt))
                    {
                        ExecuteLogic(myObject, ref businessObject);
                        scope.Complete();
                    }
                }
                else
                    ExecuteLogic(myObject, ref businessObject);
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
            finally
            {
                dt.Complete();
                dt.Dispose();

                DoProcessExecute(ref businessObject, (BusinessProcessExecute<TObject>)processFinalize);
            }
        }


        #region ExecuteThreaded

        public void ExecuteThreaded<TObject>(TObject myObject, int millisecondsTimeout)
        {
            try
            {
                if (useTransactionScope)
                {
                    using (TransactionScope scope = TransactionUtils.CreateTransactionScope())
                    {
                        //Tâche contenant le traitement à exécuter (la fonction Execute)
                        var longRunningTask = new Task(state =>
                        {
                            Tuple<OperationContext, WindowsIdentity, TObject, Transaction> tuple = (Tuple<OperationContext, WindowsIdentity, TObject, Transaction>)state;

                            //On veut que l'identité du thread soit la même que celle du thread appelant
                            using (((WindowsIdentity)tuple.Item2).Impersonate())
                            {
                                //On veut que le contexte d'exécution soit le même que celui du thread appelant
                                OperationContext.Current = (OperationContext)tuple.Item1;

                                //On passe un DependentClone de la transaction pour que le TransactionScope de l'Execute l'utilise
                                //L'option RollbackIfNotComplete permet de rollbacker la transaction de l'Execute si jamais celle dans laquelle
                                //nous sommes est complétée avant. Donc si le timeout survient, c'est nécessairement ce qui va se passer.
                                Execute((TObject)tuple.Item3, ((Transaction)tuple.Item4).DependentClone(DependentCloneOption.RollbackIfNotComplete));
                            }
                        },
                        new Tuple<OperationContext, WindowsIdentity, TObject, Transaction>(OperationContext.Current, WindowsIdentity.GetCurrent(), myObject, Transaction.Current),
                        TaskCreationOptions.LongRunning);

                        longRunningTask.Start();
                        var proxy = longRunningTask.TimeoutAfter(millisecondsTimeout);

                        proxy.Wait();

                        scope.Complete();
                    }
                }
                else
                {
                    //Tâche contenant le traitement à exécuter (la fonction Execute)
                    var longRunningTask = new Task(state =>
                    {
                        Tuple<OperationContext, WindowsIdentity, TObject> tuple = (Tuple<OperationContext, WindowsIdentity, TObject>)state;

                        //On veut que l'identité du thread soit la même que celle du thread appelant
                        using (((WindowsIdentity)tuple.Item2).Impersonate())
                        {
                            //On veut que le contexte d'exécution soit le même que celui du thread appelant
                            OperationContext.Current = (OperationContext)tuple.Item1;

                            Execute((TObject)tuple.Item3);
                        }
                    },
                    new Tuple<OperationContext, WindowsIdentity, TObject>(OperationContext.Current, WindowsIdentity.GetCurrent(), myObject),
                    TaskCreationOptions.LongRunning);

                    longRunningTask.Start();
                    var proxy = longRunningTask.TimeoutAfter(millisecondsTimeout);

                    proxy.Wait();
                }
            }
            catch (Exception e)
            {
                //On fait le traitement d'erreur seulement si le traitement a été cancellé dû au timeout, car les autres
                //erreurs vont avoir été trappées dans le Execute.
                if (e is AggregateException)
                {
                    if (e.InnerException is TimeoutException)
                        HandleException(e.InnerException, null);
                }

                throw;
            }
        }

        #endregion ExecuteThreaded

        #endregion Functions

        #region Functions Aggregator

        public TObject AddAggregator<TObject>(TObject myObject)
        {
            BusinessObjectAdd<TObject> businessObject = null;
            try
            {
                businessObject = new BusinessObjectAdd<TObject>(myObject, null);

                GetContractAddresses();

                DoValidation<TObject>(businessObject.Parameter, businessObject, FunctionName.Add);

                DoProcessAddAggregator<TObject>(ref businessObject, BusinessStep.BeforeDataMap);

                DoAddMessageToOutput(businessObject.Output, businessObject.ProcessResults);

                return businessObject.Output;
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        public TObject UpdateAggregator<TObject>(TObject myObject)
        {
            BusinessObjectUpdate<TObject> businessObject = null;
            try
            {
                businessObject = new BusinessObjectUpdate<TObject>(myObject, null);

                GetContractAddresses();

                DoValidation<TObject>(businessObject.Parameter, businessObject, FunctionName.Update);

                DoProcessUpdateAggregator<TObject>(ref businessObject, BusinessStep.BeforeDataMap);

                DoAddMessageToOutput(businessObject.Output, businessObject.ProcessResults);

                return businessObject.Output;
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        public int DeleteAggregator<TObject>(TObject myObject, bool getDeletedItems)
        {
            BusinessObjectDelete<TObject> businessObject = null;
            try
            {
                int retour = 0;

                businessObject = new BusinessObjectDelete<TObject>(myObject, null);

                GetContractAddresses();

                DoValidation<TObject>(businessObject.Parameter, businessObject, FunctionName.Delete);

                DoProcessDeleteAggregator<TObject>(ref businessObject, BusinessStep.BeforeDataMap);

                DoAddMessageToOutput(businessObject.DeletedItem, businessObject.ProcessResults);

                return retour;
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        public TObject SelectAggregator<TObject>(TObject myObject)
        {
            BusinessObjectSelect<TObject> businessObject = null;
            try
            {
                businessObject = new BusinessObjectSelect<TObject>(myObject, null);

                GetContractAddresses();

                DoValidation<TObject>(businessObject.Parameter, businessObject, FunctionName.Select);

                DoProcessSelectAggregator<TObject>(ref businessObject, BusinessStep.BeforeDataMap);

                DoAddMessageToOutput(businessObject.Output, businessObject.ProcessResults);

                return businessObject.Output;
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        public TObject EditAggregator<TObject>(TObject myObject)
        {
            BusinessObjectSelect<TObject> businessObject = null;
            try
            {
                businessObject = new BusinessObjectSelect<TObject>(myObject, null);

                GetContractAddresses();

                DoValidation<TObject>(businessObject.Parameter, businessObject, FunctionName.Edit);

                DoProcessEditAggregator<TObject>(ref businessObject, BusinessStep.BeforeDataMap);

                DoAddMessageToOutput(businessObject.Output, businessObject.ProcessResults);

                return businessObject.Output;
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        public int DeleteAggregator<TObject, TKey>(TKey key, bool getDeletedItems)
            where TKey : IKey
        {
            BusinessObjectDelete<TObject, TKey> businessObject = null;
            try
            {
                int retour = 0;

                businessObject = new BusinessObjectDelete<TObject, TKey>(key, null);

                GetContractAddresses();

                DoValidation<TKey>(businessObject.Key, businessObject, FunctionName.Delete);

                DoProcessDeleteAggregator<TObject, TKey>(ref businessObject, BusinessStep.BeforeDataMap);

                DoAddMessageToOutput(businessObject.DeletedItem, businessObject.ProcessResults);

                return retour;
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        public TObject SelectAggregator<TObject, TKey>(TKey key)
            where TKey : IKey
        {
            BusinessObjectSelect<TObject, TKey> businessObject = null;
            try
            {
                businessObject = new BusinessObjectSelect<TObject, TKey>(key, null);

                GetContractAddresses();

                DoValidation<TKey>(businessObject.Key, businessObject, FunctionName.Select);

                DoProcessSelectAggregator<TObject, TKey>(ref businessObject, BusinessStep.BeforeDataMap);

                DoAddMessageToOutput(businessObject.Output, businessObject.ProcessResults);

                return businessObject.Output;
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        public TObject EditAggregator<TObject, TKey>(TKey key)
            where TKey : IKey
        {
            BusinessObjectSelect<TObject, TKey> businessObject = null;
            try
            {
                businessObject = new BusinessObjectSelect<TObject, TKey>(key, null);

                GetContractAddresses();

                DoValidation<TKey>(businessObject.Key, businessObject, FunctionName.Edit);

                DoProcessEditAggregator<TObject, TKey>(ref businessObject, BusinessStep.BeforeDataMap);

                DoAddMessageToOutput(businessObject.Output, businessObject.ProcessResults);

                return businessObject.Output;
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        public int DeleteAggregator<TObject, TResult, TSearch>(TSearch search, bool getDeletedItems)
            where TSearch : ISearch
        {
            BusinessObjectDelete<TObject, TResult, TSearch> businessObject = null;
            try
            {
                int retour = 0;

                businessObject = new BusinessObjectDelete<TObject, TResult, TSearch>(search, null);

                GetContractAddresses();

                DoValidation<TSearch>(businessObject.Search, businessObject, FunctionName.Delete);

                DoProcessDeleteAggregator<TObject, TResult, TSearch>(ref businessObject, BusinessStep.BeforeDataMap);

                DoAddMessageToOutput(businessObject.DeletedItems, businessObject.ProcessResults);

                return retour;
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        public TResult SelectAggregator<TObject, TResult, TSearch>(TSearch search)
            where TSearch : ISearch
        {
            BusinessObjectSelect<TObject, TResult, TSearch> businessObject = null;
            try
            {
                businessObject = new BusinessObjectSelect<TObject, TResult, TSearch>(search, null);

                GetContractAddresses();

                DoValidation<TSearch>(businessObject.Search, businessObject, FunctionName.Select);

                DoProcessSelectAggregator<TObject, TResult, TSearch>(ref businessObject, BusinessStep.BeforeDataMap);

                DoAddMessageToOutput(businessObject.ListOutput, businessObject.ProcessResults);

                return businessObject.ListOutput;
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        public TResult EditAggregator<TObject, TResult, TSearch>(TSearch search)
            where TSearch : ISearch
        {
            BusinessObjectSelect<TObject, TResult, TSearch> businessObject = null;
            try
            {
                businessObject = new BusinessObjectSelect<TObject, TResult, TSearch>(search, null);

                GetContractAddresses();

                DoValidation<TSearch>(businessObject.Search, businessObject, FunctionName.Edit);

                DoProcessEditAggregator<TObject, TResult, TSearch>(ref businessObject, BusinessStep.BeforeDataMap);

                DoAddMessageToOutput(businessObject.ListOutput, businessObject.ProcessResults);

                return businessObject.ListOutput;
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        public void ExecuteAggregator<TObject>(TObject myObject)
        {
            BusinessObjectExecute<TObject> businessObject = null;
            try
            {
                businessObject = new BusinessObjectExecute<TObject>(myObject, null);

                GetContractAddresses();

                DoValidation<TObject>(businessObject.Parameter, businessObject, FunctionName.Execute);

                DoProcessExecuteAggregator<TObject>(ref businessObject, BusinessStep.BeforeDataMap);

                DoAddMessageToOutput(businessObject.Parameter, businessObject.ProcessResults);
            }
            catch (Exception ex)
            {
                HandleException(ex, businessObject);
                throw;
            }
        }

        #endregion Functions Aggregator

        #region Concurrency

        public void DoConcurrencyAdd<TObject>(BusinessObjectAdd<TObject> businessObject, BusinessStep step)
        {
            string message = string.Empty;
            if (reservation != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_RESERV, CA_CODE.CA_FONC_ADD, Enum.GetName(typeof(BusinessStep), step), g.FullName);

                if (!((IConcurrencyAdd<TObject>)reservation).IsNotReservedForAdd(step, businessObject, ref message))
                    HandleConcurrencyResult(message);
            }
        }

        public void DoConcurrencyUpdate<TObject>(BusinessObjectUpdate<TObject> businessObject, BusinessStep step)
        {
            string message = string.Empty;
            if (reservation != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_RESERV, CA_CODE.CA_FONC_UPDATE, Enum.GetName(typeof(BusinessStep), step), g.FullName);

                if (!((IConcurrencyUpdate<TObject>)reservation).IsNotReservedForUpdate(step, businessObject, ref message))
                    HandleConcurrencyResult(message);
            }
        }

        public void DoConcurrencyDelete<TObject>(BusinessObjectDelete<TObject> businessObject, BusinessStep step)
        {
            string message = string.Empty;
            if (reservation != null)
            {
                Type g = typeof(TObject);


                DoTracking(CA_CODE.CA_PROCESS_RESERV, CA_CODE.CA_FONC_DELETE, Enum.GetName(typeof(BusinessStep), step), g.FullName);

                if (!((IConcurrencyDelete<TObject>)reservation).IsNotReservedForDelete(step, businessObject, ref message))
                    HandleConcurrencyResult(message);
            }
        }

        public void DoConcurrencyDelete<TObject, TKey>(BusinessObjectDelete<TObject, TKey> businessObject, BusinessStep step)
            where TKey : IKey
        {
            string message = string.Empty;
            if (reservation != null)
            {
                Type g = typeof(TObject);

                Type g2 = typeof(TKey);


                DoTracking(CA_CODE.CA_PROCESS_RESERV, CA_CODE.CA_FONC_DELETE, Enum.GetName(typeof(BusinessStep), step), g.FullName + "\\" + g2.FullName);

                if (!((IConcurrencyDelete<TObject, TKey>)reservation).IsNotReservedForDelete(step, businessObject, ref message))
                    HandleConcurrencyResult(message);
            }
        }

        public void DoConcurrencyDelete<TObject, TResult, TSearch>(BusinessObjectDelete<TObject, TResult, TSearch> businessObject, BusinessStep step)
            where TSearch : ISearch
        {
            string message = string.Empty;
            if (reservation != null)
            {
                Type g = typeof(TObject);

                Type g2 = typeof(TResult);

                Type g3 = typeof(TSearch);
                DoTracking(CA_CODE.CA_PROCESS_RESERV, CA_CODE.CA_FONC_DELETE, Enum.GetName(typeof(BusinessStep), step), g.FullName + "\\" + g2.FullName + "\\" + g3.FullName);

                if (!((IConcurrencyDelete<TObject, TResult, TSearch>)reservation).IsNotReservedForDelete(step, businessObject, ref message))
                    HandleConcurrencyResult(message);
            }
        }

        public void DoConcurrencyEdit<TObject>(BusinessObjectSelect<TObject> businessObject, BusinessStep step)
        {
            string message = string.Empty;
            if (reservation != null)
            {
                Type g = typeof(TObject);


                DoTracking(CA_CODE.CA_PROCESS_RESERV, CA_CODE.CA_FONC_EDIT, Enum.GetName(typeof(BusinessStep), step), g.FullName);

                if (!((IConcurrencyEdit<TObject>)reservation).IsNotReservedForEdit(step, businessObject, ref message))
                    HandleConcurrencyResult(message);
            }
        }

        public void DoConcurrencyEdit<TObject, TKey>(BusinessObjectSelect<TObject, TKey> businessObject, BusinessStep step)
            where TKey : IKey
        {
            string message = string.Empty;
            if (reservation != null)
            {
                Type g = typeof(TObject);

                Type g2 = typeof(TKey);


                DoTracking(CA_CODE.CA_PROCESS_RESERV, CA_CODE.CA_FONC_EDIT, Enum.GetName(typeof(BusinessStep), step), g.FullName + "\\" + g2.FullName);

                if (!((IConcurrencyEdit<TObject, TKey>)reservation).IsNotReservedForEdit(step, businessObject, ref message))
                    HandleConcurrencyResult(message);
            }
        }

        public void DoConcurrencyEdit<TObject, TResult, TSearch>(BusinessObjectSelect<TObject, TResult, TSearch> businessObject, BusinessStep step)
            where TSearch : ISearch
        {
            string message = string.Empty;
            if (reservation != null)
            {
                Type g = typeof(TObject);

                Type g2 = typeof(TResult);

                Type g3 = typeof(TSearch);

                DoTracking(CA_CODE.CA_PROCESS_RESERV, CA_CODE.CA_FONC_EDIT, Enum.GetName(typeof(BusinessStep), step), g.FullName + "\\" + g2.FullName + "\\" + g3.FullName);

                if (!((IConcurrencyEdit<TObject, TResult, TSearch>)reservation).IsNotReservedForEdit(step, businessObject, ref message))
                    HandleConcurrencyResult(message);
            }
        }

        public void DoConcurrencyExecute<TObject>(BusinessObjectExecute<TObject> businessObject, BusinessStep step)
        {
            string message = string.Empty;
            if (reservation != null)
            {
                Type obj = typeof(TObject);
                Type g = obj.GetGenericTypeDefinition();
                DoTracking(CA_CODE.CA_PROCESS_RESERV, CA_CODE.CA_PROCESS_EXECUTE, Enum.GetName(typeof(BusinessStep), step), g.Name);

                if (!((IConcurrencyExecute<TObject>)reservation).IsNotReservedForExecute(step, businessObject, ref message))
                    HandleConcurrencyResult(message);
            }
        }

        private void HandleConcurrencyResult(string message)
        {
            
            if (string.IsNullOrEmpty(message))
                message = ErrorMessages.M003;

            ProcessResults results = new ProcessResults(TypeError.Concurrence, Severity.Error, "DoConcurrency", message, true);
            //var fault = new ExceptionProcess<ProcessResults> (results, new FaultReason(ErrorMessages.M007));
            var fault = new ExceptionProcess<ProcessResults>(results, ErrorMessages.M007);
            
            throw fault;
        }

        #endregion Concurrency

        #region Mapping

        public TObject DoMappingAdd<TObject>(BusinessObjectAdd<TObject> businessObject)
        {
            
            if (mapping != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_MAPPING, CA_CODE.CA_FONC_ADD, g.FullName);

                if (configuration != null)
                {
                    using (IDbConnection connection = configuration.GetConnection())
                    {
                        if (connection == null)
                        {
                            throw new ExceptionLog(MKS.Core.Resources.CoreResources.DESIGN_BD_CONNECTION, Globals.GetUserEnvironment);
                        }

                        connection.Open();
                        mapping.Initialize(connection);
                        businessObject.Output = ((IAdd<TObject>)mapping).Add(businessObject.Parameter);
                    }
                }
                else
                    businessObject.Output = ((IAdd<TObject>)mapping).Add(businessObject.Parameter);
                
            }
            
            return businessObject.Output;
        }

        public TObject DoMappingUpdate<TObject>(BusinessObjectUpdate<TObject> businessObject)
        {
            if (mapping != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_MAPPING, CA_CODE.CA_FONC_UPDATE, g.FullName);
                if (configuration != null)
                {
                    using (IDbConnection connection = configuration.GetConnection())
                    {
                        if (connection == null)
                            throw new ExceptionLog(MKS.Core.Resources.CoreResources.DESIGN_BD_CONNECTION, Globals.GetUserEnvironment);
                        connection.Open();
                        mapping.Initialize(connection);
                        businessObject.Output = ((IUpdate<TObject>)mapping).Update(businessObject.Parameter);
                    }
                }
                else
                    businessObject.Output = ((IUpdate<TObject>)mapping).Update(businessObject.Parameter);
                
            }
            
            return businessObject.Output;
        }

        public int DoMappingDelete<TObject>(BusinessObjectDelete<TObject> businessObject)
        {
            int retour = 0;

            if (mapping != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_MAPPING, CA_CODE.CA_FONC_DELETE, g.FullName);

                if (configuration != null)
                {
                    using (IDbConnection connection = configuration.GetConnection())
                    {
                        if (connection == null)
                            throw new ExceptionLog(MKS.Core.Resources.CoreResources.DESIGN_BD_CONNECTION, Globals.GetUserEnvironment);
                        connection.Open();
                        mapping.Initialize(connection);
                        retour = ((IDelete<TObject>)mapping).Delete(businessObject.Parameter);
                    }
                }
                else
                    retour = ((IDelete<TObject>)mapping).Delete(businessObject.Parameter);
                
            }

            return retour;
        }

        public int DoMappingDelete<TObject, TKey>(BusinessObjectDelete<TObject, TKey> businessObject)
            where TKey : IKey
        {
            int retour = 0;

            if (mapping != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_MAPPING, CA_CODE.CA_FONC_DELETE, g.FullName);
                if (configuration != null)
                {
                    using (IDbConnection connection = configuration.GetConnection())
                    {
                        if (connection == null)
                            throw new ExceptionLog(MKS.Core.Resources.CoreResources.DESIGN_BD_CONNECTION, Globals.GetUserEnvironment);
                        connection.Open();
                        mapping.Initialize(connection);
                        retour = ((IDelete<TObject, TKey>)mapping).Delete(businessObject.Key);
                    }
                }
                else
                    retour = ((IDelete<TObject, TKey>)mapping).Delete(businessObject.Key);

                
            }

            return retour;
        }

        public int DoMappingDelete<TObject, TResult, TSearch>(BusinessObjectDelete<TObject, TResult, TSearch> businessObject)
            where TSearch : ISearch
        {
            int retour = 0;

            if (mapping != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_MAPPING, CA_CODE.CA_FONC_DELETE, g.FullName);
                if (configuration != null)
                {
                    using (IDbConnection connection = configuration.GetConnection())
                    {
                        if (connection == null)
                            throw new ExceptionLog(MKS.Core.Resources.CoreResources.DESIGN_BD_CONNECTION, Globals.GetUserEnvironment);
                        connection.Open();
                        mapping.Initialize(connection);
                        retour = ((IDelete<TObject, TResult, TSearch>)mapping).Delete(businessObject.Search);
                    }
                }
                else
                    retour = ((IDelete<TObject, TResult, TSearch>)mapping).Delete(businessObject.Search);
                
            }

            return retour;
        }

        public TObject DoMappingSelect<TObject>(BusinessObjectSelect<TObject> businessObject)
        {
            if (mapping != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_MAPPING, CA_CODE.CA_FONC_SELECT, g.FullName);
                if (configuration != null)
                {
                    using (IDbConnection connection = configuration.GetConnection())
                    {
                        if (connection == null)
                            throw new ExceptionLog(MKS.Core.Resources.CoreResources.DESIGN_BD_CONNECTION, Globals.GetUserEnvironment);
                        connection.Open();
                        mapping.Initialize(connection);
                        businessObject.Output = ((ISelect<TObject>)mapping).Select(businessObject.Parameter);
                    }
                }
                else
                    businessObject.Output = ((ISelect<TObject>)mapping).Select(businessObject.Parameter);
                
            }

            return businessObject.Output;
        }

        public TObject DoMappingSelect<TObject, TKey>(BusinessObjectSelect<TObject, TKey> businessObject)
            where TKey : IKey
        {
            if (mapping != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_MAPPING, CA_CODE.CA_FONC_SELECT, g.FullName);
                if (configuration != null)
                {
                    using (IDbConnection connection = configuration.GetConnection())
                    {
                        if (connection == null)
                            throw new ExceptionLog(MKS.Core.Resources.CoreResources.DESIGN_BD_CONNECTION, Globals.GetUserEnvironment);
                        connection.Open();
                        mapping.Initialize(connection);
                        businessObject.Output = ((ISelect<TObject, TKey>)mapping).Select(businessObject.Key);
                    }
                }
                else
                    businessObject.Output = ((ISelect<TObject, TKey>)mapping).Select(businessObject.Key);
                
            }

            return businessObject.Output;
        }

        public TObject DoMappingEdit<TObject>(BusinessObjectSelect<TObject> businessObject)
        {
            if (mapping != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_MAPPING, CA_CODE.CA_FONC_EDIT, g.FullName);
                if (configuration != null)
                {
                    using (IDbConnection connection = configuration.GetConnection())
                    {
                        if (connection == null)
                            throw new ExceptionLog(MKS.Core.Resources.CoreResources.DESIGN_BD_CONNECTION, Globals.GetUserEnvironment);
                        connection.Open();
                        mapping.Initialize(connection);
                        businessObject.Output = ((IEdit<TObject>)mapping).Edit(businessObject.Parameter);
                    }
                }
                else
                    businessObject.Output = ((IEdit<TObject>)mapping).Edit(businessObject.Parameter);
                
            }

            return businessObject.Output;
        }

        public TObject DoMappingEdit<TObject, TKey>(BusinessObjectSelect<TObject, TKey> businessObject)
            where TKey : IKey
        {
            if (mapping != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_MAPPING, CA_CODE.CA_FONC_EDIT, g.FullName);
                if (configuration != null)
                {
                    using (IDbConnection connection = configuration.GetConnection())
                    {
                        if (connection == null)
                            throw new ExceptionLog(MKS.Core.Resources.CoreResources.DESIGN_BD_CONNECTION, Globals.GetUserEnvironment);
                        connection.Open();
                        mapping.Initialize(connection);
                        businessObject.Output = ((IEdit<TObject, TKey>)mapping).Edit(businessObject.Key);
                    }
                }
                else
                    businessObject.Output = ((IEdit<TObject, TKey>)mapping).Edit(businessObject.Key);
                
            }

            return businessObject.Output;
        }

        public TResult DoMappingSelect<TObject, TResult, TSearch>(BusinessObjectSelect<TObject, TResult, TSearch> businessObject)
            where TSearch : ISearch
        {
            if (mapping != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_MAPPING, CA_CODE.CA_FONC_SELECT, g.FullName);

                if (configuration != null)
                {
                    using (IDbConnection connection = configuration.GetConnection())
                    {
                        if (connection == null)
                            throw new ExceptionLog(MKS.Core.Resources.CoreResources.DESIGN_BD_CONNECTION, Globals.GetUserEnvironment);
                        connection.Open();
                        mapping.Initialize(connection);
                        businessObject.ListOutput = ((ISelect<TObject, TResult, TSearch>)mapping).Select(businessObject.Search);
                    }
                }
                else
                    businessObject.ListOutput = ((ISelect<TObject, TResult, TSearch>)mapping).Select(businessObject.Search);
                
            }

            return businessObject.ListOutput;
        }

        #endregion Mapping

        #region Activity

        public void DoActivityLogAdd<TObject>(BusinessObjectAdd<TObject> businessObject)
        {
            Type g = typeof(TObject);
            DoTracking(CA_CODE.CA_PROCESS_EVENT, CA_CODE.CA_FONC_ADD, g.FullName);

            if (_activityLogLoad != null && _activityObject != null && ((IActivityLogAdd<TObject>)eventLog).IsLogAdd(businessObject))
                _activityLogLoad.DoActivityLog(businessObject.Activity);
        }

        public void DoActivityLogUpdate<TObject>(BusinessObjectUpdate<TObject> businessObject)
        {
            DoActivityLogUpdate(businessObject, null);
        }

        public void DoActivityLogUpdate<TObject>(BusinessObjectUpdate<TObject> businessObject, ChangeDetections detectChanges)
        {
            Type g = typeof(TObject);
            DoTracking(CA_CODE.CA_PROCESS_EVENT, CA_CODE.CA_FONC_UPDATE, g.FullName);

            if (_activityLogLoad != null && _activityObject != null)
            {
                bool canLogEvent = ((IActivityLogUpdate<TObject>)eventLog).IsLogUpdate(businessObject);

                if (detectChanges != null && canLogEvent && detectChanges.Changes.Count > 0)
                    ((IActivityLogUpdate<TObject>)eventLog).IsLogUpdateDetail(businessObject, detectChanges);

                if (canLogEvent)
                    _activityLogLoad.DoActivityLog(businessObject.Activity);
            }
        }

        public void DoActivityLogDelete<TObject>(BusinessObjectDelete<TObject> businessObject)
        {
            if (_activityLogLoad != null && _activityObject != null && ((IActivityLogDelete<TObject>)eventLog).IsLogDelete(businessObject))
            {
                Type g = typeof(TObject);
                DoTracking(CA_CODE.CA_PROCESS_EVENT, CA_CODE.CA_FONC_DELETE, g.FullName);
                _activityLogLoad.DoActivityLog(businessObject.Activity);
            }
        }

        public void DoActivityLogSelect<TObject>(BusinessObjectSelect<TObject> businessObject)
        {
            if (_activityLogLoad != null && _activityObject != null && ((IActivityLogSelect<TObject>)eventLog).IsLogSelect(businessObject))
            {
                Type g = typeof(TObject);
                DoTracking(CA_CODE.CA_PROCESS_EVENT, CA_CODE.CA_FONC_SELECT, g.FullName);
                _activityLogLoad.DoActivityLog(businessObject.Activity);
            }
        }

        public void DoActivityLogDelete<TObject, TKey>(BusinessObjectDelete<TObject, TKey> businessObject)
            where TKey : IKey
        {
            if (_activityLogLoad != null && _activityObject != null && ((IActivityLogDelete<TObject, TKey>)eventLog).IsLogDelete(businessObject))
            {
                Type g = typeof(TObject);
                Type g2=typeof(TKey);
                DoTracking(CA_CODE.CA_PROCESS_EVENT, CA_CODE.CA_FONC_DELETE, g.FullName + "\\" + g2.FullName);

                _activityLogLoad.DoActivityLog(businessObject.Activity);
            }
        }

        public void DoActivityLogDelete<TObject, TResult, TSearch>(BusinessObjectDelete<TObject, TResult, TSearch> businessObject)
            where TSearch : ISearch
        {
            if (_activityLogLoad != null && _activityObject != null && ((IActivityLogDelete<TObject, TResult, TSearch>)eventLog).IsLogDelete(businessObject))
            {
                Type g=typeof(TObject);
                Type g2 = typeof(TResult);
                Type g3 = typeof(TSearch);
                DoTracking(CA_CODE.CA_PROCESS_EVENT, CA_CODE.CA_FONC_DELETE, g.FullName + "\\" + g2.FullName + "\\" + g3.FullName);

                _activityLogLoad.DoActivityLog(businessObject.Activity);
            }
        }

        public void DoActivityLogSelect<TObject, TKey>(BusinessObjectSelect<TObject, TKey> businessObject)
            where TKey : IKey
        {
            if (_activityLogLoad != null && _activityObject != null && ((IActivityLogSelect<TObject, TKey>)eventLog).IsLogSelect(businessObject))
            {
                Type g = typeof(TObject);
                Type g2 = typeof(TKey);
                DoTracking(CA_CODE.CA_PROCESS_EVENT, CA_CODE.CA_FONC_SELECT, g.FullName + "\\" + g2.FullName);

                _activityLogLoad.DoActivityLog(businessObject.Activity);
            }
        }

        public void DoActivityLogSelect<TObject, TResult, TSearch>(BusinessObjectSelect<TObject, TResult, TSearch> businessObject)
            where TSearch : ISearch
        {
            if (_activityLogLoad != null && _activityObject != null && ((IActivityLogSelect<TObject, TResult, TSearch>)eventLog).IsLogSelect(businessObject))
            {
                Type g = typeof(TObject);
                Type g2 = typeof(TResult);
                Type g3 = typeof(TSearch);
                DoTracking(CA_CODE.CA_PROCESS_EVENT, CA_CODE.CA_FONC_SELECT, g.FullName + "\\" + g2.FullName + "\\" + g3.FullName);
                _activityLogLoad.DoActivityLog(businessObject.Activity);
            }
        }

        public void DoActivityLogEdit<TObject>(BusinessObjectSelect<TObject> businessObject)
        {
            if (_activityLogLoad != null && _activityObject != null && ((IActivityLogEdit<TObject>)eventLog).IsLogEdit(businessObject))
            {
                Type g = typeof(TObject);                
                DoTracking(CA_CODE.CA_PROCESS_EVENT, CA_CODE.CA_FONC_EDIT, g.FullName);

                _activityLogLoad.DoActivityLog(businessObject.Activity);
            }
        }

        public void DoActivityLogEdit<TObject, TKey>(BusinessObjectSelect<TObject, TKey> businessObject)
            where TKey : IKey
        {
            if (_activityLogLoad != null && _activityObject != null && ((IActivityLogEdit<TObject, TKey>)eventLog).IsLogEdit(businessObject))
            {
                Type g = typeof(TObject);
                Type g2 = typeof(TKey);
                DoTracking(CA_CODE.CA_PROCESS_EVENT, CA_CODE.CA_FONC_EDIT, g.FullName + "\\" + g2.FullName);
                _activityLogLoad.DoActivityLog(businessObject.Activity);
            }
        }

        public void DoActivityLogEdit<TObject, TResult, TSearch>(BusinessObjectSelect<TObject, TResult, TSearch> businessObject)
            where TSearch : ISearch
        {
            if (_activityLogLoad != null && _activityObject != null && ((IActivityLogEdit<TObject, TResult, TSearch>)eventLog).IsLogEdit(businessObject))
            {
                Type g = typeof(TObject);
                Type g2 = typeof(TResult);
                Type g3 = typeof(TSearch);
                DoTracking(CA_CODE.CA_PROCESS_EVENT, CA_CODE.CA_FONC_EDIT, g.FullName + "\\" + g2.FullName + "\\" + g3.FullName);
                _activityLogLoad.DoActivityLog(businessObject.Activity);
            }
        }

        public void DoActivityLogExecute<TObject>(BusinessObjectExecute<TObject> businessObject)
        {
            if (_activityLogLoad != null && _activityObject != null && ((IActivityLogExecute<TObject>)eventLog).ChangeEventExecute(businessObject))
            {
                Type g = typeof(TObject);
                DoTracking(CA_CODE.CA_PROCESS_EVENT, CA_CODE.CA_FONC_EXECUTE, g.FullName);
                _activityLogLoad.DoActivityLog(businessObject.Activity);
            }
        }

        #endregion TActivityLog

        #region DetectChange

        public ChangeDetections DoDetectChange<TObject>(BusinessObjectUpdate<TObject> businessObject)
        {
            
            if (detectChange != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_EVENT, CA_CODE.CA_EVENT_DETECTCHANGE, g.FullName);
                TObject oldObject = DoMappingSelect<TObject>(new BusinessObjectSelect<TObject>(businessObject.Parameter, null));
                
                return DoDetectChange<TObject>(businessObject.Parameter, oldObject);
            }
            else
                return null;
            
        }

        public ChangeDetections DoDetectChange<TObject>(TObject updatedObject, TObject oldObject)
        {
            Type g = typeof(TObject);
            DoTracking(CA_CODE.CA_PROCESS_EVENT, CA_CODE.CA_EVENT_DETECTCHANGE,g.FullName);

            if (detectChange != null)
                return ((IChangeDetection<TObject>)detectChange).GetPropertiesAltered(updatedObject, oldObject);
            else
                return null;
        }

        #endregion DetectChange

        #region Security

        private void DoSecurityCheck(List<IPermission> lstPermission)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in lstPermission)
            {
                sb.Append(item.Code);
            }
            DoTracking(CA_CODE.CA_PROCESS_SECURITY, CA_CODE.CA_START, null, sb.ToString());

            bool authorized = false;
            //if (securityUsage.IsSecurityContractOnly())
            //    authorized = securityUsage.IsContractAuthorized(Globals.GetUserEnvironment.GetCurrentSystemCode(), Globals.GetUserEnvironment.GetUserCodeWithoutDomain(), Globals.GetUserEnvironment.GetContractName());
            //else
            //{
            IApplicationAuthorization authorizations = securityUsage.GetApplicationAuthorization(Globals.GetUserEnvironment.GetUserCodeWithoutDomain(), Globals.GetUserEnvironment.GetCurrentSystemCode());

            //if (authorizations != null)
            //{
            authorized = authorizations.IsUserAuthorized(lstPermission);

            //    if (!authorized && !securityUsage.IsSecurityUserOnly())
            //        authorized = securityUsage.IsContractAuthorized(Globals.GetUserEnvironment.GetCurrentSystemCode(), Globals.GetUserEnvironment.GetUserCodeWithoutDomain(), Globals.GetUserEnvironment.GetContractName());
            //}
            //}
            DoTracking(CA_CODE.CA_PROCESS_SECURITY,CA_CODE.CA_END,"Authorisation",authorized.ToString());
            if (!authorized)
            {
                ProcessResults results = new ProcessResults(TypeError.Security, Severity.Error, "DoSecurityCheck", ErrorMessages.M001, true);
                //var fault = new ExceptionProcess<ProcessResults> (results, new FaultReason(ErrorMessages.M006));
                var fault = new ExceptionProcess<ProcessResults>(results, ErrorMessages.M006);
                throw fault;
            }

        }

        public void DoSecurityCheck(FunctionName function)
        {
            
            if (securityPermission != null && securityUsage != null)
            {
                switch (function)
                {
                    case FunctionName.Add:
                        DoSecurityCheck(securityPermission.SecurityPermissions.PermissionAdd);
                        break;

                    case FunctionName.Update:
                        DoSecurityCheck(securityPermission.SecurityPermissions.PermissionUpdate);
                        break;

                    case FunctionName.Delete:
                    case FunctionName.DeleteKey:
                    case FunctionName.DeleteSearch:
                        DoSecurityCheck(securityPermission.SecurityPermissions.PermissionDelete);
                        break;

                    case FunctionName.Select:
                    case FunctionName.SelectKey:
                    case FunctionName.SelectSearch:
                    case FunctionName.Edit:
                    case FunctionName.EditKey:
                    case FunctionName.EditSearch:
                        DoSecurityCheck(securityPermission.SecurityPermissions.PermissionSelect);
                        break;

                    case FunctionName.Execute:
                        DoSecurityCheck(securityPermission.SecurityPermissions.PermissionExecute);
                        break;
                }
            }

        }

        #endregion Security

        #region Validation

        public void DoValidation<TObject>(TObject objectToValidate, IBusinessObject businessObject, FunctionName function)
        {
            Type g = typeof(TObject);


            

            switch (function)
            {
                case FunctionName.DeleteKey:
                    DoTracking(CA_CODE.CA_PROCESS_VALIDATION, CA_CODE.CA_FONC_DELETE, g.FullName);

                    if (validationKey != null)
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate, (IValidation<TObject>)validationKey), businessObject);
                    else
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate), businessObject);
                    break;
                case FunctionName.SelectKey:
                    DoTracking(CA_CODE.CA_PROCESS_VALIDATION, CA_CODE.CA_FONC_SELECT, g.FullName);

                    if (validationKey != null)
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate, (IValidation<TObject>)validationKey), businessObject);
                    else
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate), businessObject);
                    break;
                case FunctionName.EditKey:
                    DoTracking(CA_CODE.CA_PROCESS_VALIDATION, CA_CODE.CA_FONC_EDIT, g.FullName);

                    if (validationKey != null)
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate, (IValidation<TObject>)validationKey), businessObject);
                    else
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate), businessObject);
                    break;

                case FunctionName.DeleteSearch:
                    DoTracking(CA_CODE.CA_PROCESS_VALIDATION, CA_CODE.CA_FONC_DELETE, g.FullName);

                    if (validationSearch != null)
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate, (IValidation<TObject>)validationSearch), businessObject);
                    else
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate), businessObject);
                    break;
                case FunctionName.SelectSearch:
                    DoTracking(CA_CODE.CA_PROCESS_VALIDATION, CA_CODE.CA_FONC_SELECT, g.FullName);

                    if (validationSearch != null)
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate, (IValidation<TObject>)validationSearch), businessObject);
                    else
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate), businessObject);
                    break;
                case FunctionName.EditSearch:
                    DoTracking(CA_CODE.CA_PROCESS_VALIDATION, CA_CODE.CA_FONC_EDIT, g.FullName);

                    if (validationSearch != null)
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate, (IValidation<TObject>)validationSearch), businessObject);
                    else
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate), businessObject);
                    break;

                case FunctionName.Delete:
                    DoTracking(CA_CODE.CA_PROCESS_VALIDATION, CA_CODE.CA_FONC_DELETE, g.FullName);

                    if (validationObject != null)
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate, (IValidation<TObject>)validationObject), businessObject);
                    else
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate), businessObject);
                    break;
                case FunctionName.Select:
                    DoTracking(CA_CODE.CA_PROCESS_VALIDATION, CA_CODE.CA_FONC_SELECT, g.FullName);

                    if (validationObject != null)
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate, (IValidation<TObject>)validationObject), businessObject);
                    else
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate), businessObject);
                    break;
                case FunctionName.Edit:
                    DoTracking(CA_CODE.CA_PROCESS_VALIDATION, CA_CODE.CA_FONC_EDIT, g.FullName);

                    if (validationObject != null)
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate, (IValidation<TObject>)validationObject), businessObject);
                    else
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate), businessObject);
                    break;

                case FunctionName.Add:
                    DoTracking(CA_CODE.CA_PROCESS_VALIDATION, CA_CODE.CA_FONC_ADD, g.FullName);

                    if (validation != null)
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate, (IValidation<TObject>)validation), businessObject);
                    else
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate), businessObject);
                    break;
                case FunctionName.Update:
                    DoTracking(CA_CODE.CA_PROCESS_VALIDATION, CA_CODE.CA_FONC_UPDATE, g.FullName);

                    if (validation != null)
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate, (IValidation<TObject>)validation), businessObject);
                    else
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate), businessObject);
                    break;
                case FunctionName.Execute:
                    DoTracking(CA_CODE.CA_PROCESS_VALIDATION, CA_CODE.CA_PROCESS_EXECUTE, g.FullName);

                    if (validation != null)
                        ManageValidationResults(objectToValidate, ValidationCore.DoValidation<TObject>(objectToValidate, (IValidation<TObject>)validation), businessObject);
                    else
                        ManageValidationResults(objectToValidate,ValidationCore.DoValidation<TObject>(objectToValidate), businessObject);
                    break;
            }
        }

        private void ManageValidationResults(object objectToValidate, RuleResults validationResults, IBusinessObject businessObject)
        {
            
            businessObject.ProcessResults.AddProcessResultsToList(Utilities.ConvertRuleResultsToProcessResults(validationResults));

            if (validationResults.Count > 0 )
            {
                DoAddMessageToOutput(objectToValidate, businessObject.ProcessResults);

                //var fault = new ExceptionProcess<ProcessResults> (businessObject.ProcessResults, new FaultReason(ErrorMessages.M004));
                var fault = new ExceptionProcess<ProcessResults>(businessObject.ProcessResults, ErrorMessages.M004);
                
                throw fault;
            }
        }

        #endregion Validation

        #region Trace
        public static class CA_CODE
        {
            public const string CA_PROCESS_SECURITY = "CA_PROCESS_SECURITY";
            public const string CA_PROCESS_ERROR = "CA_PROCESS_ERROR";
            public const string CA_PROCESS = "CA_PROCESS";
            public const string CA_PROCESS_VALIDATION = "CA_PROCESS_VALIDATION";
            public const string CA_FONC_ADD = "CA_FONC_ADD";
            public const string CA_FONC_UPDATE = "CA_FONC_UPDATE";
            public const string CA_FONC_DELETE = "CA_FONC_DELETE";
            public const string CA_FONC_EDIT = "CA_FONC_EDIT";
            public const string CA_FONC_SELECT = "CA_FONC_SELECT";
            public const string CA_PROCESS_RESERV = "CA_PROCESS_RESERV";
            public const string CA_PROCESS_EVENT = "CA_PROCESS_EVENT";
            public const string CA_PROCESS_EXECUTE = "CA_PROCESS_EXECUTE";
            public const string CA_CUSTOM = "CA_CUSTOM";
            public const string CA_START = "CA_START";
            public const string CA_END = "CA_END";
            public const string CA_RULE = "CA_RULE";
            public const string CA_RULE_FAIL = "CA_RULE_FAIL";
            public const string CA_PROCESS_RESULT = "CA_PROCESS_RESULT";
            public const string CA_EVENT_DETECTCHANGE = "CA_EVENT_DETECTCHANGE";
            public const string CA_FONC_EXECUTE = "CA_FONC_EXECUTE";
            public const string CA_PROCESS_MAPPING = "CA_PROCESS_MAPPING";
            public const string CA_PROCESS_AGGREGATOR = "CA_PROCESS_AGGREGATOR";
            public const string CA_SET_PROCESS="CA_SET_PROCESS";
            public const string CA_PROCESS_TRACKING="CA_PROCESS_TRACKING";
            public const string CA_PROCESS_ROUTING="CA_PROCESS_ROUTING";
            public const string CA_CONFIG = "CA_CONFIG";
            public const string CA_IDENITIFICATION = "CA_IDENITIFICATION";
            
                
        }
        private void DoTracking(string CodeProcessus, string CodeFonction,string informations)
        {
            DoTracking(CodeProcessus, CodeFonction, informations, null);
        }

        private void DoTracking(string CodeProcessus, string CodeFonction,string informations, string value)
        {
            try
            {

                string isActive =  ConfigurationManager.AppSettings["ActiveTracking"];



                if (isActive != null && trackingAdapter != null && (isActive.ToUpper() == "TRUE" || isActive.ToUpper() == "VRAI" || isActive.ToUpper() == "OUI"))
                {
                    Trace t = new Trace();
                    t.FunctionCode = CodeFonction;
                    t.ProcessCode = CodeProcessus;                    
                    t.UserCode = Globals.GetUserEnvironment.GetClientUserCode();
                    t.Date = DateTime.Now;
                    t.Informations = informations;                    
                    t.Titre = CoreResources.ResourceManager.GetString(CodeProcessus) + "\\" + CoreResources.ResourceManager.GetString(CodeFonction);
                    t.Value = value;
                    t.AppCode = Globals.GetUserEnvironment.GetClientSystemCode();
                    t.Computer = Globals.GetUserEnvironment.GetClientMachineName();


                    t.GUID = _GUID_TRACING_EVENT;

                    trackingAdapter.Add(t);
                    
                    //else // non compatible
                    //{
                    //    System.Diagnostics.Trace.WriteLine(t.ToString(), t.AppCode);
                    //}
                }
                
            }
            catch (Exception ex)
            {

                HandleException(ex, null);
                throw;
            }
            

        }
        #endregion

        #region DoProcess

        private bool DoProcess(ref IBusinessObject businessObject, IBusinessProcess businessProcess)
        {
            var objProcessResults = new ProcessResults();
            var process = Process.Succeed;

            DoTracking(CA_CODE.CA_PROCESS,CA_CODE.CA_START,null,null);

            foreach (var businessRule in businessProcess.GetProcessRules())
            {
                try
                {
                    DoTracking(CA_CODE.CA_PROCESS, CA_CODE.CA_RULE,"En traitement",businessRule.CodeMessage);
                    process = businessProcess.DoBusinessProcess(businessRule, ref businessObject);
                }
                catch (ExceptionProcess<ProcessResults>  ex)
                {
                    businessProcess.Dispose();
                    objProcessResults.AddProcessResultsToList(ex.Results);

                    businessObject.ProcessResults.AddProcessResultsToList(objProcessResults);

                    DoTracking(CA_CODE.CA_PROCESS, CA_CODE.CA_RULE_FAIL, ex.Message,businessRule.CodeMessage);

                    throw new ExceptionProcess<ProcessResults> (objProcessResults);
                }
                
                if (process != Process.Succeed)
                {
                    
                    var rm = new ReturnMessage(TypeError.ValidationBusiness,
                                               Utilities.TranslateSeverity(businessRule.Severity),
                                               businessRule.CodeMessage,
                                               businessRule.Description,
                                               false) { RuleType = businessRule.Type };
                    objProcessResults.AddException(rm);
                }

                if (process == Process.FailedStopRules || process == Process.FailedThrow)
                    break;

            }

            
            businessObject.ProcessResults.AddProcessResultsToList(objProcessResults);

            businessProcess.Dispose();

            
            bool ret=ManageProcessResults(process, businessObject);

            DoTracking(CA_CODE.CA_PROCESS, CA_CODE.CA_END, null, null);
            return ret;
        }

        private bool ManageProcessResults(Process process, IBusinessObject businessObject)
        {
            bool isOperationSuccess = false;
            

            switch (process)
            {
                case Process.Succeed:
                    isOperationSuccess = true;
                    DoTracking(CA_CODE.CA_PROCESS, CA_CODE.CA_PROCESS_RESULT, "Process.Succeed", null);
                    break;

                case Process.FailedThrow:
                    DoTracking(CA_CODE.CA_PROCESS, CA_CODE.CA_PROCESS_RESULT, "Process.FailedThrow", null);
                    if (businessObject.ProcessResults.MessagesList.Count > 0)
                    {
                        isOperationSuccess = false;
                        //var fault = new ExceptionProcess<ProcessResults> (businessObject.ProcessResults, new FaultReason(ErrorMessages.M005));
                        var fault = new ExceptionProcess<ProcessResults>(businessObject.ProcessResults, ErrorMessages.M005);
                        throw fault;
                    }
                    break;

                case Process.FailedStopRules:
                    DoTracking(CA_CODE.CA_PROCESS, CA_CODE.CA_PROCESS_RESULT, "Process.SuccessAddMessage", null);
                    isOperationSuccess = false;
                    break;

                case Process.SuccessAddMessage:
                    DoTracking(CA_CODE.CA_PROCESS, CA_CODE.CA_PROCESS_RESULT, "Process.SuccessAddMessage", null);
                    isOperationSuccess = true;
                    break;
            }
            
            return isOperationSuccess;
        }

        public bool DoProcessAdd<TObject>(ref BusinessObjectAdd<TObject> businessObjectAdd, BusinessStep step)
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectAdd;
            if (step == BusinessStep.BeforeDataMap && processAddBefore != null)
                return DoProcess(ref businessObject, processAddBefore);
            else if (step == BusinessStep.AfterDataMap && processAddAfter != null)
                return DoProcess(ref businessObject, processAddAfter);
            else
                return true;
        }

        public bool DoProcessUpdate<TObject>(ref BusinessObjectUpdate<TObject> businessObjectUpdate, BusinessStep step)
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectUpdate;
            if (step == BusinessStep.BeforeDataMap && processUpdateBefore != null)
                return DoProcess(ref businessObject, processUpdateBefore);
            else if (step == BusinessStep.AfterDataMap && processUpdateAfter != null)
                return DoProcess(ref businessObject, processUpdateAfter);
            else
                return true;
        }

        public bool DoProcessDelete<TObject>(ref BusinessObjectDelete<TObject> businessObjectDelete, BusinessStep step)
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectDelete;
            if (step == BusinessStep.BeforeDataMap && processDeleteBefore != null)
                return DoProcess(ref businessObject, processDeleteBefore);
            else if (step == BusinessStep.AfterDataMap && processDeleteAfter != null)
                return DoProcess(ref businessObject, processDeleteAfter);
            else
                return true;
        }

        public bool DoProcessSelect<TObject>(ref BusinessObjectSelect<TObject> businessObjectSelect, BusinessStep step)
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectSelect;
            if (step == BusinessStep.BeforeDataMap && processSelectBefore != null)
                return DoProcess(ref businessObject, processSelectBefore);
            else if (step == BusinessStep.AfterDataMap && processSelectAfter != null)
                return DoProcess(ref businessObject, processSelectAfter);
            else
                return true;
        }

        public bool DoProcessDelete<TObject, TKey>(ref BusinessObjectDelete<TObject, TKey> businessObjectDeleteKey, BusinessStep step)
            where TKey : IKey
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectDeleteKey;
            if (step == BusinessStep.BeforeDataMap && processDeleteKeyBefore != null)
                return DoProcess(ref businessObject, processDeleteKeyBefore);
            else if (step == BusinessStep.AfterDataMap && processDeleteKeyAfter != null)
                return DoProcess(ref businessObject, processDeleteKeyAfter);
            else
                return true;
        }

        public bool DoProcessDelete<TObject, TResult, TSearch>(ref BusinessObjectDelete<TObject, TResult, TSearch> businessObjectDeleteSearch, BusinessStep step)
            where TSearch : ISearch
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectDeleteSearch;
            if (step == BusinessStep.BeforeDataMap && processDeleteSearchBefore != null)
                return DoProcess(ref businessObject, processDeleteSearchBefore);
            else if (step == BusinessStep.AfterDataMap && processDeleteSearchAfter != null)
                return DoProcess(ref businessObject, processDeleteSearchAfter);
            else
                return true;
        }

        public bool DoProcessSelect<TObject, TKey>(ref BusinessObjectSelect<TObject, TKey> businessObjectSelectKey, BusinessStep step)
            where TKey : IKey
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectSelectKey;
            if (step == BusinessStep.BeforeDataMap && processSelectKeyBefore != null)
                return DoProcess(ref businessObject, processSelectKeyBefore);
            else if (step == BusinessStep.AfterDataMap && processSelectKeyAfter != null)
                return DoProcess(ref businessObject, processSelectKeyAfter);
            else
                return true;
        }

        public bool DoProcessSelect<TObject, TResult, TSearch>(ref BusinessObjectSelect<TObject, TResult, TSearch> businessObjectSelectSearch, BusinessStep step)
            where TSearch : ISearch
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectSelectSearch;
            if (step == BusinessStep.BeforeDataMap && processSelectSearchBefore != null)
                return DoProcess(ref businessObject, processSelectSearchBefore);
            else if (step == BusinessStep.AfterDataMap && processSelectSearchAfter != null)
                return DoProcess(ref businessObject, processSelectSearchAfter);
            else
                return true;
        }

        public bool DoProcessEdit<TObject>(ref BusinessObjectSelect<TObject> businessObjectSelect, BusinessStep step)
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectSelect;
            if (step == BusinessStep.BeforeDataMap && processEditBefore != null)
                return DoProcess(ref businessObject, processEditBefore);
            else if (step == BusinessStep.AfterDataMap && processEditAfter != null)
                return DoProcess(ref businessObject, processEditAfter);
            else
                return true;
        }

        public bool DoProcessEdit<TObject, TKey>(ref BusinessObjectSelect<TObject, TKey> businessObjectSelectKey, BusinessStep step)
            where TKey : IKey
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectSelectKey;
            if (step == BusinessStep.BeforeDataMap && processEditKeyBefore != null)
                return DoProcess(ref businessObject, processEditKeyBefore);
            else if (step == BusinessStep.AfterDataMap && processEditKeyAfter != null)
                return DoProcess(ref businessObject, processEditKeyAfter);
            else
                return true;
        }

        public bool DoProcessEdit<TObject, TResult, TSearch>(ref BusinessObjectSelect<TObject, TResult, TSearch> businessObjectSelectSearch, BusinessStep step)
            where TSearch : ISearch
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectSelectSearch;
            if (step == BusinessStep.BeforeDataMap && processEditSearchBefore != null)
                return DoProcess(ref businessObject, processEditSearchBefore);
            else if (step == BusinessStep.AfterDataMap && processEditSearchAfter != null)
                return DoProcess(ref businessObject, processEditSearchAfter);
            else
                return true;
        }

        public bool DoProcessError(ref BusinessObjectError businessObjectError)
        {
            if (processError != null)
            {
                IBusinessObject businessObject = (IBusinessObject)businessObjectError;
                DoProcess(ref businessObject, processError);
            }

            //Comme c'est le ProcessError, on retourne toujours true, sinon on va boucler pour l'éternité!
            return true;
        }

        public bool DoProcessExecute<TObject>(ref BusinessObjectExecute<TObject> businessObject)
        {
            return DoProcessExecute<TObject>(ref businessObject, (BusinessProcessExecute<TObject>)processExecute);
        }

        private bool DoProcessExecute<TObject>(ref BusinessObjectExecute<TObject> businessObjectExecute, BusinessProcessExecute<TObject> businessProcessExecute)
        {
            if (businessProcessExecute != null)
            {
                IBusinessObject businessObject = (IBusinessObject)businessObjectExecute;
                return DoProcess(ref businessObject, businessProcessExecute);
            }
            else
                return true;
        }

        #endregion DoProcess

        #region DoProcess Aggregator

        public bool DoProcessAddAggregator<TObject>(ref BusinessObjectAdd<TObject> businessObjectAdd, BusinessStep step)
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectAdd;
            if (processAddAggregator != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_AGGREGATOR, CA_CODE.CA_FONC_ADD, g.FullName);

                ((BusinessProcessAddAggregator<TObject>)processAddAggregator).contracts = contracts;
                return DoProcess(ref businessObject, processAddAggregator);
            }
            else
                return true;
        }

        public bool DoProcessUpdateAggregator<TObject>(ref BusinessObjectUpdate<TObject> businessObjectUpdate, BusinessStep step)
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectUpdate;
            if (processUpdateAggregator != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_AGGREGATOR, CA_CODE.CA_FONC_UPDATE, g.FullName);
                ((BusinessProcessUpdateAggregator<TObject>)processUpdateAggregator).contracts = contracts;
                return DoProcess(ref businessObject, processUpdateAggregator);
            }
            else
                return true;
        }

        public bool DoProcessDeleteAggregator<TObject>(ref BusinessObjectDelete<TObject> businessObjectDelete, BusinessStep step)
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectDelete;
            if (processDeleteAggregator != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_AGGREGATOR, CA_CODE.CA_FONC_DELETE, g.FullName);
                ((BusinessProcessDeleteAggregator<TObject>)processDeleteAggregator).contracts = contracts;
                return DoProcess(ref businessObject, processDeleteAggregator);
            }
            else
                return true;
        }

        public bool DoProcessSelectAggregator<TObject>(ref BusinessObjectSelect<TObject> businessObjectSelect, BusinessStep step)
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectSelect;
            if (processSelectAggregator != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_AGGREGATOR, CA_CODE.CA_FONC_SELECT, g.FullName);
                ((BusinessProcessSelectAggregator<TObject>)processSelectAggregator).contracts = contracts;
                return DoProcess(ref businessObject, processSelectAggregator);
            }
            else
                return true;
        }

        public bool DoProcessDeleteAggregator<TObject, TKey>(ref BusinessObjectDelete<TObject, TKey> businessObjectDeleteKey, BusinessStep step)
            where TKey : IKey
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectDeleteKey;
            if (processDeleteKeyAggregator != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_AGGREGATOR, CA_CODE.CA_FONC_DELETE, g.FullName);
                ((BusinessProcessDeleteAggregator<TObject, TKey>)processDeleteKeyAggregator).contracts = contracts;
                return DoProcess(ref businessObject, processDeleteKeyAggregator);
            }
            else
                return true;
        }

        public bool DoProcessDeleteAggregator<TObject, TResult, TSearch>(ref BusinessObjectDelete<TObject, TResult, TSearch> businessObjectDeleteSearch, BusinessStep step)
            where TSearch : ISearch
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectDeleteSearch;
            if (processDeleteSearchAggregator != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_AGGREGATOR, CA_CODE.CA_FONC_DELETE, g.FullName);
                ((BusinessProcessDeleteAggregator<TObject, TResult, TSearch>)processDeleteSearchAggregator).contracts = contracts;
                return DoProcess(ref businessObject, processDeleteSearchAggregator);
            }
            else
                return true;
        }

        public bool DoProcessSelectAggregator<TObject, TKey>(ref BusinessObjectSelect<TObject, TKey> businessObjectSelectKey, BusinessStep step)
            where TKey : IKey
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectSelectKey;
            if (processSelectKeyAggregator != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_AGGREGATOR, CA_CODE.CA_FONC_SELECT, g.FullName);
                ((BusinessProcessSelectAggregator<TObject, TKey>)processSelectKeyAggregator).contracts = contracts;
                return DoProcess(ref businessObject, processSelectKeyAggregator);
            }
            else
                return true;
        }

        public bool DoProcessSelectAggregator<TObject, TResult, TSearch>(ref BusinessObjectSelect<TObject, TResult, TSearch> businessObjectSelectSearch, BusinessStep step)
            where TSearch : ISearch
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectSelectSearch;
            if (processSelectSearchAggregator != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_AGGREGATOR, CA_CODE.CA_FONC_SELECT, g.FullName);
                ((BusinessProcessSelectAggregator<TObject, TResult, TSearch>)processSelectSearchAggregator).contracts = contracts;
                return DoProcess(ref businessObject, processSelectSearchAggregator);
            }
            else
                return true;
        }

        public bool DoProcessEditAggregator<TObject>(ref BusinessObjectSelect<TObject> businessObjectEdit, BusinessStep step)
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectEdit;
            if (processEditAggregator != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_AGGREGATOR, CA_CODE.CA_FONC_EDIT, g.FullName);
                ((BusinessProcessSelectAggregator<TObject>)processEditAggregator).contracts = contracts;
                return DoProcess(ref businessObject, processEditAggregator);
            }
            else
                return true;
        }

        public bool DoProcessEditAggregator<TObject, TKey>(ref BusinessObjectSelect<TObject, TKey> businessObjectEditKey, BusinessStep step)
            where TKey : IKey
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectEditKey;
            if (processEditKeyAggregator != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_AGGREGATOR, CA_CODE.CA_FONC_EDIT, g.FullName);
                ((BusinessProcessSelectAggregator<TObject, TKey>)processEditKeyAggregator).contracts = contracts;
                return DoProcess(ref businessObject, processEditKeyAggregator);
            }
            else
                return true;
        }

        public bool DoProcessEditAggregator<TObject, TResult, TSearch>(ref BusinessObjectSelect<TObject, TResult, TSearch> businessObjectEditSearch, BusinessStep step)
            where TSearch : ISearch
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectEditSearch;
            if (processEditSearchAggregator != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_AGGREGATOR, CA_CODE.CA_FONC_EDIT, g.FullName);
                ((BusinessProcessSelectAggregator<TObject, TResult, TSearch>)processEditSearchAggregator).contracts = contracts;
                return DoProcess(ref businessObject, processEditSearchAggregator);
            }
            else
                return true;
        }

        public bool DoProcessExecuteAggregator<TObject>(ref BusinessObjectExecute<TObject> businessObjectExecute, BusinessStep step)
        {
            IBusinessObject businessObject = (IBusinessObject)businessObjectExecute;
            if (processExecuteAggregator != null)
            {
                Type g = typeof(TObject);

                DoTracking(CA_CODE.CA_PROCESS_AGGREGATOR, CA_CODE.CA_FONC_EXECUTE, g.FullName);
                ((BusinessProcessExecuteAggregator<TObject>)processExecuteAggregator).contracts = contracts;
                return DoProcess(ref businessObject, processExecuteAggregator);
            }
            else
                return true;
        }

        #endregion DoProcess Aggregator


        #region Connector

        public void AddContract(IContract contract)
        {
            if (contracts == null)
                contracts = new List<IContract>();

            contracts.Add(contract);
        }

        /// <summary>
        /// Call the routing service and populate url and id attributes of the contracts
        /// </summary>
        public void GetContractAddresses()
        {
            //Si aucun contrat n'a été ajouté, on ne fait rien
            if (contracts != null && contracts.Count > 0)
            {
                //Si des contrats ont été ajoutés, on doit absolument avoir un RoutingUsage et un SearchContract
                if (routingUsage != null && searchContract != null)
                {
                    List<IContract> lstContractNotInConfig = new List<IContract>();
                    if (Utilities.IsByPassRoutingActivated())
                    {
                        //Vérifie si la section est présente dans le fichier de configuration
                        var hashTable = ConfigurationManager.GetSection(CoreResources.MKSServicesWCF) as Hashtable;

                        if (hashTable != null)
                        {
                            foreach (var contract in contracts)
                            {
                                //On cherche l'élément sans son namespace
                                string serviceURL = (String)hashTable[contract.Name];

                                if (serviceURL != null)
                                    contract.Url = serviceURL;
                                else
                                    lstContractNotInConfig.Add(contract); //On ajoute les contrats qu'on ne trouve pas dans le fichier de config
                            }
                        }
                    }
                    else
                        lstContractNotInConfig = contracts; //On considère qu'aucun contrat n'est dans le fichier de config

                    //Si le Count est à 0, tous les contrats étaient dans le fichier de config
                    if (lstContractNotInConfig.Count != 0)
                    {
                        string caller = Globals.GetUserEnvironment.GetCurrentImpersonatedUserCode();

                        // Build the SearchContract
                        searchContract.Caller = caller;
                        searchContract.Action = Utilities.GetCallingMethodName();
                        searchContract.Contracts = new List<IContract>();

                        string cacheKey = string.Empty;
                        foreach (var contract in lstContractNotInConfig)
                        {
                            contract.ApplicationCode = contract.ApplicationCode;
                            contract.Name = contract.Name;
                            contract.Version = new Version(Assembly.GetAssembly(GetType()).GetName().Version.ToString());
                            searchContract.Contracts.Add(contract);

                            cacheKey += contract.ApplicationCode + "_" + contract.Name + "_" + contract.Version + ";";
                        }

                        MemoryCache cache = MemoryCache.Default;
                        List<IContract> lstContracts = cache[cacheKey] as List<IContract>;

                        if (lstContracts == null)
                        {
                            // Call the Routing Service
                            lstContracts = routingUsage.GetContractAddresses(searchContract);

                            //On ajoute dans la cache lorsqu'au moins 1 contrat possède un URL
                            if (Utilities.IsCachingActivated(Globals.GetUserEnvironment) && lstContracts != null && !lstContracts.All(x => string.IsNullOrWhiteSpace(x.Url)))
                            {
                                CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                                cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddMinutes(Utilities.GetCacheDuration());
                                cache.Add(cacheKey, lstContracts, cacheItemPolicy);
                            }
                        }

                        //Assigner l'url aux contrats ayant été trouvés via le routing
                        foreach (var contract in contracts)
                        {
                            IContract result = lstContracts.FirstOrDefault(c => c.ApplicationCode.Equals(contract.ApplicationCode) && c.Name.Equals(contract.Name));

                            if (result != null)
                                contract.Url = result.Url;
                        }
                    }

                    bool allURLMissing = true;
                    List<IContract> lstContractMissingURL = new List<IContract>();
                    foreach (var contract in contracts)
                    {
                        if (!string.IsNullOrWhiteSpace(contract.Url))
                            allURLMissing = false;
                        else
                            lstContractMissingURL.Add(contract);
                    }

                    //On journalise dans les logs la liste des urls manquants.
                    if (lstContractMissingURL.Count > 0)
                        PublishNoUrlError(lstContractMissingURL);

                    // On génère une exception si aucun URL valide est présent.
                    if (allURLMissing)
                    {
                        StringBuilder sb = new StringBuilder(CoreResources.EX0004);
                        sb.AppendLine();
                        sb.AppendLine("Contrat(s): ");
                        foreach (var item in lstContractMissingURL)
                        {
                            sb.AppendLine(item.Name);
                        }
                        string errorNomContrats = sb.ToString();
                        throw new Exception(errorNomContrats);
                    }
                }
                else
                    throw new Exception(CoreResources.EX0006);
            }
        }

        private void PublishNoUrlError(List<IContract> contracts)
        {
            StringBuilder sb = new StringBuilder(CoreResources.EX0005);

            StringBuilder sb2 = new StringBuilder();
            foreach (var item in contracts)
            {
                sb2.AppendFormat("NAME:{0}\\VERSION:{1}\\APPCODE:{2}\\URL:{3}", item.Name, item.Version, item.ApplicationCode, item.Url);
            }


            DoTracking(CA_CODE.CA_PROCESS, CA_CODE.CA_PROCESS_ERROR, "PublishNoUrlError", sb2.ToString());


            sb.AppendLine();
            foreach (var item in contracts)
            {
                sb.Append(item.ApplicationCode).Append(" - ").AppendLine(item.Name);
            }
            sb.AppendLine();
            Exception ex = new Exception(sb.ToString());

            ErrorLog.PublishExceptionMessage(ex,Globals.GetUserEnvironment);
        }

        #endregion Connector

        #region Dispose

        public void Dispose()
        {
            DoTracking(CA_CODE.CA_END, "Dispose Business", null);
            if (mapping != null)
                mapping.Dispose();

            if (eventLog != null)
                eventLog.Dispose();

            if (reservation != null)
                reservation.Dispose();

            if (processError != null)
                processError.Dispose();

            if (processExecute != null)
                processExecute.Dispose();

            if (processFinalize != null)
                processFinalize.Dispose();

            if (processAddBefore != null)
                processAddBefore.Dispose();

            if (processUpdateBefore != null)
                processUpdateBefore.Dispose();

            if (processDeleteBefore != null)
                processDeleteBefore.Dispose();

            if (processDeleteKeyBefore != null)
                processDeleteKeyBefore.Dispose();

            if (processDeleteSearchBefore != null)
                processDeleteSearchBefore.Dispose();

            if (processSelectBefore != null)
                processSelectBefore.Dispose();

            if (processSelectKeyBefore != null)
                processSelectKeyBefore.Dispose();

            if (processSelectSearchBefore != null)
                processSelectSearchBefore.Dispose();

            if (processEditBefore != null)
                processEditBefore.Dispose();

            if (processEditKeyBefore != null)
                processEditKeyBefore.Dispose();

            if (processEditSearchBefore != null)
                processEditSearchBefore.Dispose();

            if (processAddAfter != null)
                processAddAfter.Dispose();

            if (processUpdateAfter != null)
                processUpdateAfter.Dispose();

            if (processDeleteAfter != null)
                processDeleteAfter.Dispose();

            if (processDeleteKeyAfter != null)
                processDeleteKeyAfter.Dispose();

            if (processDeleteSearchAfter != null)
                processDeleteSearchAfter.Dispose();

            if (processSelectAfter != null)
                processSelectAfter.Dispose();

            if (processSelectKeyAfter != null)
                processSelectKeyAfter.Dispose();

            if (processSelectSearchAfter != null)
                processSelectSearchAfter.Dispose();

            if (processEditAfter != null)
                processEditAfter.Dispose();

            if (processEditKeyAfter != null)
                processEditKeyAfter.Dispose();

            if (processEditSearchAfter != null)
                processEditSearchAfter.Dispose();

            if (processAddAggregator != null)
                processAddAggregator.Dispose();

            if (processUpdateAggregator != null)
                processUpdateAggregator.Dispose();

            if (processDeleteAggregator != null)
                processDeleteAggregator.Dispose();

            if (processDeleteKeyAggregator != null)
                processDeleteKeyAggregator.Dispose();

            if (processDeleteSearchAggregator != null)
                processDeleteSearchAggregator.Dispose();

            if (processSelectAggregator != null)
                processSelectAggregator.Dispose();

            if (processSelectKeyAggregator != null)
                processSelectKeyAggregator.Dispose();

            if (processSelectSearchAggregator != null)
                processSelectSearchAggregator.Dispose();

            if (processEditAggregator != null)
                processEditAggregator.Dispose();

            if (processEditKeyAggregator != null)
                processEditKeyAggregator.Dispose();

            if (processEditSearchAggregator != null)
                processEditSearchAggregator.Dispose();
            if (trackingAdapter != null)
                trackingAdapter.Dispose();
        }

        #endregion Dispose

        /// <summary>
        ///   Vérifie si l'objet contient l'interface IReturnObject pour ajouter les exceptions.
        /// </summary>
        /// <param name="output"> Objet de type IReturnObject </param>
        public void DoAddMessageToOutput(object output, ProcessResults processResults)
        {
            if (processResults != null && processResults.MessagesList != null)
            {
                if (processResults.MessagesList.Count > 0 && output is IReturnObject)
                {
                    DoTracking(CA_CODE.CA_PROCESS, CA_CODE.CA_CUSTOM, "DoAddMessageToOutput", output.GetType().FullName);

                    var returnObject = (IReturnObject)output;

                    if (returnObject.ProcessResults == null)
                        returnObject.ProcessResults = new ProcessResults();

                    returnObject.ProcessResults.AddProcessResultsToList(processResults);
                }
            }
        }

        public void HandleException(Exception initialException, IBusinessObject businessObject)
        {
            try
            {
                DoTracking(CA_CODE.CA_PROCESS_ERROR,CA_CODE.CA_PROCESS, "HandleException", initialException.Message);

                BusinessObjectError boe = new BusinessObjectError(initialException, businessObject);
                DoProcessError(ref boe);
            }
            catch (Exception exceptionProcessError)
            {
                if (!(exceptionProcessError is ExceptionProcess<ProcessResults> ))
                    ErrorLog.PublishExceptionMessage(exceptionProcessError,Globals.GetUserEnvironment);
            }
        }

 
    }
}