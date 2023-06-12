using Db.CommonDictionaries;
using Factories.Abstractions;
using Game;
using System;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Factories
{
    public class EnemyFactory : BaseFactory<GameEnemy>, IStartable, IDisposable
    {
        private readonly CommonDictionaries _commonDictionaries;
        private readonly ReactiveCommand<GameEnemy> _onCreate = new ReactiveCommand<GameEnemy>(); 
        private GameEnemy _enemyTemplate;

        public IObservable<GameEnemy> OnCreate => _onCreate;

        public EnemyFactory(IObjectResolver objectResolver, CommonDictionaries commonDictionaries) : base(objectResolver)
        {
            _commonDictionaries = commonDictionaries;
        }

        public void Start()
        {
            _enemyTemplate = Resources.Load<GameEnemy>(Constants.ResourcesPath.ENEMY_TEMPLATE_PATH);
        }

        public GameEnemy Create(string enemyId, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion())
        {
            var enemyModel = _commonDictionaries.Enemies[enemyId];
            var enemy = UnityEngine.Object.Instantiate(_enemyTemplate, position, rotation);
            ObjectResolver.Inject(enemy);
            enemy.Init(enemyModel);
            _onCreate.Execute(enemy);

            return enemy;
        }

        public void Dispose()
        {
            _onCreate.Dispose();
        }
    }
}
