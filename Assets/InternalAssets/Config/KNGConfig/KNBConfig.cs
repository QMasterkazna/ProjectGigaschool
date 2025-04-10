using System.Collections.Generic;
using Extensions;
using InternalAssets.Config.EnemyConfigs;
using UnityEngine;

namespace InternalAssets.Config.KNGConfig
{
    [CreateAssetMenu(menuName = "Configs/KNBConfig",fileName = "KNBConfig")]

    
    public class KNBConfig : ScriptableObject
    {
        [SerializeField] private List<KNBData> _data;

        private Dictionary<EnemyType, Dictionary<EnemyType, float>> _dataMap;
        
        public float CalculateDamage(EnemyType from, EnemyType to, float damage)
        {
            if (_dataMap.IsNullOrEmpty()) FillDataMap();
            return _dataMap[from][to] * damage;
        }

        private void FillDataMap()
        {
            _dataMap = new();
            foreach (var data in _data)
            {
                _dataMap[data.From] ??= new();
                _dataMap[data.From][data.To] = data.Multiplier;
            }
        }
    }
}