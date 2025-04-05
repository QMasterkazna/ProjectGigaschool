using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EnemiesConfig",fileName = "EnemiesConfig")]

    
public class EnemiesConfig : ScriptableObject
{
        public Enemy EnemyPrefab;
        public List<EnemiesData> Enemies;

        public EnemiesData GetEnemy(string id)
        {
                foreach (var enemyData in Enemies)
                {
                        if (enemyData.Id == id) return enemyData;
                }
                Debug.LogError($"Not Found enemy with id {id}");
                return default;
        }
}