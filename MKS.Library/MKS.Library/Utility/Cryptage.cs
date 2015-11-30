using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace MKS.Library.Utility
{
    /// <summary>
    /// Classe permettant de crypter et décrypter une chaine de caractères
    /// Si aucune cle de cryptage (hashage) n'est spécifiée; la clé par défaut est utilisée
    /// </summary>
    public class Cryptage
    {

        private RijndaelManaged _rijndael = new RijndaelManaged();
        private string _cleEncryptionEnText;
        private string _cleDefaut = "Le plus clair de mon temps, je le passe à l'obscurcir, parce que la lumière me gêne. [Boris Vian]";
        private byte[] _cleHasher;

        private byte[] _vecteurInitialisation = { 13, 21, 34, 124, 45, 26, 17, 68, 49, 110, 
                                                  101, 102, 132, 114, 135, 7 };

        /// <summary>
        /// Permet de créer un objet Cryptage
        /// La clé par défaut sera utilisée pour le hachage
        /// </summary>
        public Cryptage()
        {
            _cleEncryptionEnText = _cleDefaut;
            _cleHasher = ObtenirCle(_cleEncryptionEnText);

        }

        /// <summary>
        /// Permet de créer un objet Cryptage
        /// </summary>
        /// <param name="p_cleEncryption">Clé utilisé pour le hachage</param>
        public Cryptage(string p_cleEncryption)
        {
            _cleEncryptionEnText = p_cleEncryption;
            _cleHasher = ObtenirCle(_cleEncryptionEnText);
        }


        /// <summary>
        /// Permet de crypter une chaine de caractères
        /// </summary>
        /// <param name="p_chaineACrypter">Chaine de caractères qui doit être cryptée</param>
        /// <returns>
        /// La chaine de caractères cryptée
        /// </returns>
        public string Crypter(string p_chaineACrypter)
        {
            byte[] cleEnByte = new byte[32];
            MemoryStream memoryBuffer = new MemoryStream();

            CryptoStream crypteur = new CryptoStream(memoryBuffer, _rijndael.CreateEncryptor(_cleHasher, _vecteurInitialisation), CryptoStreamMode.Write);
            crypteur.Write(Encoding.ASCII.GetBytes(p_chaineACrypter), 0, p_chaineACrypter.Length);
            crypteur.Flush();
            crypteur.Close();

            return Convert.ToBase64String(memoryBuffer.ToArray());
        }

        /// <summary>
        /// Permet de décrypter une chaine de caractères
        /// </summary>
        /// <param name="p_chaineADecrypter">Chaine de caractères qui doit être décryptée</param>
        /// <returns>
        /// La chaine de caractères décryptée
        /// </returns>
        public string Decrypter(string p_chaineADecrypter)
        {
            byte[] cleEnByte = new byte[32];
            string _chaineDecrypter;

            if (p_chaineADecrypter != "")
            {
                MemoryStream memoryBuffer = new MemoryStream(Convert.FromBase64String(p_chaineADecrypter));
                CryptoStream decrypteur = new CryptoStream(memoryBuffer, _rijndael.CreateDecryptor(_cleHasher, _vecteurInitialisation), CryptoStreamMode.Read);

                StreamReader lecteurStream = new StreamReader(decrypteur);

                _chaineDecrypter = lecteurStream.ReadToEnd();

                lecteurStream.Close();
                decrypteur.Close();
                memoryBuffer.Flush();
                memoryBuffer.Close();
            }
            else
            {
                _chaineDecrypter = "";
            }
            return _chaineDecrypter;
        }

        /// <summary>
        /// Converti une chaine de caractères en tableau d'octets (byte[])
        /// </summary>
        /// <param name="p_StringAConvertir">Chaine à convertir</param>
        /// <returns>vecteur byte correspondant la chaine p_cleEnText</returns>
        private byte[] ObtenirCle(string p_StringAConvertir)
        {
            ASCIIEncoding encodeur = new ASCIIEncoding();
            Byte[] cleTemp = new Byte[32];
            byte[] cle = new Byte[32];
            int longueurCle = p_StringAConvertir.Length;

            //On s'assure que la chaine de caractère à une longueur de 32.
            if ((longueurCle >= 32))
            {
                p_StringAConvertir = p_StringAConvertir.Substring(0, 32);
            }
            else
            {
                p_StringAConvertir = p_StringAConvertir.PadRight(32, 'X');
            }

            encodeur.GetBytes(p_StringAConvertir, 0, 31, cleTemp, 0);
            for (int i = 0; i <= 31; i++)
            {
                cle[i] = cleTemp[i];
            }

            //Code les caractères en bytes
            return cle;
        }
    }
}
