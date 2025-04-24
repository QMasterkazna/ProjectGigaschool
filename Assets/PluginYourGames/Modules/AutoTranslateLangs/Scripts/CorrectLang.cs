using UnityEngine;

namespace YG.LanguageLegacy
{
    public static class CorrectLang
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            YG2.onCorrectLang += OnChangeLang;
        }

        public static void OnChangeLang(string lang)
        {
            if (UtilsLang.LangCheckExist(lang) == false)
            {
                if (YG2.infoYG.AutoTranslateLangs.languages.en)
                    YG2.lang = "en";
                else
                    YG2.lang = "ru";
            }
        }
    }
}