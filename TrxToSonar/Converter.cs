using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using TrxToSonar.Model.Sonar;
using TrxToSonar.Model.Trx;
using File = TrxToSonar.Model.Sonar.File;

namespace TrxToSonar
{
    public class Converter : IConverter
    {   
        private readonly ILoggerFactory loggerFactory;
        private readonly ILogger logger;
        private readonly XmlParser<TrxDocument> trxParser = new XmlParser<TrxDocument>();
        private readonly XmlParser<SonarDocument> sonarParser = new XmlParser<SonarDocument>();

        public Converter(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
            this.logger = this.loggerFactory.CreateLogger<Converter>();
        }
        
        public bool Save(SonarDocument sonarDocument, string outputFilename)
        {
            return this.sonarParser.Save(sonarDocument, outputFilename);
        }
        
        public SonarDocument Parse(string solutionDirectory, bool useAbsolutePath)
        {
            if (string.IsNullOrEmpty(solutionDirectory) || !Directory.Exists(solutionDirectory))
            {
                return null;
            }

            var trxFiles = Directory.EnumerateFiles(solutionDirectory, "*.trx", SearchOption.AllDirectories);

            var sonarDocuments = new List<SonarDocument>();
            foreach (var trxFile in trxFiles)
            {
                this.logger.LogInformation($"Parsing: {trxFile}");
                var trxDocument = trxParser.Deserialize(trxFile);
                var sonarDocument = this.Convert(trxDocument, solutionDirectory, useAbsolutePath);

                if (sonarDocument != null)
                {
                    sonarDocuments.Add(sonarDocument);
                }
            }
            
            // Merge
            return this.Merge(sonarDocuments);
        }

        private SonarDocument Merge(List<SonarDocument> sonarDocuments)
        {
            this.logger.LogInformation("Merge {0} file(s).", sonarDocuments.Count);
            if (sonarDocuments.Count == 1)
            {
                return sonarDocuments.FirstOrDefault();
            }
            
            var result = new SonarDocument();
            foreach (var sonarDocument in sonarDocuments)
            {
                foreach (var sonarFile in sonarDocument.Files)
                {
                    result.Files.Add(sonarFile);
                } 
            }
            return result;
        }
        
        private SonarDocument Convert(TrxDocument trxDocument, string solutionDirectory, bool useAbsolutePath)
        {
            var sonarDocument = new SonarDocument();

            foreach (var trxResult in trxDocument.Results)
            {
                var unitTest = trxResult.GetUnitTest(trxDocument);

                var testFile = unitTest.GetTestFile(solutionDirectory, useAbsolutePath);

                /*
                if (!System.IO.File.Exists(Path.Combine(solutionDirectory,testFile)))
                {
                    this.logger.LogWarning($"Test file seems to be wrong - Unable to find it: {testFile}");    
                }
                */
                
                var file = sonarDocument.GetFile(testFile);

                if (file == null)
                {
                    file = new File(testFile);
                    sonarDocument.Files.Add(file);
                }

                var testCase = new TestCase(trxResult.TestName, Utils.ToSonarDuration(trxResult.Duration));

                if (trxResult.Outcome != Outcome.Passed)
                {
                    if (trxResult.Outcome == Outcome.NotExecuted )
                    {
                        testCase.Skipped = new Skipped();
                        this.logger.LogInformation($"Skipped: {trxResult.TestName}");
                    }else
                    {
                        testCase.Failure = new Failure(trxResult.Output?.ErrorInfo?.Message, trxResult.Output?.ErrorInfo?.StackTrace);
                        this.logger.LogInformation($"Failure: {trxResult.TestName}");
                    }
                }
                else
                {
                    this.logger.LogInformation($"Passed: {trxResult.TestName}");
                }

                file.TestCases.Add(testCase);
                
            }
            return sonarDocument;
        }        
    }
    

}