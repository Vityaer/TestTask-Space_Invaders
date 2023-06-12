using Factories;
using Game.Common;
using System;
using System.Collections.Generic;
using UniRx;
using VContainer.Unity;

namespace Game.Services.Storages
{
    public class EntityStorage : IInitializable, IDisposable
    {
        private readonly EnemyFactory _enemyFactory; 
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public readonly List<GameEnemy> Enemies = new List<GameEnemy>();
        public readonly ReactiveCommand OnClearEnemies = new ReactiveCommand();
        public readonly ReactiveCommand<GameEnemy> OnDeathEnemy = new ReactiveCommand<GameEnemy>();

        public EntityStorage(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        public void Initialize()
        {
            _enemyFactory.OnCreate.Subscribe(OnCreateEnemy).AddTo(_disposables);
        }
        
        private void OnCreateEnemy(GameEnemy newEnemy)
        {
            Enemies.Add(newEnemy);
            newEnemy.OnDeath.Subscribe(RemoveEnemy).AddTo(_disposables);
        }

        private void RemoveEnemy(GameEnemy enemy)
        {
            Enemies.Remove(enemy);
            OnDeathEnemy.Execute(enemy);

            if (Enemies.Count == 0)
                OnClearEnemies.Execute();
        }


        public void EndGame()
        {
            Enemies.Clear();
        }

        public void Dispose()
        {
            OnDeathEnemy.Dispose();
            OnClearEnemies.Dispose();
            _disposables.Dispose();
        }
    }
}
