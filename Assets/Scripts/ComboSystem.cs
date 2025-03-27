using System;
using UnityEngine;

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
        _lastHitTime = DateTime.Now;
        _firstAttack = _secondAttack;
        return _damageCombo;
    }
    
    public int GetDamageCombo(AttackTypes first, AttackTypes second)
    {   
        if (first == AttackTypes.Light)
        {
            return CheckComboTime(2);
        }

        if (first == AttackTypes.Heavy)
        {
            return CheckComboTime(10);
        }
        if (first == AttackTypes.Ride && second == AttackTypes.Light)
        {
            return CheckComboTime(15);
        }

        if (first == AttackTypes.Jump && second == AttackTypes.Heavy)
        {
            return CheckComboTime(20);
        }
            
    
        return 0 ;
    }

    private int CheckComboTime(int damage)
    {
        Debug.Log("Combo check time");
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