
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
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
    
    private int _winCount;
    private int _loseCount;
    
    public event UnityAction OnRestartClicked;
    
    public void Initialize()
    {
        _loseRestartButton.onClick.AddListener(Restart);
        _winRestartButton.onClick.AddListener(Restart);
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
        _timeWinText.text = ($"Time: {(10f - time).ToString()}");
    
    }

    private void Restart()
    {
        OnRestartClicked?.Invoke();
        gameObject.SetActive(false);
    }
}
