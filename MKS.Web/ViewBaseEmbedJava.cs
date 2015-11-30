using System;
using System.Collections.Generic;
using System.Text;

using MKS.Core.Presenter;


namespace MKS.Web
{
    /// <summary>
    /// Cette classe doit etre hérité dans le objet de vue. Elle contient la structure JSON pour les validations coté UI.
    /// </summary>
    [Obsolete("N'est plus utilisé")]
    public abstract class ViewBaseEmbedJava : ViewBase
    {   
        
        public override void AssociateViewToValidation(List<RulesAssociationsObject> rulesassociations)
        {

            if (rulesassociations.Count > 0)
            {
                StringBuilder javascriptRule = new StringBuilder();
                StringBuilder javascriptMessage = new StringBuilder();

                javascriptRule.Append("{");
                javascriptMessage.Append("{");
                int nbass = rulesassociations.Count;
                foreach (var rules in rulesassociations)
                {
                    if (rules.ListRulesAssociation != null)
                    {
                        foreach (var ruleAssociation in rules.ListRulesAssociation)
                        {
                            if (ruleAssociation.Rules != null)
                            {
                                javascriptRule.Append(ruleAssociation.ViewPropertyName + ":");
                                javascriptMessage.Append(ruleAssociation.ViewPropertyName + ":");
                                javascriptRule.Append("{");
                                javascriptMessage.Append("{");

                                int nb = ruleAssociation.Rules.Items.Count;
                                bool findrules = false;
                                foreach (var rule in ruleAssociation.Rules)
                                {
                                    if (rule.Rule != null)
                                    {
                                        

                                        switch (rule.Rule.Type)
                                        {
                                            case MKS.Core.Rule.RuleType.DateCustomRules:
                                                findrules = true;
                                                javascriptRule.Append("DateCustomRules:''");
                                                javascriptMessage.Append(@"DateCustomRules:""" + rule.Rule.Description + @"""");

                                                break;

                                            case MKS.Core.Rule.RuleType.CustomRules:
                                                findrules = true;
                                                javascriptRule.Append("CustomRules:''");
                                                javascriptMessage.Append(@"CustomRules:""" + rule.Rule.Description + @"""");

                                                break;

                                            case MKS.Core.Rule.RuleType.AlphanumericCharacters:
                                                findrules = true;
                                                javascriptRule.Append("AlphanumericCharacters:''");
                                                javascriptMessage.Append(@"AlphanumericCharacters:""" + rule.Rule.Description + @"""");
                                                break;

                                            case MKS.Core.Rule.RuleType.FloatingNumber:
                                                findrules = true;
                                                javascriptRule.Append("FloatingNumber:''");
                                                javascriptMessage.Append(@"FloatingNumber:""" + rule.Rule.Description + @"""");
                                                break;

                                            case MKS.Core.Rule.RuleType.ListNotEmpty:
                                                findrules = true;
                                                javascriptRule.Append("ListNotEmpty:''");
                                                javascriptMessage.Append(@"ListNotEmpty:""" + rule.Rule.Description + @"""");
                                                break;

                                            case MKS.Core.Rule.RuleType.Luhn:
                                                findrules = true;
                                                javascriptRule.Append("Luhn:''");
                                                javascriptMessage.Append(@"Luhn:""" + rule.Rule.Description + @"""");
                                                break;

                                            case MKS.Core.Rule.RuleType.ObjectContainsOneValidProperty:
                                                findrules = true;
                                                javascriptRule.Append("ObjectContainsOneValidProperty:''");
                                                javascriptMessage.Append(@"ObjectContainsOneValidProperty:""" + rule.Rule.Description + @"""");
                                                break;

                                            case MKS.Core.Rule.RuleType.ObjectRequired:
                                                findrules = true;
                                                javascriptRule.Append("ObjectRequired:''");
                                                javascriptMessage.Append(@"ObjectRequired:""" + rule.Rule.Description + @"""");
                                                break;

                                            case MKS.Core.Rule.RuleType.RulePhoneNumberWithExtention:
                                                findrules = true;
                                                javascriptRule.Append("RulePhoneNumberWithExtention:'" + rule.Rule.Parameter + "'");
                                                javascriptMessage.Append(@"RulePhoneNumberWithExtention:""" + rule.Rule.Description + @"""");
                                                break;
                                            case MKS.Core.Rule.RuleType.DecimalMaxNumber:
                                                findrules = true;
                                                javascriptRule.Append("DecimalMaxNumber:'" + rule.Rule.Parameter + "'");
                                                javascriptMessage.Append(@"DecimalMaxNumber:""" + rule.Rule.Description + @"""");
                                                break;
                                            case MKS.Core.Rule.RuleType.DecimalMinNumber:
                                                findrules = true;
                                                javascriptRule.Append("DecimalMinNumber:'" + rule.Rule.Parameter + "'");
                                                javascriptMessage.Append(@"DecimalMinNumber:""" + rule.Rule.Description + @"""");
                                                break;
                                            case MKS.Core.Rule.RuleType.FloatMaxNumber:
                                                findrules = true;
                                                javascriptRule.Append("FloatMaxNumber:'" + rule.Rule.Parameter + "'");
                                                javascriptMessage.Append(@"FloatMaxNumber:""" + rule.Rule.Description + @"""");
                                                break;
                                            case MKS.Core.Rule.RuleType.FloatMinNumber:
                                                findrules = true;
                                                javascriptRule.Append("FloatMinNumber:'" + rule.Rule.Parameter + "'");
                                                javascriptMessage.Append(@"FloatMinNumber:""" + rule.Rule.Description + @"""");
                                                break;
                                            case MKS.Core.Rule.RuleType.RulePostalCodeCAN:
                                                findrules = true;
                                                javascriptRule.Append("RulePostalCodeCAN:'" + rule.Rule.Parameter + "'");
                                                javascriptMessage.Append(@"RulePostalCodeCAN:""" + rule.Rule.Description + @"""");
                                                break;

                                            case MKS.Core.Rule.RuleType.RulePostalCodeUS:
                                                findrules = true;
                                                javascriptRule.Append("RulePostalCodeUS:'" + rule.Rule.Parameter + "'");
                                                javascriptMessage.Append(@"RulePostalCodeUS:""" + rule.Rule.Description + @"""");
                                                break;

                                            case MKS.Core.Rule.RuleType.EmailAddress:
                                                findrules = true;
                                                javascriptRule.Append("EmailAddress:'" + rule.Rule.Parameter + "'");
                                                javascriptMessage.Append(@"EmailAddress:""" + rule.Rule.Description + @"""");
                                                break;

                                            case MKS.Core.Rule.RuleType.RegularExpression:
                                                findrules = true;
                                                javascriptRule.Append("RegularExpression:'" + rule.Rule.Parameter + "'");
                                                javascriptMessage.Append(@"RegularExpression:""" + rule.Rule.Description + @"""");
                                                break;

                                            case MKS.Core.Rule.RuleType.StringRequired:
                                                findrules = true;
                                                javascriptRule.Append("StringRequired:''");
                                                javascriptMessage.Append(@"StringRequired:""" + rule.Rule.Description + @"""");
                                                break;

                                            case MKS.Core.Rule.RuleType.PostalCode:
                                                findrules = true;
                                                javascriptRule.Append("PostalCode:'" + rule.Rule.Parameter + "'");
                                                javascriptMessage.Append(@"PostalCode:""" + rule.Rule.Description + @"""");
                                                break;

                                            case MKS.Core.Rule.RuleType.PhoneNumber:
                                                findrules = true;
                                                javascriptRule.Append(@"PhoneNumber:'" + rule.Rule.Parameter + "'");
                                                javascriptMessage.Append(@"PhoneNumber:""" + rule.Rule.Description + @"""");
                                                break;

                                            case MKS.Core.Rule.RuleType.DateFormat:
                                                findrules = true;
                                                javascriptRule.Append("DateFormat:'" + rule.Rule.Parameter + "'");
                                                javascriptMessage.Append(@"DateFormat:""" + rule.Rule.Description + @"""");
                                                break;

                                            case MKS.Core.Rule.RuleType.DateGreaterOrEqualThan:
                                                findrules = true;
                                                javascriptRule.Append("DateGreaterOrEqualThan:''");
                                                javascriptMessage.Append(@"DateGreaterOrEqualThan:""" + rule.Rule.Description + @"""");
                                                break;

                                            case MKS.Core.Rule.RuleType.DateGreaterThan:
                                                findrules = true;
                                                javascriptRule.Append("DateGreaterThan:''");
                                                javascriptMessage.Append(@"DateGreaterThan:""" + rule.Rule.Description + @"""");
                                                break;

                                            case MKS.Core.Rule.RuleType.DateLessOrEqualThan:
                                                findrules = true;
                                                javascriptRule.Append("DateLessOrEqualThan:''");
                                                javascriptMessage.Append(@"DateLessOrEqualThan:""" + rule.Rule.Description + @"""");
                                                break;

                                            case MKS.Core.Rule.RuleType.DateLessThan:
                                                findrules = true;
                                                javascriptRule.Append("DateLessThan:''");
                                                javascriptMessage.Append(@"DateLessThan:""" + rule.Rule.Description + @"""");
                                                break;

                                            case MKS.Core.Rule.RuleType.DateRequired:
                                                findrules = true;
                                                javascriptRule.Append("DateRequired:''");
                                                javascriptMessage.Append(@"DateRequired:""" + rule.Rule.Description + @"""");
                                                break;

                                            case MKS.Core.Rule.RuleType.IntegerMaxValue:
                                                findrules = true;
                                                javascriptRule.Append("IntegerMaxValue:" + rule.Rule.Parameter);
                                                javascriptMessage.Append(@"IntegerMaxValue:""" + rule.Rule.Description + @"""");
                                                break;


                                            case MKS.Core.Rule.RuleType.StringMaxLength:
                                                findrules = true;
                                                javascriptRule.Append("StringMaxLength:" + rule.Rule.Parameter);
                                                javascriptMessage.Append(@"StringMaxLength:""" + rule.Rule.Description + @"""");
                                                break;

                                            case MKS.Core.Rule.RuleType.StringMinLength:
                                                findrules = true;
                                                javascriptRule.Append("StringMinLength:" + rule.Rule.Parameter);
                                                javascriptMessage.Append(@"StringMinLength:""" + rule.Rule.Description + @"""");
                                                break;

                                            case MKS.Core.Rule.RuleType.IntergerMinValue:
                                                findrules = true;
                                                javascriptRule.Append("IntergerMinValue:" + rule.Rule.Parameter);
                                                javascriptMessage.Append(@"IntergerMinValue:""" + rule.Rule.Description + @"""");
                                                break;
                                        }

                                        if (nb > 1 && findrules)
                                        {
                                            javascriptRule.Append(",");
                                            javascriptMessage.Append(",");
                                        }
                                    }
                                    nb--;
                                }
                                nbass--;
                                if (nbass > 1)
                                {
                                    javascriptRule.Append("},");
                                    javascriptMessage.Append("},");
                                }
                                else
                                {
                                    javascriptRule.Append("}");
                                    javascriptMessage.Append("}");
                                }
                            }
                        }
                    }
                }

                if (javascriptRule.Length > 0 && javascriptMessage.Length > 0)
                {
                    javascriptRule.Append("}");
                    javascriptMessage.Append("}");
                    ViewLogics.AssociateViewToValidationJava = new Tuple<string, string>(javascriptRule.ToString(), javascriptMessage.ToString());
                }
            }
        }
    }
}