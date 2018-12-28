using TrxToSonar.Model.Sonar;

namespace TrxToSonar
{
    public interface IConverter
    {
        SonarDocument Parse(string solutionDirectory, bool useAbsolutePath);

        bool Save(SonarDocument sonarDocument, string outputFilename);
    }
}