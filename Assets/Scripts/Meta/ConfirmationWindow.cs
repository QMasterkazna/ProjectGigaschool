using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Meta
{
    public class ConfirmationWindow : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _closeArea;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private TextMeshProUGUI _infoText;

        public void Awake()
        {
            _closeButton.onClick.AddListener(()=> gameObject.SetActive(false));
            _closeArea.onClick.AddListener(()=> gameObject.SetActive(false));
        }

        public void ShowWindowInfo(UnityAction confirmCallback, string infoText)
        {
            gameObject.SetActive(true);
            _confirmButton.onClick.RemoveAllListeners();
            _confirmButton.onClick.AddListener(()=>
            {
                confirmCallback?.Invoke();
                gameObject.SetActive(false);
            });
            _infoText.text = infoText;
        }

    }
}