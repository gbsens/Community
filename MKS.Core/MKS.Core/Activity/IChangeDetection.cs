namespace MKS.Core.Activity
{
    public interface IChangeDetection
    {
    }

    /// <summary>
    ///   Interface permetant de détecter les changements sur un objet.
    /// </summary>
    /// <typeparam name="TObject"> </typeparam>
    public interface IChangeDetection<in TObject> : IChangeDetection
    {
        /// <summary>
        ///   Fonction utilisée pour obtenir la liste des changements de deux objets. Cette liste est encapsulée dans un objet de type DetectChanges.
        /// </summary>
        /// <param name="updatedObject"> L'objet qui contient les modifications </param>
        /// <param name="oldObject"> L'objet avant les modifications </param>
        /// <returns> Objet de type DetectChanges qui contient la liste des modifications. </returns>
        ChangeDetections GetPropertiesAltered(TObject updatedObject, TObject oldObject);
    }
}