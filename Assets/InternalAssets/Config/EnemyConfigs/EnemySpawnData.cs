using System;

namespace InternalAssets.Config.EnemyConfigs {
    [Serializable]
    public struct EnemySpawnData {
        public string Id;
        public float Hp;
        public bool IsBoss;
        public float BossTime;
        public EnemyType EnemyType;
    }

    public enum EnemyType
    {
        Enemy = 0,
        SpecialEnemy = 1,
        EliteEnemy = 2 
    }
}