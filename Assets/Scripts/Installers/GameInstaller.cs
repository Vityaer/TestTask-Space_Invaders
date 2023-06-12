using Assets.Scripts.UI.Extensions;
using Factories;
using Game.Services;
using Game.Services.Enemies;
using Game.Services.Scores;
using Game.Services.Storages;
using Game.Services.Waves;
using UI.GameHud;
using UI.GameHud.GameResult;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installers
{
    public class GameInstaller : LifetimeScope
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameHudView _gameHudView;
        [SerializeField] private GameResultView _gameResultView;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            var canvas = Instantiate(_canvas);

            builder.RegisterUiView<GameHudController, GameHudView>(_gameHudView, canvas.transform);
            builder.RegisterUiView<GameResultController, GameResultView>(_gameResultView, canvas.transform);

            builder.Register<WaveService>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<ScoreService>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<EntityStorage>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<EnemyAttackService>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<EnemyMoveService>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<EnemyFactory>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<PlayerFactory>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<BulletFactory>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<WeaponFactory>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<BonusFactory>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<GameController>(Lifetime.Singleton).AsSelf();
        }
    }
}
