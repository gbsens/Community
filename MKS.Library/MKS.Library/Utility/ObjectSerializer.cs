using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;

namespace MKS.Library.Utility
{
    /// <summary>
    /// Permet de sérialiser les objets dans un fichier XML.
    /// </summary>
    /// <typeparam name="TObjectType">Type d'objet a serialiser</typeparam>
    /// <remarks>UseXMLWriter n'est pas encore supporté</remarks>
    public class ObjectSerializer<TObjectType>
    {
        private string FilePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        private string FileName = typeof(TObjectType).Name + ".xml";
        /// <summary>
        /// Constructeur
        /// </summary>
        public ObjectSerializer() { UseXmlWriter = false; }
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="fileName">Nom du fichier</param>
        /// <param name="path">Chemin d'accès du fichier</param>
        public ObjectSerializer(string path,string fileName)
        {
            FilePath = path;
            FileName = fileName;
            UseXmlWriter = false;
        }
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="fileName">Nom du fichier</param>
        public ObjectSerializer(string fileName)
        {
            FileName = fileName;
            UseXmlWriter = false;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="useXmlWriter">Fonctionnalité en béta</param>
        public ObjectSerializer(bool useXmlWriter) { UseXmlWriter = useXmlWriter; }
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="fileName">Nom du fichier</param>
        /// <param name="path">Chemin d'accès du fichier</param>
        /// <param name="useXmlWriter">Fonctionnalité en béta</param>
        public ObjectSerializer(string path, string fileName, bool useXmlWriter)
        {
            FilePath = path;
            FileName = fileName;
            UseXmlWriter = useXmlWriter;
        }
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="fileName">Nom du fichier</param>
        /// <param name="useXmlWriter">Fonctionnalité en béta</param>
        public ObjectSerializer(string fileName,bool useXmlWriter)
        {
            UseXmlWriter = useXmlWriter;
            FileName = fileName;
            UseXmlWriter = useXmlWriter;
        }
        /// <summary>
        /// Permet la sauvegarde des propriété de type Dictionnaire. Par contre, il faut placer les attributs [DataMember] sur les objets.
        /// </summary>
        public bool UseXmlWriter { get; set; }

        public void Save(List<TObjectType> objectType)
        {
            System.Threading.Monitor.Enter(objectType);
            try
            {
                if (UseXmlWriter)
                {
                    var serializer = new DataContractSerializer(typeof(List<TObjectType>));
                    string xmlString = FilePath + "\\" + FileName;

                    using (var writer = new XmlTextWriter(xmlString,null))
                    {
                        writer.Formatting = Formatting.Indented; // indent the Xml so it's human readable
                        serializer.WriteObject(writer, objectType);
                        writer.Flush();

                    }
                    
                }
                else
                {
                    XmlSerializer mySerializer = new XmlSerializer(typeof(List<TObjectType>));
                    StreamWriter myWriter = new StreamWriter(FilePath + "\\" + FileName);
                    mySerializer.Serialize(myWriter, objectType);
                    myWriter.Close();
                }
            }
            finally
            {
                System.Threading.Monitor.Exit(objectType);
            }
        }
        public List<TObjectType> LoadList()
        {
            if (File.Exists(FilePath + "\\" + FileName))
            {
               
                XmlSerializer mySerializer = new XmlSerializer(typeof(List<TObjectType>));
                FileStream myFileStream = new FileStream(FilePath + "\\" + FileName, FileMode.Open);
                List<TObjectType> lobj = (List<TObjectType>)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return (List<TObjectType>)lobj;
                
            }
            else
            {
                return (new List<TObjectType>());
            }
        }
        /// <summary>
        /// Sauvegarde l'objet à serialisé
        /// </summary>
        /// <param name="objectType">Objet a sauvegarder</param>
        public void Save(TObjectType objectType)
        {
            lock (objectType)
            {
                if (UseXmlWriter)
                {
                    var serializer = new DataContractSerializer(typeof(List<TObjectType>));

                    using (var sw = new StringWriter())
                    {
                        using (var writer = new XmlTextWriter(sw))
                        {
                            writer.Formatting = Formatting.Indented; // indent the Xml so it's human readable
                            serializer.WriteObject(writer, objectType);
                            writer.Flush();

                        }
                    }
                }
                else
                {
                    XmlSerializer mySerializer = new XmlSerializer(typeof(TObjectType));
                    StreamWriter myWriter = new StreamWriter(FilePath + "\\" + FileName);
                    mySerializer.Serialize(myWriter, objectType);
                    myWriter.Close();
                }
            }
        }
        public TObjectType Load()
        {
            if (File.Exists(FilePath + "\\" + FileName))
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(TObjectType));
                FileStream myFileStream = new FileStream(FilePath + "\\" + FileName, FileMode.Open);
                TObjectType lobj = (TObjectType)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return (TObjectType)lobj;
            }
            else
            {
                
                return ((TObjectType)Activator.CreateInstance<TObjectType>());
            }
        }
    }
    
}
