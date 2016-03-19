using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKS.Core.Business;
using MKS.Core.Presenter;

using MKS.Core.Presenter.Interfaces;
using System.ServiceModel;
using MKS.Core.Model.Error;

namespace MKS.Core.Presenter
{
    public class BusinessProcessCommand<TView,TProcess> : BusinessProcessExecute<ObjectProcess<TView>>
        where TView:IView
        where TProcess : IOperation<TView>
    {
        public override List<RuleBusiness> GetProcessRules()
        {
            List<RuleBusiness> lb = new List<RuleBusiness>();
            lb.Add(new RuleBusiness("ERR_CMD_ACTION", "Erreur dans l'exécution de la commande", Rule.RuleSeverity.Error));
            lb.Add(new RuleBusiness("ERR_CMD_SHOW", "Erreur dans l'affichage des erreurs", Rule.RuleSeverity.Error));
            return lb;
        }

        public override Process DoBusinessProcess(RuleBusiness rule, BusinessObjectExecute<ObjectProcess<TView>> businessObject)
        {

            

            IOperation<TView> p = businessObject.Parameter.GetProcess;
            
            switch (rule.CodeMessage)
            {

                case "ERR_CMD_ACTION":
                    try
                    {


                        p.OnCommand(businessObject.Parameter.Command, businessObject.Parameter.Args, businessObject.Parameter.GetView, businessObject.Parameter.Presenter);

                        return Process.Succeed;
                    }
                    catch (Exception ex)
                    {

                        Utile.DiscriminationError<TView>(ex, p, businessObject.Parameter.GetView);
                        
                        return Process.SuccessAddMessage;
                    }
                    break;
                case "ERR_CMD_SHOW":
                    bool isError = false;
                    if (businessObject.Parameter.GetView.ViewLogics.BusinessMessages != null)
                    {
                        isError = true;
                        p.ShowBusinessValidation(Resources.CoreResources.CA_PROCESS, "", businessObject.Parameter.GetView.ViewLogics.BusinessMessages);
                    }
                    if (businessObject.Parameter.GetView.ViewLogics.ContextValidationMessage != null)
                    {
                        isError = true;
                        p.ShowContextValidation(Resources.CoreResources.CA_PROCESS, "", businessObject.Parameter.GetView.ViewLogics.ContextValidationMessage);
                    }
                    if (businessObject.Parameter.GetView.ViewLogics.ReservationMessages != null)
                    {
                        isError = true;
                        p.ShowReservation(Resources.CoreResources.CA_PROCESS, "", businessObject.Parameter.GetView.ViewLogics.ReservationMessages);
                    }
                    if (businessObject.Parameter.GetView.ViewLogics.SecurityMessages != null)
                    {
                        isError = true;
                        p.ShowSecurity(Resources.CoreResources.CA_PROCESS, "", businessObject.Parameter.GetView.ViewLogics.SecurityMessages);
                    }

                    if (isError)
                    {
                        
                        return Process.FailedStopRules;
                    }


                    break;
                default:
                    break;
            }

            return Process.Succeed;
            
        }

       
    }
}
