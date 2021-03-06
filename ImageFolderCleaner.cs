using System;
using System.Collections.Generic;
using System.Threading;
using System.Text.RegularExpressions;
using ImgComparer.Utils;

namespace ImgComparer
{
    class ImageFolderCleaner : TaskRunner
    {
        public delegate void FileDeletedDelegate(object sender, FileEventArgs args);
        public event FileDeletedDelegate FileDeletedEvent;

        private string path;
        private Regex imageRegex = new Regex(@".*\.(jpg|png|jpeg)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public ImageFolderCleaner(string path)
        {
            this.path = path;
        }

        public string Path
        {
            get { return path; }
        }

        private void getFilesRecursive(string dir, Regex regex, IList<string> files)
        {
            if (System.IO.Directory.Exists(dir))
            {
                string[] allFiles = System.IO.Directory.GetFiles(dir);
                foreach(string file in allFiles)
                {
                    if (regex.Match(file).Success)
                    {
                        files.Add(file);
                    }
                }

                string[] children = System.IO.Directory.GetDirectories(dir);
                foreach(string child in children)
                {
                    getFilesRecursive(child, regex, files);
                }
            }
        }

        private IList<string> getFiles(string dir, Regex regex)
        {
            IList<string> files = new List<string>();
            getFilesRecursive(path, imageRegex, files);
            return files;
        }

        private IList<string> filteredFiles;
        public IList<string> FilteredFiles { get { return filteredFiles; } }

        protected override bool task()
        {
            IList<string> files = getFiles(path, imageRegex);
            filteredFiles = files;

            invokeStepsCountEvent(files.Count);

            IDictionary<string, string> filenameMap = new Dictionary<string, string>();
            int progress = 0;
            foreach (string imgPath in files)
            {
                progress++;
                string filename = System.IO.Path.GetFileName(imgPath);
                if (filenameMap.ContainsKey(filename))
                {
                    string refPath = filenameMap[filename];
                    if (ImageUtil.filesEqual(refPath, imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                        FileDeletedEvent?.Invoke(this, new FileEventArgs(imgPath));
                    }
                }
                else
                {
                    filenameMap.Add(filename, imgPath);
                }
                invokeProgressEvent(progress);
            }
            return true;
        }
    }
}
