using HelperLibrary.Enums;

namespace HelperLibrary;

public static class FileAnalyzerFactory
{
    public static FileNameAnalyserBase GetFileAnalyzer(FileTypeCategories fileType) => fileType switch
    {
        FileTypeCategories.Subtitle => new SubtitleFileNameAnalyzer(),
        _ => new TvSeriesFileNameAnalyzer(),
    };
}
