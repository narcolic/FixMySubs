using HelperLibrary.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace FixMySubs.Pages;
public partial class HomePageViewModel : INotifyPropertyChanged
{
    private string _selectedFolderPath;
    private int _renamedSubtitlesCount;
       
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void SetFileRenamed(string fileName)
    {
        var file = FilesToRename.FirstOrDefault(f => f.Name == fileName);
        if (file != null)
        {
            file.IsRenamed = true;
        }
    }

    public void UpdateRenamedStatus(int count)
    {
        RenamedSubtitlesCount = count;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public ObservableCollection<FileToRename> FilesToRename { get; set; } = [];

    public int RenamedSubtitlesCount
    {
        get { return _renamedSubtitlesCount; }
        set
        {
            _renamedSubtitlesCount = value;
            OnPropertyChanged(nameof(RenamedSubtitlesCount));
        }
    }

    public string SelectedFolderPath
    {
        get { return _selectedFolderPath; }
        set
        {
            _selectedFolderPath = value;
            OnPropertyChanged(nameof(SelectedFolderPath));
        }
    }

    public List<TvSeriesFile> TvSeriesFiles { get; set; }
    public List<TvSeriesFile> TvSeriesSubFiles { get; set; }
}
