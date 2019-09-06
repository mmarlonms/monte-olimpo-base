using MonteOlimpo.CoreException;

namespace MonteOlimpo.Sample.WebApi.Exceptions
{
    public class CustonException : CoreException<CustonError>
    {
        public CustonException() : base()
        {

        }

        public CustonException(params CustonError[] errors)
        {
            AddError(errors);
        }

        public override string Key => "CustonException";
    }
}
