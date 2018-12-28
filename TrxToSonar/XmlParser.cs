using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace TrxToSonar
{
    public class XmlParser<T>
    {
        private readonly XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

        public XmlParser()
        {
        }

        public bool Save(T xmlDocument, string outputFilename)
        {
            var xmlContent = this.Serialize(xmlDocument);

            if (string.IsNullOrEmpty(xmlContent))
            {
                return false;
            }

            if (File.Exists(outputFilename))
            {
                File.Delete(outputFilename);
            }

            File.WriteAllText(outputFilename, xmlContent);
            return true;
        }

        public T Deserialize(string filename)
        {
            using (var streamReader = new StreamReader(filename))
            {
                return (T) xmlSerializer.Deserialize(streamReader);
            }
        }

        private string Serialize(T xmlDocument)
        {
            var emptyNamespaces = new XmlSerializerNamespaces();
            emptyNamespaces.Add("", "");

            var xmlSettings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            };

            using (var streamWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(streamWriter, xmlSettings))
                {
                    xmlSerializer.Serialize(xmlWriter, xmlDocument, emptyNamespaces);
                    return streamWriter.ToString();
                }
            }
        }
    }
}