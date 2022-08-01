namespace TankGameAPI.Utils
{
    public class InvalidClientException : InvalidOperationException
    {
        public InvalidClientException(string? message) : base(message) { }
    }
}
