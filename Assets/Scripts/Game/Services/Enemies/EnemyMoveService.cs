using Cysharp.Threading.Tasks;
using Game.Services.Storages;
using System;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace Game.Services.Enemies
{
    public class EnemyMoveService : IInitializable, ITickable, IDisposable
    {
        private const float BASE_SPEED = 2f;
        private const float LEVEL_MULTIPLIER = 1.2f;
        private const float ENEMY_DEATH_SPEED_FACTOR = 0.2f;
        private const float DOWN_STEP = -1f;

        private readonly EntityStorage _entityStorage;
        private readonly GameController _gameController;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        private Vector2 _direction = Vector2.right;
        private float _speed;

        public EnemyMoveService(EntityStorage entityStorage, GameController gameController)
        {
            _gameController = gameController;
            _entityStorage = entityStorage;
        }

        public void Initialize()
        {
            _gameController.OnChangeLevel.Subscribe(OnChangeLevel).AddTo(_disposables);
            _gameController.OnEndGame.Subscribe(_ => OnEndGame()).AddTo(_disposables);
            _entityStorage.OnDeathEnemy.Subscribe(OnDeathEnemy).AddTo(_disposables);
        }

        private void OnChangeLevel(int level)
        {
            _speed = BASE_SPEED + level * LEVEL_MULTIPLIER;
        }

        private void OnDeathEnemy(GameEnemy enemy)
        {
            _speed += ENEMY_DEATH_SPEED_FACTOR;
        }

        public void Tick()
        {
            _direction.y = 0f;

            var enemies = _entityStorage.Enemies;

            var enemyOnBounds = enemies.Find(enemy => Mathf.Abs(enemy.transform.position.x) > 15f);
            if(enemyOnBounds != null)
            {
                _direction = (enemyOnBounds.transform.position.x > 0f) ? Vector2.left : Vector2.right;
                _direction.y = DOWN_STEP;
            }

            for (var i = 0; i < enemies.Count; i++)
            {
                enemies[i].Rigidbody.velocity = _direction * _speed;
            }

            var enemyOnEnd = enemies.Find(enemy => enemy.transform.position.y <= -10f);
            if (enemyOnEnd != null)
            {
                StopAllEnemies();
                _gameController.EndGame();
            }
        }

        private void OnEndGame()
        {
            StopAllEnemies();
        }

        private void StopAllEnemies()
        {
            for (var i = 0; i < _entityStorage.Enemies.Count; i++)
            {
                _entityStorage.Enemies[i].Rigidbody.velocity = Vector2.zero;
            }
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
