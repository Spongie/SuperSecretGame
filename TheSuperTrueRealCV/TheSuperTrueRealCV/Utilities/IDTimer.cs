namespace CV_clone.Utilities
{
    class IDTimer : Timer
    {
        private int id;

        public IDTimer(int id, float ms)
            : base(ms)
        {
            this.id = id;
        }

        public int ID
        {
            get { return id; }
        }
    }
}
