using UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class MainMenuView : UiView
    {
        [SerializeField] private Button _playButton;

        public Button PlayButton => _playButton;
    }
}
