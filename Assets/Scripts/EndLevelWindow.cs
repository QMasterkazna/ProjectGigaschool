
using System;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using InternalAssets.Config.EnemyConfigs;
using InternalAssets.Config.LevelConfigs;
using InternalAssets.Config.LevelConfigs;
using SceneManagment;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class EndLevelWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textTimer;
    [SerializeField] private TextMeshProUGUI _timeWinText;
    [SerializeField] private TextMeshProUGUI _scoreKillWinText;
    [SerializeField] private TextMeshProUGUI _scoreDeathLoseText;
    
    [SerializeField] private GameObject _loseLevelWindow;
    [SerializeField] private GameObject _winLevelWindow;

    [SerializeField] private Button _loseRestartButton;
    [SerializeField] private Button _winRestartButton;
    [SerializeField] private Button _mapButton;
    private const string SCENE_LOADER_TAG = "SceneLoader";

    private LevelData _levelData;
    
    private int _winCount;
    private int _loseCount;
    private SaveSystem _saveSystem;

    public event UnityAction OnRestartClicked;
    
    public void Initialize()
    {
        _loseRestartButton.onClick.AddListener(TravelToMap);
        _winRestartButton.onClick.AddListener(TravelLevel);
        _mapButton.onClick.AddListener(TravelToMap);
    }
    
    public void ShowLoseLevelWindow()
    {
        _loseCount++;
        _loseLevelWindow.SetActive(true);
        _winLevelWindow.SetActive(false);
        gameObject.SetActive(true);
        _scoreDeathLoseText.text = ($"Death:{_loseCount.ToString()}");
        
    }
    public void ShowWinLevelWindow()
    {
        _winCount++;
        _loseLevelWindow.SetActive(false);
        _winLevelWindow.SetActive(true);
        gameObject.SetActive(true);
        GetTime();
        _scoreKillWinText.text = ($"Kills:{_winCount.ToString()}");
    }

    private void GetTime()
    {
        float time;
        float.TryParse(_textTimer.text, out time);
        // float timeBoss = _levelData.Enemies[_levelData.Enemies.Count].BossTime;
        // Debug.LogError(timeBoss);
        // _timeWinText.text = ($"Time: {( tim - time).ToString("00:0")}");
    
    }
    
    private void Restart()
    {
        OnRestartClicked?.Invoke();
        gameObject.SetActive(false);
    }


    private void TravelLevel()
    {
        OnRestartClicked?.Invoke();
        var progress = (Progress)_saveSystem.GetData(SavableObjectType.Progress);
        var sceneLoader = GameObject.FindWithTag(SCENE_LOADER_TAG).GetComponent<SceneLoader>();
        Debug.Log(progress.CurrentLocation);
        Debug.Log(progress.CurrentLevel);
        sceneLoader.LoadGameplayScene(new GameEnterParams(progress.CurrentLocation, progress.CurrentLevel));
    }

    private void TravelToMap()
    {
        OnRestartClicked?.Invoke();
        var sceneLoader = GameObject.FindWithTag(SCENE_LOADER_TAG).GetComponent<SceneLoader>();
        sceneLoader.LoadMetaScene();
    }
}
