using Global.Audio;
using Global.SaveSystem;
using UnityEngine;

namespace SceneManagment
{
    public class MainEntryPoint : MonoBehaviour
    {
        private const string SCENE_LOADER_TAG = "commonObject";

        public void Awake()
        {
            if (GameObject.FindGameObjectWithTag(SCENE_LOADER_TAG)) return;
            
            var commonObjectPrefab = Resources.Load<commonObject>(SCENE_LOADER_TAG);
            var commonObject = Instantiate(commonObjectPrefab);
            DontDestroyOnLoad(commonObject);
            
            commonObject.AudioManager.LoadOnce();            
                
            commonObject.SceneLoader.Initialize(commonObject.AudioManager);
            
            
            
            commonObject.SaveSystem = new();
            
            commonObject.SceneLoader.LoadMetaScene();
        }
    }
}