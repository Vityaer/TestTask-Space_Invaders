using Db.CommonDictionaries;
using Factories.Abstractions;
using Game.Bullets;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Factories
{
    public class BulletFactory : BaseFactory<GameBullet>, IStartable
    {
        private readonly CommonDictionaries _commonDictionaries;
        private GameBullet _bulletTemplate;

        public BulletFactory(IObjectResolver objectResolver, CommonDictionaries commonDictionaries) : base(objectResolver)
        {
            _commonDictionaries = commonDictionaries;
        }

        public GameBullet Create(string bulletId, Vector3 position, Vector2 direction, string owner)
        {
            var bulletModel = _commonDictionaries.Bullets[bulletId];

            var rotation = Quaternion.Euler(direction);
            var bullet = Object.Instantiate(_bulletTemplate, position, rotation);
            bullet.Init(bulletModel, direction, owner);
            return bullet;
        }

        public void Start()
        {
            _bulletTemplate = Resources.Load<GameBullet>(Constants.ResourcesPath.BULLET_TEMPLATE_PATH);
        }
    }
}
