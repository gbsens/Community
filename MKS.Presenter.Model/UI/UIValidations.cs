using System.Collections.Generic;
using MKS.Core;

namespace MKS.Core.Presenter.UI
{
    public class UIValidations
    {
        private List<UIVAlidation> _validations = new List<UIVAlidation>();

        public List<UIVAlidation> UI
        {
            get { return _validations; }
            set { _validations = value; }
        }

        /// <summary>
        ///     Permet de valider les objet
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public RuleResults Validate<TObject>(TObject instance)
        {
            var rls = new RuleResults();
            foreach (var item in UI)
            {
                var rl = new RuleResults();

                var val = item as UIValidation<TObject>;
                if (val!=null)
                    rl = val.Validate(instance);
                if (rl != null) rls.Add(rl);
            }
            return rls;
        }
    }
}