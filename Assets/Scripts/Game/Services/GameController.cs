using Factories;
using Game.Services.Storages;
using Game.Services.Waves;
using UnityEngine;
using VContainer.Unity;
using UniRx;
using System;

namespace Game.Services
{
    public class GameController : IInitializable, IStartable, IDisposable
    {
        private const string DEFAULT_PLAYER_SHIP = "StandardShip"; 
        private const int START_LIFE_COUNT = 3; 

        public readonly ReactiveCommand OnEndGame = new ReactiveCommand();
        
        private readonly WaveService _waveService;
        private readonly PlayerFactory _playerFactory;
        private readonly EntityStorage _entityStorage;
        private readonly ReactiveCommand<int> _onChangeLevel = new ReactiveCommand<int>(); 
        private readonly ReactiveCommand<int> _onChangeLifeCount = new ReactiveCommand<int>(); 
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        private int _level = -1;
        private int _currentLifeCount;
        private GamePlayer _player;

        public IObservable<int> OnChangeLevel => _onChangeLevel;
        public IObservable<int> OnChangeLifeCount => _onChangeLifeCount;

        public GameController(WaveService waveService, PlayerFactory playerFactory, EntityStorage entityStorage)
        {
            _playerFactory = playerFactory;
            _waveService = waveService;
            _entityStorage = entityStorage;
        }

        public void Initialize()
        {
            _currentLifeCount = START_LIFE_COUNT;
            _entityStorage.OnClearEnemies.Subscribe(_ => NextLevel()).AddTo(_disposables);
        }

        public void Start()
        {
            CreatePlayer();
            NextLevel();
        }

        private void CreatePlayer()
        {
            _currentLifeCount -= 1;
            if (_currentLifeCount >= 0)
            {
                _onChangeLifeCount.Execute(_currentLifeCount);
                _player = _playerFactory.Create(DEFAULT_PLAYER_SHIP, Vector3.zero);
                _player.OnDeath.Subscribe(_ => CreatePlayer()).AddTo(_disposables);
            }
            else
            {
                EndGame();
            }
        }

        public void EndGame()
        {
            _entityStorage.EndGame();
            OnEndGame.Execute();
        }

        private void NextLevel()
        {
            _level += 1;
            _onChangeLevel.Execute(_level);
            _waveService.CreateEnemies();
        }

        public void Dispose()
        {
            OnEndGame.Dispose();
            _onChangeLifeCount.Dispose();
            _onChangeLevel.Dispose();
            _disposables.Dispose();
        }
    }
}
