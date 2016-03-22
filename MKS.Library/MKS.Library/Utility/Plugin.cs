using MKS.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;

namespace MKS.Core
{
    /// <summary>
    /// Permet de charger un et des DLL dynamiquement
    /// </summary>
    /// <typeparam name="IPlugin">Interface à charger</typeparam>
    public static class Plugin<IPlugin> 
    {

        public static IPlugin LoadSingle()
        {
            ICollection<IPlugin> col = Plugin<IPlugin>.Load();
            if (col != null && col.Count > 0)
            {
                IEnumerator en = col.GetEnumerator();
                en.MoveNext();
                return (IPlugin)en;
            }
            return default(IPlugin);
            
        }

        public static ICollection<IPlugin> Load()
        {
            ////return Load(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location ));
            //if (HttpContext.Current != null)
            //    return Load(HttpContext.Current.Server.MapPath("~") + "bin");
            //else
                return Load(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

        }
        /// <summary>
        /// Charge un DLL dynamiquement
        /// </summary>
        /// <param name="path">chemins d'Accès du dll a charger</param>
        /// <returns>une liste de dll chargé</returns>
        public static ICollection<IPlugin> Load(string path) 
        {
            string[] dllFileNames = null;

            if (Directory.Exists(path))
            {
                dllFileNames = Directory.GetFiles(path, "*.dll");

                ICollection<Assembly> assemblies = new List<Assembly>(dllFileNames.Length);
                foreach (string dllFile in dllFileNames)
                {
                    AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                    Assembly assembly = Assembly.Load(an);
                    assemblies.Add(assembly);
                }

                Type pluginType = typeof(IPlugin);
                ICollection<Type> pluginTypes = new List<Type>();
                foreach (Assembly assembly in assemblies)
                {
                    if (assembly != null)
                    {
                        try
                        {
                            Type[] types = assembly.GetTypes();

                            foreach (Type type in types)
                            {
                                if (type.IsInterface || type.IsAbstract)
                                {
                                    continue;
                                }
                                else
                                {
                                    if (pluginType.IsAssignableFrom(type))
                                    {
                                        pluginTypes.Add(type);
                                    }
                                }
                            }
                        }
                        catch (Exception x)
                        {
                            //TODO: valider pourquoi il y a une erreur lisant le GetTypes();
                            ErrorLog.Publish(x.Message, "PluginLoder");
                            continue;
                        }
                    }
                }

                ICollection<IPlugin> plugins = new List<IPlugin>(pluginTypes.Count);
                foreach (Type type in pluginTypes)
                {
                    IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                    plugins.Add(plugin);
                }

                return plugins;
            }

            return null;
        }
    }
}