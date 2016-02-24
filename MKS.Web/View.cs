using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKS.Web.MVP;
using MKS.Core.Presenter.Interfaces;
using MKS.Core.Presenter;
using MKS.Core;
using MKS.Core.Presenter.UI;

namespace MKS.Web
{
    public abstract class View:IView
    {
        public View()
        {
            vb.Validations = new UIValidations();
        }
        private ViewData vb = new ViewData();
        

        public ViewData ViewLogics { get { return vb; } }

        #region IViewBase

        public event CommandAction OnCommand;


        /// <summary>
        /// NOTE RELEASE
        /// </summary>
        public UIActivityLogs ActivityLogs
        {
            get
            {
                return null;
            }
            set
            {
                
            }
        }

        public virtual void Navigate(string routeKey, Dictionary<string,object> param)
        {
            vb.GoForm = new Tuple<string, Dictionary<string, object>>(routeKey, param);           
        }

        public virtual void SaveSession(string key, object sessionObject)
        {
            vb.Session = new Tuple<string, object>(key, sessionObject);

        }

        public virtual object GetSession(string key)
        {
            if (vb.Session.Item1 == key)
                return vb.Session.Item2;
            return null;
        }


        public virtual void HideMessage()
        {
            vb.ClearMessages = true;
        }

        #endregion IViewBase

        public virtual void ExecuteCommand(string command, CommandEventArgsCustom args)
        {
            if (OnCommand != null) OnCommand(command, args);
        }

        public virtual void ExecuteCommand(string command)
        {
            if (OnCommand != null) OnCommand(command, null);
        }

        public UIValidations Validations
        {
            get { return vb.Validations; }
            set { vb.Validations = value; }
        }

        public virtual void ShowBusinessValidation(string title, string message, ProcessResults processResults)
        {
            vb.BusinessMessages = processResults;
        }

        public virtual void ShowMessage(string title, string message, Severity severity)
        {
            vb.Messages = new Tuple<string, string, Severity>(title, message, severity);
        }


        public virtual void ShowContextValidation(string title, string message, List<ReturnMessage> result)
        {
            vb.ContextValidationMessage = result;
        }
        public virtual void  ShowSecurity(string title, string message, ProcessResults processResults)
        {
            vb.SecurityMessages=processResults;
        }
        public virtual void ShowReservation(string title, string message, ProcessResults processResults)
        {
            vb.ReservationMessages=processResults;
        }



    }
}
