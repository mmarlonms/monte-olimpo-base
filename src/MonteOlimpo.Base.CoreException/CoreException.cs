using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MonteOlimpo.Base.CoreException
{
    public abstract class CoreException : Exception
    {
        public abstract string Key { get; }

        protected ICollection<CoreError> _errors = new List<CoreError>();

        public IEnumerable<CoreError> Errors => _errors;

        protected CoreException(string message)
            : base(message)
        {
        }

        protected CoreException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    public abstract class CoreException<T> : CoreException
        where T : CoreError
    {
        protected CoreException()
            : base("Ocorreu um erro de negócio, verifique a propriedade 'errors' para obter detalhes.")
        {
        }

        protected CoreException(string message)
            : base(message)
        {
        }

        protected CoreException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public CoreException AddError(params T[] errors)
        {
            foreach (var error in errors)
                _errors.Add(error);

            return this;
        }
    }
}
