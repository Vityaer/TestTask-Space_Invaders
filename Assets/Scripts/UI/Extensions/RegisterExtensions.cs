using UI.Common;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.UI.Extensions
{
    public static class RegisterExtensions
    {
        public static void RegisterUiView<TController, TView>(this IContainerBuilder builder, TView viewPrefab, Transform parent)
            where TController : IUiController
            where TView : UiView
        {
            builder.Register<TController>(Lifetime.Singleton)
                .AsImplementedInterfaces().AsSelf();

            builder.Register((resolver) =>
            {
                var view = UnityEngine.Object.Instantiate(viewPrefab, parent);
                resolver.Inject(view);

                return view;
            }, Lifetime.Singleton)
                .AsImplementedInterfaces().AsSelf();
        }
    }
}
