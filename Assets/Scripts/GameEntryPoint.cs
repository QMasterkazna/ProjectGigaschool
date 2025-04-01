using System;
using Global.Audio;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using InternalAssets.Config.LevelConfigs;
using Meta.Locations;
using SceneManagment;
using UnityEngine;
using UnityEngine.Events;

public class GameEntryPoint : EntryPoint
{
    [SerializeField] private ClickButtonManager _clickButtonManager;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Timer _timer;
    [SerializeField] private EndLevelWindow _endLevelWindow;
    [SerializeField] private LevelsConfig _levelsConfig;
    private GameEnterParams _gameEnterParams;
    private SaveSystem _saveSystem;
    private AudioManager _audioManager;
    private const string SCENE_LOADER_TAG = "SceneLoader";
    private SceneLoader _sceneLoader;
    private Progress _progress;

    public override void Run(SceneEnterParams enterParams)
    {
        _sceneLoader = GameObject.FindWithTag(SCENE_LOADER_TAG).GetComponent<SceneLoader>();
        _saveSystem = FindFirstObjectByType<SaveSystem>();
        _progress = (Progress)_saveSystem.GetData(SavableObjectType.Progress);
        _audioManager = FindFirstObjectByType<AudioManager>();

        if (enterParams is not GameEnterParams gameEnterParams)
        {
            Debug.LogError("troubles with enter params into game");
            return;
        }

        _gameEnterParams = gameEnterParams;


        _clickButtonManager.Initialize();
        _enemyManager.Initialize(_healthBar, _timer);
        _endLevelWindow.Initialize();
        ComboSystem GetDamageCombo = new ComboSystem();

        _clickButtonManager.OnClickedLightAttack += () =>
        {
            int damage = GetDamageCombo.CheckCombo(AttackTypes.Light);
            _enemyManager.DamageCurrentEnemy(damage);
            // Debug.Log($"COMBO: {damage}");
        };
        _clickButtonManager.OnClickedHeavyAttack += () =>
        {
            int damage = GetDamageCombo.CheckCombo(AttackTypes.Heavy);
            _enemyManager.DamageCurrentEnemy(damage);
            // Debug.Log($"COMBO: {damage}");

            // _enemyManager.DamageCurrentEnemy(5f);
        };
        _clickButtonManager.OnClickedRideAttack += () =>
        {
            int damage = GetDamageCombo.CheckCombo(AttackTypes.Ride);
            _enemyManager.DamageCurrentEnemy(damage);
            // Debug.Log($"COMBO: {damage}");
            // _enemyManager.DamageCurrentEnemy(.5f);
        };
        _clickButtonManager.OnClickedJumpAttack += () =>
        {
            int damage = GetDamageCombo.CheckCombo(AttackTypes.Jump);
            _enemyManager.DamageCurrentEnemy(damage);
            // Debug.Log($"COMBO: {damage}");

            // _enemyManager.DamageCurrentEnemy(.2f);
        };
        // _endLevelWindow.OnRestartClicked += RestartLevel;
        _endLevelWindow.ToMap += TravelToMap;
        // _endLevelWindow.ToNextLvl += TravelLevel(_progress);
        _enemyManager.OnLevelPassed += LevelPassed;
        
        _audioManager.PlayClip(AudioNames.BackgroundGameMusic);
        StartLevel();
    }


    private void RestartLevel()
    {
        var sceneLoader = GameObject.FindWithTag(SCENE_LOADER_TAG).GetComponent<SceneLoader>();
        sceneLoader.LoadGameplayScene(_gameEnterParams);
    }


    private void LevelPassed(bool isPassed)
    {
        if (isPassed)
        {
            TrySaveProgress();
            _endLevelWindow.ShowWinLevelWindow();
        }
        else
        {
            _endLevelWindow.ShowLoseLevelWindow();
        }

        _timer.Stop();
    }

    private void TrySaveProgress()
    {
        var progress = (Progress)_saveSystem.GetData(SavableObjectType.Progress);
        var maxLevel = _levelsConfig.GetMaxLevelOnLocation(progress.CurrentLocation);
        if (_gameEnterParams.Location == progress.CurrentLocation &&
            _gameEnterParams.Level == progress.CurrentLevel)
        {
            if (progress.CurrentLevel + 1 >= maxLevel)
            {
                progress.CurrentLevel = 1;
                progress.CurrentLocation++;
            }
        }
        else
        {
            progress.CurrentLevel++;
        }

        _saveSystem.SaveData(SavableObjectType.Progress);
        Debug.LogError("Прогресс сохранен!");
    }

    private void TravelLevel(Progress progress)
    {

        Debug.Log(progress.CurrentLocation);
        Debug.Log(progress.CurrentLevel);
        _sceneLoader.LoadGameplayScene(new GameEnterParams(progress.CurrentLocation, progress.CurrentLevel));
    }

    private void TravelToMap()
    {
        _sceneLoader.LoadMetaScene();
        Debug.LogError("ПЕРЕХОД НА КАРТУ");
    }

    private void StartLevel()
    {
        Debug.Log($"Location: {_gameEnterParams.Location}\n level: {_gameEnterParams.Level}");
        var levelData = _levelsConfig.GetLevel(_gameEnterParams.Location, _gameEnterParams.Level);

        _enemyManager.StartLevel(levelData);
    }
}