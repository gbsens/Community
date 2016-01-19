using MKS.Core.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKS.Core.Presenter.Interfaces;
using MKS.Presenter.Model;
using MKS.Core.Security;
using MKS.Core.Presenter.UI;

namespace MKS.Core.Presenter
{

    

    public abstract class Presenter<TView, TProcess> : IPresenter<TView,TProcess>
        where TView:IView
        where TProcess:IOperation<TView>, new()
    {

        TView _view;
        IOperation<TView> _process;
        protected ISecurityAdapter _securityAdapter;

        BusinessPresenter<TView, TProcess> _businessPresenter;

        public Presenter(TView view)
        {
            _view = view;
            
        }
        
        public virtual void Start(bool initialisation=true)
        {

            _businessPresenter = new BusinessPresenter<TView, TProcess>(initialisation);
            
            ObjectPresenter<TView> obj = new ObjectPresenter<TView>(_view,this);

            

            _businessPresenter.Execute(obj);
            _process = obj.Process ;
            _view.OnCommand += ExecuteCommand;
           
            
        }




        public void ExecuteCommand(string command)
        {
            ExecuteCommand(command, null);
        }

        public void ExecuteCommand(string command, CommandEventArgsCustom args)
        {
            ObjectProcess<TView> obj = new ObjectProcess<TView>(_view,_process,this);
            obj.Args = args;
            obj.Command = command;

            BusinessCommand<TView,TProcess> b = new BusinessCommand<TView,TProcess>();

            b.Execute(obj);



        }


        public virtual void SaveSession(string key, object sessionObject)
        {
            _view.SaveSession(key, sessionObject);
        }

        public virtual object GetSession(string key)
        {
            return _view.GetSession(key);
        }

        public RuleResults Validate(string successMessage, params object[] objectInstance)
        {
            var result = new RuleResults();
            foreach (var itemObject in objectInstance)
            {
                var method = _view.Validations.GetType().GetMethod("Validate");

                var genericMethod = method.MakeGenericMethod(itemObject.GetType());
                var parameters = new[] { itemObject };
                var r = (RuleResults)genericMethod.Invoke(_view.Validations, parameters);
                result.Add(r);
            }
            if (!string.IsNullOrEmpty(successMessage))
            {
                if (result.Count == 0)
                {
                    _view.ShowMessage(PresenterResources.VALIDATION_SUCCESS, successMessage, Severity.Success);
                }
                else
                {
                    var sb = new StringBuilder();
                    foreach (var item in result)
                    {
                        sb.Append(item.RuleInformation.Description);
                    }
                    
                    
                    _view.ShowContextValidation(PresenterResources.ERR_VALIDATION, sb.ToString(),Utilities.ConvertRuleResultsToReturnMessageList( result));
                }
            }
            return result;
        }


        public virtual Permission AssignPermission(string permissionCode, UIBase control, AssignProperty property)
        {
            return null; 
        }

        public virtual IApplicationAuthorization Authorizations
        {
            get;
            set;
        }
    }


    public abstract class Presenter<TView, TProcess, TSecurity> : Presenter<TView, TProcess>, IPresenter<TView,TProcess>
        where TView:IView
        where TProcess:IOperation<TView>, new()
        where TSecurity:ISecurityAdapter, new()
    {

 
        public Presenter(TView view):base(view)
        {
           
        }


        public override void Start(bool initialisation=true)
        {
            _securityAdapter = new TSecurity();
            base.Start(initialisation);
        }

        /// <summary>
        ///     Assigne la permission au control UI et effectue la vefification de la permission.
        /// </summary>
        /// <param name="permissionCode">Code de la permission</param>
        /// <param name="control">Control UI</param>
        /// <param name="property">Propriete a modifier</param>
        /// <returns></returns>
        public override Permission AssignPermission(string code, UIBase control, AssignProperty property)
        {
            if (Authorizations != null)
            {
                foreach (var item in Authorizations.Permissions)
                {
                    if (item.Permission.Code == code)
                    {
                        if (property == AssignProperty.Enabled)
                            control.Enabled = item.IsUserAuthorized;
                        else
                            control.Visible = item.IsUserAuthorized;

                        return item.Permission as Permission;
                    }
                }
                if (property == AssignProperty.Enabled)
                    control.Enabled = false;
                else
                    control.Visible = false;

                return null;
            }
            return null;
        }

        /// <summary>
        /// Liste des authorisations de l'application
        /// </summary>
        public override IApplicationAuthorization Authorizations
        {
            get
            {
                if (_securityAdapter != null)
                    return _securityAdapter.GetApplicationAuthorization(Globals.GetUserEnvironment.GetCurrentUserCodeWithoutDomain(),
                            Globals.GetUserEnvironment.GetCurrentSystemCode());
                else
                    return null;
            }
            set
            {
                _securityAdapter = value as ISecurityAdapter;
            }
        }
    }

}
