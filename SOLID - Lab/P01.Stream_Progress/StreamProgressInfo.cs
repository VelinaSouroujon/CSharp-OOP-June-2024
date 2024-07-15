namespace P01.Stream_Progress
{
    public class StreamProgressInfo
    {
        private IStreamable stream;
        // We can stream everything that implements the interface IStreamable
        public StreamProgressInfo(IStreamable stream)
        {
            this.stream = stream;
        }

        public int CalculateCurrentPercent()
        {
            return (this.stream.BytesSent * 100) / this.stream.Length;
        }
    }
}
