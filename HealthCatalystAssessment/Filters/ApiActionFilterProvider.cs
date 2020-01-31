using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Unity;

namespace HealthCatalyst.Assessment.API.Filters
{
    /// <summary>
    /// An HTTP configuration provider to enable HTTP filtering
    /// </summary>
    public class ApiActionFilterProvider : ActionDescriptorFilterProvider, IFilterProvider
    {

        private readonly IUnityContainer container;
        private readonly ActionDescriptorFilterProvider _defaultProvider = new ActionDescriptorFilterProvider();


        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="container"></param>
        public ApiActionFilterProvider(IUnityContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// Registers the filters with the Unity container for a particular action method
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public new IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            var attributes = _defaultProvider.GetFilters(configuration, actionDescriptor);

            foreach (var attr in attributes)
            {
                container.BuildUp(attr.Instance.GetType(), attr.Instance);
            }
            return attributes;
        }

        /// <summary>
        /// Registers http filters provided in the config
        /// </summary>
        /// <param name="config"></param>
        public static void RegisterFilterProviders(HttpConfiguration config)
        {
            var providers = config.Services.GetFilterProviders().ToList();

            var defaultprovider = providers.FirstOrDefault(i => i is ActionDescriptorFilterProvider);
            if (defaultprovider != null)
                config.Services.Remove(typeof(IFilterProvider), defaultprovider);

            config.Services.Add(typeof(IFilterProvider), new ApiActionFilterProvider(UnityConfig.Container));
        }

    }
}