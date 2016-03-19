using MKS.Core.Presenter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application
{
    public interface IVuePersonne:IView
    {
        string Nom { get; set; }
        string Prenom { get; set; }
    }
}
