using Windows.Storage;

namespace HelperLibrary.Models;

public class FileBase
{
    public string Encoding { get; set; }
    public StorageFile File { get; set; }
    public string FileNameNoExtension { get; set; }
    public string NormalizedName { get; set; }
    public string Resolution { get; set; }
    public string SourceGroup { get; set; }
    public string SourceType { get; set; }
    public int Year { get; set; }
}