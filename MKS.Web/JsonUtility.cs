using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace MKS.Core.Presenter
{
    /// <summary>
    ///     Classe utilitaire pour la manipulation d'objet Json
    /// </summary>
    public static class JsonUtility
    {
        /// <summary>
        ///     Transforme un objet en Json
        /// </summary>
        /// <typeparam name="TBusinessObject">Type de l'objet a transformer</typeparam>
        /// <param name="obj">Instance de l'objet</param>
        /// <returns>l'objet Json</returns>
        public static string Serialize<T>(T obj)
        {
            var serializer = new DataContractJsonSerializer(obj.GetType());
            var ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            var retVal = Encoding.UTF8.GetString(ms.ToArray());
            return retVal;
        }

        /// <summary>
        ///     Transforme un json object en objet
        /// </summary>
        /// <typeparam name="TBusinessObject">Type de l'objet</typeparam>
        /// <param name="json">Objjet json</param>
        /// <returns>Instance de l'objet transformé</returns>
        public static T Deserialize<T>(string json)
        {
            var obj = Activator.CreateInstance<T>();
            var ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            var serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T) serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }
    }
}