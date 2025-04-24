using Skill;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ClickButtonManager : MonoBehaviour
{
    [SerializeField] private ClickButton _clickLightButton;
    [SerializeField] private ClickButton _clickHeavyButton;
    [SerializeField] private ClickButton _clickJumpButton;
    [SerializeField] private ClickButton _clickRideButton;
    
    [SerializeField] private ClickButton _clickEnemyButton;
    [SerializeField] private ClickButton _clickSpecialEnemyButton;
    [SerializeField] private ClickButton _clickEliteEnemyButton;
    
    
    [SerializeField] private ClickButtonConfig _buttonConfig;
    
    [SerializeField] private ClickButtonConfig _buttonEnemyConfig;
    [SerializeField] private ClickButtonConfig _buttonSpecialEnemyConfig;
    [SerializeField] private ClickButtonConfig _buttonEliteEnemyConfig;
    public event UnityAction OnClickedLightAttack;
    public event UnityAction OnClickedHeavyAttack;
    public event UnityAction OnClickedRideAttack;
    public event UnityAction OnClickedJumpAttack;
    public void Initialize(SkillSystem skillSystem)
    {
        _clickEnemyButton.Initilize(_buttonEnemyConfig.ButtonColors);
        _clickSpecialEnemyButton.Initilize(_buttonSpecialEnemyConfig.ButtonColors);
        _clickEliteEnemyButton.Initilize(_buttonEliteEnemyConfig.ButtonColors);
        // _clickLightButton.Initilize(_buttonConfig.DefaultSprite, _buttonConfig.ButtonColors);
        // _clickLightButton.SubscribeOnClickLightAttack(ShowClickLightAttack);
        _clickEnemyButton.SubscribeOnClick(() =>skillSystem.InvokeTrigger(SkillTrigger.OnEnemy));
        _clickSpecialEnemyButton.SubscribeOnClick(()=>skillSystem.InvokeTrigger(SkillTrigger.OnSpecial));
        _clickEliteEnemyButton.SubscribeOnClick(()=>skillSystem.InvokeTrigger(SkillTrigger.OnEliteEnemy));
        _clickLightButton.SubscribeOnClickLightAttack(() => OnClickedLightAttack?.Invoke());
        
        // _clickHeavyButton.SubscribeOnClickHeavyAttack(ShowClickHeavyAttack);
        _clickHeavyButton.SubscribeOnClickHeavyAttack(() => OnClickedHeavyAttack?.Invoke());
        
        // _clickJumpButton.SubscribeOnClickJump(ShowClickJump);
        _clickJumpButton.SubscribeOnClickJump(() => OnClickedJumpAttack?.Invoke());
        
        // _clickRideButton.SubscribeOnClickRide(ShowClickRide);
        _clickRideButton.SubscribeOnClickRide(() => OnClickedRideAttack?.Invoke());
        
    }
}

