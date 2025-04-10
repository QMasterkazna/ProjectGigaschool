using InternalAssets.Config.EnemyConfigs;
using InternalAssets.Config.KNGConfig;
using UnityEngine.Scripting;

namespace Skill.SkillVariants
{
    [Preserve]
    public class AdditionalDamageSkill : Skill
    {
        private EnemyManager _enemyManager;
        private SkillDataByLevel _skilldData;

        public override void Initialize(SkillScope scope, SkillDataByLevel skilldData)
        {
            _skilldData = skilldData;
            _enemyManager = scope.EnemyManager;
        }

        public override void SkillProcess()
        {
            _enemyManager.DamageCurrentEnemy(_skilldData.Value);
        }
    }
    [Preserve]
    public class EnemyDamageSkill : Skill
    {
        private EnemyManager _enemyManager;
        private SkillDataByLevel _skilldData;
        private KNBConfig _knbConfig;

        public override void Initialize(SkillScope scope, SkillDataByLevel skilldData)
        {
            _skilldData = skilldData;
            _enemyManager = scope.EnemyManager;
            _knbConfig = scope.knbConfig;
        }

        public override void SkillProcess()
        {
            var toDamageType = _enemyManager.GetCurrentDamageEnemyType();
            var calculatedDamage = _knbConfig.CalculateDamage(EnemyType.Enemy,toDamageType,_skilldData.Value);
            _enemyManager.DamageCurrentEnemy(calculatedDamage);
        }
    }
    [Preserve]
    public class SpecialEnemyDamageSkill : Skill
    {
        private EnemyManager _enemyManager;
        private SkillDataByLevel _skilldData;
        private KNBConfig _knbConfig;


        public override void Initialize(SkillScope scope, SkillDataByLevel skilldData)
        {
            _skilldData = skilldData;
            _enemyManager = scope.EnemyManager;
            _knbConfig = scope.knbConfig;

        }

        public override void SkillProcess()
        {
            var toDamageType = _enemyManager.GetCurrentDamageEnemyType();
            var calculatedDamage = _knbConfig.CalculateDamage(EnemyType.SpecialEnemy,toDamageType,_skilldData.Value);
            _enemyManager.DamageCurrentEnemy(calculatedDamage);
        }
    }
    [Preserve]
    public class ElieEnemyDamageSkill : Skill
    {
        private EnemyManager _enemyManager;
        private SkillDataByLevel _skilldData;
        private KNBConfig _knbConfig;

        public override void Initialize(SkillScope scope, SkillDataByLevel skilldData)
        {
            _skilldData = skilldData;
            _enemyManager = scope.EnemyManager;
            _knbConfig = scope.knbConfig;
        }

        public override void SkillProcess()
        {
            var toDamageType = _enemyManager.GetCurrentDamageEnemyType();
            var calculatedDamage = _knbConfig.CalculateDamage(EnemyType.EliteEnemy,toDamageType,_skilldData.Value);
            _enemyManager.DamageCurrentEnemy(calculatedDamage);
        }
    }
}