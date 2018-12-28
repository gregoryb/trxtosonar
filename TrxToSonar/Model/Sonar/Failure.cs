using System.Diagnostics;
using System.Xml.Serialization;

namespace TrxToSonar.Model.Sonar
{
    public class Failure
    {
        [XmlAttribute(AttributeName = "message")]
        public string Message { get; set; }
        
        [XmlText]
        public string Value { get; set; }

        public Failure()
        {
            
        }
        
        public Failure(string message, string value)
        {
            this.Message = message;
            this.Value = value;
        }
    }
}