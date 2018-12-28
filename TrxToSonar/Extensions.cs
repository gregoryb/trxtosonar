using System;
using System.IO;
using System.Linq;
using TrxToSonar.Model.Sonar;
using TrxToSonar.Model.Trx;
using File = TrxToSonar.Model.Sonar.File;

namespace TrxToSonar
{
    public static class Extensions
    {
        public static UnitTest GetUnitTest(this UnitTestResult unitTestResult, TrxDocument trxDocument)
        {
            return trxDocument.TestDefinitions.FirstOrDefault(x => x.Id == unitTestResult.TestId);
        }

        public static string GetTestClass(this UnitTest unitTest)
        {
            var parts = unitTest.Name.Split(".");
            
            return parts.Length > 1 ? parts[parts.Length - 1] : string.Empty;
        }
        
        public static File GetFile(this SonarDocument sonarDocument, string testFile)
        {
            return sonarDocument.Files.FirstOrDefault(x => x.Path == testFile);
        }

        public static string GetTestFile(this UnitTest unitTest, string solutionDirectory, bool useAbsolutePath)
        {
            var className = unitTest?.TestMethod?.ClassName;

            if (string.IsNullOrEmpty(className))
            {
                return string.Empty;
            }

            var pathIndex = className.LastIndexOf(".", StringComparison.Ordinal);
            var pathIndexLast = unitTest.TestMethod.CodeBase.IndexOf("\\bin\\", StringComparison.Ordinal);
            var path = unitTest.TestMethod.CodeBase.Substring(0, pathIndexLast);
            var pathIndexFirst = path.LastIndexOf("\\", StringComparison.Ordinal);
            path = path.Substring(pathIndexFirst + 1);
            
            var filename = className.Substring(pathIndex + 1, className.Length - pathIndex - 1);

            string result;
            
            if (!useAbsolutePath)
            {
                result = string.Format("{0}.cs", Path.Combine(path, filename));
            }
            else
            {
                result = string.Format("{0}.cs", Path.Combine(solutionDirectory, path, filename));
            }

            return result.Replace(" ", "\\\\ ");
        }
    }
}