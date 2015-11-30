using System;
using System.Collections.Generic;

namespace MKS.Core.Activity
{
    public interface IActivity
    {
        /// <summary>
        ///     ID de l'événement.
        /// </summary>
        int ActivityID { get; set; }

        /// <summary>
        ///     Code de l'application.
        /// </summary>
        string ApplicationCode { get; set; }

        /// <summary>
        ///     Nom du domaine.
        /// </summary>
        string DomaineName { get; set; }

        /// <summary>
        ///     Code de l'action utilisateur.
        /// </summary>
        string ActionCode { get; set; }

        /// <summary>
        ///     Nom de la machine.
        /// </summary>
        string MachineName { get; set; }

        /// <summary>
        ///     Code de l'usager.
        /// </summary>
        string UserCode { get; set; }

        /// <summary>
        ///     Date de l'événement.
        /// </summary>
        DateTime EventDate { get; set; }

        /// <summary>
        ///     Liste des sous-événements.
        /// </summary>
        List<IActivityDetail> SubActivities { get; set; }

        /// <summary>
        ///     Ajout d'un sous événement.
        /// </summary>
        /// <param name="componentName"> Nom du composant </param>
        /// <param name="actionCode"> Code de l'action </param>
        /// <param name="entityCode"> Code de l'entité </param>
        /// <param name="attributeCode"> Code de l'attribut </param>
        /// <param name="valueBefore"> Valeur avant les modifications </param>
        /// <param name="valueAfter"> Valeur après les modifications </param>
        void AddSubActivity(string componentName, string actionCode,
            string entityCode, string attributeCode,
            string valueBefore, string valueAfter);

        /// <summary>
        ///     Ajout d'un sous événement.
        /// </summary>
        /// <param name="componentName"> Nom du composant </param>
        /// <param name="actionCode"> Code de l'action </param>
        /// <param name="entityCode"> Code de l'entité </param>
        /// <param name="attributeCode"> Code de l'attribut </param>
        /// <param name="valueBefore"> Valeur avant les modifications </param>
        /// <param name="valueAfter"> Valeur après les modifications </param>
        /// <param name="description"> Informations du sous événement </param>
        void AddSubActivity(string componentName, string actionCode,
            string entityCode, string attributeCode,
            string valueBefore, string valueAfter, string description);

        /// <summary>
        ///     Ajout d'un sous événement.
        /// </summary>
        /// <param name="componentName"> Nom du composant </param>
        /// <param name="actionCode"> Code de l'action </param>
        /// <param name="entityCode"> Code de l'entité </param>
        void AddSubActivity(string componentName, string actionCode, string entityCode);
    }
}