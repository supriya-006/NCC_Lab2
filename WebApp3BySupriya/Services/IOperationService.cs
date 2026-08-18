using System;

namespace WebApp3BySupriya.Services
{
    public interface IOperation
    {
        Guid OperationId { get; }
    }

    public interface ITransientService : IOperation
    {
    }

    public interface IScopedService : IOperation
    {
    }

    public interface ISingletonService : IOperation
    {
    }
}
