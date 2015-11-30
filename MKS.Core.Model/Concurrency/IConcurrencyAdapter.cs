namespace MKS.Core
{
    /// <summary>
    ///     Interface pour les classes de réservation. Ces classes de réservation sont responsables
    ///     de la réservation et de la déréservation d'un élément.
    ///     <para> L'appel au service de réservation est de la responsabilité des classes de réservation. </para>
    /// </summary>
    public interface IConcurrencyAdapter
    {
        /// <summary>
        ///     Effectue la réservation d'un workspace.
        /// </summary>
        /// <param name="obj"> Objet contenant les informations de réservation. </param>
        /// <returns> Retourne un objet qui contient l'état de la réservation et les messages retournés s'il y à lieu. </returns>
        ConcurrencyResult DoReservationWorkSpace(LogicalLock obj);

        /// <summary>
        ///     Effectue la réservation d'une occurence.
        /// </summary>
        /// <param name="obj"> Objet contenant les informations de réservation. </param>
        /// <returns> Retourne un objet qui contient l'état de la réservation et les messages retournés s'il y à lieu. </returns>
        ConcurrencyResult DoReservationOccurence(LogicalLock obj);

        /// <summary>
        ///     Termine la réservation d'un workspace.
        /// </summary>
        /// <param name="obj"> Objet contenant les informations de réservation. </param>
        /// <returns> Retourne un objet qui contient l'état de l'opération et les messages retournés s'il y à lieu. </returns>
        ConcurrencyResult EndReservationWorkSpace(LogicalLock obj);

        /// <summary>
        ///     Termine la réservation d'une occurence.
        /// </summary>
        /// <param name="obj"> Objet contenant les informations de réservation. </param>
        /// <returns> Retourne un objet qui contient l'état de l'opération et les messages retournés s'il y à lieu. </returns>
        ConcurrencyResult EndReservationOccurence(LogicalLock obj);

        /// <summary>
        ///     Termine toutes les réservation associées à une session.
        /// </summary>
        /// <returns> Retourne un objet qui contient l'état de l'opération et les messages retournés s'il y à lieu. </returns>
        ConcurrencyResult EndSessionReservations();

        /// <summary>
        ///     Vérifie l'existence d'une réservation sur un workspace.
        /// </summary>
        /// <param name="p_keyObject"> Objet de réservation utilisé pour la recherche. </param>
        /// <returns> Retourne un booléen indiquant si le workspace est réservé ou non. </returns>
        bool CheckReservationWorkSpaceExistence(LogicalLock p_keyObject);

        /// <summary>
        ///     Vérifie l'existence d'une réservation sur une occurence.
        /// </summary>
        /// <param name="p_keyObject"> Objet de réservation utilisé pour la recherche. </param>
        /// <returns> Retourne un booléen indiquant si le workspace est réservé ou non. </returns>
        bool CheckReservationOccurenceExistence(LogicalLock p_keyObject);

        /// <summary>
        ///     Obtient les informations associées à la réservation d'un workspace.
        /// </summary>
        /// <param name="p_keyObject"> Objet de réservation utilisé pour la recherche. </param>
        /// <returns> Retourne la réservation trouvée. </returns>
        LogicalLock GetReservationWorkSpaceDetails(LogicalLock p_keyObject);

        /// <summary>
        ///     Obtient les informations associées à la réservation d'une occurence.
        /// </summary>
        /// <param name="p_keyObject"> Objet de réservation utilisé pour la recherche. </param>
        /// <returns> Retourne la réservation trouvée. </returns>
        LogicalLock GetReservationOccurenceDetails(LogicalLock p_keyObject);
    }
}