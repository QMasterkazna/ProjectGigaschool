using Global.Audio;
using Global.SaveSystem;
using Translator;
using UnityEngine;
using UnityEngine.Serialization;

namespace SceneManagment
{
    public class commonObject : MonoBehaviour
    {
        [FormerlySerializedAs("sceneLoader")] public SceneLoader SceneLoader;
        [FormerlySerializedAs("audioManager")] public AudioManager AudioManager;
        public SaveSystem SaveSystem;
        public TranslatorManager TranslatorManager;
    }
}
