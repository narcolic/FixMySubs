using HelperLibrary.Enums;
using Windows.Storage;

namespace HelperLibrary.Models;

public class FileBase
{
    public string Extension { get; set; }
    public StorageFile File { get; set; }
    public string FileName { get; set; }
    public string Name { get; set; }
    public string Quality { get; set; }
    public FileType Type { get; set; }
}
