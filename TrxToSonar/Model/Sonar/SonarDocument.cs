using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Xml.Serialization;

namespace TrxToSonar.Model.Sonar
{
    [XmlRoot(ElementName = "testExecutions")]
    [XmlType(TypeName =  "testExecutions")]
    public class SonarDocument
    {
        [XmlAttribute(AttributeName = "version")]
        public int Version  {get; set;} = 1;
        
        [XmlElement(ElementName = "file")]
        public List<File> Files { get; set; } = new List<File>();
    }
}