using UnityEditor;
using UnityEditor.SceneManagement;

namespace Editor {
    public static class PlayFromLoader
    {
        private const string loaderScenePath = "Assets/Scenes/Loader.unity"; // путь к сцене Loader

        [MenuItem("Custom/Play from Loader %#p")] // Ctrl/Cmd + Shift + P — горячая клавиша
        private static void Play()
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
                return;
            }

            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(loaderScenePath);
                EditorApplication.isPlaying = true;
            }
        }
    }
}