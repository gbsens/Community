using System;

namespace MKS.Core.Activity
{
    /// <summary>
    ///   Classe encapsulant les informations reliées à un changement effectué sur une
    ///   propriété d'un objet pour la journalisation.
    ///   <para> L'objectif est de définir les propriétés d'un opbjet pour que le Factory puise identifier les changements. </para>
    /// </summary>
    /// <example>
    ///   public class DetectChangeContact1:IChangeDetection&lt;Contact&gt;
    ///   <para> { </para>
    ///   <para> public DetectChanges GetPropertiesAltered(Contact p_newObject, Contact p_oldObject) </para>
    ///   <para> { </para>
    ///   <para> DetectChanges ds = new DetectChanges(); </para>
    ///   <para> ds.Changes.AssigneClassePermission(new DetectChange(p_newObject,p_oldObject,typeof(Contact), &quot; Nom &quot; )); </para>
    ///   <para> return(ds); </para>
    ///   <para> } </para>
    ///   <para> </para>
    ///   <para> } </para>
    /// </example>
    public class ChangeDetection
    {
        private readonly string _fullName;
        private readonly Object _newValue;
        private readonly Object _oldValue;
        private readonly string _propertyName;
        private readonly Type _typeObject;
        private readonly string _key;

        /// <summary>
        ///   Constructeur
        /// </summary>
        public ChangeDetection()
        {
        }

        /// <summary>
        ///   Constructeur
        /// </summary>
        /// <param name="newValue"> Valeur de l'objet avant les modifications </param>
        /// <param name="oldValue"> Valeur de l'objet après les modifications </param>
        /// <param name="objectType"> Type de l'objet </param>
        /// <param name="propertyName"> Nom de la propriété </param>
        public ChangeDetection(Object newValue, Object oldValue, Type objectType, string propertyName)
        {
            _newValue = newValue;
            _oldValue = oldValue;
            _typeObject = objectType;
            _propertyName = propertyName;
            _fullName = GetPropertyFullName(objectType, propertyName);
        }

        /// <summary>
        ///   Constructeur
        /// </summary>
        /// <param name="newValue"> Valeur de l'objet avant les modifications </param>
        /// <param name="oldValue"> Valeur de l'objet après les modifications </param>
        /// <param name="objectType"> Type de l'objet </param>
        /// <param name="propertyName"> Nom de la propriété </param>
        public ChangeDetection(Object newValue, Object oldValue, Type objectType, string propertyName, string key)
        {
            _newValue = newValue;
            _oldValue = oldValue;
            _typeObject = objectType;
            _propertyName = propertyName;
            _fullName = GetPropertyFullName(objectType, propertyName);
            _key = key;
        }

        public string Key
        {
            get { return _key; }
        }

        /// <summary>
        ///   Valeur de la propriété avant la modification.
        /// </summary>
        public Object OldValue
        {
            get { return _oldValue; }
        }

        /// <summary>
        ///   Valeur de la propriété après la modification.
        /// </summary>
        public Object NewValue
        {
            get { return _newValue; }
        }

        /// <summary>
        ///   Type de l'objet.
        /// </summary>
        public Type TypeObject
        {
            get { return _typeObject; }
        }

        /// <summary>
        ///   Nom de la propriété.
        /// </summary>
        public string PropertyName
        {
            get { return _propertyName; }
        }

        /// <summary>
        ///   Nom complet de la propriété incluant le namespace de l'objet.
        /// </summary>
        public string FullName
        {
            get { return _fullName; }
        }

        /// <summary>
        ///   Retourne le nom complet de la propriété incluant le namespace de l'object.
        /// </summary>
        /// <param name="type"> Type de l'objet </param>
        /// <param name="propertyName"> Nom de la propriété </param>
        /// <returns> Le nom complet </returns>
        private static string GetPropertyFullName(Type type, string propertyName)
        {
            return string.Concat(type.FullName, ".", propertyName);
        }
    }
}