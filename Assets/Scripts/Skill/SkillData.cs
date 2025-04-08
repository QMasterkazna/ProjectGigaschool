using System;
using System.Collections.Generic;
using System.Linq;

namespace Skill
{
    [Serializable]
    public struct SkillData
    {
        public string SkillId;
        public List<SkillDataByLevel> SkillsLevels;

        public SkillDataByLevel GetSkillByLevel(int level)
        {
            return SkillsLevels.Find(x => x.Level == level);
        }

        public bool isMaxLevel(int level)
        {
            return SkillsLevels.Max(x=>x.Level) == level;
        }
    }
}