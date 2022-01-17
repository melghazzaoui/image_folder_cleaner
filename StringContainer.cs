using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgComparer
{
    class StringContainer
    {
        private string str;
        public StringContainer(string str)
        {
            this.str = str;
        }

        public override string ToString()
        {
            return str;
        }
    }
}
