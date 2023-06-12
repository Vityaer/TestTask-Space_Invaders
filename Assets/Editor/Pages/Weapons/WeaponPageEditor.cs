using Db.CommonDictionaries;
using Editor.Common;
using Models;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using Utils;

namespace Editor.Pages.Weapons
{
    public class WeaponPageEditor : BasePageEditor
    {
        private CommonDictionaries _dictionaries;
        private List<WeaponModel> _weapons => _dictionaries.Weapons.Select(l => l.Value).ToList();

        public WeaponPageEditor(CommonDictionaries commonDictionaries)
        {
            _dictionaries = commonDictionaries;
            Init();
        }

        public override void Init()
        {
            base.Init();
            Weapons = _weapons.Select(f => new WeaponModelEditor(f)).ToList();
            DataExist = true;
        }

        public override void Save()
        {
            var weapons = Weapons.Select(weapon => new WeaponModel
            {
                Id = weapon.Id,
                BulletId = weapon.BulletId,
                PrefabPath = AssetDatabase.GetAssetPath(weapon.PrefabResourcePath),
                ViewResourcePath = AssetDatabase.GetAssetPath(weapon.ViewResourcePath)
            }).ToList();

            EditorUtils.Save(weapons);
            base.Save();
        }

        protected override void AddElement()
        {
            base.AddElement();
            var id = UnityEngine.Random.Range(0, 99999).ToString();
            _dictionaries.Weapons.Add(id, new WeaponModel() { Id = id });
            Weapons.Add(new WeaponModelEditor(_dictionaries.Weapons[id]));
        }

        private void RemoveElements(WeaponModelEditor light, object b, List<WeaponModelEditor> lights)
        {
            var targetElement = Weapons.First(e => e == light);
            var id = targetElement.Id;
            _dictionaries.Weapons.Remove(id);
            Weapons.Remove(targetElement);
        }

        [ShowInInspector]
        [ListDrawerSettings(HideRemoveButton = false, DraggableItems = false, Expanded = true,
            NumberOfItemsPerPage = 20,
            CustomRemoveElementFunction = nameof(RemoveElements), CustomAddFunction = nameof(AddElement))]
        [ShowIf(nameof(DataExist))]
        [HorizontalGroup("3")]
        [LabelText("Weapons")]
        [PropertyOrder(2)]
        [Searchable(FilterOptions = SearchFilterOptions.ValueToString)]
        public List<WeaponModelEditor> Weapons = new List<WeaponModelEditor>();
    }
}
