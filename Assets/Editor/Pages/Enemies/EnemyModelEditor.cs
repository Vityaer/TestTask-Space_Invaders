using Editor.Common;
using Models;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Editor.Pages.Enemies
{
    [HideReferenceObjectPicker]
    public class EnemyModelEditor : BaseModelEditor<EnemyModel>
    {
        private Object _viewResourcePath;
        private Object _bulletResourcePath;

        public EnemyModelEditor(EnemyModel model)
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
        [LabelText("Bounty")]
        [LabelWidth(150)]
        public int Bounty
        {
            get => _model.Bounty;
            set => _model.Bounty = value;
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

        [ShowInInspector]
        [LabelText("Bullet Resource Path")]
        [PreviewField(60)]
        [LabelWidth(150)]
        public Object BulletResourcePath
        {
            get
            {
                if (_bulletResourcePath == null)
                {
                    _bulletResourcePath = AssetDatabase.LoadAssetAtPath<Sprite>(_model.BulletResourcePath);
                }

                return _bulletResourcePath;
            }
            set
            {
                _bulletResourcePath = value;
                var path = AssetDatabase.GetAssetPath(_bulletResourcePath);
                _model.BulletResourcePath = path;
            }
        }
    }
}
