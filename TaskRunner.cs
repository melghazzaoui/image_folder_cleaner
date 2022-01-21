using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImgComparer
{
    abstract class TaskRunner
    {
        public delegate void TaskFinishedDelegate(object sender, TaskFinishedEventArgs args);
        public event TaskFinishedDelegate TaskFinishedEvent;

        public delegate void StepsCountComputedDelegate(object sender, StepsCountComputedEventArgs args);
        public event StepsCountComputedDelegate stepsCountComputedEvent;

        public delegate void ProgressInfoDelegate(object sender, ProgressInfoEventArgs args);
        public event ProgressInfoDelegate progressInfoEvent;

        private Thread thread;
        private bool abortRequestPosted;

        protected abstract void task();

        protected void invokeStepsCountEvent(int count)
        {
            stepsCountComputedEvent?.Invoke(this, new StepsCountComputedEventArgs(count));
        }

        protected void invokeProgressEvent(int progress)
        {
            progressInfoEvent?.Invoke(this, new ProgressInfoEventArgs(progress));
        }

        private void taskCaller()
        {
            TaskFinishedEventArgs.StatusValues status = TaskFinishedEventArgs.StatusValues.SUCCESS;
            string msg = null;
            try
            {
                task();
            }
            catch (Exception e)
            {
                status = TaskFinishedEventArgs.StatusValues.FAILURE;
                msg = e.Message;
            }
            finally
            {
                TaskFinishedEvent?.Invoke(this, new TaskFinishedEventArgs(status, msg));
            }
        }

        public void Run()
        {
            if (thread != null && thread.IsAlive)
            {
                thread.Abort();
            }
            abortRequestPosted = false;
            thread = new Thread(new ThreadStart(taskCaller));
            thread.Start();
        }

        public void Abort()
        {
            abortRequestPosted = true;
            thread.Abort();
            abortRequestPosted = false;
            thread = null;
        }

        public bool IsAbortRequestPosted()
        {
            return abortRequestPosted;
        }
    }

    
}
