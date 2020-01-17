namespace Kneat.SW.Domain.Infrastructure.Common.Model
{
    public class HttpRequestResult<T>
    {
        public T Result { get; protected set; }
        public bool Success { get; protected set; }
        public string Message { get; protected set; }

        public void SetResult(T result)
        {
            Result = result;
        }

        public void SetMessage(string message)
        {
            Message = message;
        }

        public void SetStatus(bool success)
        {
            Success = success;
        }
    }
}
