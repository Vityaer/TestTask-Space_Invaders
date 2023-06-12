using Newtonsoft.Json;
using UnityEngine;

public class Constants
{
    public class Common
    {
        public static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented
        };

        public static string DictionariesPath => "Assets/API/Dictionaries";
        public static string MainMenuSceneName => "MainMenu";
        public static string GameplaySceneName => "Gameplay";
    }

    public class ResourcesPath
    {
        public const string ENEMY_TEMPLATE_PATH = "Enemies/EnemyTemplate";
        public const string PLAYER_TEMPLATE_PATH = "Players/PlayerTemplate";
        public const string BULLET_TEMPLATE_PATH = "Bullets/BulletTemplate";
        public const string BONUS_TEMPLATE_PATH = "Bonuses/BonusTemplate";
    }
}