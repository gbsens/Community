using System;

namespace MKS.Core.Model.Error
{

    public abstract class ExceptionProcess : ApplicationException 
    {
        
        public string Message { get; set; }
    }

    /// <summary>
    /// Creer une exception géré.
    /// </summary>
    /// <typeparam name="TProcessResult">Resultat du processus en erreur</typeparam>
    public sealed class ExceptionProcess<TProcessResult> : ExceptionProcess
    {
        private TProcessResult _result ;

        public TProcessResult Results { get { return _result; } }

        public ExceptionProcess(TProcessResult result)
        {
            _result = result;
        }
        public ExceptionProcess(TProcessResult result, string message)
        {
            _result = result;
            
            Message = message;
            
        }
    }
}
