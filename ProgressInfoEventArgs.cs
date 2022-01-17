namespace ImgComparer
{
    public class ProgressInfoEventArgs
    {
        private int progress;

        public ProgressInfoEventArgs(int progress)
        {
            this.progress = progress;
        }

        public int Progress
        {
            get { return progress; }
        }
    }
}