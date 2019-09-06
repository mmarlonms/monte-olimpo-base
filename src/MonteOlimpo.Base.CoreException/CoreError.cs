namespace MonteOlimpo.CoreException
{
    public class CoreError
    {
        public string Key { get; }
        public string Message { get; }

        protected CoreError(string key, string message)
        {
            Key = key;
            Message = message;
        }
    }
}