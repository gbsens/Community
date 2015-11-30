using System;
using System.Collections.Generic;

namespace MKS.Core.Activity
{
    /// <summary>
    ///   Classe permettant de comparer les propriétés de deux objets de même type.
    ///   Cette classe possède une liste de MKS.Core.DetectChange qui permet d'encapsuler les informations des changements faits à une entité
    /// </summary>
    public class ChangeDetections
    {
        private readonly List<ChangeDetection> _changes = new List<ChangeDetection>();

        /// <summary>
        ///   Liste qui encapsule les informations sur les propriétés qui ont été modifiées
        /// </summary>
        public List<ChangeDetection> Changes
        {
            get { return _changes; }
        }

        /// <summary>
        ///   Cette fonction compare deux objets de même type.
        ///   Les objets reçus en paramètre doivent correspondent à une propriété d'une entité
        /// </summary>
        /// <param name="updatedObject"> Valeur de l'objet après les modifications </param>
        /// <param name="oldObject"> Valeur de l'objet avant les modifications </param>
        /// <param name="type"> Type de l'objet dont les propriéts sont comparées </param>
        /// <param name="propertyName"> Nom de la propriété </param>
        /// <returns> booléen indiquant si la valeurs à été modifié </returns>
        public bool ComparePropertyValue(Object updatedObject, Object oldObject, Type type, string propertyName)
        {
            if (IsPropertyChanged(updatedObject, oldObject))
            {
                _changes.Add(new ChangeDetection(updatedObject, oldObject, type, propertyName));
                return true;
            }
            return false;
        }

        /// <summary>
        ///   Cette fonction compare deux objets de même type.
        ///   Les objets reçus en paramètre doivent correspondent à une propriété d'une entité
        /// </summary>
        /// <param name="updatedObject"> Valeur de l'objet après les modifications </param>
        /// <param name="oldObject"> Valeur de l'objet avant les modifications </param>
        /// <param name="type"> Type de l'objet dont les propriéts sont comparées </param>
        /// <param name="propertyName"> Nom de la propriété </param>
        /// <returns> booléen indiquant si la valeurs à été modifié </returns>
        public bool ComparePropertyValue(Object updatedObject, Object oldObject, Type type, string propertyName, string key)
        {
            if (IsPropertyChanged(updatedObject, oldObject))
            {
                _changes.Add(new ChangeDetection(updatedObject, oldObject, type, propertyName, key));
                return true;
            }
            return false;
        }

        /// <summary>
        ///   Permet de passer un objet et de comparer selon la propriete p_propertyName si un changement a été détecté
        /// </summary>
        /// <param name="updatedObject"> Valeur de l'objet après les modifications </param>
        /// <param name="oldObject"> Valeur de l'objet avant les modifications </param>
        /// <param name="type"> Type de l'objet dont les propriéts sont comparées </param>
        /// <param name="propertyName"> Nom de la propriété </param>
        /// <returns> booléen indiquant si la valeurs à été modifié </returns>
        public bool CompareObjectProperty(Object updatedObject, Object oldObject, Type type, string propertyName)
        {
            object newvalue = Utilities.CallByName(updatedObject, propertyName, CallType.Get);
            object oldvalue = Utilities.CallByName(oldObject, propertyName, CallType.Get);
            return (ComparePropertyValue(newvalue, oldvalue, type, propertyName));
        }

        /// <summary>
        ///   Permet de passer un objet et de comparer selon la propriete p_propertyName si un changement a été détecté
        /// </summary>
        /// <param name="updatedObject"> Valeur de l'objet après les modifications </param>
        /// <param name="oldObject"> Valeur de l'objet avant les modifications </param>
        /// <param name="type"> Type de l'objet dont les propriéts sont comparées </param>
        /// <param name="propertyName"> Nom de la propriété </param>
        /// <returns> booléen indiquant si la valeurs à été modifié </returns>
        public bool CompareObjectProperty(Object updatedObject, Object oldObject, Type type, string propertyName, string key)
        {
            object newvalue = Utilities.CallByName(updatedObject, propertyName, CallType.Get);
            object oldvalue = Utilities.CallByName(oldObject, propertyName, CallType.Get);
            return (ComparePropertyValue(newvalue, oldvalue, type, propertyName, key));
        }

        /// <summary>
        ///   Retourne une valeur booléenne indiquant si la valeur de updatedObject est identique à oldObjectFromDB
        ///   Les valeurs passées en paramètre ne peuvent être des objets complexes
        /// </summary>
        /// <param name="updatedObject"> Valeur avant les changements </param>
        /// <param name="oldObject"> Valeur après les changements </param>
        /// <returns> Booléen indiquant si la valeur de la propriété a été changée </returns>
        private bool IsPropertyChanged(Object updatedObject, Object oldObject)
        {
            bool isChanged;

            if (updatedObject == null || oldObject == null)
            {
                isChanged = updatedObject != null || oldObject != null;
            }
            else
            {
                isChanged = !updatedObject.Equals(oldObject);
            }
            return isChanged;
        }
    }
}