using Factories;
using Game.Weapons.Abstractions;
using Models;
using UnityEngine;
using VContainer;

namespace Game.Weapons
{
    public class SimpleGun : GameWeapon
    {
        private BulletFactory _bulletFactory;

        [Inject]
        private void Construct(BulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
        }

        public override void Shoot(Vector3 direction)
        {
            _bulletFactory.Create(WeaponModel.BulletId, Transform.position + direction, direction, gameObject.tag);
        }
    }
}
