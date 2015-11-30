using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKS.Core.Web;
using MKS.Core.Presenter;

using System.Web.Mvc;
using MKS.Core;
using System.Collections.Specialized;

namespace MKS.Web.MVC
{
    public class Controler:Controller, IViewBase
    {
        public Controler()
        { }
        private ViewLogic vb = new ViewLogic();
        
        public ViewLogic ViewLogics { get { return vb; } }

        #region IViewBase

        public event EventCommand OnCommand;

        public object ViewParameters
        {
            get;
            set;
        }

        public virtual void ShowMessage(string titre, string message, MessageSeverity msgSeveriry)
        {
            vb.ViewMessage = new Tuple<string, string, MessageSeverity>(titre, message, msgSeveriry);
        }

        public virtual void ShowValidationMessage(string titre, string formatedMessage, List<ReturnMessage> returnMessageList, List<LinkObjectView> viewToObjectLink)
        {
            vb.ViewValidation = new Tuple<string, List<ReturnMessage>, List<LinkObjectView>>(titre, returnMessageList, viewToObjectLink);
        }

        public virtual void ShowReservationMessage(string message)
        {
            vb.ViewMessageReservation = new Tuple<string, string, MessageSeverity>(PresenterResources.ERR_RESERVATION, message, MessageSeverity.Warning); ;
        }

        public virtual void ShowBusinessValidationMessage(string titre, ProcessResults result)
        {
            vb.ViewBusinessValidation = new Tuple<string, ProcessResults>(titre, result);
        }

        public virtual void AssociateViewToValidation(List<RulesAssociationsObject> rulesassociations)
        {
            vb.ViewAssociateViewToValidation = new List<RulesAssociationsObject>();
            vb.ViewAssociateViewToValidation = rulesassociations;
        }

        public virtual void SetSecurityDisplay(bool isAuthorized, params string[] permissions)
        {
            if (vb.ViewSecurityPermission == null)
            {
                vb.ViewSecurityPermission = new List<Tuple<bool, string[]>>();
            }
            vb.ViewSecurityPermission.Add(new Tuple<bool, string[]>(isAuthorized, permissions));
        }

        public virtual void SetSecurityNoAccess(string param)
        {
            vb.ViewNoAccess = param;
        }

        public virtual void InitializeDisplay()
        {
        }

        public virtual void ShowSystemStatus(StringDictionary status)
        {
            vb.ViewSystemStatus = new System.Collections.Specialized.StringDictionary();
            vb.ViewSystemStatus = status;
        }

        public virtual void GoLocalize(string routeKey, params string[] param)
        {
            if (Localizations.Form.ContainsKey(routeKey))
            {
                string url = Localizations.Form[routeKey];

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
                    vb.ViewGolocalize = new Tuple<string, string[]>(url + sb, null);

                    //RedirectLocation(Localizations.Form[routeKey] + sb);
                    //Response.Redirect(Localizations.Form[routeKey] + sb);
                }
                else
                {
                    vb.ViewGolocalize = new Tuple<string, string[]>(url, null);

                    //RedirectLocation(Localizations.Form[routeKey]);
                    //Response.Redirect(Localizations.Form[routeKey]);
                }
            }
            else
            {
                vb.ViewGolocalize = new Tuple<string, string[]>(routeKey, param);
            }
        }

        public virtual void SaveSessionManager(string key, object sessionObject)
        {
            vb.ViewSession = new Tuple<string, object>(key, sessionObject);

        }

        public virtual object GetSessionManager(string key)
        {
            if (vb.ViewSession.Item1 == key)
                return vb.ViewSession.Item2;
            return null;
        }

        public bool IsRichClient()
        {
            return false;
        }

        public virtual void HideValidationMessage()
        {
            vb.ViewClearAllValidationMessage = true;
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

        public string commandToExecute
        {
            get;

            set;
        }
    }
}
