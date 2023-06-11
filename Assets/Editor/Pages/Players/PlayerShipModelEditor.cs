using Editor.Common;
using Models;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Editor.Pages.Players
{
    [HideReferenceObjectPicker]
    public class PlayerShipModelEditor : BaseModelEditor<PlayerShipModel>
    {
        private Object _resourcePath;

        public PlayerShipModelEditor(PlayerShipModel model)
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
        [LabelText("Health")]
        [LabelWidth(150)]
        public int Health
        {
            get => _model.Health;
            set => _model.Health = value;
        }

        [ShowInInspector]
        [LabelText("Speed")]
        [LabelWidth(150)]
        public float Speed
        {
            get => _model.Speed;
            set => _model.Speed = value;
        }

        [ShowInInspector]
        [LabelText("Resource Path")]
        [PreviewField(60)]
        [LabelWidth(150)]
        public Object ResourcePath
        {
            get
            {
                if (_resourcePath == null)
                {
                    _resourcePath = AssetDatabase.LoadAssetAtPath<GameObject>(_model.ResourcePath);
                }

                return _resourcePath;
            }
            set
            {
                _resourcePath = value;
                var path = AssetDatabase.GetAssetPath(_resourcePath);
                _model.ResourcePath = path;
            }
        }
    }
}
