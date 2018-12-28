using System.Xml.Serialization;

namespace TrxToSonar.Model.Trx
{
    public class TestMethod
    {
        [XmlAttribute(AttributeName = "codeBase")]
        public string CodeBase { get; set; }
        
        [XmlAttribute(AttributeName = "className")]
        public string ClassName { get; set; }
        
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        
        
    }
}