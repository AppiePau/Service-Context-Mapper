# Service-Context-Mapper

Creates a context based switch mechanism to inject behavior based on an ambiant variable like a userrole.

For example:

```cs
public static void RegisterScopedRoleMappings(Container container)
{
    var adapter = new SimpleInjectorAdapter(container);
    var mapper = new ContextMapper<Role>(adapter);
    container.Register<IContextProvider<Role>, UserRoleContextProvider>(SimpleInjector.Lifestyle.Scoped);

    RegisterRoleMappings(mapper);
}

private static void RegisterRoleMappings(ContextMapper<Role> mapper)
{
    RegisterDefaults(mapper);

    mapper.RegisterContext(Role.Sales)
    .Register<IAuthorizedRepository<Customer>, SalesCustomerRepository>()
    .Register<IAuthorizedRepository<Order>, SalesOrderRepository>()
    .Register<IAuthorizedRepository<Article>, SalesArticleRepository>();
    
    mapper.RegisterContext(Role.Administrator)
    .Register<IAuthorizedRepository<Customer>, AdministratorCustomerRepository>()
    .Register<IAuthorizedRepository<Order>, AdministratorOrderRepository>()
    .Register<IAuthorizedRepository<Article>, AdministratorArticleRepository>();
}
```

Register default implementation if none is registered for the current role:

```cs
 private static void RegisterDefaults(ContextMapper<Role> mapper)
{
    mapper.RegisterDefault<IAuthorizedRepository<Customer>, EmptyRepository<Customer>>();
    mapper.RegisterDefault<IAuthorizedRepository<Order>, EmptyRepository<Order>>();
    mapper.RegisterDefault<IAuthorizedRepository<Article>, EmptyRepository<Article>>();
}
```
