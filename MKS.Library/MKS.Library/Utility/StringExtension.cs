using System;
using System.Collections.Generic;
using System.Text;

namespace MKS.Library.Utility
{
    /// <summary>
    /// Classe qui ajoute des fonctions pour convertir les objets en string.
    /// 
    /// </summary>
    public static class stringExtension
    {
        /// <summary>
        /// Permet de convertir un objet en string peu importe sa valeur.
        /// </summary>
        /// <param name="input">Objet à convertir en string</param>
        /// <returns>Retourne null si l'objet est null, sinon procède avec la conversion standard de l'objet</returns>
        public static string NullPreservingToString(this object input)
        {
            return input == null ? null : input.ToString();
        }

        /// <summary>
        /// Permet de convertir un objet en string peu importe sa valeur.
        /// </summary>
        /// <param name="input">Objet à convertir en string</param>
        /// <returns>Retourne une string vide si l'objet est null, sinon procède avec la conversion standard de l'objet</returns>
        public static string ToStringOrEmpty(this object input)
        {
            return input == null ? string.Empty : input.ToString();
        }

        /// <summary>
        /// Permet de convertir un objet en string peu importe sa valeur.
        /// </summary>
        /// <param name="input">Objet à convertir en string</param>
        /// <param name="defaultValue">Valeur par défaut de la string</param>
        /// <returns>Retourne la valeur par défaut si l'objet est null, sinon procède avec la conversion standard de l'objet</returns>
        public static string ToStringOrDefault(this object input, string defaultValue)
        {
            return input == null ? defaultValue : input.ToString();
        }
    }
}