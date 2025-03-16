using System;

namespace ComboSystemSpace
{
    public class ComboSystem
    {
        private float _countDoDamage;
        private DateTime _lastHitTime;
        private float time = .8f;
        private AttackTypes _firstAttack = 0;
        private AttackTypes _secondAttack = 0;
        private int _damageCombo;
    
        public int CheckCombo(AttackTypes attackType)
        {
            _countDoDamage++;
            if (_firstAttack == 0)
            {
                _firstAttack = attackType;
                _secondAttack = _firstAttack;
                return 0;
            }
            
            
            _secondAttack = attackType;
            if ((DateTime.Now - _lastHitTime).Seconds <= time)
            {
                _countDoDamage = 0;
                _damageCombo = GetDamageCombo(_firstAttack, _secondAttack);
                return _damageCombo;
            }
    
            _lastHitTime = DateTime.Now;
            _firstAttack = _secondAttack;
            return 0;
        }
    
        public int GetDamageCombo(AttackTypes First, AttackTypes Second)
        {   
            /*
             * TODO: СДЕЛАТЬ ВСЕМ СИСТЕМУ КОМБО. 
             */
            if (First == AttackTypes.Light)
            {
                return 2;
            }

            if (First == AttackTypes.Heavy)
            {
                return 10;
            }
            if (First == AttackTypes.Ride && Second == AttackTypes.Light)
            {
                return 15;
            }

            if (First == AttackTypes.Jump && Second == AttackTypes.Heavy)
            {
                return 20;
            }
            
    
            return 0;
        }
    }
    
    public enum AttackTypes
    {
        Ride = 1,
        Light = 2,
        Heavy = 3,
        Jump = 4,
    }
}
