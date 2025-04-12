using Global.Audio;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using Meta.Locations;
using SceneManagment;
using Shop;
using Skill;
using UnityEngine;
using UnityEngine.UI;

namespace Meta
{
    public class MetaEntryPoint : EntryPoint
    {
        [SerializeField] private LocationManager _locationManager;
        [SerializeField] private ShopWindow _shopWindow;
        [SerializeField] private SkillsConfig _skillsConfig;
        private SaveSystem _saveSystem;
        private AudioManager _audioManager;
        private SceneLoader _sceneLoader;
        [SerializeField]private LocationAndShop _locationAndShop;
        private const string SCENE_LOADER_TAG = "commonObject";
        private Wallet _wallet;
        [SerializeField] private VisibleCash _visibleCash;
        public override void Run(SceneEnterParams enterParams)
        {
            var commonObject = GameObject.FindWithTag(SCENE_LOADER_TAG).GetComponent<commonObject>();
            _saveSystem = commonObject.SaveSystem;
            _audioManager = commonObject.AudioManager;
            _sceneLoader = commonObject.SceneLoader;
            _wallet = (Wallet)_saveSystem.GetData(SavableObjectType.Wallet);
            _wallet.OnCoinChanged += _visibleCash.ChangeCointText;
            _visibleCash.ChangeCointText(_wallet.Coins);
            var progress = (Progress)_saveSystem.GetData(SavableObjectType.Progress);
            
            _locationManager.Initialize(progress, StartLevel);
            _shopWindow.Initialize(_saveSystem, _skillsConfig);
            _locationAndShop.Initialize();
            _audioManager.PlayClip(AudioNames.BackgroundMetaMusic);
        }

        private void StartLevel(int location, int level)
        {
            
            _sceneLoader.LoadGameplayScene(new GameEnterParams(location, level));
        }
    }
}