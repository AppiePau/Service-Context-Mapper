namespace DependencyContextMapper.Tests
{
    using DependencyContextMapper;

    /// <summary> Provides the role for testing. </summary>
    public class RoleProvider : IContextProvider<Role>
    {
        private readonly Role role;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleProvider"/> class.
        /// </summary>
        /// <param name="role">The role.</param>
        public RoleProvider(Role role)
        {
            this.role = role;
        }

        /// <summary>
        /// Gets the role.
        /// </summary>
        /// <returns></returns>
        public Role GetContext()
        {
            return this.role;
        }
    }
}
