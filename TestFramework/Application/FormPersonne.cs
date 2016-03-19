using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application
{



    public class FormPersonne:IVuePersonne
    {
        public string Nom
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Prenom
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        private MKS.Core.Presenter.ViewData vd = new MKS.Core.Presenter.ViewData();
        public MKS.Core.Presenter.ViewData ViewLogics
        {
            get { return vd; }
        }

        public MKS.Core.Presenter.UI.UIValidations Validations
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public MKS.Core.Presenter.UI.UIActivityLogs ActivityLogs
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public event MKS.Core.Presenter.Interfaces.CommandAction OnCommand;

        public void Navigate(string routeKey, Dictionary<string, object> param)
        {
            throw new NotImplementedException();
        }

        public void SaveSession(string key, object sessionObject)
        {
            throw new NotImplementedException();
        }

        public object GetSession(string key)
        {
            throw new NotImplementedException();
        }

        public void HideMessage()
        {
           
        }

        public void ShowContextValidation(string title, string message, List<MKS.Core.ReturnMessage> result)
        {
            vd.ContextValidationMessage = result;
        }

        public void ShowBusinessValidation(string title, string message, MKS.Core.ProcessResults processResults)
        {
            vd.BusinessMessages = processResults;
        }

        public void ShowMessage(string title, string message, MKS.Core.Severity severity)
        {
            Tuple<string, string, MKS.Core.Severity> t = new Tuple<string, string, MKS.Core.Severity>(title,message,severity);

            vd.Messages = t;
        }

        public void ShowSecurity(string title, string message, MKS.Core.ProcessResults processResults)
        {
            throw new NotImplementedException();
        }

        public void ShowReservation(string title, string message, MKS.Core.ProcessResults processResults)
        {
            throw new NotImplementedException();
        }
    }
}
