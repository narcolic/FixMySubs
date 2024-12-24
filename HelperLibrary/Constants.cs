using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HelperLibrary;

public static class Constants
{
    public static readonly List<string> SubtitleFileExtensions = [".srt", ".sub"];

    public static readonly List<Regex> TvSeries_Regex_SeasonEpisode =
    [
        new Regex(@"(?i)S(?-i)(?<season>\d{1,2})(?i)E(?-i)(?<episode>\d{1,2})", RegexOptions.IgnoreCase),
        new Regex(@"(?<season>\d{1,2})x(?<episode>\d{1,2})", RegexOptions.IgnoreCase)
    ];

    public static Regex Year_Regex = new(@"\b(19|20)\d{2}\b", RegexOptions.IgnoreCase);

    public static Regex Resolution_Regex = new(@"\b(720|1080|2160)p\b", RegexOptions.IgnoreCase);

    public static Regex Encoding_Regex = new(@"\b(X|H)26(4|5)\b", RegexOptions.IgnoreCase);

    public static readonly List<string> VideoFileExtensions = [".mkv", ".avi", ".mp4", ".mpg", ".mpeg"];

    public static readonly Regex Normalize_Regex = new(@"((?:\b|_)(?<!^)(a(?!$)|an|the|and|or|of)(?!$)(?:\b|_))|\W|_", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public static readonly Regex Source_Regex = new(@"\b(?:
                                                        (?<bluray>BluRay|Blu-Ray|HD-?DVD|BDMux|BD(?!$))|
                                                        (?<webdl>WEB[-_. ]DL|WEBDL|AmazonHD|iTunesHD|MaxdomeHD|NetflixU?HD|WebHD|[. ]WEB[. ](?:DDP?5[. ]1)|[. ](?-i:WEB)$|(?:[-. ]AMZN)?WEB|WEB-DLMux|\b\s\/\sWEB\s\/\s\b|AMZN[. ]WEB[. ]|NF[. ]WEB[. ])|
                                                        (?<webrip>WebRip|Web-Rip|WEBMux)|
                                                        (?<hdtv>HDTV)|
                                                        (?<bdrip>BDRip|BDLight)|
                                                        (?<brrip>BRRip)|
                                                        (?<dvd>DVD|DVDRip|NTSC|PAL|xvidvd)|
                                                        (?<dsr>WS[-_. ]DSR|DSR)|
                                                        (?<pdtv>PDTV)|
                                                        (?<sdtv>SDTV)|
                                                        (?<tvrip>TVRip))(?:\b|$|[ .])",
                                                        RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
}
