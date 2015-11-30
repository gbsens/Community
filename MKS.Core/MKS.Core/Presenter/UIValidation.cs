
using System;
using System.Linq.Expressions;
using MKS.Core;
using MKS.Library;

namespace MKS.Core.Presentation
{
    public class UIVAlidation
    {
    }

    public class UIValidation<TViewBase> : UIVAlidation
    {
        public string ObjectPropertyName { get; set; }
        public IValidation<TViewBase> Validations { get; set; }
        public string ViewPropertyName { get; set; }
        public string ViewName { get; set; }

        public RuleResults Validate(TViewBase objectInstance)
        {
            //TViewBase obj = Activator.CreateInstance<TViewBase>();
            //Extraire la valeur de la view par invocation et l'assiner a obj. 

            var val = Validations as IValidationExtend<TViewBase>;
            var rs = new RuleResults();

            var r = ValidationCore.DoValidation(objectInstance, Validations);
            foreach (var item in r)
            {
                if (ObjectPropertyName == item.Property)
                {
                    //indique le lien avec la vue en erreur
                    item.BindObjectName = ViewName;
                    item.BindPropertytName = ObjectPropertyName;

                    rs.Add(item);
                }
            }
            return rs;
        }
    }

    public class UIValidation<TViewBase, TValidation, TView> : UIValidation<TViewBase>
        where TValidation : IValidation<TViewBase>, new()
    {
        public UIValidation(Expression<Func<TViewBase, object>> property, Expression<Func<TView, object>> propertyView)
        {
            ValidateAssociation(property, propertyView);
        }

        private void ValidateAssociation(Expression<Func<TViewBase, object>> property,
            Expression<Func<TView, object>> propertyView)
        {
            //Validations = validation;
            Validations = new TValidation();

            ObjectPropertyName = Reflect<TViewBase>.GetName(property);
            foreach (var item in Validations.GetRules())
            {
                ViewName = typeof (TView).Name;
                //item.BindingPropertyName = Reflect<TView>.GetName(propertyView);  
                //string p = ViewPropertyName.Substring(propertyView.Body.ToString().IndexOf("."), propertyView.Body.ToString().Length - 1);
                //p = p.Substring(1, p.Length - 1);
                ViewPropertyName = propertyView.Body.ToString();
            }
        }
    }
}