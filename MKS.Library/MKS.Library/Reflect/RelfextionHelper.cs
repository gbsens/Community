using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MKS.Library.Reflect
{
    public static class RelfextionHelper
    {
        public static object PrintProperties(object obj, int indent, string propertyToSearch)
        {

            if (obj == null) return obj;
            string indentString = new string(' ', indent);
            Type objType = obj.GetType();
            PropertyInfo[] properties = objType.GetProperties();
            
            foreach (PropertyInfo property in properties)
            {
                object propValue = property.GetValue(obj, null);
                if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string))
                    Console.WriteLine("{0}{1}: {2}", indentString, property.Name, propValue);
                else if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    Console.WriteLine("{0}{1}:", indentString, property.Name);
                    IEnumerable enumerable = (IEnumerable)propValue;
                    foreach (object child in enumerable)
                        return PrintProperties(child, indent + 2, propertyToSearch);
                }
                else
                {
                    Console.WriteLine("{0}{1}:", indentString, property.Name);
                    return PrintProperties(propValue, indent + 2, propertyToSearch);
                }
            }
            return null;
        }
    }

}
