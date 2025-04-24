using System;
using DG.Tweening;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using Meta;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;
using YG;

namespace RewardedAd
{
    public class RewardedAdManager : MonoBehaviour
    {
        private UnityAction _hideRewardButton;
        private UnityAction<UnityAction> _showRewardButton;
        [SerializeField] private ConfirmationWindow _confirmWindow;
        private SaveSystem _saveSystem;
        private Wallet _wallet;
        private Sequence _sequence;

        public void Initialize(SaveSystem saveSystem,UnityAction<UnityAction> showRewardButton, UnityAction hideRewardButton)
        {
            _saveSystem = saveSystem;
            _wallet = (Wallet)_saveSystem.GetData(SavableObjectType.Wallet);
            
            _hideRewardButton = hideRewardButton;
            _showRewardButton = showRewardButton;
            showRewardButton?.Invoke(OnRewardClicked);
        }

        private void OnRewardClicked()
        {
            _confirmWindow.ShowWindowInfo(ShowAdvertisement, "Посмотрите рекламу и получите монетки");
        }

        private void ShowAdvertisement()
        {
            YG2.RewardedAdvShow("metaButton", GetReward);
            _hideRewardButton?.Invoke();
            _sequence = DOTween
                .Sequence()
                .AppendInterval(120f)
                .OnComplete(() => _showRewardButton?.Invoke(OnRewardClicked));
        }

        private void GetReward()
        {
            _wallet.SetCoins(50, 1);
            _saveSystem.SaveData(SavableObjectType.Wallet);
        }

        private void OnDestroy()
        {
            _sequence.Kill();
        }
    }
}