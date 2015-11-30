using System.Collections.Specialized;
using MKS.Core;
using MKS.Core.Presentation;
using System.Collections.Generic;

namespace MKS.Core.Presentation
{
    //Definition des événements pour les actions utilisateur
    public delegate void EventCommand(string commmand, CommandEventArgsCustom e);


    /// <summary>
    ///     Interface utile au PresenterLogic pour les appels aux fonctionnalitées de base.
    /// </summary>
    public interface IViewBase
    {
        ViewData ViewLogics { get; }
        /// <summary>
        /// Liste des associations des validations entre l'objet de la structure d'information et l'objet vue de l'interface utilisateur
        /// </summary>        
        UIValidations Validations { get; set; }
        event EventCommand OnCommand;

        /// <summary>
        ///     Est invoqué lorsqu'un erreur .NET soit Exception ou FaultException survient, automatiquement par le PresenterLogic.
        /// </summary>
        /// <param name="message"> Message d'erreur </param>
        /// <param name="msgSeveriry"> Severité de l'erreur. </param>
        /// <remarks>
        ///     Cette méthode peut être utilisé àa d'autre fin que par le présenter Factory.
        ///     Cette méthode peut être appelé en tout temps
        /// </remarks>
        void ShowMessage(string titre, string message, Severity msgSeveriry);

        /// <summary>
        ///     Affiche les message de validation d'intégrité à l'interface utilisateur.
        ///     Est invoqué lorsque'un erreur de validation d'intégrité ou d'affaire survient. Est appelé directement par le
        ///     presenteur Factory.
        /// </summary>
        /// <param name="formatedMessage"> Message d'erreur préformaté </param>
        /// <param name="rules"> Liste des erreurs non formaté </param>
        /// <param name="titre"> Titre du message </param>
        /// <remarks>
        ///     Cette méthode peut être utilisé à d'autre fin que par la logique de présentation.
        ///     Cette méthode peut être appelé en tout temps
        /// </remarks>                        
        void ShowValidationMessage(string titre, string formatedMessage, RuleResults rules);
        /// <summary>
        /// Affiche les messages lors d'un erreur d'accès conccurent
        /// </summary>
        /// <param name="message">Message à afficher</param>
        /// <remarks>Cette fonction est déclanché lorsque la couche métier identifie un accès concurrent sur une ressource</remarks>
        void ShowConcurrencyMessage(string message);
        /// <summary>
        /// Affiche les erreurs métier à l'interface utilisateur
        /// </summary>
        /// <param name="titre">Titre du messgae</param>
        /// <param name="rules">Règles métier en erreur</param>
        /// <remarks>Cette fonction est déclanché lorsqu'un erreur de validation métier survient</remarks>
        void ShowBusinessValidationMessage(string titre, ProcessResults rules);

      
        /// <summary>
        ///     Permet de faire une action sur une non authorization au système
        /// </summary>
        /// <param name="param">
        ///     Parametre utile pour un urls de redirection ou autre selon la technologie de l'interface
        ///     utilisateur
        /// </param>
        void SetSecurityNoAccess(string param);

        /// <summary>
        ///     Est invoqué lors de la création du présenteur pour préparer l'interface utilisateur à un état initialle.
        /// </summary>
        void InitializeDisplay();

        /// <summary>
        ///     Est invoqué, lors de la création du présenteur pour permettre à l'interface utilisateur d'afficher les informations
        ///     sur le status du système.
        /// </summary>
        /// <param name="status"> Dictionnaire des différents status fournis par la couche métier. </param>
        void ShowSystemStatus(Dictionary<string, string> status);

        void ExecuteCommand(string command, CommandEventArgsCustom args);
        void ExecuteCommand(string command);
        void Navigate(string routeKey, params object[] param);
        void SaveSessionManager(string key, object sessionObject);
        object GetSessionManager(string key);
        bool IsRichClient();
        void HideValidationMessage();
    }
}