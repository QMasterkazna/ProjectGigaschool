using System;
using InternalAssets.Config.EnemyConfigs;

namespace InternalAssets.Config.KNGConfig
{
    [Serializable]
    public struct KNBData
    {
        public EnemyType From;
        public EnemyType To;
        public float Multiplier;
    }
}