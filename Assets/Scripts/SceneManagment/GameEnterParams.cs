namespace SceneManagment
{
    public class GameEnterParams : SceneEnterParams
    {
        public int Location { get;}
        public int Level { get;}
        public GameEnterParams(int location, int level) : base(Scenes.LevelScene)
        {
            Location = location;
            Level = level;
        }
    }
}