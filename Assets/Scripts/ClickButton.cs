using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;

    public void Initilize(ColorBlock colorBlock)
    {
        //Иницилизация палитры кнопки
        // Визуальное изменение кнопки при клике
        _button.colors = colorBlock;
    }

    public void SubscribeOnClick(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }
    public void SubscribeOnClickLightAttack(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }

    public void UnsubscribeOnClickLightAttack(UnityAction action)
    {
        _button.onClick.RemoveListener(action);
    }
    public void SubscribeOnClickHeavyAttack(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }

    public void UnsubscribeOnClickHeavyAttack(UnityAction action)
    {
        _button.onClick.RemoveListener(action);
    }

    public void SubscribeOnClickJump(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }

    public void UnsubscribeOnClickJump(UnityAction action)
    {
        _button.onClick.RemoveListener(action);
    }

    public void SubscribeOnClickRide(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }

    public void UnsubscribeOnClickRide(UnityAction action)
    {
        _button.onClick.RemoveListener(action);
    }
}
