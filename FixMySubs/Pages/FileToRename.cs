using System.ComponentModel;

namespace FixMySubs.Pages;

public class FileToRename : INotifyPropertyChanged
{
    private string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }

    private bool _isRenamed;
    public bool IsRenamed
    {
        get { return _isRenamed; }
        set
        {
            if (_isRenamed != value)
            {
                _isRenamed = value;
                OnPropertyChanged(nameof(IsRenamed));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}