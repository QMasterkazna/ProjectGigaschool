using System;
using UnityEngine;

namespace Translator
{
    public class TranslatorManager
    {
        private readonly LanguageMap _currentConfig;

        public TranslatorManager(string lang)
        {
            _currentConfig = Resources.Load<LanguageMap>("Languages/" + lang);
        }

        public string Translate(string key)
        {
            try
            {
                return typeof(LanguageMap)
                    .GetField(key).
                    GetValue(_currentConfig).
                    ToString();
            }
            catch (Exception e)
            {
                return "Exception: " + e.Message;
            }

            
        }
    }
}