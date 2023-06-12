using Game.Services;
using System;
using UI.Common;
using UniRx;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace UI.GameHud.GameResult
{
    public class GameResultController : UiController<GameResultView>, IInitializable, IDisposable
    {
        private readonly GameController _gameController;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public GameResultController(GameController gameController)
        {
            _gameController = gameController;
        }

        public void Initialize()
        {
            Close();
            _gameController.OnEndGame.Subscribe(_ => OnEndGame()).AddTo(_disposables);
            View.MainMenuButton.OnClickAsObservable().Subscribe(_ => LoadMainMenu());
        }

        private void LoadMainMenu()
        {
            SceneManager.LoadScene(Constants.Common.MainMenuSceneName);
        }

        private void OnEndGame()
        {
            Open();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
