using System;
using System.Collections.Generic;
using Global.Audio;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using InternalAssets.Config.LevelConfigs;
using Meta.Locations;
using SceneManagment;
using Skill;
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
    private const string SCENE_LOADER_TAG = "commonObject";
    private SceneLoader _sceneLoader;
    private Progress _progress;
    [SerializeField] SkillsConfig _skillsConfig;
    private SkillSystem _skillSystem;
    private EndLevelSystem _endLevelSystem;
    
    
    
    // ReSharper disable Unity.PerformanceAnalysis
    public override void Run(SceneEnterParams enterParams)
    {
        var commonObject = GameObject.FindWithTag(SCENE_LOADER_TAG).GetComponent<commonObject>();
        _saveSystem = commonObject.SaveSystem;
        _audioManager = commonObject.AudioManager;
        _sceneLoader = commonObject.SceneLoader;
        if (enterParams is not GameEnterParams gameEnterParams)
        {
            Debug.LogError("troubles with enter params into game");
            return;
        }

        _gameEnterParams = gameEnterParams;


        _enemyManager.Initialize(_healthBar, _timer);
        _endLevelWindow.Initialize();

        var openedSkills = (OpenedSkills)_saveSystem.GetData(SavableObjectType.OpenedSkills);
        _skillSystem = new SkillSystem(openedSkills, _skillsConfig, _enemyManager);
        _clickButtonManager.Initialize(_skillSystem);

        ComboSystem GetDamageCombo = new ComboSystem();

        _clickButtonManager.OnClickedLightAttack += () =>
        {
            int damage = GetDamageCombo.CheckCombo(AttackTypes.Light);
            _enemyManager.DamageCurrentEnemy(damage);
            _skillSystem.InvokeTrigger(SkillTrigger.OnDamage);
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
        _endLevelSystem = new(_endLevelWindow, _saveSystem, _gameEnterParams, _levelsConfig, _timer);
        _endLevelWindow.ToMap += TravelToMap;
        _endLevelWindow.ToNextLvl += TravelLevel;
        _enemyManager.OnLevelPassed += _endLevelSystem.LevelPassed ;
        
        _audioManager.PlayClip(AudioNames.BackgroundGameMusic);
        StartLevel();
    }


    private void RestartLevel()
    {
        var sceneLoader = GameObject.FindWithTag(SCENE_LOADER_TAG).GetComponent<SceneLoader>();
        sceneLoader.LoadGameplayScene(_gameEnterParams);
    }




    private void TravelLevel()
    {
        _progress = (Progress)_saveSystem.GetData(SavableObjectType.Progress);
        Debug.Log(_progress.CurrentLocation);
        Debug.Log(_progress.CurrentLevel);
        _sceneLoader.LoadGameplayScene(new GameEnterParams(_progress.CurrentLocation, _progress.CurrentLevel));
    }

    private void TravelToMap()
    {
        _sceneLoader.LoadMetaScene();
    }

    private void StartLevel()
    {
        var maxLocationAndLevel = _levelsConfig.GetMaxLocationAndLevel();
        var location = _gameEnterParams.Location;
        var level = _gameEnterParams.Level;
        if (location> maxLocationAndLevel.x ||
            (location == maxLocationAndLevel.x && level > maxLocationAndLevel.y))
        {
            location = maxLocationAndLevel.x;
            level = maxLocationAndLevel.y;
        }
        
        var levelData = _levelsConfig.GetLevel(location, level);
        
        _enemyManager.StartLevel(levelData);
    }
}