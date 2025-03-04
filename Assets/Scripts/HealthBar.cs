
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void SetMaxValue(float value)
    {
        
        _slider.maxValue = value;
        _slider.value = value;
    }

    public void DecreaseValue(float value)
    {
        _slider.value -= value;
    }
}
