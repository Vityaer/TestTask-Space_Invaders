namespace Extensions
{
    public static class StringExtensions
    {
        public static string ImageReplaceForResources(this string path)
        {
            path = path.Replace(".prefab", "");
            path = path.Replace("Assets/Resources/", "");
            path = path.Replace(".png", "");
            path = path.Replace(".jpg", "");
            return path;
        }

        public static string PrefabReplaceForResources(this string path)
        {
            path = path.Replace(".prefab", "");
            path = path.Replace("Assets/Resources/", "");
            return path;
        }
    }
}
