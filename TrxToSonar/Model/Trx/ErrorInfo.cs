using System.Xml.Serialization;

namespace TrxToSonar.Model.Trx
{
    public class ErrorInfo
    {
        [XmlElement(ElementName = "Message")]
        public string Message { get; set; }
        
        [XmlElement(ElementName = "StackTrace")]
        public string StackTrace { get; set; }
    }
}