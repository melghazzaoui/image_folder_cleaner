using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgComparer
{
    class FilesComparedEventArgs : EventArgs
    {
        private string file1;
        private string file2;
        private bool filesEqual;

        public FilesComparedEventArgs(bool filesEqual, string file1, string file2)
        {
            this.file1 = file1;
            this.file2 = file2;
            this.filesEqual = filesEqual;
        }

        public string File1
        {
            get { return file1; }
        }

        public string File2
        {
            get { return file2; }
        }

        public bool FilesEqual
        {
            get { return filesEqual; }
        }
    }
}
