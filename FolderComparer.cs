using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;

namespace ImgComparer
{
    abstract class FolderComparer
    {
        private Thread thread;
        private bool abortRequestPosted;
        private string refFolderPath;
        private string targetFolderPath;
        private string filterPattern;
        public delegate void SameFilenameFoundDelegate(object sender, SameFilenameEventArgs args);
        public event SameFilenameFoundDelegate sameFilenameFoundEvent;
        public event EventHandler comparisonTerminatedEvent;
        public delegate void StepsCountComputedDelegate(object sender, StepsCountComputedEventArgs args);
        public event StepsCountComputedDelegate stepsCountComputedEvent;
        public delegate void ProgressInfoDelegate(object sender, ProgressInfoEventArgs args);
        public event ProgressInfoDelegate progressInfoEvent;


        public FolderComparer(string refFolderPath, string targetFolderPath, string filterPattern)
        {
            this.refFolderPath = refFolderPath;
            this.targetFolderPath = targetFolderPath;
            this.filterPattern = filterPattern;
        }

        public string ReferenceFolderPath
        {
            get { return refFolderPath; }
        }

        public string TargetFolderPath
        {
            get { return targetFolderPath; }
        }

        private IDictionary<string, string> getDirectoryFiles(string directoryPath, string pattern)
        {
            IDictionary<string, string> fileMap = new Dictionary<string, string>();
            Regex regex = new Regex(pattern);
            System.IO.Directory.GetFiles(directoryPath, "*.*")
                .Where(path => regex.IsMatch(path.ToLower()))
                .ToList()
                .ForEach(path => fileMap.Add(System.IO.Path.GetFileName(path), path));
            return fileMap;
        }

        private void task()
        {
            IDictionary<string, string> refFiles = getDirectoryFiles(refFolderPath, filterPattern);
            IDictionary<string, string> targetFiles = getDirectoryFiles(targetFolderPath, filterPattern);
            IDictionary<string, string[]> sameFilenameMap = new Dictionary<string, string[]>();

            foreach (KeyValuePair<string, string> kv in targetFiles)
            {
                string path;
                if (refFiles.TryGetValue(kv.Key, out path))
                {
                    sameFilenameMap.Add(kv.Key, new string[] { path, kv.Value });
                }
            }

            refFiles.Clear();
            targetFiles.Clear();

            stepsCountComputedEvent?.Invoke(this, new StepsCountComputedEventArgs(sameFilenameMap.Count));

            int i = 0;
            foreach (KeyValuePair<string, string[]> kv in sameFilenameMap)
            {
                if (abortRequestPosted)
                {
                    return;
                }
                ++i;
                string path1 = kv.Value[0];
                string path2 = kv.Value[1];

                sameFilenameFoundEvent?.Invoke(this, new SameFilenameEventArgs(kv.Key, path1, path2));

                if (filesEqual(path1, path2))
                {
                    handleFilesEqual(path1, path2);
                }
                else
                {
                    handleFilesDifferent(path1, path2);
                }

                progressInfoEvent?.Invoke(this, new ProgressInfoEventArgs(i));
            }
            comparisonTerminatedEvent(this, new EventArgs());
        }

        protected abstract bool filesEqual(string file1, string file2);
        protected abstract void handleFilesEqual(string file1, string file2);
        protected abstract void handleFilesDifferent(string file1, string file2);

        public void compare()
        {
            if (thread != null && thread.IsAlive)
            {
                thread.Abort();
            }
            abortRequestPosted = false;
            thread = new Thread(new ThreadStart(task));
            thread.Start();
        }

        public void abort()
        {
            abortRequestPosted = true;
            thread.Abort();
            abortRequestPosted = false;
            thread = null;
        }

        private static string toLowerTrimEnd(string str, params char[]trimChars)
        {
            return str.ToLower().TrimEnd(trimChars);
        }

        public static string toNormalizedPath(string path)
        {
            char[] trimChars = new char[] { System.IO.Path.DirectorySeparatorChar };
            return StringUtil.trimDuplicates(path.ToLower().Replace('/', System.IO.Path.DirectorySeparatorChar).TrimEnd(trimChars), System.IO.Path.DirectorySeparatorChar);
        }

        public static bool comparePaths(string path1, string path2)
        {
            path1 = toNormalizedPath(path1);
            path2 = toNormalizedPath(path2);
            return path1.Equals(path2);
        }

        private static string[] getNormalizedPathSegments(string path)
        {
            return path.Split(new char[] { System.IO.Path.DirectorySeparatorChar },StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] getPathSegments(string path)
        {
            path = toNormalizedPath(path);
            return getNormalizedPathSegments(path);
        }

        public static bool checkFolderInclusion(string path1, string path2)
        {
            path1 = toNormalizedPath(path1);
            path2 = toNormalizedPath(path2);
            if (path1.Equals(path2))
            {
                return true;
            }

            string[] segments1 = getNormalizedPathSegments(path1);
            string[] segments2 = getNormalizedPathSegments(path2);

            string[] shorter = segments1.Length <= segments2.Length ? segments1 : segments2;
            string[] longer = segments1.Length > segments2.Length ? segments1 : segments2;

            for(int i=0; i<shorter.Length;++i)
            {
                if (!shorter[i].Equals(longer[i]))
                {
                    return false;
                }
            }

            return true;

        }

        public static bool checkFolderInclusionNormalized(string path1, string path2)
        {
            return path1.StartsWith(path2) || path2.StartsWith(path1);
        }


    }
}
