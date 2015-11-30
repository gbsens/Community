using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MKS.Core.Activity
{
    [DataContract]
    public class ActivityLocal : IActivity
    {
        #region Membres

        [DataMember(Name = "SubActivities")]
        public List<IActivityDetail> SubActivities { get; set; }

        [DataMember(Name = "EventDate")]
        public DateTime EventDate { get; set; }

        [DataMember(Name = "DomaineName")]
        public string DomaineName { get; set; }

        [DataMember(Name = "ApplicationCode")]
        public string ApplicationCode { get; set; }

        [DataMember(Name = "ActivityID")]
        public int ActivityID { get; set; }

        [DataMember(Name = "UserCode")]
        public string UserCode { get; set; }

        [DataMember(Name = "MachineName")]
        public string MachineName { get; set; }

        [DataMember(Name = "ActionCode")]
        public string ActionCode { get; set; }

        #endregion Membres

        #region Constructeurs

        /// <summary>
        /// Constructeurs par défaut
        /// </summary>
        public ActivityLocal()
        {
            SubActivities = new List<IActivityDetail>();
            EventDate = DateTime.Now;
            ApplicationCode = Globals.GetUserEnvironment.GetCurrentSystemCode();
            DomaineName = Globals.GetUserEnvironment.GetUserDomainName();
            MachineName = Globals.GetUserEnvironment.GetMachineName();
            UserCode = Globals.GetUserEnvironment.GetUserCode();
            EventDate = DateTime.Now;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="applicationCode">Code de l'action</param>
        /// <param name="domaineName">Nom du domaine</param>
        /// <param name="actionCode">Code de l'action</param>
        /// <param name="machineName">Nom de la machine</param>
        /// <param name="userCode">Code de l'usager</param>
        public ActivityLocal(string applicationCode, string domaineName, string actionCode,
                     string machineName, string userCode)
        {
            ApplicationCode = applicationCode;
            DomaineName = domaineName;
            ActionCode = actionCode;
            MachineName = machineName;
            UserCode = userCode;
            EventDate = DateTime.Now;
            SubActivities = new List<IActivityDetail>();
        }

        public ActivityLocal(string applicationCode)
        {
            ApplicationCode = applicationCode;

            EventDate = DateTime.Now;
            SubActivities = new List<IActivityDetail>();
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="applicationCode">Code de l'action</param>
        /// <param name="domaineName">Nom du domaine</param>
        /// <param name="actionCode">Code de l'action</param>
        /// <param name="machineName">Nom de la machine</param>
        /// <param name="userCode">Code de l'usager</param>
        /// <param name="eventDate">Date de l'événement</param>
        public ActivityLocal(string applicationCode, string domaineName, string actionCode,
                     string machineName, string userCode, DateTime eventDate)
        {
            ApplicationCode = applicationCode;
            DomaineName = domaineName;
            ActionCode = actionCode;
            MachineName = machineName;
            UserCode = userCode;
            EventDate = eventDate;
            SubActivities = new List<IActivityDetail>();
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="activityId">ID de l'evenement</param>
        /// <param name="applicationCode">Code de l'action</param>
        /// <param name="domaineName">Nom du domaine</param>
        /// <param name="actionCode">Code de l'action</param>
        /// <param name="machineName">Nom de la machine</param>
        /// <param name="userCode">Code de l'usager</param>
        /// <param name="eventDate">Date de l'evenement</param>
        public ActivityLocal(int activityId, string applicationCode, string domaineName, string actionCode,
                     string machineName, string userCode, DateTime eventDate)
        {
            ActivityID = activityId;
            ApplicationCode = applicationCode;
            DomaineName = domaineName;
            ActionCode = actionCode;
            MachineName = machineName;
            UserCode = userCode;
            EventDate = eventDate;
            SubActivities = new List<IActivityDetail>();
        }

        /// <summary>
        /// Ajout d'un sous événement
        /// </summary>
        /// <param name="componentName">Nom du composant</param>
        /// <param name="actionCode">Code de l'action</param>
        /// <param name="entityCode">Code de l'entité</param>
        /// <param name="attributCode">Code de l'attribut</param>
        /// <param name="valueBefore">Valeur avant les modifications</param>
        /// <param name="valueAfter">Valeur après les modifications</param>
        public void AddSubActivity(string componentName, string actionCode,
                                string entityCode, string attributCode,
                                string valueBefore, string valueAfter)
        {
            SubActivities.Add(new ActivityDetailLocal(componentName,
                                       actionCode,
                                       entityCode,
                                       attributCode,
                                       valueBefore,
                                       valueAfter));
        }

        /// <summary>
        /// Ajout d'un sous événement
        /// </summary>
        /// <param name="componentName">Nom du composant</param>
        /// <param name="actionCode">Code de l'action</param>
        /// <param name="entityCode">Code de l'entité</param>
        /// <param name="attributCode">Code de l'attribut</param>
        /// <param name="valueBefore">Valeur avant les modifications</param>
        /// <param name="valueAfter">Valeur après les modifications</param>
        /// <param name="description">Informations du sous événement</param>
        public void AddSubActivity(string componentName, string actionCode,
                                string entityCode, string attributCode,
                                string valueBefore, string valueAfter, string description)
        {
            SubActivities.Add(new ActivityDetailLocal(componentName,
                                       actionCode,
                                       entityCode,
                                       attributCode,
                                       valueBefore,
                                       valueAfter,
                                       description));
        }

        /// <summary>
        /// Ajout d'un sous événement
        /// </summary>
        /// <param name="componentName">Nom du composant</param>
        /// <param name="actionCode">Code de l'action</param>
        /// <param name="entityCode">Code de l'entité</param>
        public void AddSubActivity(string componentName, string actionCode, string entityCode)
        {
            SubActivities.Add(new ActivityDetailLocal(componentName,
                                       actionCode,
                                       entityCode,
                                       string.Empty,
                                       string.Empty,
                                       string.Empty,
                                       string.Empty));
        }

        #endregion Constructeurs
    }
}