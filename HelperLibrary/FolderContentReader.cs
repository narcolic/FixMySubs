using HelperLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;

namespace HelperLibrary;

public class FolderContentReader
{
    public void OrganizeFiles(List<StorageFile> files)
    {
        //Clear files that proper subs
        foreach (StorageFile file in files.GroupBy(x => x.DisplayName).Where(x => x.Count() == 1).Select(x => x.First()).ToList())
        {
            if (Constants.VideoFileExtensions.Contains(file.FileType))
            {
                switch (file.DisplayName)
                {
                    case string displayName when (IsTvSeries(displayName)):
                        TvSeriesFiles.Add(file.ToTVSeriesFile());
                        break;
                    case string displayName when (!IsTvSeries(displayName)):
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
                    case string displayName when (IsTvSeries(displayName)):
                        TvSeriesSubtitleFiles.Add(file.ToTVSeriesFile());
                        break;
                    case string displayName when (!IsTvSeries(displayName)):
                        MovieSubtitleFiles.Add(file);
                        break;
                    default:
                        UncategorisedSubtitleFiles.Add(file);
                        break;
                }
            }
        }

        bool IsTvSeries(string fileName) => Constants.TvSeries_Regex_SeasonEpisode.Any(regex => regex.Match(fileName).Success);
    }

    public List<StorageFile> MovieFiles { get; private set; } = new();
    public List<StorageFile> MovieSubtitleFiles { get; private set; } = new();
    public List<TvSeriesFile> TvSeriesFiles { get; private set; } = new();
    public List<TvSeriesFile> TvSeriesSubtitleFiles { get; private set; } = new();
    public List<StorageFile> UncategorisedSubtitleFiles { get; private set; } = new();
    public List<StorageFile> UncategorisedVideoFiles { get; private set; } = new();
}