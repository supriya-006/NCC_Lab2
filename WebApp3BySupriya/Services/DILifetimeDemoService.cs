namespace WebApp3BySupriya.Services
{
    public class DILifetimeDemoService
    {
        public ITransientService TransientService { get; }
        public IScopedService ScopedService { get; }
        public ISingletonService SingletonService { get; }

        public DILifetimeDemoService(
            ITransientService transientService,
            IScopedService scopedService,
            ISingletonService singletonService)
        {
            TransientService = transientService;
            ScopedService = scopedService;
            SingletonService = singletonService;
        }
    }
}
