using System;

public class ComboSystem
{
    private DateTime _lastHitTime;
    private const float Time = .8f;
    private AttackTypes _firstAttack = 0;
    private AttackTypes _secondAttack = 0;
    private int _damageCombo;
    
    public int CheckCombo(AttackTypes attackType)
    {
        if (_firstAttack == 0)
        {
            _firstAttack = attackType;
            _secondAttack = _firstAttack;
            return 2;
        }
            
            
        _secondAttack = attackType;

        _damageCombo = GetDamageCombo(_firstAttack, _secondAttack);
        return _damageCombo;
        
    
        _lastHitTime = DateTime.Now;
        _firstAttack = _secondAttack;
        return 1;
    }
    
    public int GetDamageCombo(AttackTypes first, AttackTypes second)
    {   
        if (first == AttackTypes.Light)
        {
            CheckComboTime(2);
        }

        if (first == AttackTypes.Heavy)
        {
            CheckComboTime(10);
        }
        if (first == AttackTypes.Ride && second == AttackTypes.Light)
        {
            CheckComboTime(15);
        }

        if (first == AttackTypes.Jump && second == AttackTypes.Heavy)
        {
            CheckComboTime(20);
        }
            
    
        return 3;
    }

    private int CheckComboTime(int damage)
    {
        return (DateTime.Now - _lastHitTime).Seconds <= Time ? damage : 0;
    }
}
    
public enum AttackTypes
{
    Ride = 1,
    Light = 2,
    Heavy = 3,
    Jump = 4,
}