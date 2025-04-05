using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    [CreateAssetMenu(menuName = "Configs/SkillsConfig",fileName = "SkillsConfig")]
    public class SkillsConfig : ScriptableObject
    {
        public List<SkillData> Skills;
        private Dictionary<string, Dictionary<int, SkillDataByLevel>> _skillDataByLevelMap;
        public SkillDataByLevel GetSkillData(string skillId, int level)
        {
            if (_skillDataByLevelMap == null || _skillDataByLevelMap.Count == 0)
            {
                FillSkillDataMap();
            }
            return _skillDataByLevelMap[skillId][level];
        }

        private void FillSkillDataMap()
        {
            _skillDataByLevelMap = new();
            foreach (var skill in Skills)
            {
                if (!_skillDataByLevelMap.ContainsKey(skill.SkillId))
                {
                    _skillDataByLevelMap[skill.SkillId] = new();
                };
                foreach (var skillDataByLevel in skill.SkillsLevels)
                {
                    _skillDataByLevelMap[skill.SkillId][skillDataByLevel.Level] = skillDataByLevel;
                }
            }
        }
    }
}