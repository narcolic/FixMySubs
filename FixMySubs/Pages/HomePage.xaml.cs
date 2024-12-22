using HelperLibrary;
using HelperLibrary.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public ObservableCollection<FileToRename> FilesToRename { get; set; } = [];

    public HomePage()
    {
        InitializeComponent();
        MovieListView.ItemsSource = FilesToRename;
    }

    private async void BrowseButton_ClickAsync(object sender, RoutedEventArgs e)
    {
        FolderPicker folderPicker = new() { SuggestedStartLocation = PickerLocationId.Desktop };
        folderPicker.FileTypeFilter.Add("*");

        var window = (Application.Current as App)?.m_window as MainWindow;
        InitializeWithWindow.Initialize(folderPicker, WindowNative.GetWindowHandle(window));

        var folder = await folderPicker.PickSingleFolderAsync();
        if (folder != null)
        {
            StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);

            List<StorageFile> files = [.. await folder.GetFilesAsync()];

            FilesToRename.Clear();
            FolderContentReader folderContentReader = new();
            folderContentReader.OrganizeFiles(files);

            tvSeriesFiles = folderContentReader.TvSeriesFiles;
            tvSeriesSubFiles = folderContentReader.TvSeriesSubtitleFiles;

            foreach (var file in tvSeriesFiles)
            {
                FilesToRename.Add(new FileToRename { Name = file.FileNameNoExtension, IsRenamed = false });
            }

        }

    }

    private async void RenameButton_ClickAsync(object sender, RoutedEventArgs e)
    {
        int renameCounter = 0;
        List<Task> RenameTasks = [];
        foreach (var video in tvSeriesFiles)
        {
            var sub = tvSeriesSubFiles.FirstOrDefault(sub => sub.NormalizedName.Equals(video.NormalizedName, StringComparison.OrdinalIgnoreCase)
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

            sub = tvSeriesSubFiles.FirstOrDefault(sub => sub.NormalizedName.Equals(video.NormalizedName, StringComparison.OrdinalIgnoreCase)
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

            sub = tvSeriesSubFiles.FirstOrDefault(sub => sub.NormalizedName.Equals(video.NormalizedName, StringComparison.OrdinalIgnoreCase)
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

            sub = tvSeriesSubFiles.FirstOrDefault(sub => sub.NormalizedName.Equals(video.NormalizedName, StringComparison.OrdinalIgnoreCase)
                                                         && sub.SourceGroup == video.SourceGroup
                                                         && sub.SourceType == video.SourceType
                                                         && sub.Season == video.Season
                                                         && sub.Episode == video.Episode);
            if (sub != null)
            {
                RenameSub(video, sub);
                continue;
            }

            sub = tvSeriesSubFiles.FirstOrDefault(sub => sub.NormalizedName.Equals(video.NormalizedName, StringComparison.OrdinalIgnoreCase)
                                                         && sub.SourceGroup == video.SourceGroup
                                                         && sub.Resolution == video.Resolution
                                                         && sub.Season == video.Season
                                                         && sub.Episode == video.Episode);
            if (sub != null)
            {
                RenameSub(video, sub);
                continue;
            }

            sub = tvSeriesSubFiles.FirstOrDefault(sub => sub.NormalizedName.Equals(video.NormalizedName, StringComparison.OrdinalIgnoreCase)
                                                         && sub.SourceGroup == video.SourceGroup
                                                         && sub.Season == video.Season
                                                         && sub.Episode == video.Episode);
            if (sub != null)
            {
                RenameSub(video, sub);
                continue;
            }

            sub = tvSeriesSubFiles.FirstOrDefault(sub => sub.NormalizedName.Equals(video.NormalizedName, StringComparison.OrdinalIgnoreCase)
                                                         && sub.Encoding == video.Encoding
                                                         && sub.Season == video.Season
                                                         && sub.Episode == video.Episode);
            if (sub != null)
            {
                RenameSub(video, sub);
                continue;
            }

            sub = tvSeriesSubFiles.FirstOrDefault(sub => sub.NormalizedName.Equals(video.NormalizedName, StringComparison.OrdinalIgnoreCase)
                                                        && sub.Resolution == video.Resolution
                                                        && sub.Season == video.Season
                                                        && sub.Episode == video.Episode);
            if (sub != null)
            {
                RenameSub(video, sub);
                continue;
            }

            sub = tvSeriesSubFiles.FirstOrDefault(sub => sub.NormalizedName.Equals(video.NormalizedName, StringComparison.OrdinalIgnoreCase)
                                                         && sub.Season == video.Season
                                                         && sub.Episode == video.Episode);
            if (sub != null)
            {
                RenameSub(video, sub);
                continue;
            }
        }
        await Task.WhenAll(RenameTasks);

        MovieListView.ItemsSource = null; // Refresh binding
        MovieListView.ItemsSource = FilesToRename;

        StatusTextBlock.Text = $"{renameCounter} Subtitles renamed successfully!";

        void RenameSub(TvSeriesFile video, TvSeriesFile sub)
        {
            renameCounter++;
            RenameTasks.Add(sub.File.RenameAsync($"{video.FileNameNoExtension}{sub.File.FileType}").AsTask());
            FilesToRename.First(file => file.Name == video.FileNameNoExtension).IsRenamed = true;
        }
    }
}
