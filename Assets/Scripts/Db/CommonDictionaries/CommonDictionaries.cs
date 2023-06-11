using Json;
using Models;
using System.Collections.Generic;
using Utils;

namespace Db.CommonDictionaries
{
    public class CommonDictionaries
    {
        public Dictionary<string, EnemyModel> Enemies { get; private set; }
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
        }

        private Dictionary<string, T> GetModels<T>() where T : BaseModel
        {
            var jsonData = TextUtils.GetTextFromLocalStorage<T>();
            return TextUtils.FillDictionary<T>(jsonData, _converter);
        }
    }
}