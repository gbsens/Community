using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MKS.Core
{
    /// <summary>
    ///     Classe pour l'objet de retour d'un DoReservation
    /// </summary>
    [DataContract]
    public class ConcurrencyResult : ProcessResults
    {
        /// <summary>
        ///     Constructeur
        /// </summary>
        /// <param name="lstMessages"> Liste de messages à ajouter </param>
        /// <param name="result"> Le résultat de l'opération </param>
        public ConcurrencyResult(IEnumerable<ReturnMessage> lstMessages, bool result)
        {
            if (lstMessages != null)
            {
                foreach (var message in lstMessages)
                {
                    AddException(message);
                }
                Success = result;
            }
        }

        /// <summary>
        ///     Success
        /// </summary>
        [DataMember]
        public bool Success { get; set; }
    }
}