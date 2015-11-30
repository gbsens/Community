using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using MKS.Core;
using MKS.Core.Security;
using System.Collections.Generic;
using MKS.Library;

namespace MKS.Core.Presentation
{
    /// <summary>
    ///     Calsse qui défini les actions à faire sur le formulaire qui implémante la vue
    /// </summary>
    /// <typeparam name="TView"> Instace de la vue implementé dans le formulaire </typeparam>
    public abstract class ViewProcess<View> : IViewProcess<View>
        where View : IViewBase
    {
        /// <summary>
        ///     Initialise la page au démarrage du présenteur
        /// </summary>
        /// <param name="view"> Instance de la vue implémenté dans le formulaire </param>
        /// <param name="presenterBase"> </param>
        public virtual void OnInitializeDisplay(View view, IPresenterBase presenterBase,
            IApplicationAuthorization authorization)
        {
            view.InitializeDisplay();
        }

        /// <summary>
        ///     A l'initialisation du présenteur, le PresenterLogic appel le Service Manager pour retrouver l'information sur le
        ///     Système en soit.
        /// </summary>
        /// <param name="view"> Instance de la vue implémenté dans le formulaire </param>
        /// <param name="SystemStatus"> Dictionnaire d'information du status du système </param>
        /// <param name="presenterBase"> </param>
        public virtual void ShowSystemStatus(View view, Dictionary<string, string> SystemStatus, IPresenterBase presenterBase)
        {
            view.ShowSystemStatus(SystemStatus);
        }

        /// <summary>
        ///     Fonction de commande en provenance de l'interface utilisateur.
        ///     <para> </para>
        ///     <para>
        ///         Par défaut, recherche, dans la classe de processus, une méthode ayant le nom de la commande et ayant
        ///         l'argument, la vue et le presentateur comme arguments. Si cette méthode est trouvée, elle est appelée.
        ///     </para>
        ///     <para> </para>
        ///     <para> Faire un override de cette méthode si un comportement autre est nécessaire. </para>
        /// </summary>
        /// <param name="command"> Commande à traiter </param>
        /// <param name="args"> Argument de la commande </param>
        /// <param name="view"> Référence de la vue implémenter par le formulaire </param>
        /// <param name="presenterBase"> Référence du présenteur </param>
        public virtual void OnCommand(string command, CommandEventArgsCustom args, View view,
            IPresenterBase presenterBase)
        {
            if (command!=null)
            {
                var mi = GetType().GetMethod(command, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (mi != null)
                {
                    var pi = mi.GetParameters();

                    if (pi.Length == 3 &&
                        pi[0].ParameterType == typeof(CommandEventArgsCustom) &&
                        pi[1].ParameterType == typeof(View) &&
                        pi[2].ParameterType == typeof(IPresenterBase))
                        mi.Invoke(this, new object[] { args, view, presenterBase });
                }
            }
            else
            {
                PublishException(view, "ERREUR DESIGN : La commande pour effectuer l'action est null", null);
            }
            
            
        }

        public virtual void OnCommand<TInputParameter>(string command, TInputParameter inputparameter, View view,
            IPresenterBase presenterBase)
        {
            var mi = GetType().GetMethod(command, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (mi != null)
            {
                mi.Invoke(this, new object[] {inputparameter, view, presenterBase});
            }
        }

        /// <summary>
        ///     Fonction executé lorsqu"une erreur de réservation survient.
        /// </summary>
        /// <param name="view"> Référence de la vue implémenter par le formulaire </param>
        /// <param name="message"> Message de la réservation </param>
        /// <param name="presenterBase"> Référence du présenteur </param>
        public virtual void OnReservedEvent(View view, string message, IPresenterBase presenterBase)
        {
            view.ShowConcurrencyMessage(message);
        }

        /// <summary>
        ///     Fonction exécuter lorsque des erreurs de validations d'affaires survient.
        /// </summary>
        /// <param name="view"> Référence de la vue implémenter par le formulaire </param>
        /// <param name="formatedMessage"> Message d'erreur pré-formaté </param>
        /// <param name="returnMessageList"> Liste des erreurs </param>
        /// <param name="presenterBase"> Référence du présenteur </param>
        public virtual void PublishBusinessValidations(View view, string formatedMessage,
            ProcessResults rules,
            IPresenterBase presenterBase)
        {
            //ProcessResults pr = new ProcessResults();
            //foreach (var item in rules)
            //{
            //    pr.AddException(item);
            //}
            view.ShowBusinessValidationMessage(PresenterResources.ERR_BUSINESSVALIDATION, rules);
        }

        public virtual void PublishIntegrityValidations(View view, string formatedMessage,
            RuleResults rules,
            IPresenterBase presenterbase)
        {
            view.ShowValidationMessage(PresenterResources.ERR_VALIDATION, formatedMessage, rules);
        }

        public virtual void PublishException(View view, string formatedMessage, Exception exception)
        {
            ErrorLog.PublishExceptionMessage(exception, Globals.GetUserEnvironment);
            view.ShowMessage(PresenterResources.ERR_EXCEPTION, formatedMessage, Severity.Error);
        }

        public virtual void OnSecurityAccessDenied(View view, IPresenterBase presenterbase, ReturnMessage returnMessage)
        {
            var url = ConfigurationManager.AppSettings["SecurityNoAccess"];
            if (url != null)
                view.SetSecurityNoAccess(url);
            else
            {
                view.ShowMessage(returnMessage.CodeMessage, returnMessage.Description, Severity.Error);
            }
        }
    }
}