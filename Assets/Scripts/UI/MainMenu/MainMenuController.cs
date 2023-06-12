using System;
using UI.Common;
using UniRx;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace UI.MainMenu
{
    public class MainMenuController : UiController<MainMenuView>, IInitializable, IDisposable
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public void Initialize()
        {
            View.PlayButton.OnClickAsObservable().Subscribe(_ => StartGame()).AddTo(_disposables);
        }

        private void StartGame()
        {
            SceneManager.LoadScene(Constants.Common.GameplaySceneName);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}