using Game.Common;
using Models;
using UnityEngine;
using UniRx;
using Extensions;

namespace Game.Bullets
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GameBullet : MonoBehaviour
    {
        public ReactiveCommand OnDeath = new ReactiveCommand();

        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private float _speed;
        private string _owner;

        public void Init(BulletModel bulletModel, Vector2 direction, string owner)
        {
            Destroy(gameObject, 20f);
            _owner = owner;
            _speed = bulletModel.Speed;
            _rigidbody.velocity = direction * _speed;

            var path = bulletModel.ViewResourcePath.ImageReplaceForResources();
            var sprite = Resources.Load<Sprite>(path);
            _spriteRenderer.sprite = sprite;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if(!collider.gameObject.CompareTag(_owner))
            {
                if (collider.gameObject.TryGetComponent<IDamagable>(out var gameController))
                {
                    gameController.GetDamage();
                    Destroy(gameObject);
                }
            }
        }
    }
}
