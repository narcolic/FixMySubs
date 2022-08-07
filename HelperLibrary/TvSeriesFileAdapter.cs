using HelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Windows.Storage;

namespace HelperLibrary;

public static class TvSeriesFileAdapter
{
    public static List<TvSeriesFile> ToTVSeriesFile(this List<StorageFile> files) => files.Select(file => file.ToTVSeriesFile()).ToList();

    public static TvSeriesFile ToTVSeriesFile(this StorageFile file)
    {
        string fileName = Path.GetFileNameWithoutExtension(file.Path);
        (int season, int episode) = GetSeasonEpisode(fileName);

        var sourceMatch = Constants.Source_Regex.Matches(fileName).OfType<Match>().LastOrDefault();
        var sourceGroup = Constants.Source_Regex.GetGroupNames().LastOrDefault(group => sourceMatch.Groups[group].Success);

        return new TvSeriesFile
        {
            Season = season >= 0 ? season : throw new ArgumentOutOfRangeException("Season number incorrect. Should be more than 0."),
            Episode = episode >= 0 ? episode : throw new ArgumentOutOfRangeException("Episode number incorrect. Should be more than 0."),
            Encoding = Constants.Encoding_Regex.Match(fileName).Value,
            Name = GetSeriesName(fileName),
            FileName = fileName,
            File = file,
            Resolution = Constants.Resolution_Regex.Match(fileName).Value,
            SourceGroup = sourceGroup,
            SourceType = sourceMatch.Groups[sourceGroup].Value,
            Year = int.TryParse(Constants.Year_Regex.Match(fileName).Value, out int year) ? year : -1
        };

        string GetSeriesName(string fileName)
        {
            foreach (var regex in Constants.TvSeries_Regex_SeasonEpisode.Where(regex => regex.Match(fileName).Success))
            {
                var seriesNameWithYear = Regex.Replace(Regex.Split(fileName, regex.ToString())[0], @"[^\w]+", " ", RegexOptions.Compiled);
                return Constants.Normalize_Regex.Replace(Constants.Year_Regex.Replace(seriesNameWithYear, string.Empty), string.Empty);
            }
            return string.Empty;
        }

        (int season, int episode) GetSeasonEpisode(string fileName)
        {
            int _Season = -1;
            int _Episode = -1;
            foreach (Regex regex in Constants.TvSeries_Regex_SeasonEpisode)
            {
                Match match = regex.Match(fileName);
                if (match.Success)
                {
                    _Season = Convert.ToInt32(match.Groups["season"].Value);
                    _Episode = Convert.ToInt32(match.Groups["episode"].Value);
                    if (match.Index == 0) break;
                }
            }
            return (_Season, _Episode);
        }
    }
}