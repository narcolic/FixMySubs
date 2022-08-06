using System.Collections.Generic;
using Windows.Storage;

namespace HelperLibrary;

public class DuplicateFileComparer : IEqualityComparer<StorageFile>
{
    public bool Equals(StorageFile x, StorageFile y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        //Check whether any of the compared objects is null.
        if (x is null || y is null)
        {
            return false;
        }

        //Check whether the storageFile's properties are equal.
        return x.DisplayName == y.DisplayName;
    }

    public int GetHashCode(StorageFile storageFile)
    {
        //Check whether the object is null
        if (storageFile is null)
        {
            return 0;
        }

        //Calculate the hash code for the storageFile.
        return storageFile.DisplayName.GetHashCode();
    }
}
