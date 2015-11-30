using System.Runtime.Serialization;

namespace MKS.Core
{
    /// <summary>
    ///     Enum contenant le dégré de sévérité d'une exception
    /// </summary>
    [DataContract]
    public enum Severity
    {
        /// <summary>
        ///     Indique une erreur sérieuse
        ///     d'intégrité de la règle pouvant
        ///     rendre l'objet invalide.
        /// </summary>
        [EnumMember] Error,

        /// <summary>
        ///     Indique une erreur d'importance
        ///     moyenne qui doit être rapportée
        ///     à l'utilisateur, mais qui ne
        ///     devrait pas rendre un objet
        ///     invalide.
        /// </summary>
        [EnumMember] Warning,

        /// <summary>
        ///     Indique un résultat de règle
        ///     d'importance minime mais qui doit
        ///     néanmoins être affichée à l'utilisateur.
        /// </summary>
        [EnumMember] Information,

        /// <summary>
        ///     Indique in message de succès. Est utilisé pour afficher un message de success.
        /// </summary>
        [EnumMember] Success
    }
}