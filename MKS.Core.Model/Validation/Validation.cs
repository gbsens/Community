using System;
using System.Runtime.Serialization;
using MKS.Core.Model.Ressources;

namespace MKS.Core
{
    /// <summary>
    ///     Classe de base pour la validation. Cette classe doit être héritée dans un objet de base ou
    ///     une classe de validation pour le factory ou encore dans les objets de type Search et Key.
    /// </summary>
    /// <remarks>
    ///     Validation.Validate() sera appellé si la liste des règles retournées par
    ///     Validation.GetRules() contient des RuleCustom.
    /// </remarks>
    /// <typeparam name="TObject"> L'objet à valider </typeparam>
    [DataContract]
    public abstract class Validation<TObject> : IValidationExtend<TObject>, IDisposable
    {
        public virtual void Dispose()
        {
        }

        public TObject ObjectInstance { get; set; }
        public abstract ValidationRules GetRules();

        public virtual bool Validate(Rule rule, ValidationRule ruleProperty, TObject item, RuleResults rulesResults)
        {
            throw new Exception(CoreRessources.EX0002);
        }
    }
}