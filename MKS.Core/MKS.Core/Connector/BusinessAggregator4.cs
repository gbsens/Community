using MKS.Core.Connector.Interfaces;
using MKS.Core.Model;

namespace MKS.Core.Connector
{
    public abstract class BusinessAggregator<TObject, TResult, TSearch, TKey> : BusinessAggregator<TObject, TKey>, IBusinessOperationsAggregator<TObject, TResult, TSearch, TKey>
        where TSearch : ISearch
        where TKey : IKey
    {
        public BusinessAggregator()
        {
            business = new MKS.Core.Business.Business();
        }

        #region Set

        public void SetValidationSearch<TValidation>() where TValidation : IValidation<TSearch>, new()
        {
            business.SetValidationSearch(new TValidation());
        }

        public void SetProcessSelectWithSearch<TBusinessProcess>() where TBusinessProcess : BusinessProcessSelectAggregator<TObject, TResult, TSearch>, new()
        {
            business.SetProcessSelectSearchAggregator(new TBusinessProcess());
        }

        public void SetProcessDeleteWithSearch<TBusinessProcess>() where TBusinessProcess : BusinessProcessDeleteAggregator<TObject, TResult, TSearch>, new()
        {
            business.SetProcessDeleteSearchAggregator(new TBusinessProcess());
        }

        public void SetProcessEditWithSearch<TBusinessProcess>() where TBusinessProcess : BusinessProcessSelectAggregator<TObject, TResult, TSearch>, new()
        {
            business.SetProcessEditSearchAggregator(new TBusinessProcess());
        }

        #endregion Set

        #region Functions

        public virtual TResult Select(TSearch search)
        {
            return business.SelectAggregator<TObject, TResult, TSearch>(search);
        }

        public virtual int Delete(TSearch search)
        {
            return business.DeleteAggregator<TObject, TResult, TSearch>(search, false);
        }

        public virtual TResult Edit(TSearch search)
        {
            return business.EditAggregator<TObject, TResult, TSearch>(search);
        }

        #endregion Functions
    }
}