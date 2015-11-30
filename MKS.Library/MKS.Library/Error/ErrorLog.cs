using MKS.Core.Model;
using MKS.Core.Model.Ressources;
using MKS.Library;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace MKS.Library
{
    /// <summary>
    ///   Classe erreur permettant d'encapsuler une erreur et de la publier dans le journal
    ///   d'évenements de windows. Par défaut les événements sont enregistrés dans la
    ///   source du code système. Si la source n'existe pas, l'enregistrement sera
    ///   effectué dans la source MKS.
    /// </summary>
    /// <includesource>yes</includesource>
    public static class ErrorLog
    {
        public const string LIBELLE_DETAIL_EXCEPTION = "Détail Exception : ";
        public const string LIBELLE_SOURCE_EXCEPTION = "Source : ";
        public const string LIBELLE_SEVERITE_EXCEPTION = "Sévérité : {0}";
        public const string DEFAULT_SYSTEM_CODE = "MKS";
        public const string ASPNET_SOURCE = "ASP.NET 4.5";

        public static string _clientSystemCode;
        public static string _clientUserName;
        public static string _clientMachineName;

        #region " Méthodes privées "

        /// <summary>
        ///   Initialise les variables privées
        /// </summary>
        public static void Initialize(IUserEnvironment UserEnvironment)
        {
            try
            {
                _clientSystemCode = UserEnvironment.GetCurrentSystemCode();
            }
            catch
            {
                _clientSystemCode = CoreRessources.EX0008;
            }

            try
            {
                _clientUserName = UserEnvironment.GetUserCode();
            }
            catch
            {
                _clientUserName = CoreRessources.EX0009;
            }

            try
            {
                _clientMachineName = UserEnvironment.GetMachineName();
            }
            catch
            {
                _clientMachineName = CoreRessources.EX0010;
            }
        }

        #endregion " Méthodes privées "

        /// <summary>
        ///   Publie une exception dans le journal d'événements.
        /// </summary>
        /// <param name="exception"> Exception à publier </param>
        public static void PublishExceptionMessage(Exception exception, IUserEnvironment UserEnvironment)
        {
            var sbMessage = new StringBuilder();
            sbMessage.AppendLine(exception.Message);
            sbMessage.AppendLine();
            Initialize(UserEnvironment);
            sbMessage.AppendLine(CoreRessources.EX0011 + _clientSystemCode);
            sbMessage.AppendLine(CoreRessources.EX0012 + _clientUserName);
            sbMessage.AppendLine(CoreRessources.EX0013 + _clientMachineName);
            sbMessage.AppendLine();

            if (string.IsNullOrEmpty(exception.Source))
                sbMessage.AppendLine(LIBELLE_SOURCE_EXCEPTION + Assembly.GetCallingAssembly().FullName);
            else
                sbMessage.AppendLine(LIBELLE_SOURCE_EXCEPTION + exception.Source);

            sbMessage.AppendLine();
            sbMessage.AppendLine(LIBELLE_DETAIL_EXCEPTION);
            sbMessage.AppendLine(exception.ToString());

            sbMessage.AppendLine();
            sbMessage.AppendFormat(LIBELLE_SEVERITE_EXCEPTION, EventLogEntryType.Error);
            sbMessage.AppendLine();

            StackTrace stackTrace = new StackTrace();
            sbMessage.AppendLine(stackTrace.ToString());

            Publish(sbMessage.ToString(), _clientSystemCode);
        }

        /// <summary>
        ///   Permet de publier un message dans le journal d'événements de Windows ou dans un fichier.
        /// </summary>
        /// <param name="message"> Le message à publiber </param>
        /// <param name="source"> La source dans laquelle publier </param>
        public static void Publish(string message, string source)
        {
            try
            {
                //Étant donné qu'on ne peut pas vérifier si une source est présente ou non dans l'TActivityLog sans des droits de plus haut niveau,
                //on essait d'écrire malgré tout. Si ça ne marche pas, on "catch" l'exception et on tente d'inscrire dans la source MKS
                //qui devrait normalement avoir été créée à l'installation du framework.
                try
                {
                    if (System.Diagnostics.EventLog.Exists(DEFAULT_SYSTEM_CODE))
                    {
                        if (!System.Diagnostics.EventLog.SourceExists(source))
                        {
                            System.Diagnostics.EventLog.CreateEventSource(source, DEFAULT_SYSTEM_CODE);
                        }
                        var evlog = new System.Diagnostics.EventLog { Log = DEFAULT_SYSTEM_CODE, Source = source };

                        evlog.WriteEntry(message, EventLogEntryType.Error);
                    }
                    else
                    {
                        System.Diagnostics.EventLog.WriteEntry(DEFAULT_SYSTEM_CODE, message, EventLogEntryType.Error);
                    }
                }
                catch (Exception)
                {
                    System.Diagnostics.EventLog.WriteEntry(DEFAULT_SYSTEM_CODE, message, EventLogEntryType.Error);
                }
            }
            catch (Exception exceptionLogging)
            {
                //Si la source MKS n'existe pas elle non plus (ce qui ne devrait pas arriver), on crée une exception qui va englober le
                //détails des deux exception et publier dans la source par défaut.
                var ex = new Exception(message, exceptionLogging);
                System.Diagnostics.EventLog.WriteEntry(ASPNET_SOURCE, ex.ToString(), EventLogEntryType.Error);
            }
        }

   
    }
}