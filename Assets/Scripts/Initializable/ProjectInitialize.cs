using Db.CommonDictionaries;
using UnityEngine;
using VContainer.Unity;

namespace Initializable
{
    public class ProjectInitialize : IStartable
    {
        private readonly CommonDictionaries _dictionaries;

        public ProjectInitialize(
            CommonDictionaries commonDictionaries
        )
        {
            _dictionaries = commonDictionaries;
        }

        public void Start()
        {
            _dictionaries.Init();
        }
    }
}