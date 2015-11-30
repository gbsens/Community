using System;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;

using MKS.Core;
using System.Web.UI.WebControls;
using MKS.Core.Presenter;


namespace MKS.Web.REST
{
    /// <summary>
    /// La classe contrôleur permet d'exposer en WebService les fonctions d'appels à la logique de présentation.
    /// Les commandes recu par l'interface utilisateur sont transformés en appel de fonction vers la classe de ProcessViewBase Associé au présenteur.
    /// </summary>
    /// <remarks>Attention, il faut obligatoirement que la classe TView et Tcontroler match avec la définition dans le présenteur</remarks>
    [WebService(Namespace = "http://gbsens.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public abstract class ControllerBase<TView, TPresenterLogic> : System.Web.Services.WebService, IControllerValidation
        where TView : IView, new()
        where TPresenterLogic : IPresenter, new()
    {
        ///Indique au controleur si la fonction OnInitializeDisplay du processView est géré automatique ou non.
        ///Par défaut c'est 'true' donc géré automatique par le présenteur.
        protected bool IsAutoInitialDisplay = true;

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public virtual TView ExecuteCommand(string command)
        {
            //Initialisation du presenter
            TView view = new TView();

            IPresenter controler = (IPresenter)Activator.CreateInstance(typeof(TPresenterLogic), view);

            controler.Start(IsAutoInitialDisplay);

            view.ExecuteCommand(command);

            return (view);
        }

        /// <summary>
        /// Permet d'executer une fonction du processView en passant des paramêtres
        /// </summary>
        /// <param name="parameters">Liste de paramêtre à traiter</param>
        /// <param name="command">Nom de la fonction à appeler dans le processView</param>
        /// <returns>La vue avec l'ensemble des informations</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public virtual TView ExecuteCommandParameters(List<Parameters> parameters, string command)
        {
            //List<Parameters> parameter = JsonUtility.Deserialize<List<Parameters>>parameters);

            TView view = new TView();

            IPresenter controler = (IPresenter)Activator.CreateInstance(typeof(TPresenterLogic), view);
            controler.Start(IsAutoInitialDisplay);

            CommandEventArgsCustom cm = new CommandEventArgsCustom(parameters);

            //envoie la commande
            controler.ExecuteCommand(command, cm);

            return (view);
        }

        /// <summary>
        /// Permet d'exécutrer une fonction selon le paramètre command et désirialise la vue reçu de l'interface utilisateur
        /// </summary>
        /// <param name="JSONParameter">vue de l'interface utilisateur</param>
        /// <param name="command">Nom de la fonction à appeler dans le processView</param>
        /// <returns>La vue avec l'ensemble des informations</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public virtual TView ExecuteCommandForm(string JSONParameter, string command)
        {
            TView view = JsonUtility.Deserialize<TView>(JSONParameter);

            IPresenter controler = (IPresenter)Activator.CreateInstance(typeof(TPresenterLogic), view);
            controler.Start(IsAutoInitialDisplay);

            //CommandEventArgsCustom cm = new CommandEventArgsCustom(JSONParameter);

            //envoie la commande
            //controler.ExecuteCommand(command, cm);
            controler.ExecuteCommand<TView>(command, view);

            return (view);
        }
        /// <summary>
        /// Permet d'executer une fonction du processView en passant des paramêtres
        /// </summary>
        /// <param name="parameters">Liste de paramêtre à traiter</param>
        /// <param name="command">Nom de la fonction à appeler dans le processView</param>
        /// <returns>La vue avec l'ensemble des informations</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public virtual TView ExecuteCommandFormView(TView parameters, string command)
        {
            //List<Parameters> parameter = JsonUtility.Deserialize<List<Parameters>>parameters);

            TView view = new TView();

            IPresenter controler = (IPresenter)Activator.CreateInstance(typeof(TPresenterLogic), view);
            controler.Start(IsAutoInitialDisplay);

            //envoie la commande
            controler.ExecuteCommand(command, parameters);

            return (view);
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public virtual TView ExecuteCommandGrid(int id,int pagenum, int pagesize, string command)
        {
            TView view = new TView();

            IPresenter controler = (IPresenter)Activator.CreateInstance(typeof(TPresenterLogic), view);
            controler.Start(IsAutoInitialDisplay);

            
            List<Parameters> lp = new List<Parameters>();

            Parameters p = new Parameters();
            p.Name = "Id";
            p.Value = id.ToString();
            lp.Add(p);

            Parameters p1 = new Parameters();
            p1.Name = "Page";
            p1.Value = pagenum.ToString();
            lp.Add(p1);

            Parameters p2 = new Parameters();
            p2.Name = "PageSize";
            p2.Value = pagesize.ToString();
            lp.Add(p2);

            CommandEventArgsCustom cm = new CommandEventArgsCustom(lp);

            //envoie la commande
            controler.ExecuteCommand(command,cm);

            return (view);

        }

        #region validation custom, implementation de IControlerValidation

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public virtual bool CustomRules(string ObjectReference, string PropertyReference, string parameters)
        {
            return Tools.ValidationAllCustomRules<TView, TPresenterLogic>(ObjectReference, PropertyReference, parameters, ServerValidation.CustomValidation);
        }
        
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public virtual bool DateCustomRules(string ObjectReference, string PropertyReference, string parameters)
        {
            return Tools.ValidationAllCustomRules<TView, TPresenterLogic>(ObjectReference, PropertyReference, parameters, ServerValidation.CustomDateValidation);
        }

        #endregion validation custom, implementation de IControlerValidation
    }
}