using MKS.Core.Business;
using MKS.Core.Model;

namespace MKS.Core.Concurrency
{
    public interface IConcurrencyOperation
    {
    }

    public interface IConcurrencyAdd<TObject> : IConcurrencyOperation
    {
        bool IsNotReservedForAdd(BusinessStep step, BusinessObjectAdd<TObject> businessObject, ref string message);
    }

    public interface IConcurrencyUpdate<TObject> : IConcurrencyOperation
    {
        bool IsNotReservedForUpdate(BusinessStep step, BusinessObjectUpdate<TObject> businessObject, ref string message);
    }

    public interface IConcurrencyDelete<TObject> : IConcurrencyOperation
    {
        bool IsNotReservedForDelete(BusinessStep step, BusinessObjectDelete<TObject> businessObject, ref string message);
    }

    public interface IConcurrencyDelete<TObject, TKey> : IConcurrencyOperation
        where TKey : IKey
    {
        bool IsNotReservedForDelete(BusinessStep step, BusinessObjectDelete<TObject, TKey> businessObject, ref string message);
    }

    public interface IConcurrencyDelete<TObject, TResult, TSearch> : IConcurrencyOperation
        where TSearch : ISearch
    {
        bool IsNotReservedForDelete(BusinessStep step, BusinessObjectDelete<TObject, TResult, TSearch> businessObject, ref string message);
    }

    public interface IConcurrencyEdit<TObject> : IConcurrencyOperation
    {
        bool IsNotReservedForEdit(BusinessStep step, BusinessObjectSelect<TObject> businessObject, ref string message);
    }

    public interface IConcurrencyEdit<TObject, TKey> : IConcurrencyOperation
        where TKey : IKey
    {
        bool IsNotReservedForEdit(BusinessStep step, BusinessObjectSelect<TObject, TKey> businessObject, ref string message);
    }

    public interface IConcurrencyEdit<TObject, TResult, TSearch> : IConcurrencyOperation
        where TSearch : ISearch
    {
        bool IsNotReservedForEdit(BusinessStep step, BusinessObjectSelect<TObject, TResult, TSearch> businessObject, ref string message);
    }

    public interface IConcurrencyExecute<TObject> : IConcurrencyOperation
    {
        bool IsNotReservedForExecute(BusinessStep step, BusinessObjectExecute<TObject> businessObject, ref string message);
    }
}