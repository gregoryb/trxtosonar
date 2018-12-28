using System.Collections.Generic;
using System.Xml.Serialization;

namespace TrxToSonar.Model.Sonar
{
    public class File
    {
        [XmlAttribute(AttributeName = "path")]
        public string Path { get; set; }

        [XmlElement("testCase")]
        public List<TestCase> TestCases { get; set; } = new List<TestCase>();

        public File()
        {
            
        }
        
        public File(string path)
        {
            this.Path = path;
        }
    }
}