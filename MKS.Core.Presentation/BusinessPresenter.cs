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
    /// <summary>
    /// Classe pour l'association des traitements présentation
    /// </summary>
    /// <typeparam name="TView">Modèle de vue</typeparam>
    /// <typeparam name="TProcess">Processus de présentation</typeparam>
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
    /// <summary>
    /// Classe pour l'association des traitements présentation
    /// </summary>
    /// <typeparam name="TView">Modèle de vue</typeparam>
    /// <typeparam name="TProcess">Processus de présentation</typeparam>
    /// <typeparam name="TActivityLog">Classe de journalisation</typeparam>
    public class BusinessPresenter<TView, TProcess, TActivityLog> : BusinessExecute<ObjectPresenter<TView>>
        where TView : IView
        where TProcess : IOperation<TView>, new()
        where TActivityLog : IActivityLogOperationsExecute<TView>, IActivityAdapter, new()
    {
        public BusinessPresenter(bool initialisation = true)
            : base(false)
        {
            if (initialisation)
                SetProcessExecute<BusinessProcessPresenter<TView, TProcess>>();
            else
                SetProcessExecute<BusinessProcessPresenterNoInit<TView, TProcess>>();
            SetProcessError<BusinessError>();
            
        }
        
    }

}
