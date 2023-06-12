using Extensions;
using Models;
using UnityEngine;

namespace Game.GameModels
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class GameBonus : MonoBehaviour
    {
        private const string PLAYER_TAG = "Player";

        [SerializeField] private SpriteRenderer _spriteRenderer;

        private WeaponModel _weaponModel;

        public void Init(WeaponModel weaponModel)
        {
            Destroy(gameObject, 20f);
            var path = weaponModel.ViewResourcePath.ImageReplaceForResources();
            var sprite = Resources.Load<Sprite>(path);
            _spriteRenderer.sprite = sprite;
            _weaponModel = weaponModel;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag(PLAYER_TAG))
            {
                if (collider.gameObject.TryGetComponent<GamePlayer>(out var gamePlayer))
                {
                    gamePlayer.SetWeapon(_weaponModel.Id);
                    Destroy(gameObject);
                }
            }
        }
    }
}
