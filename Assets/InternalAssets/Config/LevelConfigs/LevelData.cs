using System;
using System.Collections.Generic;
using InternalAssets.Config.EnemyConfigs;

namespace InternalAssets.Config.LevelConfigs {
    [Serializable]
    public struct LevelData {
        public int Location;
        public int LevelNumber;
        public List<EnemySpawnData> Enemies;
        public int Reward;
    }
}