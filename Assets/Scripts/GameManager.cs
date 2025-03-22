using SceneManagment;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : EntryPoint
{
    [SerializeField] private ClickButtonManager _clickButtonManager;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Timer _timer;
    [SerializeField] private EndLevelWindow _endLevelWindow;
    private const string SCENE_LOADER_TAG = "SceneLoader";


    public override void Run(SceneEnterParams enterParams)
    {
        _clickButtonManager.Initialize();
        _enemyManager.Initialize(_healthBar);
        _endLevelWindow.Initialize();
        ComboSystem GetDamageCombo = new ComboSystem();
        
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
            Debug.Log($"COMBO: {damage}");

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
            Debug.Log($"COMBO: {damage}");

            // _enemyManager.DamageCurrentEnemy(.2f);
        };
        _endLevelWindow.OnRestartClicked += RestartLevel;
        _enemyManager.OnLevelPassed += LevelPassed;
        
        StartLevel();
    }

    private void RestartLevel()
    {
        var sceneLoader = GameObject.FindWithTag(SCENE_LOADER_TAG).GetComponent<SceneLoader>();
        sceneLoader.LoadGameplayScene();
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
