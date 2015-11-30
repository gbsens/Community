using System.Runtime.Serialization;

namespace MKS.Core.Business
{
    /// <summary>
    ///   Règle à utiliser lors de la création d'une règle dans un businessProcessObject process.  Cette règle ne doit pas être utilisée
    ///   dans une classe de validation, si elle est utilisée dans une telle classe elle sera ignorée.
    /// </summary>
    [DataContract]
    public class RuleBusiness : Rule
    {
        /// <summary>
        ///   Règle à utiliser lors de la création d'une règle dans un businessProcessObject process.  Cette règle ne doit pas être utilisée
        ///   dans une classe de validation, si elle est utilisée dans une telle classe elle sera ignorée.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        public RuleBusiness(string codeMessage, string description, RuleSeverity severite)
            : base(codeMessage, description, severite, RuleType.BusinessRules, null, null, null, false)
        {
        }
    }
}