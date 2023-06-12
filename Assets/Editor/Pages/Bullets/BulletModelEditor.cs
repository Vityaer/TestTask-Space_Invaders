using Editor.Common;
using Models;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Editor.Pages.Bullets
{

    [HideReferenceObjectPicker]
    public class BulletModelEditor : BaseModelEditor<BulletModel>
    {
        private Object _viewResourcePath;

        public BulletModelEditor(BulletModel model)
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
        [LabelText("Speed")]
        [LabelWidth(150)]
        public float Speed
        {
            get => _model.Speed;
            set => _model.Speed = value;
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
