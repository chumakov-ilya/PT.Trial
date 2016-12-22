namespace PT.Trial.Common
{
    public class Message<T> where T : class
    {
        public T Payload { get; set; }

        public int ThreadId { get; set; }

        public Message() {}

        public Message(T payload, int threadId)
        {
            Payload = payload;
            ThreadId = threadId;
        }
    }
}