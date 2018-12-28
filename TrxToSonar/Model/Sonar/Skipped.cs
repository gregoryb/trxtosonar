using System.Xml.Serialization;

namespace TrxToSonar.Model.Sonar
{
    public class Skipped
    {
        [XmlAttribute(AttributeName = "message")]
        public string Message { get; set; }
        
        [XmlText]
        public string Value { get; set; }
    }
}