using System.Collections.Generic;

namespace MKS.Core.Presenter
{
    /// <summary>
    ///     Cette classe permet de garder en mémoire les différentes possibilités de localisation des différents formulaire ou
    ///     page Web.
    ///     Elle est utile pour permettre au Processus de présentation d'avoir accès au différents pages de redirections.
    /// </summary>
    public class Navigation
    {
        private static readonly Dictionary<string, object> _FormRoute = new Dictionary<string, object>();

        /// <summary>
        ///     Dictionnaire qui contient une clé pour préciser l'élément de redirection et d'une valeur de redirection.
        /// </summary>
        /// <remarks>
        ///     Dans une page web le deuxième élément du dictionnaire représente un URL de redirection
        /// </remarks>
        public static Dictionary<string, object> Form
        {
            get { return _FormRoute; }
        }
    }
}