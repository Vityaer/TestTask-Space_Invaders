using Db.CommonDictionaries;
using Factories.Abstractions;
using Game.Bullets;
using Game.GameModels;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Factories
{
    public class BonusFactory : BaseFactory<GameBullet>, IStartable
    {
        private readonly CommonDictionaries _commonDictionaries;

        private GameBonus _bonusTemplate;
        
        public BonusFactory(IObjectResolver objectResolver, CommonDictionaries commonDictionaries) : base(objectResolver)
        {
            _commonDictionaries = commonDictionaries;
        }

        public void Start()
        {
            _bonusTemplate = Resources.Load<GameBonus>(Constants.ResourcesPath.BONUS_TEMPLATE_PATH);
        }

        public GameBonus Create(string weaponId, Vector3 position)
        {
            var weaponModel = _commonDictionaries.Weapons[weaponId];

            var bonus = Object.Instantiate(_bonusTemplate, position, Quaternion.identity);
            bonus.Init(weaponModel);
            return bonus;
        }
    }
}
