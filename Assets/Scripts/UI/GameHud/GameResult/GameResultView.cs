using UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameHud.GameResult
{
    public class GameResultView : UiView
    {
        [SerializeField] private Button _mainMenuButton;

        public Button MainMenuButton => _mainMenuButton;
    }
}
