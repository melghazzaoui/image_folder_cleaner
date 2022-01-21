using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgComparer
{
    class TaskFinishedEventArgs
    {
        public enum StatusValues
        {
            SUCCESS,
            FAILURE
        }

        private StatusValues status;
        private string msg;

        public TaskFinishedEventArgs(StatusValues status, string msg)
        {
            this.status = status;
            this.msg = msg;
        }

        public StatusValues Status
        {
            get { return status; }
        }

        public string Msg
        {
            get { return msg; }
        }
    }
}
