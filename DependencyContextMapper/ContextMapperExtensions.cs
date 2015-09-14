namespace DependencyContextMapper
{
    using Core;

    /// <summary> Provides extensions for the context mapper that should not be built in to the core class. </summary>
    public static class ContextMapperExtensions
    {
        /// <summary>
        /// Registers a context based service registration.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="mapper">The context mapper.</param>
        /// <param name="contexts">The context to associate the service with.</param>
        /// <returns></returns>
        public static FluentContextMappingRegistrar<TContext> RegisterContext<TContext>(this ContextMapper<TContext> mapper, params TContext[] contexts)
        {
            return new FluentContextMappingRegistrar<TContext>(mapper, contexts);
        }
    }
}
