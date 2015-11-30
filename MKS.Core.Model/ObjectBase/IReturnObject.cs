using System;
using System.Collections.Generic;

namespace MKS.Core
{
    /// <summary>
    ///     Interface utilisée pour le retour de messages par la logique d'affaires.
    /// </summary>
    public interface IReturnObject
    {
        /// <summary>
        ///     Message de transport
        /// </summary>
        Dictionary<string, string> Messages { get; set; }

        /// <summary>
        ///     Objet de type ProcessResults qui contient les exceptions des validations d'affaires ou d'intégrité qui ont une
        ///     sévérité Informative ou Warning.
        /// </summary>
        ProcessResults ProcessResults { get; set; }
    }
}