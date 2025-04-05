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
}