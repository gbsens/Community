using MKS.Core.Activity;

namespace MKS.Core.Business.Interfaces
{
    public interface IBusinessObject
    {
        /// <summary>
        ///   Objet d'événement en cours d'instance
        /// </summary>
        /// <remarks>
        ///   Cet objet peut être à Null, s'il n'y a pas de classe de journalisation d'associée
        /// </remarks>
        IActivity Activity { get; set; }

        /// <summary>
        ///   Donne la liste des résultats de processus d'affaire en cours
        /// </summary>
        ProcessResults ProcessResults { get; set; }
    }
}