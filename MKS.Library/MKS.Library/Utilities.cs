using MKS.Core.Model;
using MKS.Core.Model.Ressources;
using MKS.Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace MKS.Core
{
    /// <summary>
    ///   Méthode utilitaire pour les règles de validations
    /// </summary>
    public static class Utilities
    {
        private const int DEFAULT_CACHE_DURATION = 20;

        /// <summary>
        ///   Permet de changer le language de l'utilisateur. Affecte le MKS.Core.UserEnvironment.
        /// </summary>
        /// <param name="language"> Code de la culture. </param>
        public static void SetCurrentCulture(string language)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
        }

        /// <summary>
        ///   Permet de changer la language de l'utilisateur. Affecte le MKS.Core.UserEnvironment.
        /// </summary>
        /// <param name="language"> Information de culture. </param>
        public static void SetCurrentCulture(CultureInfo language)
        {
            Thread.CurrentThread.CurrentCulture = language;
        }

        /// <summary>
        ///   Converti un objet de type RulesResults en objet de type ProcessResults
        /// </summary>
        /// <param name="results"> Objet de type RuleResults à convertir. </param>
        /// <returns> Retourne l'objet de type ProcessResults converti. </returns>
        public static ProcessResults ConvertRuleResultsToProcessResults(RuleResults results)
        {
            var processresults = new ProcessResults();

            foreach (var objRuleResultTemp in results)
            {
                var r = new ReturnMessage(TypeError.ValidationObjet,
                                          TranslateSeverity(objRuleResultTemp.RuleInformation.Severity),
                                          objRuleResultTemp.RuleInformation.CodeMessage,
                                          objRuleResultTemp.RuleInformation.Description,
                                          false,
                                          objRuleResultTemp.Property) { ObjectName = objRuleResultTemp.ObjectName, ObjectPropertyName = objRuleResultTemp.Property };
                processresults.AddException(r);
            }
            return (processresults);
        }
        
        /// <summary>
        ///   Converti un objet de type RuleResults en une liste de message de type ReturnMessage.
        /// </summary>
        /// <param name="results"> Objet de type RuleResults à convertir. </param>
        /// <returns> Retourne une liste de messages de type ReturnMessage. </returns>
        public static List<ReturnMessage> ConvertRuleResultsToReturnMessageList(RuleResults results)
        {
            if (results != null)
            {
                List<ReturnMessage> returnmessagelist =
                    results.Select(
                        objRuleResultTemp =>
                        new ReturnMessage(TypeError.ValidationObjet,
                                          TranslateSeverity(objRuleResultTemp.RuleInformation.Severity),
                                          objRuleResultTemp.RuleInformation.CodeMessage,
                                          objRuleResultTemp.RuleInformation.Description, false,
                                          objRuleResultTemp.Property)
                        {
                            ObjectName = objRuleResultTemp.ObjectName,
                            ObjectPropertyName = objRuleResultTemp.Property,
                            RuleType = objRuleResultTemp.RuleInformation.Type
                        }).ToList();

                return (returnmessagelist);
            }
            return null;
        }
        
        /// <summary>
        ///   Copie un stream dans un autre stream. Utilisé pour transférer des données réseaux (input) vers la mémoire locale (output).
        /// </summary>
        /// <param name="input"> Stream d'entrée </param>
        /// <param name="output"> Stream de sortie </param>
        public static void CopyTo(Stream input, Stream output)
        {
            const int bufferSize = 4096;
            var buffer = new byte[bufferSize];
            int bytes;

            while ((bytes = input.Read(buffer, 0, bufferSize)) > 0)
            {
                output.Write(buffer, 0, bytes);
            }
        }

        /// <summary>
        ///   Permet de transformer un objet Datacontract en stream de mémoire XML.  Le stream généré est au moins 33% plus gros que
        ///   le stream créé par GetBinaryStreamFromObject(object serializableObject)
        /// </summary>
        /// <remarks>
        ///   Si la taille du stream est importante il est préférable de rendre l'objet Serializable et d'utiliser la méthode
        ///   GetSerializableObjectStream.
        /// </remarks>
        /// <typeparam name="DatacontracObject"> Type de l'object Datacontract </typeparam>
        /// <param name="datacontracObject"> Objet à transformer en stream </param>
        /// <returns> Un stream Mémoire de l'objet fourni </returns>
        public static MemoryStream GetDatacontractStreamFromObject<DatacontracObject>(
            DatacontracObject datacontracObject)
        {
            var memoryStreamObject = new MemoryStream();
            var dcSerializer = new DataContractSerializer(typeof(DatacontracObject));
            dcSerializer.WriteObject(memoryStreamObject, datacontracObject);
            //Mettre la position du stream à 0 pour que la lecture soit possible
            memoryStreamObject.Position = 0;
            return memoryStreamObject;
        }

        /// <summary>
        ///   Transforme le stream Datacontract d'un objet en ce dernier.
        ///   Cette méthode permet d'inverser le travail effectué par GetDatacontractStreamFromObject&lt;DatacontracObject&gt;(DatacontracObject datacontracObject)
        /// </summary>
        /// <typeparam name="DatacontracObject"> Type de l'objet dans le Stream. </typeparam>
        /// <param name="datacontractStream"> Stream de l'objet à désérialiser. </param>
        /// <returns> L'objet contenu dans le stream fourni </returns>
        public static DatacontracObject GetObjectFromDatacontractStream<DatacontracObject>(Stream datacontractStream)
        {
            var dcSerializer = new DataContractSerializer(typeof(DatacontracObject));
            //On doit mettre la position du stream à 0 pour pouvoir le lire
            if (datacontractStream.Position != 0 && datacontractStream.CanSeek)
                datacontractStream.Position = 0;
            var returnObject = (DatacontracObject)dcSerializer.ReadObject(datacontractStream);
            return returnObject;
        }

        /// <summary>
        ///   Transforme un objet marqué Serializable en Stream binaire
        /// </summary>
        /// <param name="serializableObject"> Objet Serializable </param>
        /// <returns> Un stream Mémoire de l'objet fourni </returns>
        public static MemoryStream GetBinaryStreamFromObject(object serializableObject)
        {
            var memoryStreamObject = new MemoryStream();
            var bf = new BinaryFormatter();
            bf.Serialize(memoryStreamObject, serializableObject);
            memoryStreamObject.Position = 0;
            return memoryStreamObject;
        }

        /// <summary>
        ///   Transforme un stream binaire Serializable en objet.
        ///   Cette méthode permet d'inverser le travail effectué par GetBinaryStreamFromObject(object serializableObject)
        /// </summary>
        /// <param name="binaryStream"> Stream de l'objet à désérialiser. </param>
        /// <returns> L'objet contenu dans le stream fourni. </returns>
        public static object GetObjectFromBinaryStream(Stream binaryStream)
        {
            var bf = new BinaryFormatter();
            //On doit mettre la position du stream à 0 pour pouvoir le lire
            if (binaryStream.Position != 0 && binaryStream.CanSeek)
                binaryStream.Position = 0;
            return bf.Deserialize(binaryStream);
        }

        /// <summary>
        ///   Permet un appel générique de méthodes ou de propriétés d'un objet
        ///   sans que la structure de celui-ci ne soit connue.
        /// </summary>
        /// <param name="target"> Objet dont on veut lire ou éditer une méthode ou une propriété. </param>
        /// <param name="methodName"> Nom de la propriété ou de la méthode. On peut utiliser le séparateur '.' afin d'atteindre la propriété d'un sous objet. </param>
        /// <param name="callType"> Spécifie comment invoquer la méthode (lecture ou ecriture). </param>
        /// <param name="args"> Liste des arguments à passer dans la méthode. </param>
        /// <returns> Résultat de la propriété ou de l'invocation de la méthode. </returns>
        public static object CallByName(object target, string methodName, CallType callType, params object[] args)
        {
            string[] sousObjets = methodName.Split('.');

            try
            {
                if (sousObjets.Length <= 1)
                {
                    switch (callType)
                    {
                        case CallType.Get:
                            {
                                try
                                {
                                    PropertyInfo p = target.GetType().GetProperty(methodName);
                                    return p.GetValue(target, args);
                                }
                                catch
                                {
                                    FieldInfo f = target.GetType().GetField(methodName);
                                    return f.GetValue(target);
                                }
                            }
                        case CallType.Set:
                            {
                                try
                                {
                                    PropertyInfo p = target.GetType().GetProperty(methodName);
                                    p.SetValue(target, args[0], null);
                                }
                                catch
                                {
                                    FieldInfo f = target.GetType().GetField(methodName);
                                    f.SetValue(target, args[0]);
                                }
                                return null;
                            }
                        case CallType.Method:
                            {
                                MethodInfo m = target.GetType().GetMethod(methodName);
                                return m.Invoke(target, args);
                            }
                    }
                }
                else
                {
                    for (int i = 0; i < sousObjets.Length - 1; i++)
                    {
                        //Appeler la méthode jusqu'à ce que l'on atteingne la dernière propriété
                        target = CallByName(target, sousObjets[i], CallType.Get);
                    }
                    return CallByName(target, sousObjets[sousObjets.Length - 1], callType, args);
                }
                return null;
            }
            catch (MissingMemberException ex)
            {
                //La méthode CallByName a lancé une exception MissingMember dans sa récursive.
                throw new MissingMemberException(string.Format(CoreRessources.EX0027, methodName), ex);
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException != null) //remet l'erreur dans l'erreur initiale
                    throw ex.InnerException;
            }
            return (null);
        }

        /// <summary>
        ///   Traduit un Rule.RuleSeverity en un Severity.
        /// </summary>
        /// <param name="ruleSeverity"> Valeur de type Rule.RuleSeverity à traduire. </param>
        /// <returns> Severity traduit </returns>
        public static Severity TranslateSeverity(Rule.RuleSeverity ruleSeverity)
        {
            if (ruleSeverity == Rule.RuleSeverity.Error)
                return Severity.Error;
            if (ruleSeverity == Rule.RuleSeverity.Information)
                return Severity.Information;
            return Severity.Warning;
        }

        /// <summary>
        ///   Traduit un Severity en un Rule.RuleSeverity
        /// </summary>
        /// <param name="messageSeverity"> Valeur de type Severity à traduire. </param>
        /// <returns> Rule.RuleSeverity traduit </returns>
        public static Rule.RuleSeverity TranslateSeverity(Severity messageSeverity)
        {
            if (messageSeverity == Severity.Error)
                return Rule.RuleSeverity.Error;
            if (messageSeverity == Severity.Information)
                return Rule.RuleSeverity.Information;
            return Rule.RuleSeverity.Warning;
        }

        /// <summary>
        ///   Convertis en string la liste des propriétés d'un objet.
        /// </summary>
        /// <param name="p_object"> Objet à convertir. </param>
        /// <returns> String contenant la liste des propriétés. </returns>
        public static string GetPropertiesStringFromObject(Object p_object)
        {
            var PropertiesString = new StringBuilder();
            if (p_object != null)
            {
                PropertyInfo[] propertyInfos = p_object.GetType().GetProperties();

                foreach (var pInfo in propertyInfos)
                {
                    PropertiesString.Append(pInfo.Name);
                    PropertiesString.Append(":");
                    if (pInfo.GetValue(p_object, null) != null)
                    {
                        PropertiesString.Append(pInfo.GetValue(p_object, null));
                    }
                    else
                    {
                        PropertiesString.Append("null");
                    }
                    PropertiesString.Append(", ");
                }
            }
            return PropertiesString.ToString();
        }

        public static string GetResourceLookup(Type resourceType, string resourceName)
        {
            if ((resourceType != null) && (resourceName != null))
            {
                PropertyInfo property = resourceType.GetProperty(resourceName, BindingFlags.Public | BindingFlags.Static);
                if (property == null)
                {
                    throw new InvalidOperationException(string.Format(CoreRessources.EX0029));
                }
                if (property.PropertyType != typeof(string))
                {
                    throw new InvalidOperationException(string.Format(CoreRessources.EX0030));
                }
                return (string)property.GetValue(null, null);
            }
            return null;
        }

        /// <summary>
        /// Get the name of the calling function
        /// </summary>
        /// <returns></returns>
        public static string GetCallingMethodName()
        {
            string name = string.Empty;

            try
            {
                StackTrace stackTrace = new StackTrace();
                StackFrame stackFrame = stackTrace.GetFrame(3);
                MethodBase methodBase = stackFrame.GetMethod();
                name = methodBase.Name;
            }
            catch (Exception)
            {
                name = "Unknown";
            }

            return name;
        }

        /// <summary>
        /// Permet de savoir si le caching est activé
        /// </summary>
        /// <returns>True s'il est activé, false dans le cas contraire</returns>
        public static bool IsCachingActivated(IUserEnvironment UserEnvironment)
        {
            //Si le caching n'est pas désactivé via le fichier de configuration
            string cachingValue = ConfigurationManager.AppSettings[CoreRessources.CACHING];
            if (string.IsNullOrEmpty(cachingValue) || !cachingValue.Equals(false.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                //Si le DebugMode est activé, cela désactive automatiquement le caching
                return !UserEnvironment.IsDebugModeActivated();
            }
            else
                return false;
        }

        /// <summary>
        /// Retourne la durée avant l'expiration de la cache en minutes.
        /// </summary>
        /// <returns>On retourne la valeur de la clé DefaultCacheDuration si elle est spécifiée. Sinon on retourne 20 minutes.</returns>
        public static int GetCacheDuration()
        {
            int cacheExpiration = 0;

            //Si la valeur a été spécifiée dans le fichier de configuration
            string cacheExpirationSetting = ConfigurationManager.AppSettings[CoreRessources.CACHE_DURATION];

            if (!string.IsNullOrEmpty(cacheExpirationSetting) && int.TryParse(cacheExpirationSetting, out cacheExpiration))
                return cacheExpiration;
            else
                return DEFAULT_CACHE_DURATION;
        }

        /// <summary>
        /// Permet de savoir si le DebugMode est activé
        /// </summary>
        /// <returns>True s'il est activé, false dans le cas contraire</returns>
        public static bool IsDebugModeActivated()
        {
            string debugModeValue = ConfigurationManager.AppSettings[CoreRessources.DEBUG_MODE];
            if (!string.IsNullOrEmpty(debugModeValue) && debugModeValue.Equals(true.ToString(), StringComparison.OrdinalIgnoreCase))
                return true;
            else
                return false;
        }

        public static bool IsByPassRoutingActivated()
        {
            string byPassRoutingValue = ConfigurationManager.AppSettings[CoreRessources.BYPASS_ROUTING];
            if (!string.IsNullOrEmpty(byPassRoutingValue) && byPassRoutingValue.Equals(true.ToString(), StringComparison.OrdinalIgnoreCase))
                return true;
            else
                return false;
        }
    }

    /// <summary>
    ///   Options d'appel de méthodes ou de propriétés d'un objet.
    /// </summary>
    /// <remarks>
    ///   Cette énumération est notamment utilisée avec la fonction CallByName.
    /// </remarks>
    /// <seealso>Systeme.Utilities.CallByName
    ///   <cref>M:MKS.Core.Utilities.CallByName(System.ObjectType,System.String,MKS.Systeme.CallType,System.ObjectType[])</cref>
    /// </seealso>
    public enum CallType
    {
        /// <summary>
        ///   Gets a value from a property.
        /// </summary>
        Get,

        /// <summary>
        ///   Invokes a method.
        /// </summary>
        Method,

        /// <summary>
        ///   Sets a value into a property.
        /// </summary>
        Set
    }
}