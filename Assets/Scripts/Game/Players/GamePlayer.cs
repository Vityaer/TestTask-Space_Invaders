using Extensions;
using Factories;
using Game.Common;
using Game.Weapons.Abstractions;
using Models;
using System;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class GamePlayer : MonoBehaviour, IDamagable, IDisposable
    {
        public ReactiveCommand OnDeath = new ReactiveCommand();

        [SerializeField] private Transform _transform;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Rigidbody2D _rigidbody;

        private float _speed;
        private WeaponFactory _weaponFactory;
        private GameWeapon _weapon;
        private Vector2 _direction = Vector2.zero;
        private Vector3 _attackDirection = Vector3.up;

        [Inject]
        private void Construct(WeaponFactory weaponFactory)
        {
            _weaponFactory = weaponFactory;
        }

        public void Init(PlayerShipModel playerShipModel)
        {
            _speed = playerShipModel.Speed;

            var path = playerShipModel.ViewResourcePath.ImageReplaceForResources();
            var sprite = Resources.Load<Sprite>(path);
            _spriteRenderer.sprite = sprite;

            SetWeapon(playerShipModel.DefaultWeaponId);
        }

        public void SetWeapon(string weaponId)
        {
            if (_weapon != null)
                Destroy(_weapon.gameObject);

            _weapon = _weaponFactory.Create(weaponId, transform.position, gameObject.tag, transform);
        }

        private void Update()
        {
            _direction = Vector2.zero;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _direction.x = -1;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                _direction.x = 1;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                _direction.y = 1;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                _direction.y = -1;
            }

            _rigidbody.velocity = _direction.normalized * _speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            _weapon.Shoot(_attackDirection);
        }

        public void GetDamage()
        {
            OnDeath.Execute();
            Destroy(gameObject);
        }

        public void Dispose()
        {
            OnDeath.Dispose();
        }
    }
}
