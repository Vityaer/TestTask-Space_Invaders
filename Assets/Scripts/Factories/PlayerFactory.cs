using Db.CommonDictionaries;
using Factories.Abstractions;
using Game;
using Models;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Factories
{
    public class PlayerFactory : BaseFactory<GamePlayer>, IStartable
    {
        private readonly CommonDictionaries _commonDictionaries;
        private GamePlayer _playerTemplate;

        public PlayerFactory(IObjectResolver objectResolver, CommonDictionaries commonDictionaries) : base(objectResolver)
        {
            _commonDictionaries = commonDictionaries;
        }

        public void Start()
        {
            _playerTemplate = Resources.Load<GamePlayer>(Constants.ResourcesPath.PLAYER_TEMPLATE_PATH);
        }

        public GamePlayer Create(string playerShipId, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion())
        {
            var playerShipModel = _commonDictionaries.PlayerShips[playerShipId];

            var player = Object.Instantiate(_playerTemplate, position, rotation);
            ObjectResolver.Inject(player);
            player.Init(playerShipModel);

            return player;
        }
    }
}
