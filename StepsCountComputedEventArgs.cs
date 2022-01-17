using System;

namespace ImgComparer
{
    public class StepsCountComputedEventArgs : EventArgs
    {
        private int count;
        public int Count
        {
            get { return count; }
        }

        public StepsCountComputedEventArgs(int count)
        {
            this.count = count;
        }
    }
}