using System;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;

using MKS.Core;
using System.Web.UI.WebControls;

using MKS.Core.Presenter.Interfaces;
using MKS.Core.Presenter;
using System.Reflection;
using MKS.Core.Presenter.UI;


namespace MKS.Web.REST
{
    /// <summary>
    /// La classe contrôleur permet d'exposer en WebService les fonctions d'appels à la logique de présentation.
    /// Les commandes recu par l'interface utilisateur sont transformés en appel de fonction vers la classe de ProcessViewBase Associé au présenteur.
    /// </summary>
    /// <remarks>Attention, il faut obligatoirement que la classe TView et Tcontroler match avec la définition dans le présenteur</remarks>
    [WebService(Namespace = "http://gbsens.ca/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public abstract class ControllerREST<TView, TPresenterLogic> : System.Web.Services.WebService
        where TView : IView, new()
        where TPresenterLogic : IPresenter, new()
    {
        ///Indique au controleur si la fonction OnInitializeDisplay du processView est géré automatique ou non.
        ///Par défaut c'est 'true' donc géré automatique par le présenteur.
        protected bool IsAutoInitialDisplay = true;

        /// <summary>
        /// Permet de lancer une commande pour atteindre les fonctions personalisé dans la classe de processus
        /// </summary>
        /// <param name="command">Nom de la commande, elle doit correspondre au nom de la fonction défini dans la classe de processus</param>
        /// <returns>L'ensemble de la vue en JSON</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json,UseHttpGet=true)]
        public virtual TView ExecuteCommand(string command)
        {
            //Initialisation du presenter
            TView view = new TView();

            IPresenter controler = (IPresenter)Activator.CreateInstance(typeof(TPresenterLogic), view);

            controler.Start(IsAutoInitialDisplay);

            controler.ExecuteCommand(command);

            return (view);
        }

        /// <summary>
        /// Permet de lancer une commande pour atteindre les fonctions personalisé dans la classe de processus
        /// </summary>
        /// <param name="parameters">Liste de paramêtre à traiter qui sera recu dans le Args de la fonction personnalisé du processus</param>
        /// <param name="command">Nom de la commande, elle doit correspondre au nom de la fonction défini dans la classe de processus</param>
        /// <returns>L'ensemble de la vue en JSON</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json,UseHttpGet=true)]
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
        /// <param name="JSONView">vue de l'interface utilisateur</param>
        /// <param name="command">Nom de la fonction à appeler dans le processView</param>
        /// <returns>La vue avec l'ensemble des informations</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public virtual TView ExecuteCommandForm(string JSONView, string command)
        {
            TView view = JsonUtility.Deserialize<TView>(JSONView);

            IPresenter controler = (IPresenter)Activator.CreateInstance(typeof(TPresenterLogic), view);
            controler.Start(IsAutoInitialDisplay);

            //CommandEventArgsCustom cm = new CommandEventArgsCustom(JSONView);

            //envoie la commande
            //controler.ExecuteCommand(command, cm);
            controler.ExecuteCommand(command);

            return (view);
        }


        /// <summary>
        /// Permet de lancer une commande pour atteindre les fonctions personalisé dans la classe de processus
        /// </summary>
        /// <param name="parameters">Retourne la vue complète</param>
        /// <param name="command">Nom de la commande, elle doit correspondre au nom de la fonction défini dans la classe de processus</param>
        /// <returns>L'ensemble de la vue en JSON</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public virtual TView ExecuteCommandFormView(TView parameters, string command)
        {
            //List<Parameters> parameter = JsonUtility.Deserialize<List<Parameters>>parameters);

            TView view = parameters;

            IPresenter controler = (IPresenter)Activator.CreateInstance(typeof(TPresenterLogic), view);
            controler.Start(IsAutoInitialDisplay);

            //envoie la commande
            //controler.ExecuteCommand(command, parameters);
            controler.ExecuteCommand(command);

            return (view);
        }

        /// <summary>
        /// Permet de lancer une commande pour atteindre les fonctions personalisé dans la classe de processus
        /// </summary>
        /// <param name="parameters">Retourne la vue complète</param>
        /// <param name="command">Nom de la commande, elle doit correspondre au nom de la fonction défini dans la classe de processus</param>
        /// <returns>L'ensemble de la vue en JSON</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public virtual TView ExecuteCommandFormObject(object parameters, string command)
        {
            //List<Parameters> parameter = JsonUtility.Deserialize<List<Parameters>>(parameters);
            Dictionary<string, object> dic =( Dictionary<string, object>)parameters;

            TView view = new TView();

            

            //foreach (var item in dic)
            //{
            //    Type myType = view.GetType();
            //    IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            //    foreach (PropertyInfo prop in props)
            //    {
            //        if(item.Key == prop.Name)
            //        {
            //            switch (prop.PropertyType.Name)
            //            {
            //                case "Input":
            //                    Input i = new Input(item.Value as string);
            //                    prop.SetValue(view,i);
            //                    break;
            //                case "Label":
            //                    MKS.Core.Presenter.UI.Label l = new MKS.Core.Presenter.UI.Label(item.Value as string);
            //                    prop.SetValue(view,l);
            //                    break;
            //                case "Menu":
            //                    //MKS.Core.Presenter.UI.Menu i = new MKS.Core.Presenter.UI.Menu(item.Value);
            //                    //prop.SetValue(view,i);
            //                    break;
            //                case "Option":
            //                    MKS.Core.Presenter.UI.Option o = new MKS.Core.Presenter.UI.Option();
            //                    o.Value - item.Value as bool;
            //                    prop.SetValue(view,o);
            //                    break;
            //                case "CheckBox":
            //                    MKS.Core.Presenter.UI.CheckBox c = new MKS.Core.Presenter.UI.CheckBox();
            //                    ci.Value = item.Value as bool;
            //                    prop.SetValue(view,ci);
            //                    break;
            //                case "NumericInput":
            //                    NumericInput n = new NumericInput();
            //                    n.Value = item.Value as int;
            //                    prop.SetValue(view,n);
            //                    break;
            //                case "NumericInputLong":
            //                    NumericInputLong lo = new NumericInputLong();
            //                    lo.Value = item.Value as long;
            //                    prop.SetValue(view,lo);
            //                    break;
            //                case "NumericInputDecimal":
            //                    NumericInputDecimal d = new NumericInputDecimal();
            //                    d.Value = item.Value as decimal;
            //                    prop.SetValue(view,d);
            //                    break;
            //                case "NumericInputDouble":
            //                    NumericInputDouble dob = new NumericInputDouble();
            //                    dob.Value = item.Value as double;
            //                    prop.SetValue(view,dob);
            //                    break;
            //                case "DateInput":
            //                    DateInput dt = new DateInput(item.Value as DateTime);
            //                    prop.SetValue(view,dt);
            //                    break;
            //                case "Button":
            //                    //MKS.Core.Presenter.UI.Button i = new MKS.Core.Presenter.UI.Button(item.Value);
            //                    //prop.SetValue(view,i);
            //                    break;
            //                case "Tab":
                                
                                
            //                    break;
            //                case "Grid":
            //                    break;
            //                default:
            //                    prop.SetValue(view, item.Value);
            //                    break;
            //            }
            //            prop.SetValue(view, item.Value);
            //        }
                        

            //        // Do something with propValue
            //    }
	        //} 



            IPresenter controler = (IPresenter)Activator.CreateInstance(typeof(TPresenterLogic), view);
            controler.Start(IsAutoInitialDisplay);

            CommandEventArgsCustom cm = new CommandEventArgsCustom(parameters);

            //envoie la commande
            controler.ExecuteCommand(command, cm);
            //controler.ExecuteCommand(command);

            return (view);
        }



        //[WebMethod]
        //[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        //public virtual TView ExecuteCommandGrid(int id,int pagenum, int pagesize, string command)
        //{
        //    TView view = new TView();

        //    IPresenter controler = (IPresenter)Activator.CreateInstance(typeof(TPresenterLogic), view);
        //    controler.Start(IsAutoInitialDisplay);

            
        //    List<Parameters> lp = new List<Parameters>();

        //    Parameters p = new Parameters();
        //    p.Name = "Id";
        //    p.Value = id.ToString();
        //    lp.Add(p);

        //    Parameters p1 = new Parameters();
        //    p1.Name = "Page";
        //    p1.Value = pagenum.ToString();
        //    lp.Add(p1);

        //    Parameters p2 = new Parameters();
        //    p2.Name = "PageSize";
        //    p2.Value = pagesize.ToString();
        //    lp.Add(p2);

        //    CommandEventArgsCustom cm = new CommandEventArgsCustom(lp);

        //    //envoie la commande
        //    controler.ExecuteCommand(command,cm);

        //    return (view);

        //}

        //#region validation custom, implementation de IControlerValidation

        ///// <summary>
        ///// Permet de déclanché une validation personnalisé à partir de l'interface utilisateur       
        ///// Cette fonction est appelé lorsque une validation de contexte est assigné sur une champs de la vue.
        ///// </summary>
        ///// <param name="ObjectReference">L'objet à valider</param>
        ///// <param name="PropertyReference">Propriété à vérifier</param>
        ///// <param name="parameters">Valeur de la propriété à valider</param>
        ///// <returns>True si aucune erreur de validation</returns>
        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public virtual bool CustomRules(string ObjectReference, string PropertyReference, string parameters)
        //{
        //    return Tools.ValidationAllCustomRules<TView, TPresenterLogic>(ObjectReference, PropertyReference, parameters, ServerValidation.CustomValidation);
        //}
        
        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public virtual bool DateCustomRules(string ObjectReference, string PropertyReference, string parameters)
        //{
        //    return Tools.ValidationAllCustomRules<TView, TPresenterLogic>(ObjectReference, PropertyReference, parameters, ServerValidation.CustomDateValidation);
        //}

        //#endregion validation custom, implementation de IControlerValidation
    }
}