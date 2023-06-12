using Db.CommonDictionaries;
using Extensions;
using Factories.Abstractions;
using Game.Bullets;
using Game.GameModels;
using Game.Weapons;
using Game.Weapons.Abstractions;
using UnityEngine;
using VContainer;

namespace Factories
{
    public class WeaponFactory : BaseFactory<GameWeapon>
    {
        private readonly CommonDictionaries _commonDictionaries;

        public WeaponFactory(IObjectResolver objectResolver, CommonDictionaries commonDictionaries) : base(objectResolver)
        {
            _commonDictionaries = commonDictionaries;
        }

        public GameWeapon Create(string weaponId, Vector3 position, string owner, Transform parent)
        {
            var weaponModel = _commonDictionaries.Weapons[weaponId];
            var path = weaponModel.PrefabPath.PrefabReplaceForResources();
            var gameWeapon = Resources.Load<GameWeapon>(path);
            var weapon = Object.Instantiate(gameWeapon, position, Quaternion.identity, parent);
            ObjectResolver.Inject(weapon);
            weapon.Init(weaponModel, owner);
            return weapon;
        }
    }
}
