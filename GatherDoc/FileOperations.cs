namespace GatherDoc
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class FileOperations
    {
        public List<string> GetFolderSubPaths(string folderAbsolutePath, ReadType readType, PathType pathType)
        {
            List<string> fileAbsolutePaths = new List<string>();
            DirectoryInfo folder = new DirectoryInfo(folderAbsolutePath);
            FileSystemInfo[] nextFileSystemInfos;
            switch (readType)
            {
                case ReadType.File:
                    // Traversing files
                    nextFileSystemInfos = folder.GetFiles();
                    break;
                case ReadType.Directory:
                    // Traversing folders
                    nextFileSystemInfos = folder.GetDirectories();
                    break;
                default:
                    throw new Exception("Not support folder read type!");
            }
            foreach (FileSystemInfo nextFileSystemInfo in nextFileSystemInfos)
            {
                switch (pathType)
                {
                    case PathType.Absolute:
                        fileAbsolutePaths.Add(nextFileSystemInfo.FullName);
                        break;
                    case PathType.Relative:
                        fileAbsolutePaths.Add(nextFileSystemInfo.Name);
                        break;
                    default:
                        throw new Exception("Not support folder path type!");
                }
            }
            return fileAbsolutePaths;
        }

        public List<string> GetAllFile(string folderAbsolutePath)
        {
            return GetAllFileBFS(new List<string>() { folderAbsolutePath });
        }


        private List<string> GetAllFileBFS(List<string> folderAbsolutePaths)
        {
            var folderList = new List<string>();
            var fileList = new List<string>();
            if (folderAbsolutePaths.Count == 0)
            {
                return fileList;
            }
            foreach (var folderAbsolutePath in folderAbsolutePaths)
            {
                folderList.AddRange(GetFolderSubPaths(folderAbsolutePath, ReadType.Directory, PathType.Absolute));
                var subFileList = GetFolderSubPaths(folderAbsolutePath, ReadType.File, PathType.Absolute);
                foreach (var subFile in subFileList)
                {
                    fileList.Add(subFile);
                }
            }
            fileList.AddRange(GetAllFileBFS(folderList));
            return fileList;
        }
    }
    public enum ReadType
    {
        File = 1,
        Directory = 2
    }

    public enum PathType
    {
        Absolute = 1,
        Relative = 2
    }
}
