using MKS.Core.Connector.Interfaces;
using MKS.Core.Model;

namespace MKS.Core.Connector
{
    public abstract class BusinessAggregator<TObject, TKey> : BusinessAggregator<TObject>, IBusinessOperationsAggregator<TObject, TKey>
        where TKey : IKey
    {

        public BusinessAggregator()
        {
            business = new MKS.Core.Business.Business();
        }

        #region Set

        public void SetValidationKey<TValidation>() where TValidation : IValidation<TKey>, new()
        {
            business.SetValidationKey(new TValidation());
        }

        public void SetProcessSelectWithKey<TBusinessProcess>() where TBusinessProcess : BusinessProcessSelectAggregator<TObject, TKey>, new()
        {
            business.SetProcessSelectKeyAggregator(new TBusinessProcess());
        }

        public void SetProcessDeleteWithKey<TBusinessProcess>() where TBusinessProcess : BusinessProcessDeleteAggregator<TObject, TKey>, new()
        {
            business.SetProcessDeleteKeyAggregator(new TBusinessProcess());
        }

        public void SetProcessEditWithKey<TBusinessProcess>() where TBusinessProcess : BusinessProcessSelectAggregator<TObject, TKey>, new()
        {
            business.SetProcessEditKeyAggregator(new TBusinessProcess());
        }

        #endregion Set

        #region Functions

        public virtual TObject Select(TKey key)
        {
            return business.SelectAggregator<TObject, TKey>(key);
        }

        public virtual int Delete(TKey key)
        {
            return business.DeleteAggregator<TObject, TKey>(key, false);
        }

        public virtual TObject Edit(TKey key)
        {
            return business.EditAggregator<TObject, TKey>(key);
        }

        #endregion Functions
    }
}