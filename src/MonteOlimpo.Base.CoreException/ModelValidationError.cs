namespace MonteOlimpo.Base.CoreException
{
    public class ModelValidationError : CoreError
    {
        public ModelValidationError(string key, string message) : base(key, message)
        {
        }
    }
}