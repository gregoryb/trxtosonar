using System.Xml.Serialization;

namespace TrxToSonar.Model.Trx
{
    public class Output
    {
        [XmlElement (ElementName = "StdOut")]
        public string StdOut { get; set; }
        
        [XmlElement (ElementName = "ErrorInfo")]
        public ErrorInfo ErrorInfo { get; set; }
    }
}