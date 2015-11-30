using System;
using System.ServiceModel;
using MKS.Core;
using MKS.Core.Security;
using MKS.Presenter.UI;
using MKS.Core.Model.Error;

namespace MKS.Core.Presentation
{
    public enum AssignProperty
    {
        Enabled,
        Visible
    }

    /// <summary>
    ///     Interface pour pour le PresenterLogic
    /// </summary>
    public interface IPresenterBase
    {
        /// <summary>
        /// Contien la liste des authorisations du système
        /// </summary>
        IApplicationAuthorization Authorizations { get; set; }
        /// <summary>
        /// Permet de publier un erreur de type Exception à l'interface utilisateur
        /// </summary>
        /// <param name="ex">Exception a publier à l'interface utilisateur</param>
        void PublishError(Exception ex);
        /// <summary>
        /// Permet de publier un erreur de type FaultException à l'interface utilisateur
        /// </summary>
        /// <param name="ex">FaultException a publier à l'interface utilisateur</param>
        void PublishError(FaultException ex);
        /// <summary>
        /// Permet de publier un erreur de validation d'intégrité de type ExceptionProcess<ProcessResults>  à l'interface utilisateur
        /// </summary>
        /// <param name="ex">FaultException a publier à l'interface utilisateur</param>        
        /// <remarks>En générale les erreurs ExceptionProcess<ProcessResults>  proviennent de la couche métier lors de la validation d'intégrité</remarks>
        void PublishValidations(ExceptionProcess<ProcessResults>  ex);
        void PublishValidations(FaultException<ProcessResults> ex);
        /// <summary>
        /// Permet de publier un erreur de validation d'intégrité de type RuleResults à l'interface utilisateur
        /// </summary>
        /// <param name="rules">RuleResults a publier à l'interface utilisateur</param>        
        void PublishValidations(RuleResults rules);
        /// <summary>
        /// Lance les évenements de l'interface utilisateur à la couche de présentation
        /// </summary>
        /// <param name="command">Commande à exécuter</param>
        /// <remarks>Généralement les commandes sont utiliser pour démarrer les fonctions de la couche de présentation</remarks>
        void ExecuteCommand(string command);
        /// <summary>
        /// Lance les évenements de l'interface utilisateur à la couche de présentation
        /// </summary>
        /// <param name="command">Commande à exécuter</param>
        /// <param name="args">Argument de la commande</param>
        /// <remarks>Généralement les commandes sont utiliser pour démarrer les fonctions de la couche de présentation</remarks>
        void ExecuteCommand(string command, CommandEventArgsCustom args);
        /// <summary>
        /// Lance les évenements de l'interface utilisateur à la couche de présentation
        /// </summary>
        /// <param name="TInputParameter">Classe de parametre</param>
        /// <param name="command">Commande à exécuter</param>
        /// <param name="args">Argument de la commande en fonction de la classe de parametre</param>
        /// <remarks>Généralement les commandes sont utiliser pour démarrer les fonctions de la couche de présentation</remarks>        
        void ExecuteCommand<TInputParameter>(string command, TInputParameter args);
        /// <summary>
        /// Sauvegarde un objet en session
        /// </summary>
        /// <param name="key">Clé</param>
        /// <param name="sessionObject">Objet à sauvegarder</param>
        /// <remarks>Les sessions sont persisté en fonction des tehchnologies d'interface utilisateur</remarks>
        void SaveSession(string key, object sessionObject);
        /// <summary>
        /// Retrouve un objet de session
        /// </summary>
        /// <param name="key">Clé unique sur l'objet de session a retrouver</param>
        /// <returns>Objet trouvé</returns>
        /// <remarks>Les sessions sont persisté en fonction des tehchnologies d'interface utilisateur</remarks>
        object GetSession(string key);
        /// <summary>
        /// Permet de faire une validation d'intégrité sur un ensemble d'objet
        /// </summary>
        /// <param name="sucessMessage">Message à afficher sur validation réussi</param>
        /// <param name="objectInstance">Les objets à valider</param>
        /// <returns>RuleResults des validations en erreur</returns>
        /// <remarks>La validation coté logique de présentation dépend de la définition de l'association, généralement défini dans le InitialDisplay</remarks>
        RuleResults Validate(string sucessMessage, params object[] objectInstance);
        /// <summary>
        /// Assigne une permission à un control d'interface
        /// </summary>
        /// <param name="permissionCode">Code de permission a assigner</param>
        /// <param name="control">Control d'interface affecter par la permission</param>
        /// <param name="property">Quel propriété doit être affecté par la permission Enabled ou Visible</param>
        /// <returns></returns>
        Permission AssignPermission(string permissionCode, UIBase control, AssignProperty property);
    }

    /// <summary>
    ///     Interface pour l'appluication UI.
    /// </summary>
    public interface IPresenter : IPresenterBase
    {
        void Start();
        void Start(bool useInitialDisplay);
    }
}