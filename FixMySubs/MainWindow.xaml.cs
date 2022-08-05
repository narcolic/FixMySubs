using HelperLibrary;
using HelperLibrary.Enums;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
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
            FolderContentReader folderContentReader = new(files);

            var tvSeriesFiles = folderContentReader.TvSeriesFiles;
            var tvSeriesSubFiles = folderContentReader.TvSeriesSubtitleFiles;
            var movieFiles = folderContentReader.MovieFiles;
            var movieSubFiles = folderContentReader.MovieSubtitleFiles;

            FileNameAnalyserBase tvSeriesFileNameAnalyzer = FileAnalyzerFactory.GetFileAnalyzer(FileTypeCategories.TvSeries);

            //List<TvSeriesTitle> tvSeriesVideoModels = tvSeriesFileNameAnalyzer.ConvertPathsToModels(tvSeriesFiles).OfType<TvSeriesTitle>().ToList();
            //List<TvSeriesTitle> tvSeriesSubtitlesModels = tvSeriesFileNameAnalyzer.ConvertPathsToModels(tvSeriesSubFiles).OfType<TvSeriesTitle>().ToList();
        }
    }
}