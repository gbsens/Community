namespace MKS.Core.Activity
{
    public interface IActivityDetail
    {
        /// <summary>
        ///     Id du sous événement, cet id est généralement fournis par la base de données
        /// </summary>
        int DetailId { get; set; }

        /// <summary>
        ///     Id de l'événement, cet id est généralement fournis par la base de données
        /// </summary>
        int ActivityId { get; set; }

        /// <summary>
        ///     Nom du composant. En générale c'est le nom de la classe d'affaire.
        /// </summary>
        string ComponentName { get; set; }

        /// <summary>
        ///     Le code d'action indique l'action fait sur l'objet.
        /// </summary>
        string ActionCode { get; set; }

        /// <summary>
        ///     Code de l'entité. En générale c'est le nom de l'objet.
        /// </summary>
        string EntityCode { get; set; }

        /// <summary>
        ///     Code de l'attribut. En générale c'est le nom da propriété de l'objet
        /// </summary>
        string AttributeCode { get; set; }

        /// <summary>
        ///     Valeur avant les modifications. En général c'est la valeur de la propriété de l'objet avant sa modification.
        /// </summary>
        string ValueBefore { get; set; }

        /// <summary>
        ///     Valeur après les modifications. En général c'est la valeur de la propriété de l'objet après sa modification.
        /// </summary>
        string ValueAfter { get; set; }

        /// <summary>
        ///     Valeur après les modifications. En générale, c'est une information supplémentaire facultative.
        /// </summary>
        string Description { get; set; }
    }
}