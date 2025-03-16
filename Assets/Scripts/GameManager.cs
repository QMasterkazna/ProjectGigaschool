using UnityEngine;
using UnityEngine.Events;
using ComboSystemSpace;
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
        ComboSystem GetDamageCombo = new ComboSystem();
        /*
         * Прописать всем подвязку к GetDamageCombo
         */
        
        _clickButtonManager.OnClickedLightAttack += () =>
        {
            int damage = GetDamageCombo.CheckCombo(AttackTypes.Light);
            _enemyManager.DamageCurrentEnemy(damage);
            Debug.Log($"COMBO: {damage}");
        };
        _clickButtonManager.OnClickedHeavyAttack += () =>
        {
            int damage = GetDamageCombo.CheckCombo(AttackTypes.Heavy);
            _enemyManager.DamageCurrentEnemy(damage);
            // _enemyManager.DamageCurrentEnemy(5f);
        };
        _clickButtonManager.OnClickedRideAttack += () =>
        {
            int damage = GetDamageCombo.CheckCombo(AttackTypes.Ride);
            _enemyManager.DamageCurrentEnemy(damage);
            Debug.Log($"COMBO: {damage}");
            // _enemyManager.DamageCurrentEnemy(.5f);
        };
        _clickButtonManager.OnClickedJumpAttack += () =>
        {
            int damage = GetDamageCombo.CheckCombo(AttackTypes.Jump);
            _enemyManager.DamageCurrentEnemy(damage);
            // _enemyManager.DamageCurrentEnemy(.2f);
        };
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
