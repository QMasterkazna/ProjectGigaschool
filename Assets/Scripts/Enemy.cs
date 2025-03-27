using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Image _image;
    
    public event UnityAction<float> OnDamaged;
    public event UnityAction OnDead;
    
    private float _health;
    private Sequence _currentSequenceDamage;


    public void Initialize(Sprite sprite, float health)
    {
        _health = health;
        _image.sprite = sprite;
        SetCurrentSequenceMethod();
    }

    private void SetCurrentSequenceMethod()
    {
        _currentSequenceDamage = DOTween.Sequence()
            .AppendCallback(() => transform.localScale = new Vector3(1, 1, 1))
            .Append(transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.2f))
            .Append(transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f))
            .SetAutoKill(false)
            .Pause();
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
        
        Debug.Log(damage);
        _health -= damage;
        _currentSequenceDamage.Restart();
   
        OnDamaged?.Invoke(damage);
    }
    

    public float GetHealth()
    {
        return _health;
    }
}
