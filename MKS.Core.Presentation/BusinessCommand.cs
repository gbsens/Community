using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKS.Core.Business;
using MKS.Core.Presenter;
using MKS.Core.Presenter.Interfaces;

namespace MKS.Core.Presenter
{
    public class BusinessCommand<TView,TProcess>:BusinessExecute<ObjectProcess<TView>>
        where TView:IView
        where TProcess:IOperation<TView>
    {
        public BusinessCommand():base(false)
        {
            
            SetProcessExecute<BusinessProcessCommand<TView, TProcess>>();
            SetProcessError<BusinessError>();
        }

    }
}
