using MKS.Core.Model.Error;
using MKS.Core.Resources;
using System;
using System.Collections.Generic;

namespace MKS.Core
{
    /// <summary>
    ///   Classe permettant de lancer une validation spécifiée sur un objet donné.
    /// </summary>
    public static class ValidationCore
    {
        /// <summary>
        ///   Valide une règle personnalisée
        /// </summary>
        /// <typeparam name="TObject"> Type de l'objet associer à la validation </typeparam>
        /// <param name="objectToValidate"> Objet associer à la validation </param>
        /// <param name="customValidationClass"> instance d'une class de validation personnalisé qui implémente l'interface ICustomValidation </param>
        /// <returns> Objet de type RuleResults encapsultant les validations qui n'ont pas été réussies </returns>
        private static RuleResults ValidateCustomRules<TObject>(TObject objectToValidate, IValidation<TObject> customValidationClass)
        {
            var listRuleResults = new RuleResults();

            if (customValidationClass is IValidationExtend<TObject> && objectToValidate != null)
            {
                var v = customValidationClass as IValidationExtend<TObject>;
                v.ObjectInstance = objectToValidate;
            }

            foreach (var rule in customValidationClass.GetRules().Items)
            {
                CheckValidation(rule, objectToValidate, customValidationClass, listRuleResults);
            }

            return listRuleResults;
        }

        /// <summary>
        ///   Valide une règle d'intégrité
        /// </summary>
        /// <typeparam name="TObject"> Type de l'objet associé à la validation </typeparam>
        /// <param name="ruleProperty"> Objet de type RuleToItem </param>
        /// <param name="objectToValidate"> Objet associé à la validation </param>
        /// <param name="originalObject"> Instance d'une classe de validation qui implémente l'interface IValidation </param>
        /// <param name="globalRuleResults"> Liste de résultats de validation. On peut ajouter la liste de RuleResult retournée par une autre validation à cette liste afin que les messages soient retournés. </param>
        /// <returns> Objet de type RuleResults encapsulant les validations qui n'ont pas été réussies </returns>
        private static void CheckValidation<TObject>(ValidationRule ruleProperty, TObject objectToValidate, IValidation<TObject> originalObject, RuleResults globalRuleResults)
        {
            try
            {
                var listRuleResults = new RuleResults();
                bool result = false;

                switch (ruleProperty.Rule.Type)
                {
                    case Rule.RuleType.CustomRules:
                        result = originalObject.Validate(ruleProperty.Rule, ruleProperty, objectToValidate, globalRuleResults);
                        break;

                    case Rule.RuleType.ObjectRequired:
                        result = CommonRules.ObjectRequired(objectToValidate, ruleProperty.PropertyName);
                        break;

                    case Rule.RuleType.StringRequired:
                        result = CommonRules.StringRequired(objectToValidate, ruleProperty.PropertyName);
                        break;

                    case Rule.RuleType.StringMaxLength:
                        result = CommonRules.StringMaxLength(objectToValidate, ruleProperty.PropertyName,
                                                             (int)ruleProperty.Rule.Parameter,
                                                             ruleProperty.Rule.AllowNull);
                        break;

                    case Rule.RuleType.DateFormat:
                        result = CommonRules.DateFormat(objectToValidate, ruleProperty.PropertyName,
                                                        (string)ruleProperty.Rule.Parameter,
                                                        ruleProperty.Rule.AllowNull);
                        break;

                    case Rule.RuleType.IntegerMaxValue:
                        result = CommonRules.IntegerMaxValue(objectToValidate, ruleProperty.PropertyName,
                                                             (int)ruleProperty.Rule.Parameter,
                                                             ruleProperty.Rule.AllowNull);
                        break;

                    case Rule.RuleType.IntergerMinValue:
                        result = CommonRules.IntegerMinValue(objectToValidate, ruleProperty.PropertyName,
                                                             (int)ruleProperty.Rule.Parameter,
                                                             ruleProperty.Rule.AllowNull);
                        break;

                    case Rule.RuleType.StringMinLength:
                        result = CommonRules.StringMinLength(objectToValidate, ruleProperty.PropertyName,
                                                             (int)ruleProperty.Rule.Parameter,
                                                             ruleProperty.Rule.AllowNull);
                        break;

                    case Rule.RuleType.PostalCode:
                        result = CommonRules.CodePostal(objectToValidate, ruleProperty.PropertyName,
                                                        ruleProperty.Rule.AllowNull);
                        break;

                    case Rule.RuleType.PhoneNumber:
                        result = CommonRules.PhoneNumber(objectToValidate, ruleProperty.PropertyName,
                                                         ruleProperty.Rule.AllowNull);
                        break;

                    case Rule.RuleType.EmailAddress:
                        result = CommonRules.EmailAddress(objectToValidate, ruleProperty.PropertyName,
                                                          ruleProperty.Rule.AllowNull);
                        break;

                    case Rule.RuleType.RegularExpression:
                        result = CommonRules.RegularExpression(objectToValidate, ruleProperty.PropertyName,
                                                               (string)ruleProperty.Rule.Parameter,
                                                               ruleProperty.Rule.AllowNull);
                        break;

                    case Rule.RuleType.DateGreaterThan:
                        result = CommonRules.DateGreaterThan(objectToValidate, ruleProperty.PropertyName,
                                                             (DateTime)ruleProperty.Rule.Parameter,
                                                             ruleProperty.Rule.AllowNull);
                        break;

                    case Rule.RuleType.DateGreaterOrEqualThan:
                        result = CommonRules.DateGreaterOrEqualThan(objectToValidate, ruleProperty.PropertyName,
                                                                    (DateTime)ruleProperty.Rule.Parameter,
                                                                    ruleProperty.Rule.AllowNull);
                        break;

                    case Rule.RuleType.DateLessThan:
                        result = CommonRules.DateLessThan(objectToValidate, ruleProperty.PropertyName,
                                                          (DateTime)ruleProperty.Rule.Parameter,
                                                          ruleProperty.Rule.AllowNull);
                        break;

                    case Rule.RuleType.DateLessOrEqualThan:
                        result = CommonRules.DateLessOrEqualThan(objectToValidate, ruleProperty.PropertyName,
                                                                 (DateTime)ruleProperty.Rule.Parameter,
                                                                 ruleProperty.Rule.AllowNull);
                        break;

                    case Rule.RuleType.DateRequired:
                        result = CommonRules.DateRequired(objectToValidate, ruleProperty.PropertyName,
                                                          ruleProperty.Rule.AllowNull);
                        break;

                    case Rule.RuleType.Luhn:
                        result = CommonRules.Luhn(objectToValidate, ruleProperty.PropertyName,
                                                  ruleProperty.Rule.AllowNull);
                        break;

                    case Rule.RuleType.ListNotEmpty:
                        result = CommonRules.ListNotEmpty(objectToValidate, ruleProperty.PropertyName,
                                                          ruleProperty.Rule.AllowNull);
                        break;

                    case Rule.RuleType.ObjectContainsOneValidProperty:
                        result = CommonRules.ObjectContainsOneValidProperty(objectToValidate,
                                                                            (List<string>)ruleProperty.Rule.Parameter,
                                                                            ruleProperty.Rule.AllowNull);
                        break;

                    case Rule.RuleType.StringExactLength:
                        result = CommonRules.StringExactLength(objectToValidate, ruleProperty.PropertyName,
                                                               (int)ruleProperty.Rule.Parameter,
                                                               ruleProperty.Rule.AllowNull);
                        break;

                    case Rule.RuleType.DecimalMaxValue:
                        result = CommonRules.DecimalMaxValue(objectToValidate, ruleProperty.PropertyName,
                                                             (decimal)ruleProperty.Rule.Parameter,
                                                             ruleProperty.Rule.AllowNull);
                        break;

                    case Rule.RuleType.DecimalMinValue:
                        result = CommonRules.DecimalMinValue(objectToValidate, ruleProperty.PropertyName,
                                                             (decimal)ruleProperty.Rule.Parameter,
                                                             ruleProperty.Rule.AllowNull);
                        break;

                    case Rule.RuleType.DoubleMaxValue:
                        result = CommonRules.DoubleMaxValue(objectToValidate, ruleProperty.PropertyName,
                                                             (double)ruleProperty.Rule.Parameter,
                                                             ruleProperty.Rule.AllowNull);
                        break;

                    case Rule.RuleType.DoubleMinValue:
                        result = CommonRules.DoubleMinValue(objectToValidate, ruleProperty.PropertyName,
                                                             (double)ruleProperty.Rule.Parameter,
                                                             ruleProperty.Rule.AllowNull);
                        break;
                }

                if (result == false)
                {
                    //Effectue un remplacement d'argument dans la description de la règle par le nom convivial de la propriété. Recherche le tag {0}
                    Rule rule = ruleProperty.Rule.CloneObject();

                    rule.Description = string.Format(rule.Description, ruleProperty.FriendlyName, ruleProperty.FriendlyName1, ruleProperty.FriendlyName2);

                    listRuleResults.Add(new RuleResult(rule,
                                                                 typeof(TObject).ToString(),
                                                                 ruleProperty.PropertyName,
                                                                 typeof(TObject).Name,
                                                                 ruleProperty.BindingObjectName ?? typeof(TObject).Name,
                                                                 ruleProperty.BindingPropertyName ?? ruleProperty.PropertyName));
                }
                globalRuleResults.Add(listRuleResults);
            }
            catch (Exception ex)
            {
                if (ex.GetType() != typeof(NotImplementedException))
                    throw new ValidationException(ruleProperty.Rule.Type, ruleProperty.PropertyName, typeof(TObject), ex);

                throw;
            }
        }

        #region Méthodes exposées

        /// <summary>
        ///   Permet de lancer des validations d'integrite sur les objets et classe de validation
        /// </summary>
        /// <typeparam name="TObject"> Type de l'objet a valider. </typeparam>
        /// <param name="objectToValidate"> Instance de l'objet a valider. </param>
        /// <returns> La liste des erreurs de validation </returns>
        public static RuleResults DoValidation<TObject>(TObject objectToValidate)
        {
            return DoValidation(objectToValidate, null);
        }

        /// <summary>
        ///   Teste la validation d'integrité sur l'objet passé en paramètre. Dans cette
        ///   classe, l'ensemble des règles sont testées avant d'en retourner le résultat.
        /// </summary>
        /// <typeparam name="TObject"> Type de l'objet qui sera validé. </typeparam>
        /// <param name="objectToValidate"> Instance de l'objet à valider. </param>
        /// <param name="validationClass"> Classe de validation. </param>
        /// <returns> La liste des erreurs de validation </returns>
        public static RuleResults DoValidation<TObject>(TObject objectToValidate, IValidation<TObject> validationClass)
        {
            if (objectToValidate == null)
            {
                //Lance l'exception et c'est au système appelant de la loguer s'il le désire.
                throw new NullReferenceException(string.Format(CoreResources.EX0025, typeof(TObject).Name));
            }

            var listRuleResults = new RuleResults();

            //Vérifie si l'objet à valider contient une validation
            if (objectToValidate is IValidation<TObject>)
            {
                var objCustomValidation = (IValidation<TObject>)objectToValidate;
                listRuleResults = ValidateCustomRules(objectToValidate, objCustomValidation);
            }

            var globalRuleResults = new RuleResults();

            //Ajoute les résultats à la liste globale
            if (listRuleResults.Count > 0)
                globalRuleResults.Add(listRuleResults);

            if (validationClass != null)
            {
                listRuleResults = ValidateCustomRules(objectToValidate, validationClass);

                //Ajoute les resultats à la liste globale
                if (listRuleResults.Count > 0)
                    globalRuleResults.Add(listRuleResults);
            }



            return globalRuleResults;
        }

        /// <summary>
        ///   Teste la validation d'integrité sur l'objet passé en paramètre. Dans cette
        ///   classe, l'ensemble des règles sont testées avant d'en retourner les résultats.
        /// </summary>
        /// <typeparam name="TObject"> Type de l'objet qui sera validé. </typeparam>
        /// <typeparam name="TValidation"> Classe de validation. </typeparam>
        /// <param name="objectToValidate"> Instance de l'objet à valider. </param>
        /// <returns> La liste des erreurs de validation </returns>
        public static RuleResults DoValidation<TObject, TValidation>(TObject objectToValidate)
            where TValidation : IValidation<TObject>, new()
        {
            return DoValidation(objectToValidate, new TValidation());
        }

        /// <summary>
        ///   Permet de générer une erreur de validation
        /// </summary>
        /// <param name="ruleResults"> </param>
        public static void CheckResultOfValidation(RuleResults ruleResults)
        {
            if (ruleResults != null && ruleResults.Count > 0)
            {
                var _exceptions = new ProcessResults();

                foreach (var objRuleResultTemp in ruleResults)
                {
                    var r = new ReturnMessage(TypeError.ValidationObjet,
                                              Utilities.TranslateSeverity(objRuleResultTemp.RuleInformation.Severity),
                                              objRuleResultTemp.RuleInformation.CodeMessage,
                                              objRuleResultTemp.RuleInformation.Description,
                                              false,
                                              objRuleResultTemp.Property
                                              )
                                {
                                    ObjectName = objRuleResultTemp.ObjectName,
                                    ObjectPropertyName = objRuleResultTemp.Property,
                                    RuleType = objRuleResultTemp.RuleInformation.Type,
                                    RuleReference = objRuleResultTemp.RuleInformation
                                };
                    _exceptions.AddException(r);
                }

                if (ruleResults.Count > 0)
                {
                    throw new ExceptionProcess<ProcessResults> (_exceptions);
                }
            }
        }

        #endregion Méthodes exposées
    }
}