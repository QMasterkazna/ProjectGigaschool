using Global.Audio;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using Meta.Locations;
using SceneManagment;
using UnityEngine;

namespace Meta
{
    public class MetaEntryPoint : EntryPoint
    {
        [SerializeField] private LocationManager _locationManager;
        private SaveSystem _saveSystem;
        private AudioManager _audioManager;
        private SceneLoader _sceneLoader;
        private const string SCENE_LOADER_TAG = "commonObject";

        public override void Run(SceneEnterParams enterParams)
        {
            var commonObject = GameObject.FindWithTag(SCENE_LOADER_TAG).GetComponent<commonObject>();
            _saveSystem = commonObject.SaveSystem;
            _audioManager = commonObject.AudioManager;
            _sceneLoader = commonObject.SceneLoader;
            var progress = (Progress)_saveSystem.GetData(SavableObjectType.Progress);
            
            _locationManager.Initialize(progress, StartLevel);
            _audioManager.PlayClip(AudioNames.BackgroundMetaMusic);
        }

        private void StartLevel(int location, int level)
        {
            
            var sceneLoader = GameObject.FindWithTag(SCENE_LOADER_TAG).GetComponent<SceneLoader>();
            sceneLoader.LoadGameplayScene(new GameEnterParams(location, level));
        }
    }
}