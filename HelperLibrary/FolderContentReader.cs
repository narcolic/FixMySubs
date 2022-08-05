using System.Collections.Generic;
using System.Text.RegularExpressions;
using Windows.Storage;

namespace HelperLibrary;

public class FolderContentReader
{
    private List<StorageFile> __StorageFiles;

    public FolderContentReader(List<StorageFile> files)
    {
        TvSeriesRegex = new Regex(@"S(?<season>\d{1,2})E(?<episode>\d{1,2})");
        __StorageFiles = files;
        OrganizeFiles();
    }

    private static bool IsTvSeries(List<Regex> regexes, string fileName)
    {
        foreach (Regex regex in regexes)
        {
            if (regex.Match(fileName).Success)
            {
                return true;
            }
        }

        return false;
    }

    private void OrganizeFiles()
    {
        foreach (StorageFile file in __StorageFiles)
        {
            if (Constants.VideoFileExtensions.Contains(file.FileType))
            {
                switch (file.DisplayName)
                {
                    case string displayName when (IsTvSeries(Constants.TvSeries_Regex_SeasonEpisode, displayName)):
                        TvSeriesFiles.Add(file);
                        break;
                    case string displayName when (!IsTvSeries(Constants.TvSeries_Regex_SeasonEpisode, displayName)):
                        MovieFiles.Add(file);
                        break;
                    default:
                        UncategorisedVideoFiles.Add(file);
                        break;
                }
                continue;
            }

            if (Constants.SubtitleFileExtensions.Contains(file.FileType))
            {
                switch (file.DisplayName)
                {
                    case string displayName when (IsTvSeries(Constants.TvSeries_Regex_SeasonEpisode, displayName)):
                        TvSeriesSubtitleFiles.Add(file);
                        break;
                    case string displayName when (!IsTvSeries(Constants.TvSeries_Regex_SeasonEpisode, displayName)):
                        MovieSubtitleFiles.Add(file);
                        break;
                    default:
                        UncategorisedSubtitleFiles.Add(file);
                        break;
                }
            }
        }
    }

    public List<StorageFile> MovieFiles { get; private set; } = new List<StorageFile>();
    public List<StorageFile> MovieSubtitleFiles { get; private set; } = new List<StorageFile>();
    public List<StorageFile> TvSeriesFiles { get; private set; } = new List<StorageFile>();
    public Regex TvSeriesRegex { get; }
    public List<StorageFile> TvSeriesSubtitleFiles { get; private set; } = new List<StorageFile>();
    public List<StorageFile> UncategorisedSubtitleFiles { get; private set; } = new List<StorageFile>();
    public List<StorageFile> UncategorisedVideoFiles { get; private set; } = new List<StorageFile>();
}
