using System;
using System.Linq;
using System.Configuration;
using Unity;
using Unity.RegistrationByConvention;
using Unity.Injection;
using Unity.Lifetime;
using System.Data.Entity;
using HealthCatalyst.Assessment.API.Logging;
using HealthCatalyst.Assessment.Domain.DataAccess;
using HealthCatalyst.Assessment.Domain.Services;
using HealthCatalyst.Assessment.API;

namespace HealthCatalyst.Assessment.API
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterComponents(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// A reference name for the default ISessionFactory instances
        /// </summary>
        public const string DatabaseName = "RosterContext";


        /// <summary>
        /// Register components
        /// </summary>
        /// <param name="container"></param>
        public static void RegisterComponents(IUnityContainer container)
        {
            RegisterTypes(container);
            Logger.Initialize(System.Configuration.ConfigurationManager.AppSettings["loggerName"]);
        }

        /// <summary>
        /// Initializes a database session for connecting to the database
        /// </summary>
        /// <param name="container"></param>
        public static DbContext InitiateDataContext(IUnityContainer container)
        {
            var dbSession = new RosterContext(DatabaseName);
            dbSession.Configuration.EnsureTransactionsForFunctionsAndCommands = false;
            dbSession.Database.Log = (msg) => Logger.WriteLine(msg);
            return dbSession;
        }


        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterTypes(AllClasses.FromLoadedAssemblies().Where(t => t.GetType().Name.EndsWith("Facade")),
                WithMappings.FromAllInterfaces, WithName.Default, WithLifetime.ContainerControlled);

            container.RegisterInstance<AutoMapper.IMapper>(MappingConfig.Mapper.CreateMapper(), InstanceLifetime.Singleton);

            var dbSession = InitiateDataContext(container);
            container.RegisterInstance(DatabaseName, dbSession);

            bool.TryParse(ConfigurationManager.AppSettings["searchStrictMatching"], out bool searchStrictMatching);
            container.RegisterType<IRosterService, RosterService>(new InjectionConstructor(dbSession, searchStrictMatching));
            container.RegisterType(typeof(Filters.DBTransactionFilter), new InjectionProperty(nameof(Filters.DBTransactionFilter.DbSession), dbSession));
        }
    }
}