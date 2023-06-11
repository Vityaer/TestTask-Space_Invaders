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

        public const string SPAWN_UNIT_DELAY = nameof(SPAWN_UNIT_DELAY);
        public const string UI_HEALTH_HEIGHT_SCALE = nameof(UI_HEALTH_HEIGHT_SCALE);
        public const string UI_HEALTH_WIDTH_SCALE = nameof(UI_HEALTH_WIDTH_SCALE);
        public const string TIER_INT_FORMAT = "{0}_{1}_tierInt";
        public const string NICK_NAME = "NickName";
        public const int WAIT_TIME_FOR_PLAY_WITH_BOT = 10;
        public const int DROP_CARD_COST = 30;

        public static string DictionariesPath
        {
            get
            {
#if UNITY_EDITOR
                return "Assets/API/Dictionaries";
#elif UNITY_STANDALONE || UNITY_SERVER
                return Application.streamingAssetsPath;
#elif UNITY_ANDROID
                return Application.persistentDataPath;
#endif
            }
        }

        #region "UI Scroll"
        public const float SENSITIVITY_SWAP_WIDTH = 0.1f;
        public const float TANGENT_HORIZON_DRAG = 0.5f;
        public const float DECK_CARD_CORRECTION_DELTA_Y = -0.2f;
        public const int MAIN_MENU_PAGES_COUNT = 5;
        #endregion

        public const string GAME_FIELD_ROOT_NAME = "GameFieldGridRoot";
        public const string GRID_LAYER = "Grid";
        public const int CELL_SIZE = 1;

        public static readonly Vector3[] GameplayCameraPositions = {
            new Vector3(3, 13, -1.5f),
            new Vector3(3, 13, 12.5f)
        };

        public static readonly Vector3[] GameplayCameraRotations = {
            new Vector3(65, 0, 0),
            new Vector3(65, 180, 0)
        };

        public static string GAME_SERVER_LOCALHOST => "localhost";
        public static string GAME_SERVER_ADDRESS => "37.140.198.191";
        public static string GAME_DATA_SERVER_ADDRESS => $"http://{GAME_SERVER_ADDRESS}/Data/"; //"http://192.168.150.97:8887/UserData/gamedata/"

        public static float MIN_LIMIT_COOLDOWN = 0.01f;
        public static int OVER_TIME_SECONDS = 10;

        public static int DECK_COMMON_CARDS_COUNT = 8;
    }

    public class ResourcesPath
    {
        //TODO: не забыть поменять при запаливании ресурсной системы
        public const string WALL_PATCH = "Prefabs/Wall";
        public const string CAMERA_PREFAB = "Prefabs/GameplayCamera";
        public const string TOWER_OUTLINE_PREFAB = "Outline/TowerOutline";
        public const string SPELL_OUTLINE_PREFAB = "Outline/SpellOutline";
        public const string SFX_PREFAB = "Prefabs/SFXPlayer";
        public const string CASTLE_TEMPLATE_PATH = "Castles/CastleTemplate";
        public const string UNIT_TEMPLATE_PATH = "Units/UnitTemplate";
        public const string TOWER_TEMPLATE_PATH = "Towers/TowerTemplate";
        public const string HERO_TEMPLATE_PATH = "Towers/HeroTemplate";
    }
    //TODO: Добавить палитру для кнопок
    public class ColorCodes
    {
        public static readonly Color BUTTON_SELECTED_COLOR = new Color(70f / 255f, 62f / 255f, 85f / 255f);
        public static readonly Color BUTTON_DESELECTED_COLOR = new Color(113f / 255f, 79f / 255f, 170f / 255f);
    }

    public class MatchResult
    {
        public static string WIN_ICON = "WinIcon";
        public static string LOSE_ICON = "LoseIcon";
        public static string SERIES_CREATED_TOWERS = "CreatedTowers";
        public static string SERIES_CREATED_MERCENARY = "CreatedMercenary";
        public static string SERIES_GET_RESOURCE = "GetResource";
        public static string SERIES_SPEND_RESOURCE_ON_TOWER = "SpendOnTowerResource";
        public static string SERIES_SPEND_RESOURCE_ON_MERCENARY = "SpendOnMercenaryResource";
        public static string SERIES_MADE_DAMAGE = "MadeDamage";
        public static string SERIES_MADE_KILLINGS = "MadeKillings";

    }
    
    public class CardInfoFields
    {
        public static string COST = "Cost";
        public static string DAMAGE = "Damage";
        public static string ATTACK_SPEED = "AttackSpeed";
        public static string HEALTH = "Health";
        public static string MOVE_SPEED = "MoveSpeed";
        public static string BOUNTY = "Bounty";
        public static string CASHBACK = "Cashback";
        public static string COUNT = "Count";
        public static string RADIUS = "Radius";
        public static string SPAWN_SPEED = "SpawnSpeed";
        public static string DURATION = "Duration";
        public static string BULLET_VELOCITY = "BulletVelocity";

    }
}