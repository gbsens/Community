namespace MKS.Core.Security
{
    public interface IPermission
    {
        /// <summary>
        ///     Identifiant unique de la permission
        /// </summary>
        int? Id { get; set; }

        /// <summary>
        ///     Titre de la permission
        /// </summary>
        string Code { get; set; }

        /// <summary>
        ///     Informations de la permission
        /// </summary>
        string Description { get; set; }
    }
}