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

                        DiscriminationError(ex, p, businessObject.Parameter.GetView);

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
                    
                    if(isError)
                        return Process.FailedStopRules;

                    break;
                default:
                    break;
            }

            return Process.Succeed;
            
        }

        private void DiscriminationError(Exception pex, IOperation<TView> ProcessInstance,TView view)
        {
            FaultException<ProcessResults> exr = null;
            if (pex.InnerException is FaultException<ProcessResults>)
            {
                exr = pex.InnerException as FaultException<ProcessResults>;
            }
            else if (pex is FaultException<ProcessResults>)
            {
                exr = (FaultException<ProcessResults>)pex;
            }
                
            else if (pex.InnerException is ExceptionProcess<ProcessResults>)
            {
                ExceptionProcess<ProcessResults> exrProcess = null;
                exrProcess = pex.InnerException as ExceptionProcess<ProcessResults>;


                if (exrProcess != null)
                {
                    foreach (var item in exrProcess.Results.MessagesList)
                    {
                        if (item.TypeErrorMessage == TypeError.Concurrence)
                        {
                            //ProcessInstance.OnReservedEvent(mView, item.Description, this);
                            //ProcessInstance.ShowReservation(item.CodeMessage, item.Description, exrProcess.Results);
                            if (view.ViewLogics.ReservationMessages == null) view.ViewLogics.ReservationMessages = new ProcessResults();
                            view.ViewLogics.ReservationMessages.AddException(item);
                            
                        }
                        if (item.TypeErrorMessage == TypeError.Security)
                        {
                            //ProcessInstance.OnSecurityAccessDenied(mView, this, item);
                            //ProcessInstance.ShowSecurity(item.CodeMessage, item.Description, exrProcess.Results);
                            if (view.ViewLogics.SecurityMessages == null) view.ViewLogics.SecurityMessages = new ProcessResults();
                            view.ViewLogics.SecurityMessages.AddException(item);
                            
                        }
                        if (item.TypeErrorMessage == TypeError.ValidationBusiness)
                        {

                            //ProcessInstance.ShowBusinessValidation(item.CodeMessage, item.Description, exrProcess.Results);
                            if (view.ViewLogics.BusinessMessages == null) view.ViewLogics.BusinessMessages = new ProcessResults();
                            view.ViewLogics.BusinessMessages.AddException(item);
                            
                        }
                        if(item.TypeErrorMessage == TypeError.ValidationObjet)
                        {
                            if (view.ViewLogics.ContextValidationMessage == null) view.ViewLogics.ContextValidationMessage = new List<ReturnMessage>();
                            view.ViewLogics.ContextValidationMessage.Add(item);
                            //ProcessInstance.ShowContextValidation(item.CodeMessage, item.Description, exrProcess.Results.MessagesList);
                            
                        }

                    }
                }


            }

            if (exr != null)
            {
                foreach (var item in exr.Detail.MessagesList)
                {
                    if (item.TypeErrorMessage == TypeError.Concurrence)
                    {
                        if (view.ViewLogics.ReservationMessages == null) view.ViewLogics.ReservationMessages = new ProcessResults();
                        view.ViewLogics.ReservationMessages.AddException(item);
                        
                    }
                    if (item.TypeErrorMessage == TypeError.Security)
                    {
                        if (view.ViewLogics.SecurityMessages == null) view.ViewLogics.SecurityMessages = new ProcessResults();
                        view.ViewLogics.SecurityMessages.AddException(item);
                        
                    }
                    if (item.TypeErrorMessage == TypeError.ValidationBusiness)
                    {
                        if (view.ViewLogics.BusinessMessages == null) view.ViewLogics.BusinessMessages = new ProcessResults();
                        view.ViewLogics.BusinessMessages.AddException(item);
                        
                    }
                    if (item.TypeErrorMessage == TypeError.ValidationObjet)
                    {
                        if (view.ViewLogics.ContextValidationMessage == null) view.ViewLogics.ContextValidationMessage = new List<ReturnMessage>();
                        view.ViewLogics.ContextValidationMessage.Add(item);
                       
                    }
                }
            }
            else if(pex!=null && exr==null)
            {
                MKS.Library.ErrorLog.PublishExceptionMessage(exr, Globals.GetUserEnvironment);

                string msg=pex.Message;
                if (pex.InnerException!=null)
                    msg=msg+pex.InnerException.Message;
                ProcessInstance.ShowMessage("Erreur non géré", msg, Severity.Error);
                
            }
                

            
        }        
    }
}
