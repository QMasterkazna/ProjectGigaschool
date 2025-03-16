using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Image _image;
    
    public event UnityAction<float> OnDamaged;
    public event UnityAction OnDead;
    
    private float _health;
    


    public void Initialize(EnemiesData data)
    {
        _health = data.health;
        _image.sprite = data.sprite;
    }

    public void DoDamage(float damage, float time)
    {
        if (damage >= _health)
        {
            _health = 0;
            OnDamaged?.Invoke(damage);
            OnDead?.Invoke();
            return;
        }
        _health -= damage;
        OnDamaged?.Invoke(damage);
    }
    

    public float GetHealth()
    {
        return _health;
    }
}
