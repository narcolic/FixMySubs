using HelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace HelperLibrary;

public class TvSeriesFileNameAnalyzer : FileNameAnalyserBase
{
    private TvSeriesTitle __TvSeriesTitle;

    public TvSeriesFileNameAnalyzer() { }

    public override List<TitleBase> ConvertPathsToModels(List<string> fileNames)
    {
        List<TitleBase> _TvSeriesTitles = new();
        fileNames.ForEach(fileName => _TvSeriesTitles.Add(ConvertPathToModel(fileName)));
        return _TvSeriesTitles;
    }

    public override TitleBase ConvertPathToModel(string filePath)
    {
        string fileName = Path.GetFileName(filePath);
        (int _SeasonNumber, int _EpisodeNumber) = GetSeasonEpisode(Constants.TvSeries_Regex_SeasonEpisode, fileName);

        __TvSeriesTitle = new TvSeriesTitle
        {
            FileNamePath = filePath,
            Season = _SeasonNumber >= 0 ? _SeasonNumber : throw new ArgumentOutOfRangeException("Season number incorrect. Should be more than 0."),
            Episode = _EpisodeNumber >= 0 ? _EpisodeNumber : throw new ArgumentOutOfRangeException("Episode number incorrect. Should be more than 0."),
            Name = GetSeriesName(Constants.TvSeries_Regex_SeasonEpisode, fileName)
        };
        return __TvSeriesTitle;
    }

    private static (int season, int episode) GetSeasonEpisode(List<Regex> regexes, string fileName)
    {
        int _Season = -1;
        int _Episode = -1;
        foreach (Regex regex in regexes)
        {
            Match match = regex.Match(fileName);
            if (regex.Match(fileName).Success)
            {
                _Season = Convert.ToInt32(match.Groups["season"].Value);
                _Episode = Convert.ToInt32(match.Groups["episode"].Value);
                if (match.Index == 0) break;
            }
        }

        return (_Season, _Episode);
    }

    private static string GetSeriesName(List<Regex> regexes, string fileName)
    {
        foreach (Regex regex in regexes)
        {
            _ = regex.Match(fileName);
            if (regex.Match(fileName).Success)
            {
                return Regex.Replace(Regex.Split(fileName, regex.ToString())[0], @"[^\w]+", " ", RegexOptions.Compiled);
            }
        }

        return string.Empty;
    }
}