namespace DependencyContextMapper.Core
{
    using System;

    internal sealed class LambaContextProvider<TContext> : IContextProvider<TContext>
    {
        private readonly Func<TContext> lambda;

        internal LambaContextProvider(Func<TContext> lambda)
        {
            this.lambda = lambda;
        }

        public TContext GetContext()
        {
            return this.lambda.Invoke();
        }
    }
}
