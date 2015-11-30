using System;
using System.Collections.Specialized;
using MKS.Core;
using MKS.Core.Security;
using System.Collections.Generic;

namespace MKS.Core.Presentation
{
    /// <summary>
    ///     Interface qui définit les fonctionnalités de base d'un TProcessView.
    /// </summary>
    /// <typeparam name="TView"> Vue associée au presenter. </typeparam>
    public interface IViewProcess<in View>
        where View : IViewBase
    {
        void OnInitializeDisplay(View view, IApplicationAuthorization authorization);

        void ShowSystemStatus(View view, Dictionary<string, string> SystemStatus);

        void PublishBusinessValidations(View view, string formatedMessage, ProcessResults rules,
            IPresenterBase presenterBase);



        void OnCommand(string command, CommandEventArgsCustom args, View view);

        void OnCommand<TInputParameter>(string command, TInputParameter inputparameter, View view,
            IPresenterBase presenterBase);



        void OnReservedEvent(View view, string message);

        void PublishIntegrityValidations(View view, string formatedMessage, RuleResults rules,
            IPresenterBase presenterBase);

        void PublishException(View view, string formatedMessage, Exception exception);
        void OnSecurityAccessDenied(View view, ReturnMessage returnMessage);
    }
}