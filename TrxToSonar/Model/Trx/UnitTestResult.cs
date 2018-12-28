using System;
using System.Xml.Serialization;

namespace TrxToSonar.Model.Trx
{
    public class UnitTestResult
    {
        [XmlAttribute(AttributeName = "executionId")]
        public string ExecutionId { get; set; }
        
        [XmlAttribute(AttributeName = "testId")]
        public string TestId { get; set; }
        
        [XmlAttribute(AttributeName = "testName")]
        public string TestName { get; set; }

        [XmlAttribute(AttributeName = "duration")]
        public string Duration { get; set; }
        
        [XmlAttribute(AttributeName = "startTime")]
        public DateTime StartTime { get; set; }
        
        [XmlAttribute(AttributeName = "endTime")]
        public DateTime EndTime { get; set; }
        
        [XmlAttribute(AttributeName = "outcome")]
        public Outcome Outcome { get; set; }
        
        [XmlElement(ElementName = "Output")]
        public Output Output { get; set; }
    }
}