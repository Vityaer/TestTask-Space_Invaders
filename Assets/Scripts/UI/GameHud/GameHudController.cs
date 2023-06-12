using Game.Services;
using Game.Services.Scores;
using System;
using UI.Common;
using UniRx;
using VContainer.Unity;

namespace UI.GameHud
{
    public class GameHudController : UiController<GameHudView>, IInitializable, IDisposable
    {
        private readonly ScoreService _scoreService;
        private readonly GameController _gameController;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public GameHudController(ScoreService scoreService, GameController gameController)
        {
            _gameController = gameController;
            _scoreService = scoreService;
        }

        public void Initialize()
        {
            _scoreService.OnChangeScore.Subscribe(OnChangeScore).AddTo(_disposables);
            _gameController.OnChangeLifeCount.Subscribe(OnChangeLifeCount).AddTo(_disposables);
        }

        private void OnChangeScore(int score)
        {
            View.ScoreText.text = $"{score}";
        }

        private void OnChangeLifeCount(int lifeCount)
        {
            View.LifeCountText.text = $"{lifeCount}";
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
