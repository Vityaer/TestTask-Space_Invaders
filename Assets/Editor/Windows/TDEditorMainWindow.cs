using Common;
using Db.CommonDictionaries;
using Editor.Common;
using Editor.Pages.Enemies;
using Editor.Pages.Levels;
using Editor.Pages.Players;
using Editor.Pages.Weapons;
using Json.Impl;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Editor.Windows
{
    public class TDEditorMainWindow : OdinMenuEditorWindow
    {
        private CommonDictionaries _dictionaries;
        private ConfigVersion ConfigVersion;

        private EnemyPageEditor _enemyPageEditor;
        private WeaponPageEditor _weaponPageEditor;
        private LevelPageEditor _levelPageEditor;
        private PlayerShipPageEditor _playerShipPageEditor;
        private List<BasePageEditor> _allPages;
        
        private OdinMenuTree _tree;

        [MenuItem("TD_Editor/Main _%#T")]
        public static void OpenWindow()
        {
            GetWindow<TDEditorMainWindow>().Show();
        }

        protected override void DrawMenu()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Save All",
                    new GUILayoutOption[] { GUILayout.Width(100), GUILayout.Height(40) }))
            {
                Saving();
            }

            if (GUILayout.Button("Load All",
                    new GUILayoutOption[] { GUILayout.Width(100), GUILayout.Height(40) }))
            {
                ForceMenuTreeRebuild();
            }

            GUILayout.EndHorizontal();
            base.DrawMenu();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            InitPages();
            _tree = new OdinMenuTree();
            FillTree();
            return _tree;
        }

        private void FillTree()
        {
            _tree.Selection.SupportsMultiSelect = false;
            _tree.Add("Enemies/Enemies editor", _enemyPageEditor);
            _tree.Add("Levels/Levels editor", _levelPageEditor);
            _tree.Add("Player/Weapons editor", _weaponPageEditor);
            _tree.Add("Player/Ships editor", _playerShipPageEditor);
        }

        private void InitPages()
        {
            ConfigVersion = EditorUtils.Load<ConfigVersion>();
            if (ConfigVersion == null)
            {
                ConfigVersion = new ConfigVersion();
            }

            Debug.Log($"Config loaded. Current version: {ConfigVersion.Version}");

            var converter = new JsonConverter();
            _dictionaries = new CommonDictionaries(converter);
            _dictionaries.Init();

            _allPages = new List<BasePageEditor>();

            _enemyPageEditor = new EnemyPageEditor(_dictionaries);
            _allPages.Add(_enemyPageEditor);

            _levelPageEditor = new LevelPageEditor(_dictionaries);
            _allPages.Add(_levelPageEditor);

            _weaponPageEditor = new WeaponPageEditor(_dictionaries);
            _allPages.Add(_weaponPageEditor);

            _playerShipPageEditor = new PlayerShipPageEditor(_dictionaries);
            _allPages.Add(_playerShipPageEditor);
        }

        private void OnValueSaved()
        {
            EditorUtils.Save(ConfigVersion);
        }

        protected override void OnDestroy()
        {
            _allPages?.Clear();
            _allPages = null;
            base.OnDestroy();
        }

        protected override void OnGUI()
        {
            base.OnGUI();
            var condition = Event.current.type == EventType.KeyUp
                            && Event.current.modifiers == EventModifiers.Control
                            && Event.current.keyCode == KeyCode.S;

            if (condition)
            {
                Saving();
            }
        }

        private void Saving()
        {
            ConfigVersion.Version++;
            ConfigVersion.FilesCount = _allPages.Count;

            Debug.Log("Save configs start");
            foreach (var page in _allPages)
            {
                page.Save();
            }

            OnValueSaved();
            InitPages();

            Debug.Log("Save configs complete");
        }
    }
}