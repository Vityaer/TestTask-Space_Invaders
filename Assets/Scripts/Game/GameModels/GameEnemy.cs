using Db.CommonDictionaries;
using Extensions;
using Factories;
using Game.Common;
using Game.Weapons.Abstractions;
using Models;
using System;
using System.Linq;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class GameEnemy : MonoBehaviour, IDamagable, IDisposable
    {
        private const string DEFAULT_ENEMY_WEAPON_ID = "SimpleGun"; 
        private const float BONUS_POSIBILITY = 0.1f;

        [SerializeField] private Transform _transform;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Rigidbody2D _rigidbody;

        private WeaponFactory _weaponFactory;
        private GameWeapon _weapon;
        private CommonDictionaries _commonDictionaries;
        private Vector3 _attackDirection = Vector3.down;
        private ReactiveCommand<GameEnemy> _onDeath = new ReactiveCommand<GameEnemy>();
        private BonusFactory _bonusFactory;
        private EnemyModel _enemyModel;

        public IReactiveCommand<GameEnemy> OnDeath => _onDeath;
        public Rigidbody2D Rigidbody => _rigidbody;
        public EnemyModel Model => _enemyModel;

        [Inject]
        private void Construct(WeaponFactory weaponFactory, BonusFactory bonusFactory, CommonDictionaries commonDictionaries)
        {
            _commonDictionaries = commonDictionaries;
            _bonusFactory = bonusFactory;
            _weaponFactory = weaponFactory;
        }

        public void Init(EnemyModel enemyModel)
        {
            _enemyModel = enemyModel;
            var path = enemyModel.ViewResourcePath.ImageReplaceForResources();
            var sprite = Resources.Load<Sprite>(path);
            _spriteRenderer.sprite = sprite;

            _weapon = _weaponFactory.Create(DEFAULT_ENEMY_WEAPON_ID, transform.position, gameObject.tag, transform);
        }

        public void Shoot()
        {
            _weapon.Shoot(_attackDirection);
        }

        public void GetDamage()
        {
            _onDeath.Execute(this);
            if (UnityEngine.Random.Range(0f, 1f) < BONUS_POSIBILITY)
            {
                var countWeapons = _commonDictionaries.Weapons.Count;
                var randomWeapon = _commonDictionaries.Weapons.ElementAt(UnityEngine.Random.Range(0, countWeapons)).Value;
                _bonusFactory.Create(randomWeapon.Id, transform.position);
            }
            Destroy(gameObject);
        }

        public void Dispose()
        {
            _onDeath.Dispose();
        }
    }
}
