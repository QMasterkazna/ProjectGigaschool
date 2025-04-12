using UnityEngine.Events;

namespace Global.SaveSystem.SavableObjects
{
    public class Wallet : ISavable {
        public int Coins;

        public void SetCoins(int newCoins, int LogicManipulation)
        {
            switch (LogicManipulation)
            {
                case 1:
                    Coins += newCoins;
                    break;
                case 2:
                    Coins -= newCoins;
                    break;
            }
            OnCoinChanged?.Invoke(newCoins);
        }
        public event UnityAction<int> OnCoinChanged; 
    }
    
}