using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Meta.Locations
{
    public class LocationManager : MonoBehaviour
    {
        [SerializeField] private Button _previousButton;
        [SerializeField] private Button _nextButton;
        
        [SerializeField] private List<Location> _locations;
        private int _currentLocation;

        public void Initialize(int currentLocation, UnityAction<Vector2Int> startLevelCallback)
        {
            _currentLocation = currentLocation;
            InitLocations(currentLocation, startLevelCallback);
            InitializeMoveLocationButtons();
        }

        private void InitializeMoveLocationButtons()
        {
            _previousButton.onClick.AddListener(ShowPreviousLocation);
            _nextButton.onClick.AddListener(ShowNextLocation);
            if (_currentLocation == _locations.Count)
            {
                _nextButton.gameObject.SetActive(false);
            }

            if (_currentLocation == 1)
            {
                _previousButton.gameObject.SetActive(false);
            }
        }

        private void ShowNextLocation()
        {
            _locations[_currentLocation - 1].SetActive(false);
            _currentLocation++;
            _locations[_currentLocation - 1].SetActive(true);

            if (_currentLocation == _locations.Count)
            {
                _nextButton.gameObject.SetActive(false);
            }

            if (_currentLocation == 2)
            {
                _previousButton.gameObject.SetActive(true);
            }
        }

        private void ShowPreviousLocation()
        {
            _locations[_currentLocation - 1].SetActive(false);
            _currentLocation--;
            _locations[_currentLocation - 1].SetActive(true);
            if (_currentLocation == _locations.Count - 1)
            {
                _nextButton.gameObject.SetActive(true);
            }

            if (_currentLocation == 1)
            {
                _previousButton.gameObject.SetActive(false);
            }
        }

        private void InitLocations(int currentLocation, UnityAction<Vector2Int> startLevelCallback)
        {
            for (var i = 0; i < _locations.Count; i++)
            {
                var locationNumber = i + 1;
                _locations[i].Initialize(level => startLevelCallback?.Invoke(new Vector2Int(locationNumber, level)));
                _locations[i].SetActive(currentLocation == locationNumber);
            }
        }
    }
}