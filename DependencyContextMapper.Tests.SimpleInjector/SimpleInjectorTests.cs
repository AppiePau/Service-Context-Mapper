namespace DependencyContextMapper.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DependencyContextMapper.Tests.Adapters;
    using SimpleInjector;

    /// <summary> Tests the ContextMapper class. </summary>
    [TestClass]
    public class SimpleInjectorTests
    {
        [TestMethod]
        public void RegisterDefault_DefaultImplementation_ReturnsDefaultImplementation()
        {
            var adapter = GetSimpleInjectorAdapter();

            TestScenarios.UserRole_DefaultImplementation_ReturnsDefaultImplementation(adapter);
        }

        [TestMethod]
        public void ForContext_AdministratorImplementation_ReturnsDefaultImplementation()
        {
            var adapter = GetSimpleInjectorAdapter();

            TestScenarios.UserRole_AdministratorImplementation_ReturnsDefaultImplementation(adapter);
        }

        [TestMethod]
        public void ForContext_AdministratorContextAndImplementation_ReturnsAdministratorImplementation()
        {
            var adapter = new SimpleInjectorAdapter(new Container());
            TestScenarios.AdministratorRole_AdministratorImplementation_ReturnsAdministratorImplementation(adapter);
        }

        private static SimpleInjectorAdapter GetSimpleInjectorAdapter()
        {
            return new SimpleInjectorAdapter(new Container());
        }
    }
}