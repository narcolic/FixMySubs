using FixMySubs.Pages;
using HelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixMySubs.Helper;
public class TvSeriesFileRenamer
{
    private static bool MatchesVideo(TvSeriesFile video, TvSeriesFile sub)
    {
        // Exact Match 
        if (string.Equals(video.NormalizedName, sub.NormalizedName, StringComparison.OrdinalIgnoreCase)
            && sub.SourceGroup == video.SourceGroup
            && sub.SourceType == video.SourceType
            && sub.Encoding == video.Encoding
            && sub.Resolution == video.Resolution
            && sub.Year == video.Year
            && sub.Season == video.Season
            && sub.Episode == video.Episode)
        {
            return true;
        }

        // Match excluding Resolution
        if (string.Equals(video.NormalizedName, sub.NormalizedName, StringComparison.OrdinalIgnoreCase)
            && sub.SourceGroup == video.SourceGroup
            && sub.SourceType == video.SourceType
            && sub.Encoding == video.Encoding
            && sub.Year == video.Year
            && sub.Season == video.Season
            && sub.Episode == video.Episode)
        {
            return true;
        }

        // Match excluding Resolution and Encoding
        if (string.Equals(video.NormalizedName, sub.NormalizedName, StringComparison.OrdinalIgnoreCase)
            && sub.SourceGroup == video.SourceGroup
            && sub.SourceType == video.SourceType
            && sub.Year == video.Year
            && sub.Season == video.Season
            && sub.Episode == video.Episode)
        {
            return true;
        }

        // Match excluding Resolution, Encoding, and SourceType
        if (string.Equals(video.NormalizedName, sub.NormalizedName, StringComparison.OrdinalIgnoreCase)
            && sub.SourceGroup == video.SourceGroup
            && sub.Year == video.Year
            && sub.Season == video.Season
            && sub.Episode == video.Episode)
        {
            return true;
        }

        // Match excluding Resolution, Encoding, SourceType, and SourceGroup
        if (string.Equals(video.NormalizedName, sub.NormalizedName, StringComparison.OrdinalIgnoreCase)
            && sub.Year == video.Year
            && sub.Season == video.Season
            && sub.Episode == video.Episode)
        {
            return true;
        }

        // Match excluding Resolution, Encoding, SourceType, SourceGroup, and Year 
        if (string.Equals(video.NormalizedName, sub.NormalizedName, StringComparison.OrdinalIgnoreCase)
            && sub.Season == video.Season
            && sub.Episode == video.Episode)
        {
            return true;
        }

        // Match excluding Resolution, Encoding, SourceType, SourceGroup, Year, and NormalizedName 
        if (sub.Season == video.Season
            && sub.Episode == video.Episode)
        {
            return true;
        }

        return false;
    }

    public static async Task RenameFilesAsync(HomePageViewModel viewModel)
    {
        int renameCounter = 0;
        List<Task> renameTasks = [];

        foreach (var video in viewModel.TvSeriesFiles)
        {
            var sub = viewModel.TvSeriesSubFiles.FirstOrDefault(sub => MatchesVideo(video, sub));
            if (sub != null)
            {
                renameTasks.Add(RenameSubtitleAsync(video, sub, viewModel));
                renameCounter++;
            }
        }

        await Task.WhenAll(renameTasks);

        viewModel.UpdateRenamedStatus(renameCounter);
    }

    private static async Task RenameSubtitleAsync(TvSeriesFile video, TvSeriesFile sub, HomePageViewModel viewModel)
    {
        await sub.File.RenameAsync($"{video.FileNameNoExtension}{sub.File.FileType}").AsTask();
        viewModel.SetFileRenamed(video.FileNameNoExtension);
    }
}
