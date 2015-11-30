using MKS.Core.Connector.Interfaces;

namespace MKS.Core.Connector
{
    public abstract class BusinessAggregator<TObject> : IBusinessOperationsAggregator<TObject>
    {
        internal MKS.Core.Business.Business business;

        public BusinessAggregator()
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

        public void SetValidationObject<TValidation>() where TValidation : IValidation<TObject>, new()
        {
            business.SetValidationObject(new TValidation());
        }

        public void SetProcessAdd<TBusinessProcess>() where TBusinessProcess : BusinessProcessAddAggregator<TObject>, new()
        {
            business.SetProcessAddAggregator(new TBusinessProcess());
        }

        public void SetProcessUpdate<TBusinessProcess>() where TBusinessProcess : BusinessProcessUpdateAggregator<TObject>, new()
        {
            business.SetProcessUpdateAggregator(new TBusinessProcess());
        }

        public void SetProcessSelect<TBusinessProcess>() where TBusinessProcess : BusinessProcessSelectAggregator<TObject>, new()
        {
            business.SetProcessSelectAggregator(new TBusinessProcess());
        }

        public void SetProcessDelete<TBusinessProcess>() where TBusinessProcess : BusinessProcessDeleteAggregator<TObject>, new()
        {
            business.SetProcessDeleteAggregator(new TBusinessProcess());
        }

        public void SetProcessEdit<TBusinessProcess>() where TBusinessProcess : BusinessProcessSelectAggregator<TObject>, new()
        {
            business.SetProcessEditAggregator(new TBusinessProcess());
        }

        #endregion Set

        #region Functions

        public virtual TObject Add(TObject myObject)
        {
            return business.AddAggregator<TObject>(myObject);
        }

        public virtual TObject Update(TObject myObject)
        {
            return business.UpdateAggregator<TObject>(myObject);
        }

        public virtual TObject Select(TObject myObject)
        {
            return business.SelectAggregator<TObject>(myObject);
        }

        public virtual int Delete(TObject myObject)
        {
            return business.DeleteAggregator<TObject>(myObject, false);
        }

        public virtual TObject Edit(TObject myObject)
        {
            return business.EditAggregator<TObject>(myObject);
        }

        #endregion Functions

        public void AddContract(IContract contract)
        {
            business.AddContract(contract);
        }
    }
}