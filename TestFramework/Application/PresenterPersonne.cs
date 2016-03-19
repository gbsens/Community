using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKS.Core.Presenter;
using MKS.Presenter;

namespace TestFramework.Application
{
    public class PresenterPersonne:Presenter<IVuePersonne,PresenterPersonneProcess>
    {
        public PresenterPersonne(IVuePersonne vue):base(vue)
        { }
    }
    public class PresenterPersonne2 : Presenter<IVuePersonne, PresenterPersonneProcess2>
    {
        public PresenterPersonne2(IVuePersonne vue)
            : base(vue)
        { }
    }
}
