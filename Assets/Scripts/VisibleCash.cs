using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using TMPro;
using UnityEngine;

public class VisibleCash : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI _textCoin;
   public void ChangeCointText(int newCoins)
   {
      _textCoin.text = newCoins.ToString("00.0$");
   }
}
