using MKS.Core.Business;
using MKS.Core.Model;

namespace MKS.Core.Concurrency
{
    public abstract class Concurrency<TObject> : IConcurrencyOperations<TObject>
    {
        public virtual bool IsNotReservedForAdd(BusinessStep step, BusinessObjectAdd<TObject> businessObject, ref string message)
        {
            return true;
        }

        public virtual bool IsNotReservedForUpdate(BusinessStep step, BusinessObjectUpdate<TObject> businessObject, ref string message)
        {
            return true;
        }

        public virtual bool IsNotReservedForDelete(BusinessStep step, BusinessObjectDelete<TObject> businessObject, ref string message)
        {
            return true;
        }

        public virtual bool IsNotReservedForEdit(BusinessStep step, BusinessObjectSelect<TObject> businessObject, ref string message)
        {
            return true;
        }

        public virtual void Dispose()
        {
        }
    }

    public abstract class Concurrency<TObject, TKey> : IConcurrencyOperations<TObject, TKey>
        where TKey : IKey
    {
        public virtual bool IsNotReservedForAdd(BusinessStep step, BusinessObjectAdd<TObject> businessObject, ref string message)
        {
            return true;
        }

        public virtual bool IsNotReservedForUpdate(BusinessStep step, BusinessObjectUpdate<TObject> businessObject, ref string message)
        {
            return true;
        }

        public virtual bool IsNotReservedForDelete(BusinessStep step, BusinessObjectDelete<TObject> businessObject, ref string message)
        {
            return true;
        }

        public virtual bool IsNotReservedForEdit(BusinessStep step, BusinessObjectSelect<TObject> businessObject, ref string message)
        {
            return true;
        }

        public virtual bool IsNotReservedForEdit(BusinessStep step, BusinessObjectSelect<TObject, TKey> businessObject, ref string message)
        {
            return true;
        }

        public virtual bool IsNotReservedForDelete(BusinessStep step, BusinessObjectDelete<TObject, TKey> businessObject, ref string message)
        {
            return true;
        }

        public virtual void Dispose()
        {
        }
    }

    public abstract class Concurrency<TObject, TResult, TSearch> : IConcurrencyOperations<TObject, TResult, TSearch>
        where TSearch : ISearch
    {
        public virtual bool IsNotReservedForAdd(BusinessStep step, BusinessObjectAdd<TObject> businessObject, ref string message)
        {
            return true;
        }

        public virtual bool IsNotReservedForUpdate(BusinessStep step, BusinessObjectUpdate<TObject> businessObject, ref string message)
        {
            return true;
        }

        public virtual bool IsNotReservedForDelete(BusinessStep step, BusinessObjectDelete<TObject> businessObject, ref string message)
        {
            return true;
        }

        public virtual bool IsNotReservedForEdit(BusinessStep step, BusinessObjectSelect<TObject> businessObject, ref string message)
        {
            return true;
        }

        public virtual bool IsNotReservedForEdit(BusinessStep step, BusinessObjectSelect<TObject, TResult, TSearch> businessObject, ref string message)
        {
            return true;
        }

        public virtual bool IsNotReservedForDelete(BusinessStep step, BusinessObjectDelete<TObject, TResult, TSearch> businessObject, ref string message)
        {
            return true;
        }

        public virtual void Dispose()
        {
        }
    }

    public abstract class Concurrency<TObject, TResult, TSearch, TKey> : IConcurrencyOperations<TObject, TResult, TSearch, TKey>
        where TSearch : ISearch
        where TKey : IKey
    {
        public virtual bool IsNotReservedForAdd(BusinessStep step, BusinessObjectAdd<TObject> businessObject, ref string message)
        {
            return true;
        }

        public virtual bool IsNotReservedForUpdate(BusinessStep step, BusinessObjectUpdate<TObject> businessObject, ref string message)
        {
            return true;
        }

        public virtual bool IsNotReservedForDelete(BusinessStep step, BusinessObjectDelete<TObject> businessObject, ref string message)
        {
            return true;
        }

        public virtual bool IsNotReservedForEdit(BusinessStep step, BusinessObjectSelect<TObject> businessObject, ref string message)
        {
            return true;
        }

        public virtual bool IsNotReservedForEdit(BusinessStep step, BusinessObjectSelect<TObject, TKey> businessObject, ref string message)
        {
            return true;
        }

        public virtual bool IsNotReservedForEdit(BusinessStep step, BusinessObjectSelect<TObject, TResult, TSearch> businessObject, ref string message)
        {
            return true;
        }

        public virtual bool IsNotReservedForDelete(BusinessStep step, BusinessObjectDelete<TObject, TKey> businessObject, ref string message)
        {
            return true;
        }

        public virtual bool IsNotReservedForDelete(BusinessStep step, BusinessObjectDelete<TObject, TResult, TSearch> businessObject, ref string message)
        {
            return true;
        }

        public virtual void Dispose()
        {
        }
    }
}