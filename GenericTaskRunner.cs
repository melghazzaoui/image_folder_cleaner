using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgComparer
{
    class GenericTaskRunner : TaskRunner
    {
        public delegate bool fct(object obj);
        fct f;
        object args;
        public GenericTaskRunner(fct f, object args)
        {
            this.f = f;
            this.args = args;
        }
        protected override bool task()
        {
            return f.Invoke(args);
        }
    }
}
