using System;
using System.Runtime.Serialization;

namespace MKS.Core
{
    /// <summary>
    ///     Énumeration utilisée pour identifier l'étape à laquelle l'appel à l'interface de réservation est effectué.
    /// </summary>
    public enum ConcurrencyStep
    {
        /// <summary>
        ///     Indique que l'appel est effectué avant la transaction
        /// </summary>
        BEFORE_PROCESS,

        /// <summary>
        ///     Indique que l'appel est effectué après la transaction
        /// </summary>
        AFTER_PROCESS
    }

    /// <summary>
    ///     Objet de réservation pour la gestion de la concurrence.  Cette classe fourni les
    ///     informations relatives aux données réservées ainsi qu'à l'utilisateur ayant
    ///     réservé ces données.
    /// </summary>
    [Serializable]
    public class LogicalLock
    {
        /// <summary>
        ///     Constructeur
        /// </summary>
        public LogicalLock()
        {
        }

        /// <summary>
        ///     Constructeur
        /// </summary>
        /// <param name="p_workSpace">Identifiant de l'espace de travail</param>
        public LogicalLock(string workSpace)
            : this(workSpace, null, 0)
        {
        }

        /// <summary>
        ///     Constructeur
        /// </summary>
        /// <param name="workSpace">Identifiant de l'espace de travail</param>
        /// <param name="duration">Durée avant que la réservation ne se termine.</param>
        public LogicalLock(string workSpace,
            int duration)
            : this(workSpace, null, duration)
        {
        }

        /// <summary>
        ///     Contructeur
        /// </summary>
        /// <param name="workSpace">Identifiant de l'espace de travail</param>
        /// <param name="occurrenceId">Identifiant de l'occurrence. Il est optionnel.</param>
        /// <param name="duration">Durée avant que la réservation ne se termine.</param>
        public LogicalLock(string workSpace,
            string occurrenceId)
            : this(workSpace, occurrenceId, 0)
        {
            WorkSpace = workSpace;
            OccurenceId = occurrenceId;
        }

        /// <summary>
        ///     Contructeur
        /// </summary>
        /// <param name="workSpace"> Identifiant de l'espace de travail </param>
        /// <param name="occurrenceId"> Identifiant de l'occurrence. Il est optionnel. </param>
        /// <param name="duration"> Durée avant que la réservation ne se termine. </param>
        public LogicalLock(string workSpace,
            string occurrenceId,
            int duration)
        {
            Duration = duration;
            WorkSpace = workSpace;
            OccurenceId = occurrenceId;
        }

        /// <summary>
        ///     Contructeur
        /// </summary>
        /// <param name="concurrencyId"> ConcurrencyId </param>
        /// <param name="applicationCode"> Code de l'application </param>
        /// <param name="userName"> Nom d'utilisateur </param>
        /// <param name="sessionId"> Identifiant de la session </param>
        /// <param name="occurrenceId"> Identifiant de l'occurrence </param>
        /// <param name="workSpace"> Identifiant de l'espace de travail pour identifier le vérrou </param>
        /// <param name="expirationDate"> Date d'expiration </param>
        /// <param name="duration"> Durée </param>
        public LogicalLock(string workSpace,
            string occurrenceId,
            int concurrencyId,
            int duration,
            string applicationCode,
            string userName,
            string sessionId,
            DateTime expirationDate
            )
        {
            ConcurrencyId = concurrencyId;
            ApplicationCode = applicationCode;
            ExpirationDate = expirationDate;
            Duration = duration;
            WorkSpace = workSpace;
            OccurenceId = occurrenceId;
            UserCode = userName;
            SessionId = sessionId;
        }

        /// <summary>
        ///     ID de la réservation
        /// </summary>
        [DataMember]
        public int ConcurrencyId { get; set; }

        /// <summary>
        ///     Code de l'application qui a réservé l'occurrence.
        /// </summary>
        [DataMember]
        public string ApplicationCode { get; set; }

        /// <summary>
        ///     Identification de l'utilisateur qui détient le reservation
        /// </summary>
        [DataMember]
        public string UserCode { get; set; }

        /// <summary>
        ///     Identification de la session
        /// </summary>
        [DataMember]
        public string SessionId { get; set; }

        /// <summary>
        ///     Identification de l'Occurrence de l'espace de travail.
        /// </summary>
        [DataMember]
        public string OccurenceId { get; set; }

        /// <summary>
        ///     Identification de l'espace de travail pour identifier le vérrou
        /// </summary>
        [DataMember]
        public string WorkSpace { get; set; }

        /// <summary>
        ///     Date de l'expiration
        /// </summary>
        [DataMember]
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        ///     Durée de la réservation. Par défaut la durée est de 1 heure (60000 ms)
        /// </summary>
        [DataMember]
        public int Duration { get; set; }
    }
}