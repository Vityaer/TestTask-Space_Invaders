using Db.CommonDictionaries;
using Editor.Common;
using Models;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;

namespace Editor.Pages.Levels
{
    [HideReferenceObjectPicker]
    public class EnemyChainEditor : BaseModelEditor<EnemyChainModel>
    {
        private readonly CommonDictionaries _dictionaries;

        private List<string> _enemies => _dictionaries.Enemies
            .Select(l => l.Value).ToList()
            .Select(r => r.Id).ToList();

        public EnemyChainEditor(EnemyChainModel model, CommonDictionaries dictionaries)
        {
            _dictionaries = dictionaries;
            _model = model;
        }

        [ShowInInspector]
        [HorizontalGroup("1")]
        [ListDrawerSettings(ShowItemCount = true, ShowIndexLabels = true, Expanded = true, DraggableItems = false)]
        [LabelText("Enemies")]
        [LabelWidth(60)]
        [ValueDropdown(nameof(_enemies), IsUniqueList = true, DropdownWidth = 250, SortDropdownItems = true)]
        public string Id
        {
            get
            {
                var result = _model.Id;
                if (string.IsNullOrEmpty(result))
                {
                    _model = new EnemyChainModel(_enemies.FirstOrDefault(), 0);
                }

                return result;
            }
            set => _model = new EnemyChainModel(value, _model.Count);
        }

        [ShowInInspector]
        [HorizontalGroup("1")]
        [LabelWidth(100)]
        [LabelText("Count in chain")]
        public int Count
        {
            get => _model.Count;
            set => _model = new EnemyChainModel(_model.Id, value);
        }
    }

}
