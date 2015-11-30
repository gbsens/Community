using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MKS.Core
{
    /// <summary>
    ///     Liste des éléments d'une règle d'intégrité
    /// </summary>
    [DataContract]
    public class ValidationRules : IEnumerable<ValidationRule>
    {
        public ValidationRules()
        {
            Items = new List<ValidationRule>();
        }

        /// <summary>
        ///     Retourne une liste d'item de la règle
        /// </summary>
        [DataMember(Name = "Items")]
        public List<ValidationRule> Items { get; set; }

        /// <summary>
        ///     Permet d'ajouter d'autre RuleToItem a une liste existante.
        /// </summary>
        /// <param name="objValidationList"> RuleToItemList à ajouter à liste existante </param>
        /// <returns> Retourne la liste avec les ajouts </returns>
        public ValidationRules AddList(ValidationRules objValidationList)
        {
            foreach (var item in objValidationList)
            {
                Add(item.Rule, item.PropertyName);
            }
            return this;
        }

        /// <summary>
        ///     Ajout d'une nouvelle règle
        /// </summary>
        /// <param name="rule"> Règle à ajouter </param>
        /// <param name="propertyName">
        ///     Nom de la propriété de l'objet a valider. Si Rule=CustomRule, le propertyName est à titre
        ///     informatif
        /// </param>
        public void Add(Rule rule, string propertyName)
        {
            Items.Add(new ValidationRule(rule, propertyName, propertyName));
        }

        /// <summary>
        ///     Ajout d'une nouvelle règle
        /// </summary>
        /// <param name="rule"> Règle à ajouter </param>
        /// <param name="propertyName"> Nom de la propriété de l'objet a valider </param>
        /// <param name="friendlyName"> Nom de la propriété tel qu'il sera affiché à l'utilisateur. </param>
        public void Add(Rule rule, string propertyName, object friendlyName)
        {
            Items.Add(new ValidationRule(rule, propertyName, friendlyName));
        }

        /// <summary>
        ///     Ajout d'une nouvelle règle
        /// </summary>
        /// <param name="rule"> Règle à ajouter </param>
        /// <param name="propertyName"> Nom de la propriété de l'objet a valider </param>
        /// <param name="friendlyName"> Nom de la propriété tel qu'il sera affiché à l'utilisateur. </param>
        public void Add(Rule rule, string propertyName, object friendlyName, object friendlyName1)
        {
            Items.Add(new ValidationRule(rule, propertyName, friendlyName, friendlyName1));
        }

        /// <summary>
        ///     Ajout d'une nouvelle règle
        /// </summary>
        /// <param name="rule"> Règle à ajouter </param>
        /// <param name="propertyName"> Nom de la propriété de l'objet a valider </param>
        /// <param name="friendlyName"> Nom de la propriété tel qu'il sera affiché à l'utilisateur. </param>
        public void Add(Rule rule, string propertyName, object friendlyName, object friendlyName1, object friendlyName2)
        {
            Items.Add(new ValidationRule(rule, propertyName, friendlyName, friendlyName1, friendlyName2));
        }

        /// <summary>
        ///     Ajout d'une nouvelle règle
        /// </summary>
        /// <param name="rule"> Règle à ajouter </param>
        /// <param name="propertyName"> Nom de la propriété de l'objet a valider </param>
        /// <param name="friendlyName"> Nom de la propriété tel qu'il sera affiché à l'utilisateur. </param>
        /// <param name="bindedObjectName"> Nom de l'objet qui est associé à cette règle </param>
        /// <param name="bindedpropertyName"> Nom de la propriété qui est associé à cette règle </param>
        public void Add(Rule rule, string propertyName, string bindedObjectName, string bindedpropertyName,
            object friendlyName)
        {
            Items.Add(new ValidationRule(rule, propertyName, bindedObjectName, bindedpropertyName, friendlyName));
        }

        /// <summary>
        ///     Ajout d'une nouvelle règle
        /// </summary>
        /// <param name="rule"> Règle à ajouter </param>
        /// <param name="propertyName"> Nom de la propriété de l'objet a valider </param>
        /// <param name="friendlyName"> Nom de la propriété tel qu'il sera affiché à l'utilisateur. </param>
        /// <param name="bindedObjectName"> Nom de l'objet qui est associé à cette règle </param>
        /// <param name="bindedpropertyName"> Nom de la propriété qui est associé à cette règle </param>
        public void Add(Rule rule, string propertyName, string bindedObjectName, string bindedpropertyName,
            object friendlyName, object friendlyName1)
        {
            Items.Add(new ValidationRule(rule, propertyName, bindedObjectName, bindedpropertyName, friendlyName,
                friendlyName1));
        }

        /// <summary>
        ///     Ajout d'une nouvelle règle
        /// </summary>
        /// <param name="rule"> Règle à ajouter </param>
        /// <param name="propertyName"> Nom de la propriété de l'objet a valider </param>
        /// <param name="friendlyName"> Nom de la propriété tel qu'il sera affiché à l'utilisateur. </param>
        /// <param name="bindedObjectName"> Nom de l'objet qui est associé à cette règle </param>
        /// <param name="bindedpropertyName"> Nom de la propriété qui est associé à cette règle </param>
        public void Add(Rule rule, string propertyName, string bindedObjectName, string bindedpropertyName,
            object friendlyName, object friendlyName1, object friendlyName2)
        {
            Items.Add(new ValidationRule(rule, propertyName, bindedObjectName, bindedpropertyName, friendlyName,
                friendlyName1, friendlyName2));
        }

        #region IEnumerable<RuleToItem> Members

        /// <summary>
        ///     Permet de supporter la recherche dans une collection.
        /// </summary>
        /// <returns> </returns>
        public IEnumerator<ValidationRule> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        /// <summary>
        ///     Permet de supporter la recherche dans une collection.
        /// </summary>
        /// <returns> </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        #endregion
    }
}