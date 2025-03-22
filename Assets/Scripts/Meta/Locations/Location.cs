using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Meta.Locations
{
    public class Location : MonoBehaviour
    {
        [SerializeField] private List<PinViews> _pins;

        public void Initialize(UnityAction<int> levelStartCallback)
        {
            var currentlevel = 3;
            
            for (var i = 0; i < _pins.Count; i++)
            {
                var level = i + 1;
                var pinType = currentlevel > i + 1 
                    ?PinType.Passed: currentlevel == i + 1 
                        ? PinType.Current 
                        : PinType.Closed;
                _pins[i].Initialize(i+1, pinType, () => levelStartCallback?.Invoke(level));
            }
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}