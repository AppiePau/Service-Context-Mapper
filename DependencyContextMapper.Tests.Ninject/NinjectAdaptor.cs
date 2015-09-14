namespace DependencyContextMapper.Tests.Adapters
{
    using Ninject;
    using DependencyContextMapper;
    using System;

    /// <summary> Provides an adapter for the SimpleInjectoContainer. </summary>
    public class NinjectAdapter : IContainerAdapter
    {
        private readonly IKernel container;

        public NinjectAdapter(IKernel container)
        {
            this.container = container;
        }

        public void Register<TService>(Func<TService> instance) where TService : class
        {
            this.container.Bind<TService>().ToMethod<TService>(context => instance.Invoke());
        }

        public object GetInstance(Type type)
        {
            return this.container.Get(type);
        }
    }
}
