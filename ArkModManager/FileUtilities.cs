// See https://aka.ms/new-console-template for more information

namespace ArkModManager;

public class FileUtilities
{
    public static void DeleteDirectory(String directory)
    {
        DeleteDirectory(new DirectoryInfo(directory));
    }

    public static void DeleteDirectory(DirectoryInfo directoryInfo)
    {
        foreach (var file in directoryInfo.EnumerateFiles("*", SearchOption.TopDirectoryOnly))
        {
            file.Attributes &= ~FileAttributes.ReadOnly;
            file.Delete();
        }

        foreach (var subdirectory in directoryInfo.EnumerateDirectories("*", SearchOption.TopDirectoryOnly))
            DeleteDirectory(subdirectory);

        directoryInfo.Delete(false /* Not recursive delete. */);
    }
}