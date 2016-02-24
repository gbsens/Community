using MKS.Library;
using System;
using System.Linq.Expressions;
using MKS.Core;

namespace MKS.Core.Presenter.UI
{
    public class UIVAlidation
    {
    }

    public class UIValidation<TObject> : UIVAlidation
    {
        public string ObjectPropertyName { get; set; }
        public IValidation<TObject> Validations { get; set; }
        public string ViewPropertyName { get; set; }
        public string ViewName { get; set; }
        public ValidationRules ValidationRules{ get; set; }

        public RuleResults Validate(TObject objectInstance)
        {
            //TObject obj = Activator.CreateInstance<TObject>();
            //Extraire la valeur de la view par invocation et l'assiner a obj. 

            var val = Validations as IValidationExtend<TObject>;
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

    public class UIValidation<TObject, TValidation, TView> : UIValidation<TObject>
        where TValidation : IValidation<TObject>, new()
    {
        public UIValidation(Expression<Func<TObject, object>> property, Expression<Func<TView, object>> propertyView)
        {
            ValidateAssociation(property, propertyView);
        }

        private void ValidateAssociation(Expression<Func<TObject, object>> property,
            Expression<Func<TView, object>> propertyView)
        {
            //Validations = validation;
            Validations = new TValidation();
            ObjectPropertyName = Reflect<TObject>.GetName(property);
            ViewName = typeof(TView).Name;
            ViewPropertyName = propertyView.Body.ToString();
            ValidationRules =  Validations.GetRules();

            //foreach (var item in Validations.GetRules())
            //{
            //    ViewName = typeof (TView).Name;
            //    //item.BindingPropertyName = Reflect<TView>.GetName(propertyView);  
            //    //string p = ViewPropertyName.Substring(propertyView.Body.ToString().IndexOf("."), propertyView.Body.ToString().Length - 1);
            //    //p = p.Substring(1, p.Length - 1);
            //    ViewPropertyName = propertyView.Body.ToString();
                
            //}
        }
    }
}