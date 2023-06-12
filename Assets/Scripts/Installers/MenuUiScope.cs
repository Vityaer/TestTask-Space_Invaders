using Assets.Scripts.UI.Extensions;
using UI.MainMenu;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installers
{
    public class MenuUiScope : LifetimeScope
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private MainMenuView _mainMenuView;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            var canvas = Instantiate(_canvas);

            builder.RegisterUiView<MainMenuController, MainMenuView>(_mainMenuView, canvas.transform);
        }
    }
}
