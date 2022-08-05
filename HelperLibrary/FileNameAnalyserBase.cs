using HelperLibrary.Models;
using System.Collections.Generic;

namespace HelperLibrary;

public abstract class FileNameAnalyserBase
{
    public abstract TitleBase ConvertPathToModel(string fileName);

    public abstract List<TitleBase> ConvertPathsToModels(List<string> fileNames);
}
