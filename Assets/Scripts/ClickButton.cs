using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;

    public void Initilize(Sprite sprite, ColorBlock colorBlock)
    {
        //Иницилизация палитры кнопки
        // Визуальное изменение кнопки при клике
        _image.sprite = sprite;
        _button.colors = colorBlock;
    }
    
    /* TODO:
     * Сделать подписки на 4 кнопки. Легкая, Тяжелая атака, прыжок и подкат
     * Также переименовать подписку для легкой атаки
     */
    public void SubscribeOnClick(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }

    public void UnSubscribeOnClick(UnityAction action)
    {
        _button.onClick.RemoveListener(action);
    }
    
}
