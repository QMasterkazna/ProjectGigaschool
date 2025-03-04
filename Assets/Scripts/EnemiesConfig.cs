using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EnemiesConfig",fileName = "EnemiesConfig")]

    
public class EnemiesConfig : ScriptableObject
{
        public Enemy EnemyPrefab;
        public List<EnemiesData> Enemies;
        
}


