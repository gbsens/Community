using MKS.Core;
using MKS.Presenter;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

using MKS.Core.Presenter;
using MKS.Core.Presenter.Interfaces;
using MKS.Core.Presenter.UI;

namespace MKS.Web.MVP
{
    public abstract class Page:System.Web.UI.Page,IView
    {
        public Page()
        {
            vb.Validations = new UIValidations();
        }
        private ViewData vb = new ViewData();

        public ViewData ViewLogics { get { return vb; } }

        #region IViewBase

        public event CommandAction OnCommand;
        
       
      

        public virtual void Navigate(string routeKey, params object[] param)
        {
            if (Navigation.Form.ContainsKey(routeKey))
            {
                string url = (string)Navigation.Form[routeKey];

                if (param != null && param.Length > 0)
                {
                    var sb = new StringBuilder();
                    sb.Append("?");
                    for (int i = 0; i < param.Length; i = i + 2)
                    {
                        sb.AppendFormat("{0}={1}", param[i], param[i + 1]);
                        if (i < param.Length - 2)
                            sb.Append("&");
                    }
                    vb.GoForm = new Tuple<string, object[]>(url + sb, null);

                    //RedirectLocation(Localizations.Form[routeKey] + sb);
                    Response.Redirect(Navigation.Form[routeKey] as string + sb);
                }
                else
                {
                    vb.GoForm = new Tuple<string, object[]>(url, null);

                    //RedirectLocation(Localizations.Form[routeKey]);
                    Response.Redirect(Navigation.Form[routeKey] as string);
                }
            }
            else
            {
                vb.GoForm = new Tuple<string, object[]>(routeKey, param);
            }
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
