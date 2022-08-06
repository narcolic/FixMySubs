using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HelperLibrary;

public static class Constants
{
    public static readonly List<string> SubtitleFileExtensions = new() { ".srt", ".sub" };

    public static readonly List<Regex> TvSeries_Regex_SeasonEpisode = new()
    {
        new Regex(@"(?i)S(?-i)(?<season>\d{1,2})(?i)E(?-i)(?<episode>\d{1,2})", RegexOptions.IgnoreCase),
        new Regex(@"(?<season>\d{1,2})x(?<episode>\d{1,2})", RegexOptions.IgnoreCase)
    };

    public static readonly List<string> VideoFileExtensions = new() { ".mkv", ".avi", ".mp4", ".mpg", ".mpeg" };
}
