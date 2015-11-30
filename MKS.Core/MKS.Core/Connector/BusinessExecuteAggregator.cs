using MKS.Core.Connector.Interfaces;

namespace MKS.Core.Connector
{
    public abstract class BusinessExecuteAggregator<TObject> : IBusinessOperationsExecuteAggregator<TObject>
    {
        internal MKS.Core.Business.Business business;

        public BusinessExecuteAggregator()
        {
            business = new MKS.Core.Business.Business();
        }

        #region Set

        public void SetRouting<TRouting>() where TRouting : IRoutingAdapter, new()
        {
            business.SetRouting(new TRouting());
        }

        public void SetValidation<TValidation>() where TValidation : IValidation<TObject>, new()
        {
            business.SetValidation(new TValidation());
        }

        public void SetProcessExecute<TBusinessProcess>() where TBusinessProcess : BusinessProcessExecuteAggregator<TObject>, new()
        {
            business.SetProcessExecuteAggregator(new TBusinessProcess());
        }

        #endregion Set

        #region Functions

        public virtual void Execute(TObject myObject)
        {
            business.ExecuteAggregator<TObject>(myObject);
        }

        #endregion Functions

        public void AddContract(IContract contract)
        {
            business.AddContract(contract);
        }
    }
}