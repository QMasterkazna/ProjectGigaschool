namespace SceneManagment
{
    public abstract class SceneEnterParams
    {
        public string SceneName { get; }

        public SceneEnterParams(string sceneName)
        {
            SceneName = sceneName;
        }
    }
}