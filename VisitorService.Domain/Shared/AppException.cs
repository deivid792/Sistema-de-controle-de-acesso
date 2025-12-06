namespace VisitorService.Domain.Shared.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
        public DomainException(string message, Exception innerException) : base(message, innerException) {}
    }

    public class InfrastructureException : DomainException
    {
        public InfrastructureException(string message) : base(message) { }
        public InfrastructureException(string message, Exception innerException) : base(message, innerException) {}
    }
    public class unexpectedException : DomainException
    {
        public unexpectedException(string message) : base(message) { }
        public unexpectedException(string message, Exception innerException): base(message, innerException) {}
    }
}
