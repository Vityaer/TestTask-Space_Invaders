using Db.CommonDictionaries;
using Editor.Common;
using Editor.Pages.Bullets;
using Models;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using Utils;

namespace Pages.Bullets
{
    public class BulletPageEditor : BasePageEditor
    {
        private CommonDictionaries _dictionaries;
        private List<BulletModel> _bullets => _dictionaries.Bullets.Select(l => l.Value).ToList();

        public BulletPageEditor(CommonDictionaries commonDictionaries)
        {
            _dictionaries = commonDictionaries;
            Init();
        }

        public override void Init()
        {
            base.Init();
            Bullets = _bullets.Select(f => new BulletModelEditor(f)).ToList();
            DataExist = true;
        }

        public override void Save()
        {
            var bullets = Bullets.Select(bulletEditor => new BulletModel
            {
                Id = bulletEditor.Id,
                Speed = bulletEditor.Speed,
                ViewResourcePath = AssetDatabase.GetAssetPath(bulletEditor.ViewResourcePath)
            }).ToList();

            EditorUtils.Save(bullets);
            base.Save();
        }

        protected override void AddElement()
        {
            base.AddElement();
            var id = UnityEngine.Random.Range(0, 99999).ToString();
            _dictionaries.Bullets.Add(id, new BulletModel() { Id = id });
            Bullets.Add(new BulletModelEditor(_dictionaries.Bullets[id]));
        }

        private void RemoveElements(BulletModelEditor light, object b, List<BulletModelEditor> lights)
        {
            var targetElement = Bullets.First(e => e == light);
            var id = targetElement.Id;
            _dictionaries.Levels.Remove(id);
            Bullets.Remove(targetElement);
        }

        [ShowInInspector]
        [ListDrawerSettings(HideRemoveButton = false, DraggableItems = false, Expanded = true,
            NumberOfItemsPerPage = 20,
            CustomRemoveElementFunction = nameof(RemoveElements), CustomAddFunction = nameof(AddElement))]
        [ShowIf(nameof(DataExist))]
        [HorizontalGroup("3")]
        [LabelText("Bullets")]
        [PropertyOrder(2)]
        [Searchable(FilterOptions = SearchFilterOptions.ValueToString)]
        public List<BulletModelEditor> Bullets = new List<BulletModelEditor>();
    }
}
