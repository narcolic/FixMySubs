using FixMySubs.Helper;
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
    private readonly HomePageViewModel _viewModel;

    public HomePage()
    {
        InitializeComponent();
        _viewModel = new HomePageViewModel();
        DataContext = _viewModel;
        MovieListView.ItemsSource = _viewModel.FilesToRename;
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
            _viewModel.FilesToRename.Clear();
            _viewModel.SelectedFolderPath = folder.Path;

            FolderContentReader folderContentReader = new();
            folderContentReader.OrganizeFiles([.. await folder.GetFilesAsync()]);

            _viewModel.TvSeriesFiles = folderContentReader.TvSeriesFiles;
            _viewModel.TvSeriesSubFiles = folderContentReader.TvSeriesSubtitleFiles;

            foreach (var file in _viewModel.TvSeriesFiles)
            {

                _viewModel.FilesToRename.Add(new FileToRename { Name = file.FileNameNoExtension, IsRenamed = false });
            }

            RenameButton.IsEnabled = _viewModel.FilesToRename.Count > 0;
        }

    }

    private async void RenameButton_ClickAsync(object sender, RoutedEventArgs e)
    {
        await TvSeriesFileRenamer.RenameFilesAsync(_viewModel);
        StatusTextBlock.Text = $"{_viewModel.RenamedSubtitlesCount} Subtitles renamed successfully!";
    }

}
