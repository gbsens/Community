using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MKS.Core
{
    /// <summary>
    ///     Collection indiquant les règles de validation qui ne sont pas passées
    /// </summary>
    /// <remarks>
    ///     la classe RuleResults est plus communément utilisée que la classe RuleResult
    ///     pour connaître le résultat d'une validation. Chaque élément RuleResult
    ///     correspond à une règle qui n'est pas passée.
    /// </remarks>
    [DataContract]
    public class RuleResults : IEnumerable<RuleResult>
    {
        public RuleResults()
        {
            Items = new List<RuleResult>();
        }

        [DataMember(Name = "Items")]
        public List<RuleResult> Items { get; set; }

        /// <summary>
        ///     Nombre de règles qui ne sont pas passées
        /// </summary>
        public int Count
        {
            get { return (Items.Count); }
        }

        /// <summary>
        ///     Indique si la validation est passée correctement
        /// </summary>
        /// <value>
        ///     Vrai si la validation est passée (aucun résultat de règle), Faux si la validation n'est pas passée (au moins un
        ///     résultat de règle non validée)
        /// </value>
        public bool IsValid
        {
            get
            {
                if (Items.Count > 0)
                {
                    return (false);
                }
                return (true);
            }
        }

        /// <summary>
        ///     Ajoute un résultat de règle non validée à la liste RuleResults
        /// </summary>
        /// <remarks>
        ///     N'est normalement utilisée que par la fonction ValidationCore.DoValidation
        /// </remarks>
        /// <param name="ruleresult"> </param>
        public void Add(RuleResult ruleresult)
        {
            Items.Add(ruleresult);
        }

        /// <summary>
        ///     Ajoute un résultat de règle non validée à la liste RuleResults
        /// </summary>
        /// <remarks>
        ///     N'est normalement utilisée que par la fonction ValidationCore.DoValidation
        /// </remarks>
        /// <param name="ruleresult"> </param>
        public void Add(RuleResults ruleresult)
        {
            foreach (var r in ruleresult)
            {
                Items.Add(r);
            }
        }

        #region IEnumerable<RuleResult> Members

        /// <summary>
        ///     Enumerator de l'interface IEnumerable.
        /// </summary>
        /// <returns> Enumerator </returns>
        public IEnumerator<RuleResult> GetEnumerator()
        {
            return (Items.GetEnumerator());
        }

        /// <summary>
        ///     Enumerator de l'interface IEnumerable.
        /// </summary>
        /// <remarks>
        ///     N'est pas implémentée.
        /// </remarks>
        /// <returns> </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        #endregion
    }
}