using Autofac;

namespace Harkulwant.PalindromeChecker.Service
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<RepositoryService>().As<IRepositoryService>();
            builder.RegisterType<PalindromeService>().As<IPalindromeService>();
        }
    }
}
