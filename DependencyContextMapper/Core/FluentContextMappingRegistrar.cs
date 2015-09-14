/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * SERVICE CONTEXT MAPPER - Copyright 2013 (c) Paul Appeldoorn (DEVSHED.NL)
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
namespace DependencyContextMapper.Core
{
    /// <summary>
    /// Makes fluent registrations of context based mappings.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    public sealed class FluentContextMappingRegistrar<TContext>
    {
        private ContextMapper<TContext> mappings;

        private readonly TContext[] contexts;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentContextMappingRegistrar{TContext}"/> class.
        /// </summary>
        /// <param name="mappings">The mappings.</param>
        /// <param name="contexts">The contexts.</param>
        public FluentContextMappingRegistrar(ContextMapper<TContext> mappings, params TContext[] contexts)
        {
            this.mappings = mappings;
            this.contexts = contexts;
        }

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <returns></returns>
        public FluentContextMappingRegistrar<TContext> Register<TService, TImplementation>()
            where TService : class
            where TImplementation : TService
        {
            foreach (var context in this.contexts)
            {
                this.mappings.Register<TService, TImplementation>(context);
            }
       
            return this;
        }
    }
}
