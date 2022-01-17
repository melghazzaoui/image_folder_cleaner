using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgComparer
{
    class FileEventArgs : EventArgs
    {
        private string path;

        public string Path
        {
            get { return path; }
        }
        public FileEventArgs(string path)
        {
            this.path = path;
        }
    }
}
