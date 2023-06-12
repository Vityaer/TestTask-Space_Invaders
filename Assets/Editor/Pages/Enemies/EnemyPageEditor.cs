using Db.CommonDictionaries;
using Editor.Common;
using Models;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using Utils;

namespace Editor.Pages.Enemies
{
    public class EnemyPageEditor : BasePageEditor
    {
        private CommonDictionaries _dictionaries;
        private List<EnemyModel> _enemies => _dictionaries.Enemies.Select(l => l.Value).ToList();

        public EnemyPageEditor(CommonDictionaries commonDictionaries)
        {
            _dictionaries = commonDictionaries;
            Init();
        }

        public override void Init()
        {
            base.Init();
            Enemies = _enemies.Select(f => new EnemyModelEditor(f)).ToList();
            DataExist = true;
        }

        public override void Save()
        {
            var enemies = Enemies.Select(enemy => new EnemyModel
            {
                Id = enemy.Id,
                Bounty = enemy.Bounty,
                ViewResourcePath = AssetDatabase.GetAssetPath(enemy.ViewResourcePath),
                BulletResourcePath = AssetDatabase.GetAssetPath(enemy.BulletResourcePath)
            }).ToList();

            EditorUtils.Save(enemies);
            base.Save();
        }

        protected override void AddElement()
        {
            base.AddElement();
            var id = UnityEngine.Random.Range(0, 99999).ToString();
            _dictionaries.Enemies.Add(id, new EnemyModel() { Id = id });
            Enemies.Add(new EnemyModelEditor(_dictionaries.Enemies[id]));
        }

        private void RemoveElements(EnemyModelEditor light, object b, List<EnemyModelEditor> lights)
        {
            var targetElement = Enemies.First(e => e == light);
            var id = targetElement.Id;
            _dictionaries.Enemies.Remove(id);
            Enemies.Remove(targetElement);
        }

        [ShowInInspector]
        [ListDrawerSettings(HideRemoveButton = false, DraggableItems = false, Expanded = true,
            NumberOfItemsPerPage = 20,
            CustomRemoveElementFunction = nameof(RemoveElements), CustomAddFunction = nameof(AddElement))]
        [ShowIf(nameof(DataExist))]
        [HorizontalGroup("3")]
        [LabelText("Enemies")]
        [PropertyOrder(2)]
        [Searchable(FilterOptions = SearchFilterOptions.ValueToString)]
        public List<EnemyModelEditor> Enemies = new List<EnemyModelEditor>();
    }
}
