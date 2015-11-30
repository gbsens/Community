using MKS.Core.Activity;
using MKS.Core.Business.Interfaces;
using MKS.Core.Mapping;
using MKS.Core.Model;
using System;
using System.Data;

namespace MKS.Core.Business
{
    public class BusinessObject : IBusinessObject
    {
        public ProcessResults ProcessResults { get; set; }

        public IActivity Activity { get; set; }

        public IDbConnection Connection { get; set; }

        public BusinessObject()
        {
            ProcessResults = new ProcessResults();
        }
    }

    public class BusinessObjectAdd<TObject> : BusinessObject
    {
        public TObject Parameter { get; set; }

        public TObject Output { get; set; }

        public BusinessObjectAdd(TObject parameter, IActivity activityInstance)
            : base()
        {
            Parameter = parameter;
            Activity = activityInstance;
        }
        //public IDataOperations<TObject> DataMap { get; set; }
        public IDataOperations DataMap { get; set; }
    }

    public class BusinessObjectUpdate<TObject> : BusinessObject
    {
        public TObject Parameter { get; set; }

        public TObject Output { get; set; }

        public BusinessObjectUpdate(TObject parameter, IActivity activityInstance)
            : base()
        {
            Parameter = parameter;
            Activity = activityInstance;
        }
        //public IDataOperations<TObject> DataMap { get; set; }
        public IDataOperations DataMap { get; set; }
    }

    public class BusinessObjectDelete<TObject> : BusinessObject
    {
        public TObject Parameter { get; set; }

        public TObject DeletedItem { get; set; }

        public BusinessObjectDelete(TObject parameter, IActivity activityInstance)
            : base()
        {
            Parameter = parameter;
            Activity = activityInstance;
        }
        //public IDataOperations<TObject> DataMap { get; set; }
        public IDataOperations DataMap { get; set; }
    }

    public class BusinessObjectDelete<TObject, TKey> : BusinessObject
        where TKey : IKey
    {
        public TKey Key { get; set; }

        public TObject DeletedItem { get; set; }

        public BusinessObjectDelete(TKey key, IActivity activityInstance)
            : base()
        {
            Key = key;
            Activity = activityInstance;
        }
        //public IDataOperations<TObject,TKey> DataMap { get; set; }
        public IDataOperations DataMap { get; set; }
    }

    public class BusinessObjectDelete<TObject, TResult, TSearch> : BusinessObject
        where TSearch : ISearch
    {
        public TSearch Search { get; set; }

        public TResult DeletedItems { get; set; }

        public BusinessObjectDelete(TSearch search, IActivity activityInstance)
            : base()
        {
            Search = search;
            Activity = activityInstance;
        }
        //public IDataOperations<TObject,TResult,TSearch> DataMap { get; set; }
        public IDataOperations DataMap { get; set; }
    }

    public class BusinessObjectSelect<TObject> : BusinessObject
    {
        public TObject Parameter { get; set; }

        public TObject Output { get; set; }

        public BusinessObjectSelect(TObject parameter, IActivity activityInstance)
            : base()
        {
            Parameter = parameter;
            Activity = activityInstance;
        }
        //public IDataOperations<TObject> DataMap { get; set; }
        public IDataOperations DataMap { get; set; }
    }

    public class BusinessObjectSelect<TObject, TKey> : BusinessObject
        where TKey : IKey
    {
        public TKey Key { get; set; }

        public TObject Output { get; set; }

        public BusinessObjectSelect(TKey key, IActivity activityInstance)
            : base()
        {
            Key = key;
            Activity = activityInstance;
        }
        //public IDataOperations<TObject, TKey> DataMap { get; set; }
        public IDataOperations DataMap { get; set; }
    }

    public class BusinessObjectSelect<TObject, TResult, TSearch> : BusinessObject
        where TSearch : ISearch
    {
        public TSearch Search { get; set; }

        public TResult ListOutput { get; set; }

        public BusinessObjectSelect(TSearch search, IActivity activityInstance)
            : base()
        {
            Search = search;
            Activity = activityInstance;
        }
        //public IDataOperations<TObject, TResult, TSearch> DataMap { get; set; }
        public IDataOperations DataMap { get; set; }
    }

    public class BusinessObjectError : BusinessObject
    {
        public Exception Exception { get; set; }

        public IBusinessObject BusinessObject { get; set; }

        public BusinessObjectError(Exception ex)
            : base()
        {
            Exception = ex;
        }

        public BusinessObjectError(IBusinessObject businessObject)
            : base()
        {
            BusinessObject = businessObject;
        }

        public BusinessObjectError(Exception ex, IBusinessObject businessObject)
            : base()
        {
            Exception = ex;
            BusinessObject = businessObject;
        }

    }

    public class BusinessObjectExecute<TObject> : BusinessObject
    {
        public TObject Parameter { get; set; }

        public BusinessObjectExecute(TObject parameter, IActivity activityInstance)
            : base()
        {
            Parameter = parameter;
            Activity = activityInstance;
        }
        //public IDataOperations<TObject> DataMap { get; set; }
        public IDataOperations DataMap { get; set; }
    }
}