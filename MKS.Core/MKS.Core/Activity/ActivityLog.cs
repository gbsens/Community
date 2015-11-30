using MKS.Core.Business;
using MKS.Core.Model;

namespace MKS.Core.Activity
{
    public abstract class ActivityLog<TObject> : IActivityLogOperations<TObject>
    {
        public virtual bool IsLogAdd(BusinessObjectAdd<TObject> businessObject)
        {
            return false;
        }

        public virtual bool IsLogUpdate(BusinessObjectUpdate<TObject> businessObject)
        {
            return false;
        }

        public virtual bool IsLogUpdateDetail(BusinessObjectUpdate<TObject> businessObject, ChangeDetections detectChanges)
        {
            return false;
        }

        public virtual bool IsLogSelect(BusinessObjectSelect<TObject> businessObject)
        {
            return false;
        }

        public virtual bool IsLogDelete(BusinessObjectDelete<TObject> businessObject)
        {
            return false;
        }

        public virtual bool IsLogEdit(BusinessObjectSelect<TObject> businessObject)
        {
            return false;
        }

        public virtual void Dispose()
        {
        }
    }

    public abstract class ActivityLog<TObject, TKey> : IActivityLogOperations<TObject, TKey>
        where TKey : IKey
    {
        public virtual bool IsLogAdd(BusinessObjectAdd<TObject> businessObject)
        {
            return false;
        }

        public virtual bool IsLogUpdate(BusinessObjectUpdate<TObject> businessObject)
        {
            return false;
        }

        public virtual bool IsLogUpdateDetail(BusinessObjectUpdate<TObject> businessObject, ChangeDetections detectChanges)
        {
            return false;
        }

        public virtual bool IsLogSelect(BusinessObjectSelect<TObject> businessObject)
        {
            return false;
        }

        public virtual bool IsLogDelete(BusinessObjectDelete<TObject> businessObject)
        {
            return false;
        }

        public virtual bool IsLogSelect(BusinessObjectSelect<TObject, TKey> businessObject)
        {
            return false;
        }

        public virtual bool IsLogEdit(BusinessObjectSelect<TObject> businessObject)
        {
            return false;
        }

        public virtual bool IsLogEdit(BusinessObjectSelect<TObject, TKey> businessObject)
        {
            return false;
        }

        public virtual bool IsLogDelete(BusinessObjectDelete<TObject, TKey> businessObject)
        {
            return false;
        }

        public virtual void Dispose()
        {
        }
    }

    public abstract class ActivityLog<TObject, TResult, TSearch> : IActivityLogOperations<TObject, TResult, TSearch>
        where TSearch : ISearch
    {
        public virtual bool IsLogAdd(BusinessObjectAdd<TObject> businessObject)
        {
            return false;
        }

        public virtual bool IsLogUpdate(BusinessObjectUpdate<TObject> businessObject)
        {
            return false;
        }

        public virtual bool IsLogUpdateDetail(BusinessObjectUpdate<TObject> businessObject, ChangeDetections detectChanges)
        {
            return false;
        }

        public virtual bool IsLogSelect(BusinessObjectSelect<TObject> businessObject)
        {
            return false;
        }

        public virtual bool IsLogDelete(BusinessObjectDelete<TObject> businessObject)
        {
            return false;
        }

        public virtual bool IsLogSelect(BusinessObjectSelect<TObject, TResult, TSearch> businessObject)
        {
            return false;
        }

        public virtual bool IsLogEdit(BusinessObjectSelect<TObject> businessObject)
        {
            return false;
        }

        public virtual bool IsLogEdit(BusinessObjectSelect<TObject, TResult, TSearch> businessObject)
        {
            return false;
        }

        public virtual bool IsLogDelete(BusinessObjectDelete<TObject, TResult, TSearch> businessObject)
        {
            return false;
        }

        public virtual void Dispose()
        {
        }
    }

    public abstract class ActivityLog<TObject, TResult, TSearch, TKey> : IActivityLogOperations<TObject, TResult, TSearch, TKey>
        where TSearch : ISearch
        where TKey : IKey
    {
        public virtual bool IsLogAdd(BusinessObjectAdd<TObject> businessObject)
        {
            return false;
        }

        public virtual bool IsLogUpdate(BusinessObjectUpdate<TObject> businessObject)
        {
            return false;
        }

        public virtual bool IsLogUpdateDetail(BusinessObjectUpdate<TObject> businessObject, ChangeDetections detectChanges)
        {
            return false;
        }

        public virtual bool IsLogSelect(BusinessObjectSelect<TObject> businessObject)
        {
            return false;
        }

        public virtual bool IsLogDelete(BusinessObjectDelete<TObject> businessObject)
        {
            return false;
        }

        public virtual bool IsLogSelect(BusinessObjectSelect<TObject, TResult, TSearch> businessObject)
        {
            return false;
        }

        public virtual bool IsLogSelect(BusinessObjectSelect<TObject, TKey> businessObject)
        {
            return false;
        }

        public virtual bool IsLogEdit(BusinessObjectSelect<TObject> businessObject)
        {
            return false;
        }

        public virtual bool IsLogEdit(BusinessObjectSelect<TObject, TKey> businessObject)
        {
            return false;
        }

        public virtual bool IsLogEdit(BusinessObjectSelect<TObject, TResult, TSearch> businessObject)
        {
            return false;
        }

        public virtual bool IsLogDelete(BusinessObjectDelete<TObject, TKey> businessObject)
        {
            return false;
        }

        public virtual bool IsLogDelete(BusinessObjectDelete<TObject, TResult, TSearch> businessObject)
        {
            return false;
        }

        public virtual void Dispose()
        {
        }
    }

    public abstract class ActivityLogExecute<TObject> : IActivityLogOperationsExecute<TObject>
    {
        public virtual bool ChangeEventExecute(BusinessObjectExecute<TObject> businessObject)
        {
            return false;
        }

        public virtual void Dispose()
        {
        }
    }
}