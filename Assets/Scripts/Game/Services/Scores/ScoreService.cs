using Game.Services.Storages;
using System;
using UniRx;
using VContainer.Unity;

namespace Game.Services.Scores
{
    public class ScoreService : IInitializable, IDisposable
    {
        private readonly EntityStorage _entityStorage;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        private readonly ReactiveCommand<int> _onChangeScore = new ReactiveCommand<int>();
        private readonly GameController _gameController;

        private int _bonus;
        private int _currentScore;

        public IObservable<int> OnChangeScore => _onChangeScore;

        public ScoreService(EntityStorage entityStorage, GameController gameController)
        {
            _gameController = gameController;
            _entityStorage = entityStorage;
        }

        public void Initialize()
        {
            _gameController.OnChangeLevel.Subscribe(OnChangeLevel).AddTo(_disposables);
            _entityStorage.OnDeathEnemy.Subscribe(OnDeathEnemy).AddTo(_disposables);
        }

        private void OnChangeLevel(int level)
        {
            _bonus = (level - 1) * 5;
        }

        private void OnDeathEnemy(GameEnemy enemy)
        {
            _currentScore += enemy.Model.Bounty + _bonus;
            _onChangeScore.Execute(_currentScore);
        }

        public void Dispose()
        {
            _disposables.Dispose();
            _onChangeScore.Dispose();
        }
    }
}
