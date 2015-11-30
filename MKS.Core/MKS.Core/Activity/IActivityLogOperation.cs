using MKS.Core.Business;
using MKS.Core.Model;

namespace MKS.Core.Activity
{
    public interface IActivityLogOperation
    {
    }

    public interface IActivityLogAdd<TObject> : IActivityLogOperation
    {
        bool IsLogAdd(BusinessObjectAdd<TObject> businessObject);
    }

    public interface IActivityLogUpdate<TObject> : IActivityLogOperation
    {
        bool IsLogUpdate(BusinessObjectUpdate<TObject> businessObject);

        bool IsLogUpdateDetail(BusinessObjectUpdate<TObject> businessObject, ChangeDetections detectChanges);
    }

    public interface IActivityLogDelete<TObject> : IActivityLogOperation
    {
        bool IsLogDelete(BusinessObjectDelete<TObject> businessObject);
    }

    public interface IActivityLogDelete<TObject, TKey> : IActivityLogOperation
        where TKey : IKey
    {
        bool IsLogDelete(BusinessObjectDelete<TObject, TKey> businessObject);
    }

    public interface IActivityLogDelete<TObject, TResult, TSearch> : IActivityLogOperation
        where TSearch : ISearch
    {
        bool IsLogDelete(BusinessObjectDelete<TObject, TResult, TSearch> businessObject);
    }

    public interface IActivityLogSelect<TObject> : IActivityLogOperation
    {
        bool IsLogSelect(BusinessObjectSelect<TObject> businessObject);
    }

    public interface IActivityLogSelect<TObject, TKey> : IActivityLogOperation
        where TKey : IKey
    {
        bool IsLogSelect(BusinessObjectSelect<TObject, TKey> businessObject);
    }

    public interface IActivityLogSelect<TObject, TResult, TSearch> : IActivityLogOperation
        where TSearch : ISearch
    {
        bool IsLogSelect(BusinessObjectSelect<TObject, TResult, TSearch> businessObject);
    }

    public interface IActivityLogEdit<TObject> : IActivityLogOperation
    {
        bool IsLogEdit(BusinessObjectSelect<TObject> businessObject);
    }

    public interface IActivityLogEdit<TObject, TKey> : IActivityLogOperation
        where TKey : IKey
    {
        bool IsLogEdit(BusinessObjectSelect<TObject, TKey> businessObject);
    }

    public interface IActivityLogEdit<TObject, TResult, TSearch> : IActivityLogOperation
        where TSearch : ISearch
    {
        bool IsLogEdit(BusinessObjectSelect<TObject, TResult, TSearch> businessObject);
    }

    public interface IActivityLogExecute<TObject> : IActivityLogOperation
    {
        bool ChangeEventExecute(BusinessObjectExecute<TObject> businessObject);
    }
}