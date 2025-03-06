using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ClickButtonManager _clickButtonManager;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Timer _timer;
    [SerializeField] private EndLevelWindow _endLevelWindow;
    private void Awake()
    {
        _clickButtonManager.Initialize();
        _enemyManager.Initialize(_healthBar);
        _endLevelWindow.Initialize();
        
        _clickButtonManager.OnClicked += () => _enemyManager.DamageCurrentEnemy(1f);
        _endLevelWindow.OnRestartClicked += StartLevel;
        _enemyManager.OnLevelPassed += LevelPassed;
        
        StartLevel();
    }

    private void LevelPassed()
    {
        _endLevelWindow.ShowWinLevelWindow();
        _timer.Stop();
    }

    private void StartLevel()
    {
        
        _timer.OnTimerEnd += _endLevelWindow.ShowLoseLevelWindow;
        _timer.Initialize(10f);
        _enemyManager.SpawnEnemy();
    }
}
