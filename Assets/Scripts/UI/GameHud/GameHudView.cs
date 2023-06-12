using TMPro;
using UI.Common;
using UnityEngine;

namespace UI.GameHud
{
    public class GameHudView : UiView
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _lifeCountText;

        public TMP_Text ScoreText => _scoreText;
        public TMP_Text LifeCountText => _lifeCountText;
    }
}
