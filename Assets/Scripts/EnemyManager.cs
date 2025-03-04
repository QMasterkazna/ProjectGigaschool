
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private EnemiesConfig _enemiesConfig;
    private EnemiesData _currentEnemyData;
    private Enemy _currentEnemy;
    private HealthBar _healthBar;

    public void Initialize(HealthBar healthBar)
    {
        _healthBar = healthBar;
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        _currentEnemyData = _enemiesConfig.Enemies[0];
        _currentEnemy = Instantiate(_enemiesConfig.EnemyPrefab, _enemyContainer);
        _currentEnemy.Initialize(_currentEnemyData);
        
        InitHPbar();
    }

    public void InitHPbar()
    {
        _healthBar.Show();
        _healthBar.SetMaxValue(_currentEnemyData.health);
        _currentEnemy.OnDamaged += _healthBar.DecreaseValue;
        _currentEnemy.OnDead += _healthBar.Hide;
    }

    public void DamageCurrentEnemy(float damage)
    {
        _currentEnemy.DoDamage(damage);
        // Debug.Log($"Damaged!- {damage}");
    }
    

}
