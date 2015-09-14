namespace DependencyContextMapper.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ninject;
    using DependencyContextMapper.Tests.Adapters;

    [TestClass]
    public sealed class NinjectTests
    {
        [TestMethod]
        public void RegisterDefault_DefaultImplementation_ReturnsDefaultImplementation()
        {
            var container = GetNinjectAdapter();

            TestScenarios.UserRole_DefaultImplementation_ReturnsDefaultImplementation(container);
        }

        [TestMethod]
        public void ForRole_AdministratorImplementation_ReturnsDefaultImplementation()
        {
            var container = GetNinjectAdapter();

            TestScenarios.UserRole_AdministratorImplementation_ReturnsDefaultImplementation(container);
        }

        [TestMethod]
        public void ForRole_AdministratorRoleAndImplementation_ReturnsAdministratorImplementation()
        {
            var container = GetNinjectAdapter();

            TestScenarios.AdministratorRole_AdministratorImplementation_ReturnsAdministratorImplementation(container);
        }

        private static NinjectAdapter GetNinjectAdapter()
        {
            return new NinjectAdapter(new StandardKernel());
        }
    }
}
