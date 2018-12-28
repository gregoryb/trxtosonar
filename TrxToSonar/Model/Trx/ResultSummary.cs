using System.Xml.Serialization;

namespace TrxToSonar.Model.Trx
{
    public class ResultSummary
    {
        [XmlAttribute(AttributeName = "outcome")]
        public string Outcome { get; set; }
        
        [XmlElement(ElementName = "Counters")]
        public Counters Counters { get; set; }
        
        [XmlElement(ElementName = "Output")]
        public Output Output { get; set; }
    }
}