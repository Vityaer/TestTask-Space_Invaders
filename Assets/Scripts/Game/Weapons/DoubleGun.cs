using Factories;
using Game.Weapons.Abstractions;
using Models;
using UnityEngine;
using VContainer;

namespace Game.Weapons
{
    public class DoubleGun : GameWeapon
    {
        private BulletFactory _bulletFactory;

        private Vector3 _leftChunkDirection = Vector3.left;
        private Vector3 _rightChunkDirection = Vector3.right;

        [Inject]
        private void Construct(BulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
        }

        public override void Shoot(Vector3 direction)
        {
            _bulletFactory.Create(WeaponModel.BulletId, Transform.position + direction + _leftChunkDirection, direction, gameObject.tag);
            _bulletFactory.Create(WeaponModel.BulletId, Transform.position + direction + _rightChunkDirection, direction, gameObject.tag);
        }
    }
}
