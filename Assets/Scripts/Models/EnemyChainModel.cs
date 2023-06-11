namespace Models
{
    [System.Serializable]
    public class EnemyChainModel
    {
        public string Id;
        public int Count;

        public EnemyChainModel(string id, int count)
        {
            Id = id;
            Count = count;
        }
    }
}
