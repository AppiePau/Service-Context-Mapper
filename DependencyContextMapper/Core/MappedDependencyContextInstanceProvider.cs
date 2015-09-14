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
    /// <summary>Gets registered dependency according to the configured context. </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    public sealed class MappedDependencyContextInstanceProvider<TContext>
    {
        private readonly IContainerAdapter container;

        private readonly ContextMapper<TContext> mappings;

        private readonly IContextProvider<TContext> user;

        /// <summary>
        /// Initializes a new instance of the <see cref="MappedDependencyContextInstanceProvider{TContext}" /> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="mappings">The mappings.</param>
        /// <param name="container">The container.</param>
        public MappedDependencyContextInstanceProvider(IContextProvider<TContext> user, ContextMapper<TContext> mappings, IContainerAdapter container)
        {
            this.user = user;
            this.mappings = mappings;
            this.container = container;
        }

        /// <summary>
        /// Gets the implementation of the service based on the context.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns></returns>
        public TService GetInstance<TService>()
        {
            var context = this.user.GetContext();
            var implementation = this.mappings.GetInstance<TService>(context);

            return (TService)this.container.GetInstance(implementation);
        }
    }
}
