using System;

namespace MonteOlimpo.Base.CoreException
{
    public  class InternalError
    {
        public Guid LogEntryId { get; set; }
        public Exception Exception { get; set; }
    }
}