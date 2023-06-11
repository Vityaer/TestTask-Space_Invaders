using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class LevelModel : BaseModel
    {
        public List<EnemyChainModel> Waves = new List<EnemyChainModel>(); 
    }
}
