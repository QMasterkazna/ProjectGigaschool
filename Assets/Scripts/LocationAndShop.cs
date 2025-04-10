using UnityEngine;
using UnityEngine.UI;

public class LocationAndShop : MonoBehaviour
{
    [SerializeField] private Button _buttonShop;
    [SerializeField] private Button _buttonLocation;
    [SerializeField] private GameObject _shop;
    [SerializeField] private GameObject _location;
    public void Initialize()
    {
        _buttonShop.onClick.AddListener(()=>
        {
            _shop.SetActive(true);
            _location.SetActive(false);
        });
        _buttonLocation.onClick.AddListener(() =>
        {
            
            _location.SetActive(true);
            _shop.SetActive(false);
        });
    }
}
