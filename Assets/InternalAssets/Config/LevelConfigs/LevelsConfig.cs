using System.Collections.Generic;
using Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace InternalAssets.Config.LevelConfigs {
    [CreateAssetMenu(menuName="Configs/LevelsConfig", fileName = "LevelsConfig")]
    public class LevelsConfig : ScriptableObject {
        [FormerlySerializedAs("Levels")][SerializeField] private List<LevelData> _levels;
        
        private Dictionary<int, Dictionary<int, LevelData>> _levelsMap;
        

        public LevelData GetLevel(int location, int level)
        {
            if(_levelsMap.IsNullOrEmpty()) FillLevelMap();
            return _levelsMap[location][level];
        }

        public int GetMaxLevelOnLocation(int location)
        {
           if (_levelsMap.IsNullOrEmpty()) FillLevelMap();
           var maxLevel = 0;
           foreach (var levelNumber in _levelsMap[location].Keys)
           {
               if(levelNumber <= maxLevel) continue;
               maxLevel = levelNumber;
           }
           return maxLevel;
        }
        public Vector2Int GetMaxLocationAndLevel() {
            if(_levelsMap.IsNullOrEmpty()) FillLevelMap();
            var locationAndLevel = new Vector2Int();
            foreach (var locationNumber in _levelsMap.Keys) {
                if (locationNumber <= locationAndLevel.x) continue;
                locationAndLevel.x = locationNumber;
            }
            
            foreach (var levelNumber in _levelsMap[locationAndLevel.x].Keys) {
                if (levelNumber <= locationAndLevel.y) continue;
                locationAndLevel.y = levelNumber;
            }

            return locationAndLevel;
        }
        private void FillLevelMap()
        {
            _levelsMap = new ();
            foreach (var levelData in _levels)
            {
                var locationMap = _levelsMap.GetOrCreate(levelData.Location);
                locationMap[levelData.LevelNumber] = levelData;
            }
           
        }
       
    }
    
}