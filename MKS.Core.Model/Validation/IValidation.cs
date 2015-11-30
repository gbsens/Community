namespace MKS.Core
{
    public interface IValidation
    {
        /// <summary>
        ///     Obtient un objet de type RuleToItemList qui regroupe les règles à valider.
        /// </summary>
        /// <returns> </returns>
        ValidationRules GetRules();
    }

    /// <summary>
    ///     Interface de base pour les validation.
    /// </summary>
    /// <remarks>
    ///     Lors de la validation des règles de la classe de validation, un objet de type
    ///     RuleResult est construit avec une description de chacune des règles en erreur.
    ///     Si une des règles en erreur a une sévérité &quot;Error&quot; une FaultException
    ///     est lancée stoppant ainsi le processus. Si les règles en erreur sont de sévérité
    ///     &quot;Information&quot; ou &quot;Warning&quot; les descriptions des règles en
    ///     erreur sont placées dans la propriété ProcessResults du BusinessFactory.
    /// </remarks>
    public interface IValidation<in TObject> : IValidation
    {
        /// <summary>
        ///     Effectue le traitement de validation
        /// </summary>
        /// <param name="rule"> règle de validation de type MKS.Core.Rule </param>
        /// <param name="ruleProperty"> Item de propriété d'une règle </param>
        /// <param name="item"> Objet associé à la validation </param>
        /// <param name="rulesResults">
        ///     Liste de résultats de validation. On peut ajouter la liste de RuleResult retourné par une
        ///     autre validation à cette liste afin que les messages soient retournés.
        /// </param>
        /// <returns> Booléen indiquant si la validation à été réussi </returns>
        bool Validate(Rule rule, ValidationRule ruleProperty, TObject item, RuleResults rulesResults);
    }

    /// <summary>
    ///     Interface implémentée par les classes de validation d'intégrité.
    /// </summary>
    public interface IValidationExtend<TObject> : IValidation<TObject>
    {
        /// <summary>
        ///     Objet à valider.
        /// </summary>
        TObject ObjectInstance { get; set; }
    }
}