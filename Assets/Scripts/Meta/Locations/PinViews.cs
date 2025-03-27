using System;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

namespace Meta.Locations
{
    public class PinViews : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;

        [SerializeField] private Color _currentLevel;
        [SerializeField] private Color _passedLevel;
        [SerializeField] private Color _closedlevel;

        private Sequence _currentLevelSequence;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public void Initialize(int levelNumber, ProgressState progressState, UnityAction clickCallback)
        {
            SetupCurrentLevelSequence();
            _text.text = $"Ур {levelNumber}";

            _image.color = progressState switch
            {
                ProgressState.Current => _currentLevel,
                ProgressState.Closed => _closedlevel,
                ProgressState.Passed => _passedLevel,
                
            };
            
            if (progressState == ProgressState.Current)
            {
                transform.DORotate(new Vector3(0, 0, 10f), 0.2f).OnComplete(() => _currentLevelSequence.Play());
                
            }
            _button.onClick.AddListener(() => clickCallback?.Invoke());

            
        }

        private void SetupCurrentLevelSequence()
        {
            _currentLevelSequence = DOTween.Sequence()
                .Append(transform.DORotate(new Vector3(0,0, -10f), 0.2f))
                .Append(transform.DORotate(new Vector3(0,0,10f), 0.2f))
                .SetLoops(-1, LoopType.Yoyo)
                .Pause();
        }

        private void OnDestroy()
        {
            _currentLevelSequence.Complete(); // Завершить выполнение последовательности
            _currentLevelSequence.Kill();     // Убить последовательность
            _currentLevelSequence = null;     // Обнулить ссылку
        }
        
        
    }

}
