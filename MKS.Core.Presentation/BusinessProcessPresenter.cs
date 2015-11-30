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
    public class BusinessProcessPresenter<TView, TProcess> : BusinessProcessExecute<ObjectPresenter<TView>>
        where TView:IView
        where TProcess:IOperation<TView>, new()
    {
        public override List<RuleBusiness> GetProcessRules()
        {
            List<RuleBusiness> lr = new List<RuleBusiness>();
            lr.Add(new RuleBusiness("ERR_PRES_INIT", "Initialisation du présenter", Rule.RuleSeverity.Error));
            return lr;
        }

        public override Process DoBusinessProcess(RuleBusiness rule, BusinessObjectExecute<ObjectPresenter<TView>> businessObject)
        {

            TProcess proc = new TProcess();

            //permet d'initier la vue dans declancher l'initialisatoin de la page.
            proc.AssignView(businessObject.Parameter.GetView);
            

            switch (rule.CodeMessage)
            {
                case "ERR_PRES_INIT":
                    proc.Initialisation(true,businessObject.Parameter.GetView, businessObject.Parameter.Presenter);
                    businessObject.Parameter.Process = proc;        
                    break;

            }

            

            return Process.Succeed;
        }
    }

    public class BusinessProcessPresenterNoInit<TView, TProcess> : BusinessProcessExecute<ObjectPresenter<TView>>
        where TView : IView
        where TProcess : IOperation<TView>, new()
    {
        public override List<RuleBusiness> GetProcessRules()
        {
            List<RuleBusiness> lr = new List<RuleBusiness>();
            lr.Add(new RuleBusiness("ERR_PRES", "Démarrage du présenter", Rule.RuleSeverity.Error));
            return lr;
        }

        public override Process DoBusinessProcess(RuleBusiness rule, BusinessObjectExecute<ObjectPresenter<TView>> businessObject)
        {

            TProcess proc = new TProcess();

            //permet d'initier la vue dans declancher l'initialisatoin de la page.
            proc.AssignView(businessObject.Parameter.GetView);


            switch (rule.CodeMessage)
            {
                case "ERR_PRES":
                    proc.Initialisation(false,businessObject.Parameter.GetView, businessObject.Parameter.Presenter);
                    businessObject.Parameter.Process = proc;
                    break;

            }



            return Process.Succeed;
        }
    }
}
