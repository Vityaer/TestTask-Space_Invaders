using Editor.Common;
using Models;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Editor.Pages.Weapons
{
    [HideReferenceObjectPicker]
    public class WeaponModelEditor : BaseModelEditor<WeaponModel>
    {
        private Object _viewResourcePath;
        private Object _prefabResourcePath;

        public WeaponModelEditor(WeaponModel model)
        {
            _model = model;
        }

        [ShowInInspector]
        [LabelText("Id")]
        [LabelWidth(150)]
        public string Id
        {
            get => _model.Id;
            set => _model.Id = value;
        }

        [ShowInInspector]
        [LabelText("BulletId")]
        [LabelWidth(150)]
        public string BulletId
        {
            get => _model.BulletId;
            set => _model.BulletId = value;
        }

        [ShowInInspector]
        [LabelText("Prefab Resource Path")]
        [PreviewField(60)]
        [LabelWidth(150)]
        public Object PrefabResourcePath
        {
            get
            {
                if (_prefabResourcePath == null)
                {
                    _prefabResourcePath = AssetDatabase.LoadAssetAtPath<GameObject>(_model.PrefabPath);
                }

                return _prefabResourcePath;
            }
            set
            {
                _prefabResourcePath = value;
                var path = AssetDatabase.GetAssetPath(_prefabResourcePath);
                _model.PrefabPath = path;
            }
        }

        [ShowInInspector]
        [LabelText("View Resource Path")]
        [PreviewField(60)]
        [LabelWidth(150)]
        public Object ViewResourcePath
        {
            get
            {
                if (_viewResourcePath == null)
                {
                    _viewResourcePath = AssetDatabase.LoadAssetAtPath<Sprite>(_model.ViewResourcePath);
                }

                return _viewResourcePath;
            }
            set
            {
                _viewResourcePath = value;
                var path = AssetDatabase.GetAssetPath(_viewResourcePath);
                _model.ViewResourcePath = path;
            }
        }
    }
}
