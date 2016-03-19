using MKS.Core.Model.Error;
using MKS.Core.Presenter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MKS.Core.Presenter
{
    static class Utile
    {
        public static void DiscriminationError<TView>(Exception pex, IOperation<TView> ProcessInstance, TView view) where TView:IView
        {
            bool Error = true;
            FaultException<ProcessResults> exr = null;
            if (pex.InnerException is FaultException<ProcessResults>)
            {
                exr = pex.InnerException as FaultException<ProcessResults>;
            }
            else if (pex is FaultException<ProcessResults>)
            {
                exr = (FaultException<ProcessResults>)pex;
            }
            
            else if (pex is ExceptionProcess<ProcessResults> || pex.InnerException is ExceptionProcess<ProcessResults>)
            {
                ExceptionProcess<ProcessResults> exrProcess = null;
                if(pex.InnerException!=null)
                    exrProcess = pex.InnerException as ExceptionProcess<ProcessResults>;
                else
                    exrProcess = pex as ExceptionProcess<ProcessResults>;
                
                Error = false;

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
                        if (item.TypeErrorMessage == TypeError.ValidationObjet)
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
                Error = false;
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
            else if (Error)
            {
                MKS.Library.ErrorLog.PublishExceptionMessage(pex, Globals.GetUserEnvironment);

                string msg = pex.Message;
                if (pex.InnerException != null)
                    msg = msg + pex.InnerException.Message;
                ProcessInstance.ShowMessage(pex.GetType().ToString(), msg, Severity.Error);

            }



        }        
    }
}
