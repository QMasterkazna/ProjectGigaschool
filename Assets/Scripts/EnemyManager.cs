
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private EnemiesConfig _enemiesConfig;
    private EnemiesData _currentEnemyData;
    private Enemy _currentEnemy;
    private HealthBar _healthBar;

    public event UnityAction OnLevelPassed; 
    
    public void Initialize(HealthBar healthBar)
    {
        _healthBar = healthBar;
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        
        _currentEnemyData = _enemiesConfig.Enemies[0];
        InitHPbar();
        if (_currentEnemy == null)
        {
            _currentEnemy = Instantiate(_enemiesConfig.EnemyPrefab, _enemyContainer);
            _currentEnemy.OnDead += () => { OnLevelPassed?.Invoke(); };
            _currentEnemy.OnDamaged += _healthBar.DecreaseValue;
            _currentEnemy.OnDead += _healthBar.Hide;
        }

        _currentEnemy.Initialize(_currentEnemyData);
        
        
    }

    public void InitHPbar()
    {
        _healthBar.Show();
        _healthBar.SetMaxValue(_currentEnemyData.health);

    }

    public void DamageCurrentEnemy(float damage)
    {
        _currentEnemy.DoDamage(damage, .8f);
        // Debug.Log($"Damaged!- {damage}");
    }
    

}
