using System;

namespace MonteOlimpo.CoreException
{
    public  class InternalError
    {
        public Guid LogEntryId { get; set; }
        public Exception Exception { get; set; }
    }
}