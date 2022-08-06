using HelperLibrary;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FixMySubs;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    List<HelperLibrary.Models.TvSeriesFile> tvSeriesFiles;
    List<HelperLibrary.Models.TvSeriesFile> tvSeriesSubFiles;

    public MainWindow() => InitializeComponent();

    private async void Open_Button_ClickAsync(object sender, RoutedEventArgs e)
    {
        FolderPicker folderPicker = new() { SuggestedStartLocation = PickerLocationId.Desktop };

        // Associate the HWND with the folder picker
        InitializeWithWindow.Initialize(folderPicker, WindowNative.GetWindowHandle(this));

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

            importResult.Text = $"Found {tvSeriesFiles.Count} TV Series video files and {tvSeriesSubFiles.Count} subtitle files.";
        }
    }

    private async void Rename_ClickAsync(object sender, RoutedEventArgs e)
    {
        int renameCounter = 0;
        foreach (var video in tvSeriesFiles)
        {
            var sub = tvSeriesSubFiles.FirstOrDefault(sub => sub.Name.Equals(video.Name, StringComparison.OrdinalIgnoreCase) && sub.Season == video.Season && sub.Episode == video.Episode);

            if (sub != null)
            {
                renameCounter++;
                await sub.File.RenameAsync($"{video.FileName}{sub.File.FileType}");
            }
        }
        renameResult.Text = $"Renamed {renameCounter} files";
    }
}