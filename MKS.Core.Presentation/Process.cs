using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKS.Core.Presenter.Interfaces;
using MKS.Core.Security;
using System.Reflection;

namespace MKS.Core.Presenter
{
    public abstract class Process<TView>:IOperation<TView> 
        where TView:IView
    {

        TView _view;

        public void AssignView(TView view)
        {
            _view = view;
        }


        public virtual void Initialisation(bool isInit, TView view, IPresenter presenter)
        {
            AssignView(view);
            
        }

        public virtual void OnCommand(string command, CommandEventArgsCustom args, TView view, IPresenter presenter)
        {
            if (command != null)
            {
                var mi = GetType().GetMethod(command, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (mi != null)
                {
                    var pi = mi.GetParameters();

                    if (pi.Length == 3 &&
                        pi[0].ParameterType == typeof(CommandEventArgsCustom) &&
                        pi[1].ParameterType == typeof(TView) &&
                        pi[2].ParameterType == typeof(IPresenter))
                        mi.Invoke(this, new object[] { args, view, presenter });
                }
            }
            
        }

        public virtual void OnCommand<TInputParameter>(string command, TInputParameter inputparameter, TView view, IPresenter presenter)
        {
            
            var mi = GetType().GetMethod(command, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (mi != null)
            {
                mi.Invoke(this, new object[] { inputparameter, view, presenter });
            }
        }


        public virtual void ShowContextValidation(string title, string message, List<ReturnMessage> result)
        {
            _view.ShowContextValidation(title, message, result);
        }

        public virtual void ShowBusinessValidation(string title, string message, ProcessResults processResults)
        {
            _view.ShowBusinessValidation(title, message, processResults);
        }

        public virtual void ShowMessage(string title, string message, Severity severity)
        {
            _view.ShowMessage(title, message, severity);
        }


        public virtual void ShowSecurity(string title, string message, ProcessResults processResults)
        {
            _view.ShowSecurity(title, message, processResults);
        }

        public virtual void ShowReservation(string title, string message, ProcessResults processResults)
        {
            _view.ShowReservation(title, message, processResults);
        }
    }
    
   

}
