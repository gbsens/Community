using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKS.Core.Business;
using MKS.Core.Activity;

using MKS.Core.Presenter.Interfaces;


namespace MKS.Core.Presenter
{
    public class BusinessPresenter<TView, TProcess>:BusinessExecute<ObjectPresenter<TView>>  
        where TView:IView
        where TProcess:IOperation<TView>, new()
    {

        public BusinessPresenter(bool initialisation=true):base(false)
        {
            if (initialisation)
                SetProcessExecute<BusinessProcessPresenter<TView,TProcess>>();
            else
                SetProcessExecute<BusinessProcessPresenterNoInit<TView, TProcess>>();

            SetProcessError<BusinessError>();
            
        }
    }

    public class BusinessPresenter<TView, TProcess, TAvtivityLog> : BusinessExecute<ObjectPresenter<TView>>
        where TView : IView
        where TProcess : IOperation<TView>, new()
        where TAvtivityLog :IActivityLogExecute<TView>, new()
    {
        public BusinessPresenter(bool initialisation = true)
            : base(false)
        {
            if (initialisation)
                SetProcessExecute<BusinessProcessPresenter<TView, TProcess>>();
            else
                SetProcessExecute<BusinessProcessPresenterNoInit<TView, TProcess>>();
            SetProcessError<BusinessError>();
            //SetActivityLog<TView>();

        }
    }

}
