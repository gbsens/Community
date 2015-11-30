using MKS.Core.Resources;
using System;

namespace MKS.Core
{
    /// <summary>
    ///   Gestion d'erreur pour la validationException.
    /// </summary>
    public sealed class ValidationException : MissingFieldException
    {
        /// <summary>
        ///   Constructeur
        /// </summary>
        public ValidationException()
            : base(CoreResources.EX0021)
        {
        }

        /// <summary>
        ///   Constructeur.
        /// </summary>
        /// <param name="typeRegle"> Type de la règle. </param>
        /// <param name="PropertyName"> Propriété visée par l'exception. </param>
        /// <param name="objectType"> Type de l'objet validé. </param>
        /// <param name="ex"> Exception qui a déclenchée l'erreur. </param>
        public ValidationException(Rule.RuleType typeRegle, string PropertyName, Type objectType, Exception ex)
            : base(string.Format(CoreResources.EX0023, typeRegle, PropertyName, objectType.Name), ex)
        {
        }

        /// <summary>
        ///   Constructeur.
        /// </summary>
        /// <param name="ex"> Exception qui a déclenchée l'erreur. </param>
        public ValidationException(Exception ex)
            : base(CoreResources.EX0021, ex)
        {
        }
    }
}