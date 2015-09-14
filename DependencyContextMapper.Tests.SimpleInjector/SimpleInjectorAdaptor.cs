namespace DependencyContextMapper.Tests.Adapters
{
    using DependencyContextMapper;
    using SimpleInjector;
    using System;

    /// <summary> Provides an adapter for the SimpleInjector dependency injection container. </summary>
    public class SimpleInjectorAdapter : IContainerAdapter
    {
        private readonly Container container;

        public SimpleInjectorAdapter(Container container)
        {
            this.container = container;
        }

        public void Register<TService>(Func<TService> instance) where TService : class
        {
            this.container.Register<TService>(instance);
        }

        public object GetInstance(Type type)
        {
            return this.container.GetInstance(type);
        }
    }
}
