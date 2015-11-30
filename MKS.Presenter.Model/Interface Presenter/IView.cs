using MKS.Core;
using MKS.Core.Presenter.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Ensemble des fonctions selon l'implémentation des processus de de vue.
//Ces interfaces soivent être implémenté par l'interface utilisateur en fonction de la technologies utilisé
//Exemple : Application Web, la page de base implémente ses interfaces pour permttre aux processus de vue d'interagir avec l'UI.
namespace MKS.Core.Presenter.Interfaces
{
    public delegate void CommandAction(string commmand, CommandEventArgsCustom e);

    public interface IView
    {
        //Garde en mémoire l'ensemble des inforamtions utile à l'interfaces UI. Peut-être utilisé en MVC.
        ViewData ViewLogics { get; }

        //Pour l'assignation des validations de contexte sur la vue 
        UIValidations Validations { get; set; }

        //permet à l'interface d'envoyer démarrer les opérations dans les processus de vue. Exemple : une commande sur un action d'un boutton dans une interface.
        event CommandAction OnCommand;

        void Navigate(string routeKey, params object[] param);
        void SaveSession(string key, object sessionObject);
        object GetSession(string key);
        void HideMessage();

        void ShowContextValidation(string title, string message, List<ReturnMessage> result);
        void ShowBusinessValidation(string title, string message, ProcessResults processResults);
        void ShowMessage(string title, string message, Severity severity);
        
        //Meme si les processus de vue ne gere pas la securité ou la reservation, les messages peuvent surgirent de la couche métier.
        void ShowSecurity(string title, string message, ProcessResults processResults);
        void ShowReservation(string title, string message, ProcessResults processResults);
    }

    

}
