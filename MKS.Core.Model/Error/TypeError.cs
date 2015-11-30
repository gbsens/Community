using System.Runtime.Serialization;

namespace MKS.Core
{
    /// <summary>
    ///     Enum des types d'erreurs disponibles dans le Framework MKS
    /// </summary>
    [DataContract]
    public enum TypeError
    {
        /// <summary>
        ///     Erreur lors d'une validation sur l'intégrité de l'objet
        /// </summary>
        [EnumMember] ValidationObjet,

        /// <summary>
        ///     Erreur lors d'une validation lors d'un traitement d'affaires
        /// </summary>
        [EnumMember] ValidationBusiness,

        /// <summary>
        ///     Exception non gérée
        /// </summary>
        [EnumMember] Exception,

        /// <summary>
        ///     Erreur lors de la validation de la sécurité
        /// </summary>
        [EnumMember] Security,

        /// <summary>
        ///     Erreur de concurrence
        /// </summary>
        [EnumMember] Concurrence
    }
}