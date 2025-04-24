using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Translator
{
    [CreateAssetMenu(menuName = "Configs/LanguageMap",fileName = "LanguageMap")]
    public class LanguageMap : ScriptableObject
    {
        public string AdditionalDamageSkillLabel;
        public string AddtionalDamageSkillDescription;
        
    }
    
}