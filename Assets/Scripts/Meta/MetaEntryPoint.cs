using Global.Audio;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using Meta.Locations;
using RewardedAd;
using SceneManagment;
using Shop;
using Skill;
using Translator;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Meta
{
    public class MetaEntryPoint : EntryPoint
    {
        [SerializeField] private LocationManager _locationManager;
        [SerializeField] private ShopWindow _shopWindow;
        [SerializeField] private SkillsConfig _skillsConfig;
        [SerializeField] private Button _rewardedButton;
        [SerializeField] private RewardedAdManager _rewardedAdManager;
        private SaveSystem _saveSystem;
        private AudioManager _audioManager;
        private SceneLoader _sceneLoader;
        private TranslatorManager _translatorManager;
        
        [SerializeField]private LocationAndShop _locationAndShop;
        private const string SCENE_LOADER_TAG = "commonObject";
        private Wallet _wallet;
        [SerializeField] private VisibleCash _visibleCash;

        public void ShowRewardedButton(UnityAction callback)
        {
            _rewardedButton.onClick.RemoveAllListeners();
            _rewardedButton.onClick.AddListener(()=>callback?.Invoke());
        }

        public void HideRewardedButton()
        {
            _rewardedButton.onClick.RemoveAllListeners();
        }
        public override void Run(SceneEnterParams enterParams)
        {
            var commonObject = GameObject.FindWithTag(SCENE_LOADER_TAG).GetComponent<commonObject>();
            _translatorManager = commonObject.TranslatorManager;
            _saveSystem = commonObject.SaveSystem;
            _audioManager = commonObject.AudioManager;
            _sceneLoader = commonObject.SceneLoader;
            _wallet = (Wallet)_saveSystem.GetData(SavableObjectType.Wallet);
            _wallet.OnCoinChanged += _visibleCash.ChangeCointText;
            _visibleCash.ChangeCointText(_wallet.Coins);
            var progress = (Progress)_saveSystem.GetData(SavableObjectType.Progress);
            
            _locationManager.Initialize(progress, StartLevel);
            _shopWindow.Initialize(_saveSystem, _skillsConfig, _translatorManager);
            _locationAndShop.Initialize();
            _rewardedAdManager.Initialize(_saveSystem, 
                (callback)=>ShowRewardedButton(callback),
                ()=>HideRewardedButton());
            _audioManager.PlayClip(AudioNames.BackgroundMetaMusic);
            
        }

        private void StartLevel(int location, int level)
        {
            
            _sceneLoader.LoadGameplayScene(new GameEnterParams(location, level));
        }
    }
}