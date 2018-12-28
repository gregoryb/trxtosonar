using System.Xml.Serialization;

namespace TrxToSonar.Model.Sonar
{
    public class TestCase
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        
        [XmlAttribute(AttributeName = "duration")]
        public long Duration { get; set; }
        
        [XmlElement(ElementName = "error")]
        public Error Error { get; set; }
        
        [XmlElement(ElementName = "skipped")]
        public Skipped Skipped { get; set; }
        
        [XmlElement(ElementName = "failure")]
        public Failure Failure { get; set; }

        public TestCase()
        {
            
        }
        
        public TestCase(string name, long duration)
        {
            this.Name = name;
            this.Duration = duration;
        }
    }
}