using System;

namespace MKS.Core.Presentation
{
    /// <summary>
    ///     Classe qui transporte les paramètres d'un évenement en provenance de l'interface utilisateur.
    /// </summary>
    public class CommandEventArgsCustom : EventArgs
    {
        public CommandEventArgsCustom(object parameter)
        {
            Parameters = parameter;
        }

        public object Parameters { get; set; }
    }
}