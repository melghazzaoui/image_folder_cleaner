using System;
using ImgComparer.Utils;

namespace ImgComparer
{
    class ImageFolderComparer : FolderComparer
    {
        public delegate void FilesComparedDelegate(object sender, FilesComparedEventArgs args);
        public event FilesComparedDelegate FilesComparedEvent;

        public ImageFolderComparer(string referenceFolderPath, string targetFolderPath)
            : base(referenceFolderPath, targetFolderPath, @"(.*\.jpg)|(.*\.png)")
        {

        }

        protected override bool filesEqual(string file1, string file2)
        {
            return ImageUtil.filesEqual(file1, file2);
        }

        protected override void handleFilesEqual(string file1, string file2)
        {
            FilesComparedEvent?.Invoke(this, new FilesComparedEventArgs(true, file1, file2));
        }

        protected override void handleFilesDifferent(string file1, string file2)
        {
            FilesComparedEvent?.Invoke(this, new FilesComparedEventArgs(false, file1, file2));
        }
    }
}
