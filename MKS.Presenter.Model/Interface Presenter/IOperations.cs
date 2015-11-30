
using MKS.Core;
using MKS.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

//Implémenter par le processus d'un vue.
namespace MKS.Core.Presenter.Interfaces
{
    public interface IOperationBase<TView> where TView : IView
    {
        //Permet au processus de vue de recevoir les commandes à partir de l'interface utilisateur.
        void OnCommand(string command, CommandEventArgsCustom args, TView view, IPresenter presenter);

        //Permet au precessus de vue d'agire sur l'affichage avant d'envoyer à l'interface utilisateur.
        void ShowContextValidation(string title, string message, List<ReturnMessage> result);
        void ShowBusinessValidation(string title, string message, ProcessResults processResults);
        void ShowMessage(string title, string message, Severity severity);

        //Meme si les processus de vue ne gere pas la securité ou la reservation, les messages peuvent surgirent de la couche métier.
        void ShowSecurity(string title, string message, ProcessResults processResults);
        void ShowReservation(string title, string message, ProcessResults processResults);

        void AssignView(TView view);
    }

    public interface IOperation<TView> : IOperationBase<TView> where TView : IView
    {
        [OperationContract]
        void Initialisation(bool isInit, TView view, IPresenter presenter);
        

    }
    public interface IOperationSecurity<TView> : IOperationBase<TView> where TView : IView 
    {
        [OperationContract]
        void Initialisation(bool isInit, TView view, IPresenter presenter, IApplicationAuthorization authorization);        

    }
    
    public interface IOperationReservation<TView,TReservationAdapter> : IOperationBase<TView> 
        where TView : IView
        where TReservationAdapter:IConcurrencyAdapter
    {
        [OperationContract]
        void ReservationValidation(TView view, IPresenter presenter,TReservationAdapter reservationAdapter);
        
        [OperationContract]
        void Initialisation(TView view, IPresenter presenter);
    }

    public interface IOperationReservationSecurity<TView, TReservationAdapter, TSecurityAdapter> : IOperationBase<TView>
        where TView : IView
        where TReservationAdapter : IConcurrencyAdapter
        where TSecurityAdapter : ISecurityAdapter
    {
        [OperationContract]
        void ReservationValidation(bool isInit, TView view, IPresenter presenter, TReservationAdapter reservationAdapter);
        
        [OperationContract]
        void Initialisation(bool isInit, TView view, IPresenter presenter, IApplicationAuthorization authorization, TSecurityAdapter securityAdapter);
    }
}
