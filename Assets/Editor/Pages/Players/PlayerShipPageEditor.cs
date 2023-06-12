using Db.CommonDictionaries;
using Editor.Common;
using Models;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using Utils;

namespace Editor.Pages.Players
{
    public class PlayerShipPageEditor : BasePageEditor
    {
        private CommonDictionaries _dictionaries;
        private List<PlayerShipModel> _playerShips => _dictionaries.PlayerShips.Select(l => l.Value).ToList();

        public PlayerShipPageEditor(CommonDictionaries commonDictionaries)
        {
            _dictionaries = commonDictionaries;
            Init();
        }

        public override void Init()
        {
            base.Init();
            PlayerShips = _playerShips.Select(f => new PlayerShipModelEditor(f)).ToList();
            DataExist = true;
        }

        public override void Save()
        {
            var playerShips = PlayerShips.Select(shipEditor => new PlayerShipModel
            {
                Id = shipEditor.Id,
                Speed = shipEditor.Speed,
                DefaultWeaponId = shipEditor.DefaultBulletId,
                ViewResourcePath = AssetDatabase.GetAssetPath(shipEditor.ViewResourcePath)
            }).ToList();

            EditorUtils.Save(playerShips);
            base.Save();
        }

        protected override void AddElement()
        {
            base.AddElement();
            var id = UnityEngine.Random.Range(0, 99999).ToString();
            _dictionaries.PlayerShips.Add(id, new PlayerShipModel() { Id = id });
            PlayerShips.Add(new PlayerShipModelEditor(_dictionaries.PlayerShips[id]));
        }

        private void RemoveElements(PlayerShipModelEditor light, object b, List<PlayerShipModelEditor> lights)
        {
            var targetElement = PlayerShips.First(e => e == light);
            var id = targetElement.Id;
            _dictionaries.Levels.Remove(id);
            PlayerShips.Remove(targetElement);
        }

        [ShowInInspector]
        [ListDrawerSettings(HideRemoveButton = false, DraggableItems = false, Expanded = true,
            NumberOfItemsPerPage = 20,
            CustomRemoveElementFunction = nameof(RemoveElements), CustomAddFunction = nameof(AddElement))]
        [ShowIf(nameof(DataExist))]
        [HorizontalGroup("3")]
        [LabelText("PlayerShips")]
        [PropertyOrder(2)]
        [Searchable(FilterOptions = SearchFilterOptions.ValueToString)]
        public List<PlayerShipModelEditor> PlayerShips = new List<PlayerShipModelEditor>();
    }
}
