using MonteOlimpo.CoreException;

namespace MonteOlimpo.Sample.WebApi.Exceptions
{
    public class CustonError : CoreError
    {
        protected CustonError(string key, string message) : base(key, message)
        {
        }


        public static readonly CustonError CustonErrorSample = new CustonError("custonErrorSample", "Exemplo de Erro");
    }
}