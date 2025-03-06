
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EndLevelWindow : MonoBehaviour
{
    [SerializeField] private GameObject _loseLevelWindow;
    [SerializeField] private GameObject _winLevelWindow;

    [SerializeField] private Button _loseRestartButton;
    [SerializeField] private Button _winRestartButton;
    
    public event UnityAction OnRestartClicked;
    
    public void Initialize()
    {
        _loseRestartButton.onClick.AddListener(Restart);
        _winRestartButton.onClick.AddListener(Restart);
    }
    
    public void ShowLoseLevelWindow()
    {
        _loseLevelWindow.SetActive(true);
        _winLevelWindow.SetActive(false);
        gameObject.SetActive(true);
    }
    public void ShowWinLevelWindow()
    {
        _loseLevelWindow.SetActive(false);
        _winLevelWindow.SetActive(true);
        gameObject.SetActive(true);
    }

    private void Restart()
    {
        OnRestartClicked?.Invoke();
        gameObject.SetActive(false);
    }
}
