using Db.CommonDictionaries;
using Editor.Common;
using Models;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Editor.Pages.Levels
{
    public class LevelPageEditor : BasePageEditor
    {
        private CommonDictionaries _dictionaries;
        private List<LevelModel> _levels => _dictionaries.Levels.Select(l => l.Value).ToList();

        public LevelPageEditor(CommonDictionaries commonDictionaries)
        {
            _dictionaries = commonDictionaries;
            Init();
        }

        public override void Init()
        {
            base.Init();
            Levels = _levels.Select(f => new LevelModelEditor(f, _dictionaries)).ToList();
            DataExist = true;
        }

        public override void Save()
        {
            var levels = Levels.Select(levelEditor => new LevelModel
            {
                Id = levelEditor.Id,
                Waves = levelEditor.Waves.Select(chain => chain.GetModel()).ToList() 

            }).ToList();

            EditorUtils.Save(levels);
            base.Save();
        }

        protected override void AddElement()
        {
            base.AddElement();
            var id = UnityEngine.Random.Range(0, 99999).ToString();
            _dictionaries.Levels.Add(id, new LevelModel() { Id = id });
            Levels.Add(new LevelModelEditor(_dictionaries.Levels[id], _dictionaries));
        }

        private void RemoveElements(LevelModelEditor light, object b, List<LevelModelEditor> lights)
        {
            var targetElement = Levels.First(e => e == light);
            var id = targetElement.Id;
            _dictionaries.Levels.Remove(id);
            Levels.Remove(targetElement);
        }

        [ShowInInspector]
        [ListDrawerSettings(HideRemoveButton = false, DraggableItems = false, Expanded = true,
            NumberOfItemsPerPage = 20,
            CustomRemoveElementFunction = nameof(RemoveElements), CustomAddFunction = nameof(AddElement))]
        [ShowIf(nameof(DataExist))]
        [HorizontalGroup("3")]
        [LabelText("Levels")]
        [PropertyOrder(2)]
        [Searchable(FilterOptions = SearchFilterOptions.ValueToString)]
        public List<LevelModelEditor> Levels = new List<LevelModelEditor>();
    }
}
