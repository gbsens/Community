using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKS.Core.Presenter;
using MKS.Core.Presenter.Interfaces;
using MKS.Core;

using TestFramework.Application;

namespace TestFramework.Application
{
    public class PresenterPersonneProcess:Process<IVuePersonne>
    {
        public override void Initialisation(bool isInit, IVuePersonne view, IPresenter presenter)
        {
            ServicePersonne s = new ServicePersonne();
            SearchPersonne sp = new SearchPersonne();
            sp.Nom = "LOLO";
            s.SelectList(sp);
        }

    }
    public class PresenterPersonneProcess2 : Process<IVuePersonne>
    {
        public override void Initialisation(bool isInit, IVuePersonne view, IPresenter presenter)
        {

        }
        public  void Test(CommandEventArgsCustom args, IVuePersonne view, IPresenter presenter)
        {
            ServicePersonne s = new ServicePersonne();
            SearchPersonne sp = new SearchPersonne();
            sp.Nom = "LOLO";
            s.SelectList(sp);
        }
    }
}
