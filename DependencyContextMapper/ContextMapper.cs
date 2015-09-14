/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DEPENDENCY CONTEXT MAPPER - Copyright 2013 (c) Paul Appeldoorn (DEVSHED.NL)
 * 
 * To contact me, please visit my blog at http://blog.devshed.nl/.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
 * associated documentation files (the "Software"), to deal in the Software without restriction, including 
 * without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
 * copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the 
 * following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial 
 * portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT 
 * LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO 
 * EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER 
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE 
 * USE OR OTHER DEALINGS IN THE SOFTWARE.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
namespace DependencyContextMapper
{
    using Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Registers services in the dependency injection container with a wrapper in order to return the instance keyed by context.
    /// </summary>
    /// <example><![CDATA[var container = new Container();
    /// var mapper = new ContextMapper<TContext>(container, new ContextProvider(Role.User));
    ///  
    /// mapper.RegisterDefault<IService, DefaultImplementation>();
    /// mapper.RegisterContext(Role.Administrator).Register<IService, AdministratorImplementation>();
    ///     
    /// var service = container.GetInstance<IService>();]]></example>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    public sealed class ContextMapper<TContext>
    {
        private readonly IContainerAdapter container;

        private readonly Dictionary<Type, Type> defaults;

        private readonly Dictionary<TContext, Dictionary<Type, Type>> mappings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextMapper{TContext}"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="contextProvider">The context provider.</param>
        public ContextMapper(IContainerAdapter container, IContextProvider<TContext> contextProvider)
            : this(container)
        {
            container.Register<IContextProvider<TContext>>(() => new TracingContextProviderDecorator<TContext>(contextProvider));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextMapper{TContext}"/> class.
        /// </summary>
        /// <param name="container"> The dependency injection container adapter. </param>
        /// <remarks>
        ///     By only instantiating with the container 
        /// </remarks>
        public ContextMapper(IContainerAdapter container)
        {
            this.container = container;

            this.container.Register(() => this.container);
            this.container.Register(() => this);

            this.mappings = new Dictionary<TContext, Dictionary<Type, Type>>();
            this.defaults = new Dictionary<Type, Type>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextMapper{TContext}"/> class. 
        /// With a lambda to provide the context. The expression is invoked every time when a service is requested.
        /// </summary>
        /// <param name="container">The container to register against.</param>
        /// <param name="lambda">The lambda expression to invoke for the current context.</param>
        public ContextMapper(IContainerAdapter container, Func<TContext> lambda)
            : this(container, new LambaContextProvider<TContext>(lambda))
        {
        }
        
        /// <summary>
        /// Registers a service with a specified context.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="context">The context.</param>
        /// <exception cref="System.InvalidOperationException">The specific RService has already been registered for Context TContext.</exception>
        public void Register<TService, TImplementation>(TContext context)
            where TService : class
            where TImplementation : TService
        {
            if (!this.mappings.ContainsKey(context))
            {
                this.mappings.Add(context, new Dictionary<Type, Type>());
            }

            if (!this.mappings[context].ContainsKey(typeof(TService)))
            {
                this.RegisterInContainer<TService>();
                this.mappings[context].Add(typeof(TService), typeof(TImplementation));
            }
            else
            {
                throw new InvalidOperationException(
                    "The specific Context " + typeof(TService).Name
                    + " has already been registered for Context '" + context.ToString() + "'.");
            }
        }

        /// <summary>
        /// Registers the default implementation for a service interface when a context is not configured.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <exception cref="System.InvalidOperationException">The default Context  + typeof(TService).Name +  has already been registered.</exception>
        public void RegisterDefault<TService, TImplementation>()
            where TService : class
            where TImplementation : TService
        {
            if (!this.defaults.ContainsKey(typeof(TService)))
            {
                this.RegisterInContainer<TService>();
                this.defaults.Add(typeof(TService), typeof(TImplementation));
            }
            else
            {
                throw new InvalidOperationException(
                    "The default Context " + typeof(TService).Name + " has already been registered.");
            }
        }

        internal Type GetInstance<TInterface>(TContext context)
        {
            if (!this.mappings.ContainsKey(context) || !this.mappings[context].ContainsKey(typeof(TInterface)))
            {
                if (!this.defaults.ContainsKey(typeof(TInterface)))
                {
                    throw new InvalidOperationException(
                        "No default registration for type '" + typeof(TInterface).Name + "' found. "
                        + "Please use RegisterDefault<TService, TImplementation>() to make a registration for any context.");
                }

                return this.defaults[typeof(TInterface)];
            }

            return this.mappings[context][typeof(TInterface)];
        }

        private void RegisterInContainer<TService>() where TService : class
        {
            if (!this.IsRegisteredAsDefault<TService>() && !this.IsRegisteredAsSpecific<TService>())
            {
                this.container.Register<TService>(this.GetInstanceProviderDelegate<TService>(this.container));
            }
        }

        private bool IsRegisteredAsSpecific<TService>() where TService : class
        {
            return this.mappings.Values.Any(e => e.ContainsKey(typeof(TService)));
        }

        private bool IsRegisteredAsDefault<TService>() where TService : class
        {
            return this.defaults.ContainsKey(typeof(TService));
        }

        private Func<TService> GetInstanceProviderDelegate<TService>(IContainerAdapter container)
        {
            return () => GetMappedDependencyContextInstanceProvider(container).GetInstance<TService>();
        }

        private static MappedDependencyContextInstanceProvider<TContext> GetMappedDependencyContextInstanceProvider(IContainerAdapter container)
        {
            return (MappedDependencyContextInstanceProvider<TContext>)container.GetInstance(typeof(MappedDependencyContextInstanceProvider<TContext>));
        }
    }
}