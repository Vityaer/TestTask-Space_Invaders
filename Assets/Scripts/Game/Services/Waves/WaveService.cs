using Db.CommonDictionaries;
using Factories;
using Models;
using System;
using System.Linq;
using UnityEngine;
using VContainer.Unity;

namespace Game.Services.Waves
{
    public class WaveService
    {
        private readonly CommonDictionaries _commonDictionaries;
        private readonly EnemyFactory _enemyFactory;

        private Vector3 _startPos = new Vector3(-5, 5, 0);
        private const float HORIZONTAL_SPACE = 3f;
        private const float VERTICALL_SPACE = 2.5f;

        public WaveService(CommonDictionaries commonDictionaries, EnemyFactory enemyFactory)
        {
            _commonDictionaries = commonDictionaries;
            _enemyFactory = enemyFactory;
        }

        public void CreateEnemies()
        {
            CreateEnemies(_commonDictionaries.Levels.ElementAt(0).Value);
        }

        private void CreateEnemies(LevelModel level)
        {
            var row = -1;
            foreach (var wave in level.Waves)
            {
                row += 1;
                for (var i = 0; i < wave.Count; i++)
                {
                    _enemyFactory.Create(wave.Id, _startPos + new Vector3(HORIZONTAL_SPACE * i, VERTICALL_SPACE * row, 0));
                }
            }
        }
    }
}
