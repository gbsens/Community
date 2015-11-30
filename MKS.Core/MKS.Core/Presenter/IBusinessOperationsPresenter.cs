using MKS.Core.Business.Interfaces;
using MKS.Core.Model;

namespace MKS.Core.Presentation
{
    public interface IBusinessOperationsPresenter
    {}
    public interface IBusinessOperationsPresenter<TView> : IBusinessOperationsPresenter, IViewProcess<TView>
    {
        void SetViewProcess<TProcess>()
            where TProcess : IViewProcess<TView>, new();

        void SetValidation<TValidation>() where TValidation : IValidation<TView>, new();

    }

    
}