using Common;
using Db.CommonDictionaries;
using Editor.Common;
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
            //_tree.Add("City/Mines", _minePageEditor);

        }

        private async void InitPages()
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

            //_heroPageEditor = new HeroPageEditor(_dictionaries);
            //_allPages.Add(_heroPageEditor);
        }

        private void OnValueSaved()
        {
            EditorUtils.Save(ConfigVersion);
        }

        protected override void OnDestroy()
        {
            _allPages.Clear();
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