using System;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;

using MKS.Core;
using System.Web.UI.WebControls;
using MKS.Core.Presenter;

namespace MKS.Web.REST
{
    public abstract class Controller<TView, TPresenterLogic> : ControllerBase<TView, TPresenterLogic>
        where TView : IView, new()
        where TPresenterLogic : IPresenter, new()
    {
    }

    public abstract class Controller<TView, TPresenterLogic,TInputObject> : ControllerBase<TView,TPresenterLogic>
        where TView : IView, new()
        where TPresenterLogic : IPresenter, new()
    {
        /// <summary>
        /// Permet d'executer une fonction du processView en passant des paramêtres
        /// </summary>
        /// <param name="parameters">Liste de paramêtre à traiter</param>
        /// <param name="command">Nom de la fonction à appeler dans le processView</param>
        /// <returns>La vue avec l'ensemble des informations</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public virtual TView ExecuteCommandFormObject(TInputObject parameters, string command)
        {
            //List<Parameters> parameter = JsonUtility.Deserialize<List<Parameters>>parameters);

            TView view = new TView();

            IPresenter controler = (IPresenter)Activator.CreateInstance(typeof(TPresenterLogic), view);
            controler.Start(IsAutoInitialDisplay);

            //envoie la commande
            controler.ExecuteCommand(command, parameters);

            return (view);
        }

    }
}