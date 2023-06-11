using Db.CommonDictionaries;
using Initializable;
using Json;
using Json.Impl;
using VContainer;
using VContainer.Unity;

namespace Installer
{
    public class ProjectLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.Register<JsonConverter>(Lifetime.Singleton).As<IJsonConverter>();

            builder.Register<CommonDictionaries>(Lifetime.Singleton);
            builder.RegisterEntryPoint<ProjectInitialize>();
        }
    }
}