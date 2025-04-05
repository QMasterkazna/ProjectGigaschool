using System.Collections.Generic;

namespace Global.SaveSystem.SavableObjects
{
    public class OpenedSkills : ISavable {
        public List<SkillWithLevel> Skills = new()
        {
            new SkillWithLevel()
            {
                Id = "AdditionalDamageSkill", 
                Level = 1
            }
        };
    }
}