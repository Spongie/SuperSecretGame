using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace CVStorage
{
    public static class ObjectSerializer
    {
        /// <summary>
        /// Sparar ett object till fil. OBS! Klassen måste vara public
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="piObject"></param>
        /// <param name="piFileName"></param>
        public static void Serialize<T>(T piObject, string piFileName)
        {
            var serializer = new XmlSerializer(typeof(T));

            var pathName = GetPathName(piFileName);

            if (!Directory.Exists(pathName))
                Directory.CreateDirectory(pathName);

            var fileStream = new FileStream(piFileName, FileMode.Create);
            serializer.Serialize(fileStream, piObject);

            fileStream.Flush();
            fileStream.Close();
        }

        private static string GetPathName(string piFileName)
        {
            return piFileName.Substring(0, piFileName.LastIndexOf("\\"));
        }

        /// <summary>
        /// Laddar ett object från fil. OBS! Klassen måste vara public
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="piFileName"></param>
        /// <returns></returns>
        public static T DeSerialize<T>(string piFileName)
        {
            var serializer = new XmlSerializer(typeof(T));
            var fileStream = new FileStream(piFileName, FileMode.Open);
            var reader = XmlReader.Create(fileStream);

            T deserializedObject = (T)serializer.Deserialize(reader);

            fileStream.Flush();
            fileStream.Close();

            return deserializedObject;
        }
    }
}
