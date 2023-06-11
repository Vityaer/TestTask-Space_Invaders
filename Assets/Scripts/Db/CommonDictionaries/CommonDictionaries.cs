using Json;
using Models;
using System.Collections.Generic;
using Utils;

namespace Db.CommonDictionaries
{
    public class CommonDictionaries
    {
        public Dictionary<string, EnemyModel> Enemies = new Dictionary<string, EnemyModel>();
        public Dictionary<string, LevelModel> Levels = new Dictionary<string, LevelModel>();
        public Dictionary<string, WeaponModel> Weapons = new Dictionary<string, WeaponModel>();
        public Dictionary<string, PlayerShipModel> PlayerShips = new Dictionary<string, PlayerShipModel>();

        private readonly IJsonConverter _converter;
        private bool _isInited;

        public bool Inited => _isInited;

        public CommonDictionaries(IJsonConverter converter)
        {
            _converter = converter;
        }

        public void Init()
        {
            LoadFromLocalDirectory();
            _isInited = true;
        }

        private void LoadFromLocalDirectory()
        {
            Enemies = GetModels<EnemyModel>();
            Levels = GetModels<LevelModel>();
            Weapons = GetModels<WeaponModel>();
            PlayerShips = GetModels<PlayerShipModel>();
        }

        private Dictionary<string, T> GetModels<T>() where T : BaseModel
        {
            var jsonData = TextUtils.GetTextFromLocalStorage<T>();
            return TextUtils.FillDictionary<T>(jsonData, _converter);
        }
    }
}