using Meta.Locations;
using SceneManagment;
using UnityEngine;

namespace Meta
{
    public class MetaEntryPoint : EntryPoint
    {
        [SerializeField] private LocationManager _locationManager;
        private const string SCENE_LOADER_TAG = "SceneLoader";

        public override void Run(SceneEnterParams enterParams)
        {
            _locationManager.Initialize(1, StartLevel);
        }

        private void StartLevel(Vector2Int locationLevel)
        {
            var sceneLoader = GameObject.FindWithTag(SCENE_LOADER_TAG).GetComponent<SceneLoader>();
            sceneLoader.LoadGameplayScene();
        }
    }
}