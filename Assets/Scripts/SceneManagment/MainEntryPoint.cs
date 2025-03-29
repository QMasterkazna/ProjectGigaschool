using Global.Audio;
using Global.SaveSystem;
using UnityEngine;

namespace SceneManagment
{
    public class MainEntryPoint : MonoBehaviour
    {
        private const string SCENE_LOADER_TAG = "SceneLoader";

        public void Awake()
        {
            if (GameObject.FindGameObjectWithTag(SCENE_LOADER_TAG)) return;
            
            var sceneLoaderPrefab = Resources.Load<SceneLoader>(SCENE_LOADER_TAG);
            var sceneLoader = Instantiate(sceneLoaderPrefab);
            DontDestroyOnLoad(sceneLoader);

            var audioManagerPrefab = Resources.Load<AudioManager>("AudioManager");
            var audioManager = Instantiate(audioManagerPrefab);
            audioManager.LoadOnce();
            DontDestroyOnLoad(audioManager);
            
            sceneLoader.Initialize(audioManager);
            
            var saveSystem = new GameObject().AddComponent<SaveSystem>();
            saveSystem.Initialize();
            DontDestroyOnLoad(saveSystem);
            
            sceneLoader.LoadMetaScene();
        }
    }
}