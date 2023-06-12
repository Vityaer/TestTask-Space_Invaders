using Game.Services.Storages;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace Game.Services.Enemies
{
    public class EnemyAttackService : ITickable
    {
        private const float BASE_TIME = 5f;
        private const float LEVEL_MULTIPLIER = 0.5f;
        private const float ENEMY_DEATH_SPEED_FACTOR = 0.05f;

        private readonly EntityStorage _entityStorage;
        private readonly GameController _gameController;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        private float _timeForAttack = 5f; 
        private float _time;

        public EnemyAttackService(EntityStorage entityStorage)
        {
            _entityStorage = entityStorage;
            _time = _timeForAttack;
        }

        public void Initialize()
        {
            _gameController.OnChangeLevel.Subscribe(OnChangeLevel).AddTo(_disposables);
            _entityStorage.OnDeathEnemy.Subscribe(OnDeathEnemy).AddTo(_disposables);
        }

        private void OnChangeLevel(int level)
        {
            _timeForAttack = BASE_TIME - level * LEVEL_MULTIPLIER;
            _time = _timeForAttack;
        }

        private void OnDeathEnemy(GameEnemy enemy)
        {
            _timeForAttack -= ENEMY_DEATH_SPEED_FACTOR;
        }

        public void Tick()
        {
            _time -= Time.deltaTime;
            if (_time < 0)
            {
                var enemies = _entityStorage.Enemies;
                if (enemies.Count > 0)
                {
                    var randomEnemy = enemies[Random.Range(0, enemies.Count)];
                    randomEnemy.Shoot();
                }
                _time = _timeForAttack;
            }
        }
    }
}
