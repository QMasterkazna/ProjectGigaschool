using System;
using System.Collections.Generic;

namespace Skill
{
    [Serializable]
    public struct SkillData
    {
        public string SkillId;
        public List<SkillDataByLevel> SkillsLevels;
    }
}