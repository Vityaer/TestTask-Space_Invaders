using Db.CommonDictionaries;
using Editor.Common;
using Models;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;

namespace Editor.Pages.Levels
{
    [HideReferenceObjectPicker]
    public class LevelModelEditor : BaseModelEditor<LevelModel>
    {
        private CommonDictionaries _dictionaries;

        private List<string> _enemyModels => _dictionaries.Enemies
                .Select(l => l.Value).ToList()
                .Select(r => r.Id).ToList();

        public LevelModelEditor(LevelModel model, CommonDictionaries commonDictionaries)
        {
            _dictionaries = commonDictionaries;
            _model = model;

            if (model.Waves == null || model.Waves.Count == 0)
            {
                model.Waves = new List<EnemyChainModel>() { new EnemyChainModel(_enemyModels.FirstOrDefault(), 0) };
            }

            _model = model;
            Waves = _model.Waves.Select(f => new EnemyChainEditor(f, _dictionaries)).ToList();
        }

        [ShowInInspector]
        [HorizontalGroup("1")]
        [LabelText("Id")]
        [LabelWidth(150)]
        public string Id
        {
            get => _model.Id;
            set => _model.Id = value;
        }

        [ShowInInspector]
        [ListDrawerSettings(HideRemoveButton = false, DraggableItems = true, AddCopiesLastElement = true)]
        [HorizontalGroup("2")]
        [PropertyOrder(2)]
        [LabelText("Waves", true)]
        public List<EnemyChainEditor> Waves;
    }
}
