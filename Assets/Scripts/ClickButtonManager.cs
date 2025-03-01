using UnityEngine;
using UnityEngine.Serialization;

public class ClickButtonManager : MonoBehaviour
{
    [SerializeField] private ClickButton _clickLightButton;
    [SerializeField] private ClickButton _clickHeavyButton;
    [SerializeField] private ClickButton _clickJumpButton;
    [SerializeField] private ClickButton _clickRideButton;
    
    [SerializeField] private ClickButtonConfig _buttonConfig;

    public void Initialize()
    {
        _clickLightButton.Initilize(_buttonConfig.DefaultSprite, _buttonConfig.ButtonColors);
        _clickLightButton.SubscribeOnClickLightAttack(ShowClickLightAttack);
        _clickHeavyButton.SubscribeOnClickHeavyAttack(ShowClickHeavyAttack);
        _clickJumpButton.SubscribeOnClickJump(ShowClickJump);
        _clickRideButton.SubscribeOnClickRide(ShowClickRide);
    }

    private void ShowClickRide()
    {
        Debug.Log("Ride");
    }

    private void ShowClickJump()
    {
        Debug.Log("Jump");
    }

    private void ShowClickHeavyAttack()
    {
        Debug.Log("Heavy attack");
    }

    private void ShowClickLightAttack()
    {
        Debug.Log("Light attack");
    }
}

