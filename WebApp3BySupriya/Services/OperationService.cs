using System;

namespace WebApp3BySupriya.Services
{
    public class OperationService : ITransientService, IScopedService, ISingletonService
    {
        public Guid OperationId { get; }

        public OperationService()
        {
            OperationId = Guid.NewGuid();
        }
    }
}
