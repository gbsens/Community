using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKS.Core.Utility
{
    public static class Reflection
    {
        /// <summary>
        /// Lis les propriété d'un object
        /// </summary>
        /// <typeparam name="T">Type de l'objet</typeparam>
        /// <param name="obj">Instance de l'objet</param>
        /// <returns>Liste de propiétés</returns>
        public static IEnumerable<KeyValuePair<string, T>> PropertiesOfType<T>(object obj)
        {
            return from p in obj.GetType().GetProperties() 
                   where p.PropertyType == typeof(T)
                   select new KeyValuePair<string, T>(p.Name, (T)p.GetValue(obj));
        }
    }
}
