using System.Xml.Serialization;

namespace TrxToSonar.Model.Trx
{
    public class Execution
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
    }
}