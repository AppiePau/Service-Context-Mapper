namespace DependencyContextMapper.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class TestScenarios
    {
        public static void UserRole_DefaultImplementation_ReturnsDefaultImplementation(IContainerAdapter container)
        {
            var mapper = new ContextMapper<Role>(container, new RoleProvider(Role.User));

            mapper.RegisterDefault<IService, DefaultImplementation>();

            var service = container.GetInstance(typeof(IService));
            Assert.IsInstanceOfType(service, typeof(DefaultImplementation));
        }

        public static void UserRole_AdministratorImplementation_ReturnsDefaultImplementation(IContainerAdapter container)
        {
            var mapper = new ContextMapper<Role>(container, new RoleProvider(Role.User));

            mapper.RegisterDefault<IService, DefaultImplementation>();
            mapper.RegisterContext(Role.Administrator).Register<IService, AdministratorImplementation>();

            var service = container.GetInstance(typeof(IService));
            Assert.IsInstanceOfType(service, typeof(DefaultImplementation));
        }

        public static void AdministratorRole_AdministratorImplementation_ReturnsAdministratorImplementation(IContainerAdapter container)
        {
            var mapper = new ContextMapper<Role>(container, new RoleProvider(Role.Administrator));

            mapper.RegisterDefault<IService, DefaultImplementation>();
            mapper.RegisterContext(Role.Administrator).Register<IService, AdministratorImplementation>();

            var service = container.GetInstance(typeof(IService));
            Assert.IsInstanceOfType(service, typeof(AdministratorImplementation));
        }
    }
}
