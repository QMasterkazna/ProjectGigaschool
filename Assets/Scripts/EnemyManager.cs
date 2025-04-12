
using InternalAssets.Config.EnemyConfigs;
using InternalAssets.Config.LevelConfigs;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private EnemiesConfig _enemiesConfig;
    [SerializeField] private TextMeshProUGUI _enemyTypeText;
    private Enemy _currentEnemy;
    private HealthBar _healthBar;
    private Timer _timer;
    private LevelData _levelData;
    private int _currentEnemyIndex;
    private EnemyType _currentEnemyDamageType;
    
    public event UnityAction<bool> OnLevelPassed; 
    
    public void Initialize(HealthBar healthBar, Timer timer)
    {
        _healthBar = healthBar;
        _timer = timer;
    }

    public void StartLevel(LevelData levelData)
    {
        _levelData = levelData;
        _currentEnemyIndex = -1;
        if (_currentEnemy == null)
        {
            _currentEnemy = Instantiate(_enemiesConfig.EnemyPrefab, _enemyContainer);
            _currentEnemy.OnDead += SpawnEnemy;
            _currentEnemy.OnDamaged += _healthBar.DecreaseValue;
        }
        
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        _currentEnemyIndex++;
        _timer.Stop();
        if (_currentEnemyIndex >= _levelData.Enemies.Count)
        {
            OnLevelPassed?.Invoke(true);
            _timer.Stop();
            return;
        }
        var currentEnemy = _levelData.Enemies[_currentEnemyIndex];
        _currentEnemyDamageType = currentEnemy.EnemyType;

        _timer.SetActive(currentEnemy.IsBoss);
        if (currentEnemy.IsBoss)
        {
            _timer.SetValue(currentEnemy.BossTime);
            _timer.OnTimerEnd += () => OnLevelPassed?.Invoke(false);
        }
        

        var _currentEnemyData = _enemiesConfig.GetEnemy(currentEnemy.Id);
        _currentEnemyData = _enemiesConfig.GetEnemy(currentEnemy.Id);
        
        InitHPbar(currentEnemy.Hp);
        _enemyTypeText.text = currentEnemy.EnemyType.ToString();
        
        _currentEnemy.Initialize(_currentEnemyData.Sprite, currentEnemy.Hp);
        
        
    }

    public void InitHPbar(float health)
    {
        _healthBar.Show();
        _healthBar.SetMaxValue(health);

    }

    public void DamageCurrentEnemy(float damage)
    {
        _currentEnemy.DoDamage(damage, .8f);
        // Debug.Log($"Damaged!- {damage}");
    }

    public EnemyType GetCurrentDamageEnemyType()
    {
        return _currentEnemyDamageType;
    }


}
