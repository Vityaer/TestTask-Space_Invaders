using Models;
using UnityEngine;

namespace Game.Weapons.Abstractions
{
    public abstract class GameWeapon : MonoBehaviour
    {
        [SerializeField] protected Transform Transform;
        protected WeaponModel WeaponModel;

        public void Init(WeaponModel weaponModel, string owner)
        {
            gameObject.tag = owner;
            WeaponModel = weaponModel;
        }
        public abstract void Shoot(Vector3 direction);
    }
}
