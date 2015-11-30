using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MKS.Core
{
    /// <summary>
    /// Entité contenant les paramètres utilisés pour la recherche d'une page d'éléments.
    /// </summary>
    [DataContract]
    public class VirtualFilters
    {
        /// <summary>
        ///     Constructeur.
        /// </summary>
        public VirtualFilters()
        {
        }

        /// <summary>
        ///     Constructeur.
        /// </summary>
        public VirtualFilters(string searchString, string searchOperation)
        {
            SearchString = searchString;
            SearchOperation = searchOperation;
        }

        /// <summary>
        ///     Obtient ou définit les filtres de recherche.
        /// </summary>
        [DataMember(Name = "SearchString")]
        public string SearchString { get; set; }

        /// <summary>
        ///     Obtient ou définit le type d'opération de recherche en provenance d'un objet permettant les criteres de recherche tel qu'une gille UI.
        /// </summary>
        [DataMember(Name = "SearchOperation")]
        public string SearchOperation { get; set; }
    }

    /// <summary>
    ///     Entité contenant les paramètres utilisés pour la recherche d'une page d'éléments.
    /// </summary>
    [DataContract]
    public class VirtualSkip
    {
        /// <summary>
        ///     Constructeur.
        /// </summary>
        public VirtualSkip()
        {
            Page = 0;
            PageSize = 0;
            SortExpression = string.Empty;
            IsSortAsc = true;
            Filters = new Dictionary<string, VirtualFilters>();
        }

        /// <summary>
        ///     Constructeur.
        /// </summary>
        /// <param name="p_page"> Index de la page contenant les informations à retourner. </param>
        /// <param name="p_PageSize"> Nombre d'éléments par page. </param>
        /// <param name="p_SortBy"> Propriété utilisée pour le tri. </param>
        /// <param name="p_IsSortAsc"> Indique si le tri est effectué en ordre croissant. Si faux, trier en ordre décroissant. </param>
        /// <param name="p_Filters"> Filtres de recherche. </param>
        public VirtualSkip(int p_page,
            int p_PageSize,
            string p_SortBy,
            bool p_IsSortAsc,
            Dictionary<string, VirtualFilters> p_Filters)
        {
            Page = p_page;
            PageSize = p_PageSize;
            SortExpression = p_SortBy;
            IsSortAsc = p_IsSortAsc;
            Filters = p_Filters;
        }

        /// <summary>
        ///     Obtient ou définit l'index de la page contenant les informations à retourner.
        /// </summary>
        [DataMember(Name = "Page")]
        public int Page { get; set; }

        /// <summary>
        ///     Obtient ou définit le nombre d'éléments par page.
        /// </summary>
        [DataMember(Name = "PageSize")]
        public int PageSize { get; set; }

        /// <summary>
        ///     Obtient ou définit la propriété utilisée pour le tri.
        /// </summary>
        [DataMember(Name = "SortExpression")]
        public string SortExpression { get; set; }

        /// <summary>
        ///     Obtient ou définit l'indicateur d'ordre du tri. Si vrai, trier en ordre croissant. Si faux, trier en ordre
        ///     décroissant.
        /// </summary>
        [DataMember(Name = "IsSortedAsc")]
        public bool IsSortAsc { get; set; }

        /// <summary>
        ///     Obtient ou définit les filtres de recherche.
        /// </summary>
        [DataMember(Name = "Filters")]
        public Dictionary<string, VirtualFilters> Filters { get; set; }

        /// <summary>
        ///     Obtient ou définit le nombre d'element total
        /// </summary>
        [DataMember(Name = "MaxRecord")]
        public int MaxRecord { get; set; }
    }
}