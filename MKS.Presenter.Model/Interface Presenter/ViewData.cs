using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using MKS.Core;
using MKS.Core.Presenter;
using MKS.Core.Presenter.UI;

namespace MKS.Core.Presenter
{
    /// <summary>
    ///     Cette objet permet de contenir l'ensemble des informations utile à la couche UI. Elle est incluse dans le ViewBase.
    /// </summary>
    public class ViewData
    {
        public enum PropertyDisplay
        {
            Visible,
            Enabled
        }

        /// <summary>
        ///     Contient l'ensemble des validations d'affaire en erreur.
        /// </summary>
        public ProcessResults BusinessMessages { get; set; }
        public ProcessResults SecurityMessages { get; set; }

        /// <summary>
        ///     Contient l'ensemble des messages à transmettre à l'interface UI
        /// </summary>
        public Tuple<string, string, Severity> Messages { get; set; }

        /// <summary>
        ///     Contient l'ensemble des validation d'intégrité en erreur.
        /// </summary>
        //OLD public Tuple<string, List<ReturnMessage>, List<LinkObjectView>> ContextValidation { get; set; }
        public List<ReturnMessage> ContextValidationMessage { get; set; }

        /// <summary>
        ///     Contient l'ensemble des erreurs de réservation
        /// </summary>
        public ProcessResults ReservationMessages { get; set; }

        public UIValidations Validations { get; set; }

        /// <summary>
        ///     Permet de controler les propriete d'une vue pour l'interface au niveau de la securité.
        /// </summary>
        public List<Tuple<bool, string>> SecurityForms { get; set; }

        /// <summary>
        ///     Contien la liste des permission que l'utilisateur a droit on non.
        /// </summary>
        public List<Tuple<bool, string[]>> SecurityPermission { get; set; }

        /// <summary>
        ///     Permet de controler les proprietes d'un objet pour l'interface.
        /// </summary>
        public List<Tuple<bool, PropertyDisplay, string>> ControlEnabled { get; set; }

        public Dictionary<string, string> SystemStatus { get; set; }
        public Tuple<string, object[]> GoForm { get; set; }
        public Tuple<string, object> Session { get; set; }
        public Tuple<string, object> ApplicationSession { get; set; }
        public bool ClearMessages { get; set; }
        public string AccessDenied { get; set; }
    }
}