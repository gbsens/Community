using System.Runtime.Serialization;

namespace MKS.Core
{
    /// <summary>
    ///     Classe d'itentification d'un item contenu dans la règle
    /// </summary>
    [DataContract]
    public class ValidationRule
    {
        /// <summary>
        ///     Constructeur d'un nouvel item d'une règle
        /// </summary>
        /// <param name="rule"> Référence sur la règle </param>
        /// <param name="propertyName"> </param>
        /// <param name="friendlyName"> Nom de la propriété tel qu'il apparaitra dans le message de validation </param>
        public ValidationRule(Rule rule, string propertyName, object friendlyName)
        {
            PropertyName = propertyName;
            FriendlyName = friendlyName;
            Rule = rule;
        }

        /// <summary>
        ///     Constructeur d'un nouvel item d'une règle
        /// </summary>
        /// <param name="rule"> Référence sur la règle </param>
        /// <param name="propertyName"> </param>
        /// <param name="friendlyName"> Nom de la propriété tel qu'il apparaitra dans le message de validation </param>
        public ValidationRule(Rule rule, string propertyName, object friendlyName, object friendlyName1)
        {
            PropertyName = propertyName;
            FriendlyName1 = friendlyName1;
            FriendlyName = friendlyName;
            Rule = rule;
        }

        /// <summary>
        ///     Constructeur d'un nouvel item d'une règle
        /// </summary>
        /// <param name="rule"> Référence sur la règle </param>
        /// <param name="propertyName"> </param>
        /// <param name="friendlyName"> Nom de la propriété tel qu'il apparaitra dans le message de validation </param>
        public ValidationRule(Rule rule, string propertyName, object friendlyName, object friendlyName1,
            object friendlyName2)
        {
            PropertyName = propertyName;
            FriendlyName = friendlyName;
            FriendlyName1 = friendlyName1;
            FriendlyName2 = friendlyName2;
            Rule = rule;
        }

        /// <summary>
        ///     Constructeur d'un nouvel item d'une règle
        /// </summary>
        /// <param name="rule"> Référence sur la règle </param>
        /// <param name="propertyName"> </param>
        /// <param name="friendlyName"> Nom de la propriété tel qu'il apparaitra dans le message de validation </param>
        /// <param name="bindedObjectName"> Nom de l'objet qui est associé à cette règle </param>
        /// <param name="bindedPropertyName"> Nom de la propriété qui est associé à cette règle </param>
        public ValidationRule(Rule rule, string propertyName, string bindedObjectName, string bindedPropertyName,
            object friendlyName)
        {
            PropertyName = propertyName;

            FriendlyName = friendlyName;
            BindingObjectName = bindedObjectName;
            BindingPropertyName = bindedPropertyName;
            Rule = rule;
        }

        /// <summary>
        ///     Constructeur d'un nouvel item d'une règle
        /// </summary>
        /// <param name="rule"> Référence sur la règle </param>
        /// <param name="propertyName"> </param>
        /// <param name="friendlyName"> Nom de la propriété tel qu'il apparaitra dans le message de validation </param>
        /// <param name="bindedObjectName"> Nom de l'objet qui est associé à cette règle </param>
        /// <param name="bindedPropertyName"> Nom de la propriété qui est associé à cette règle </param>
        public ValidationRule(Rule rule, string propertyName, string bindedObjectName, string bindedPropertyName,
            object friendlyName, object friendlyName1)
        {
            PropertyName = propertyName;

            FriendlyName = friendlyName;
            FriendlyName1 = friendlyName1;
            BindingObjectName = bindedObjectName;
            BindingPropertyName = bindedPropertyName;
            Rule = rule;
        }

        /// <summary>
        ///     Constructeur d'un nouvel item d'une règle
        /// </summary>
        /// <param name="rule"> Référence sur la règle </param>
        /// <param name="propertyName"> </param>
        /// <param name="friendlyName"> Nom de la propriété tel qu'il apparaitra dans le message de validation </param>
        /// <param name="bindedObjectName"> Nom de l'objet qui est associé à cette règle </param>
        /// <param name="bindedPropertyName"> Nom de la propriété qui est associé à cette règle </param>
        public ValidationRule(Rule rule, string propertyName, string bindedObjectName, string bindedPropertyName,
            object friendlyName, object friendlyName1, object friendlyName2)
        {
            PropertyName = propertyName;

            FriendlyName = friendlyName;
            FriendlyName1 = friendlyName1;
            FriendlyName2 = friendlyName2;
            BindingObjectName = bindedObjectName;
            BindingPropertyName = bindedPropertyName;
            Rule = rule;
        }

        /// <summary>
        ///     Nom de la propriété de l'objet
        /// </summary>
        [DataMember(Name = "PropertyName")]
        public string PropertyName { get; set; }

        /// <summary>
        ///     Nom convivial de la propriété de l'objet
        /// </summary>
        [DataMember(Name = "FriendlyName")]
        public object FriendlyName { get; set; }

        [DataMember(Name = "FriendlyName1")]
        public object FriendlyName1 { get; set; }

        [DataMember(Name = "FriendlyName2")]
        public object FriendlyName2 { get; set; }

        /// <summary>
        ///     Nom de l'objet qui est associée à cette règle
        /// </summary>
        [DataMember(Name = "BindingObjectName")]
        public string BindingObjectName { get; set; }

        /// <summary>
        ///     Nom de la propriété qui est associée à cette règle
        /// </summary>
        [DataMember(Name = "BindingPropertyName")]
        public string BindingPropertyName { get; set; }

        /// <summary>
        ///     Règle associée a l'item
        /// </summary>
        [DataMember(Name = "Rule")]
        public Rule Rule { get; set; }
    }
}