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
        private const string SCENE_LOADER_TAG = "SceneLoader";

        public override void Run(SceneEnterParams enterParams)
        {
            _saveSystem = FindFirstObjectByType<SaveSystem>();
            _audioManager = FindFirstObjectByType<AudioManager>();
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