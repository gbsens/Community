using System;
using System.Runtime.Serialization;

namespace MKS.Core.Activity
{
    /// <summary>
    /// Classe de Sous événements de journalisation.
    /// </summary>
    [DataContract]
    public class ActivityDetailLocal : IActivityDetail
    {
        /// <summary>
        /// ID du sous événement
        /// </summary>
        [DataMember]
        public Int32 DetailId { get; set; }

        /// <summary>
        /// ID de l'événement
        /// </summary>
        [DataMember]
        public Int32 ActivityId { get; set; }

        /// <summary>
        /// Nom du composant
        /// </summary>
        [DataMember]
        public string ComponentName { get; set; }

        /// <summary>
        /// Code de l'action
        /// </summary>
        [DataMember]
        public string ActionCode { get; set; }

        /// <summary>
        /// Code de l'entité
        /// </summary>
        [DataMember]
        public string EntityCode { get; set; }

        /// <summary>
        /// Code de l'attribut
        /// </summary>
        [DataMember]
        public string AttributeCode { get; set; }

        /// <summary>
        /// Valeur avant les modifications
        /// </summary>
        [DataMember]
        public string ValueBefore { get; set; }

        /// <summary>
        /// Valeur après les modifications
        /// </summary>
        [DataMember]
        public string ValueAfter { get; set; }

        /// <summary>
        /// Valeur après les modifications
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Constructeur de class
        /// </summary>
        public ActivityDetailLocal()
        {
        }

        /// <summary>
        /// Constructeur de class
        /// </summary>
        /// <param name="p_componentName">Nom du composant</param>
        /// <param name="p_actionCode">Code de l'action</param>
        /// <param name="p_entityCode">Code de l'entité</param>
        public ActivityDetailLocal(string p_componentName, string p_actionCode, string p_entityCode)
        {
            ComponentName = p_componentName;
            ActionCode = p_actionCode;
            EntityCode = p_entityCode;
        }

        /// <summary>
        /// Constructeur de class
        /// </summary>
        /// <param name="p_componentName">Nom du composant</param>
        /// <param name="p_actionCode">Code de l'action</param>
        /// <param name="p_entityCode">Code de l'entité</param>
        /// <param name="p_attributCode">Code de l'attribut</param>
        /// <param name="p_valueBefore">Valeur avant les modifications</param>
        /// <param name="p_valueAfter">Valeur après les modifications</param>
        public ActivityDetailLocal(string p_componentName, string p_actionCode, string p_entityCode,
                        string p_attributCode, string p_valueBefore, string p_valueAfter)
        {
            ComponentName = p_componentName;
            ActionCode = p_actionCode;
            EntityCode = p_entityCode;
            AttributeCode = p_attributCode;
            ValueBefore = p_valueBefore;
            ValueAfter = p_valueAfter;
        }

        /// <summary>
        /// Constructeur de class
        /// </summary>
        /// <param name="p_componentName">Nom du composant</param>
        /// <param name="p_actionCode">Code de l'action</param>
        /// <param name="p_entityCode">Code de l'entité</param>
        /// <param name="p_attributCode">Code de l'attribut</param>
        /// <param name="p_valueBefore">Valeur avant les modifications</param>
        /// <param name="p_valueAfter">Valeur après les modifications</param>
        /// <param name="p_description">Informations du sous événement</param>
        public ActivityDetailLocal(string p_componentName, string p_actionCode, string p_entityCode,
                        string p_attributCode, string p_valueBefore, string p_valueAfter, string p_description)
        {
            ComponentName = p_componentName;
            ActionCode = p_actionCode;
            EntityCode = p_entityCode;
            AttributeCode = p_attributCode;
            ValueBefore = p_valueBefore;
            ValueAfter = p_valueAfter;
            Description = p_description;
        }

        /// <summary>
        /// Constructeur de class
        /// </summary>
        /// <param name="p_componentName">Nom du composant</param>
        /// <param name="p_actionCode">Code de l'action</param>
        /// <param name="p_entityCode">Code de l'entité</param>
        /// <param name="p_attributCode">Code de l'attribut</param>
        /// <param name="p_valueBefore">Valeur avant les modifications</param>
        /// <param name="p_valueAfter">Valeur après les modifications</param>
        /// <param name="p_description">Informations du sous événement</param>
        public ActivityDetailLocal(int pSubActivityId, int pActivityId, string p_componentName, string p_actionCode, string p_entityCode,
                        string p_attributCode, string p_valueBefore, string p_valueAfter, string p_description)
        {
            DetailId = pSubActivityId;
            ActivityId = pActivityId;
            ComponentName = p_componentName;
            ActionCode = p_actionCode;
            EntityCode = p_entityCode;
            AttributeCode = p_attributCode;
            ValueBefore = p_valueBefore;
            ValueAfter = p_valueAfter;
            Description = p_description;
        }
    }
}