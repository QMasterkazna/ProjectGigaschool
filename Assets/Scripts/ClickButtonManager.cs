using UnityEngine;
using UnityEngine.Serialization;

public class ClickButtonManager : MonoBehaviour
{
    [SerializeField] private ClickButton _clickButton;
    [SerializeField] private ClickButton _clickHeavyButton;
    
    [SerializeField] private ClickButtonConfig _buttonConfig;

    public void Initialize()
    {
        _clickButton.Initilize(_buttonConfig.DefaultSprite, _buttonConfig.ButtonColors);
        _clickButton.SubscribeOnClick(ShowClick);
        //создать подписку на heavy button
    }

    private void ShowClick()
    {
        Debug.Log("Light attack");
    }
}

