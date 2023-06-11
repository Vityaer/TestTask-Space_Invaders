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
        private Object _resourcePath;

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
        [LabelText("Damage")]
        [LabelWidth(150)]
        public int DamageAmount
        {
            get => _model.DamageAmount;
            set => _model.DamageAmount = value;
        }

        [ShowInInspector]
        [LabelText("Cooldown")]
        [LabelWidth(150)]
        public float Cooldown
        {
            get => _model.Cooldown;
            set => _model.Cooldown = value;
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
