using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgComparer
{
    class SameFilenameEventArgs : EventArgs
    {
        private string filename;
        private string path1;
        private string path2;

        public SameFilenameEventArgs(string filename, string path1, string path2): base()
        {
            this.filename = filename;
            this.path1 = path1;
            this.path2 = path2;
        }
    }
}
