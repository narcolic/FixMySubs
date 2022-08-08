using HelperLibrary;
using HelperLibrary.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace FixMySubs.Pages;

public sealed partial class HomePage : Page
{
    List<TvSeriesFile> tvSeriesFiles;
    List<TvSeriesFile> tvSeriesSubFiles;

    public HomePage()
    {
        this.InitializeComponent();
    }

    private async void Open_Button_ClickAsync(object sender, RoutedEventArgs e)
    {
        FolderPicker folderPicker = new() { SuggestedStartLocation = PickerLocationId.Desktop };

        var window = (Application.Current as App)?.m_window as MainWindow;
        // Associate the HWND with the folder picker
        InitializeWithWindow.Initialize(folderPicker, WindowNative.GetWindowHandle(window));

        folderPicker.FileTypeFilter.Add("*");
        StorageFolder folder = await folderPicker.PickSingleFolderAsync();

        if (!Equals(folder, null))
        {
            StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);

            List<StorageFile> files = new();
            files.AddRange(await folder.GetFilesAsync());

            FolderContentReader folderContentReader = new();
            folderContentReader.OrganizeFiles(files);

            tvSeriesFiles = folderContentReader.TvSeriesFiles;
            tvSeriesSubFiles = folderContentReader.TvSeriesSubtitleFiles;

            //importResult.Text = $"Found {tvSeriesFiles.Count} TV Series video files and {tvSeriesSubFiles.Count} subtitle files.";
        }

        BaseExample.ItemsSource = tvSeriesFiles;
    }

    private async void Rename_ClickAsync(object sender, RoutedEventArgs e)
    {
        int renameCounter = 0;
        List<Task> RenameTasks = new();
        foreach (var video in tvSeriesFiles)
        {
            var sub = tvSeriesSubFiles.FirstOrDefault(sub => sub.Name.Equals(video.Name, StringComparison.OrdinalIgnoreCase)
                                                             && sub.SourceGroup == video.SourceGroup
                                                             && sub.SourceType == video.SourceType
                                                             && sub.Encoding == video.Encoding
                                                             && sub.Resolution == video.Resolution
                                                             && sub.Year == video.Year
                                                             && sub.Season == video.Season
                                                             && sub.Episode == video.Episode);
            if (sub != null)
            {
                RenameSub(video, sub);
                continue;
            }

            sub = tvSeriesSubFiles.FirstOrDefault(sub => sub.Name.Equals(video.Name, StringComparison.OrdinalIgnoreCase)
                                                         && sub.SourceGroup == video.SourceGroup
                                                         && sub.SourceType == video.SourceType
                                                         && sub.Encoding == video.Encoding
                                                         && sub.Resolution == video.Resolution
                                                         && sub.Season == video.Season
                                                         && sub.Episode == video.Episode);
            if (sub != null)
            {
                RenameSub(video, sub);
                continue;
            }

            sub = tvSeriesSubFiles.FirstOrDefault(sub => sub.Name.Equals(video.Name, StringComparison.OrdinalIgnoreCase)
                                                         && sub.SourceGroup == video.SourceGroup
                                                         && sub.SourceType == video.SourceType
                                                         && sub.Encoding == video.Encoding
                                                         && sub.Season == video.Season
                                                         && sub.Episode == video.Episode);
            if (sub != null)
            {
                RenameSub(video, sub);
                continue;
            }

            sub = tvSeriesSubFiles.FirstOrDefault(sub => sub.Name.Equals(video.Name, StringComparison.OrdinalIgnoreCase)
                                                         && sub.SourceGroup == video.SourceGroup
                                                         && sub.SourceType == video.SourceType
                                                         && sub.Season == video.Season
                                                         && sub.Episode == video.Episode);
            if (sub != null)
            {
                RenameSub(video, sub);
                continue;
            }

            sub = tvSeriesSubFiles.FirstOrDefault(sub => sub.Name.Equals(video.Name, StringComparison.OrdinalIgnoreCase)
                                                         && sub.SourceGroup == video.SourceGroup
                                                         && sub.Resolution == video.Resolution
                                                         && sub.Season == video.Season
                                                         && sub.Episode == video.Episode);
            if (sub != null)
            {
                RenameSub(video, sub);
                continue;
            }

            sub = tvSeriesSubFiles.FirstOrDefault(sub => sub.Name.Equals(video.Name, StringComparison.OrdinalIgnoreCase)
                                                         && sub.SourceGroup == video.SourceGroup
                                                         && sub.Season == video.Season
                                                         && sub.Episode == video.Episode);
            if (sub != null)
            {
                RenameSub(video, sub);
                continue;
            }

            sub = tvSeriesSubFiles.FirstOrDefault(sub => sub.Name.Equals(video.Name, StringComparison.OrdinalIgnoreCase)
                                                         && sub.Encoding == video.Encoding
                                                         && sub.Season == video.Season
                                                         && sub.Episode == video.Episode);
            if (sub != null)
            {
                RenameSub(video, sub);
                continue;
            }

            sub = tvSeriesSubFiles.FirstOrDefault(sub => sub.Name.Equals(video.Name, StringComparison.OrdinalIgnoreCase)
                                                        && sub.Resolution == video.Resolution
                                                        && sub.Season == video.Season
                                                        && sub.Episode == video.Episode);
            if (sub != null)
            {
                RenameSub(video, sub);
                continue;
            }

            sub = tvSeriesSubFiles.FirstOrDefault(sub => sub.Name.Equals(video.Name, StringComparison.OrdinalIgnoreCase)
                                                         && sub.Season == video.Season
                                                         && sub.Episode == video.Episode);
            if (sub != null)
            {
                RenameSub(video, sub);
                continue;
            }
        }
        await Task.WhenAll(RenameTasks);
        //renameResult.Text = $"Renamed {renameCounter} files.";

        void RenameSub(TvSeriesFile video, TvSeriesFile sub)
        {
            renameCounter++;
            RenameTasks.Add(sub.File.RenameAsync($"{video.FileName}{sub.File.FileType}").AsTask());
        }
    }
}
