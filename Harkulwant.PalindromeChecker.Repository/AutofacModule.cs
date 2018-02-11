using Autofac;
using Harkulwant.PalindromeChecker.Repository.Context;
using Microsoft.Extensions.Configuration;

namespace Harkulwant.PalindromeChecker.Repository
{
    public class AutofacModule : Autofac.Module
    {
        const string configParameterName = "config";
        protected IConfiguration Configuration { get; }

        public AutofacModule(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<DemoDatabaseContext>().As<IDemoDatabaseContext>().WithParameter(configParameterName, Configuration);
            builder.RegisterType<PalindromeRepository>().As<IPalindromeRepository>();
        }
    }
}
