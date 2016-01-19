using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKS.Core.Activity;
using MKS.Core.Presenter.Interfaces;

namespace MKS.Core.Presenter
{
    public class Journalisation<TView>:ActivityLogExecute<TView> where TView:IView
    {
        
    }
}
