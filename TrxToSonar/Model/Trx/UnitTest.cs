using System.Xml.Serialization;

namespace TrxToSonar.Model.Trx
{
    public class UnitTest
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        
        [XmlAttribute(AttributeName = "storage")]
        public string Storage { get; set; }
        
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        
        [XmlElement(ElementName = "Execution")]
        public Execution Execution { get; set; }
        
        [XmlElement(ElementName = "TestMethod")]
        public TestMethod TestMethod { get; set; }
    }
}